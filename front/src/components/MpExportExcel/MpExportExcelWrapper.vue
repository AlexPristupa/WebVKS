<template>
  <div>
    <mp-export-excel
      v-if="exportVisible"
      :visible="exportVisible"
      :listQuery="listQuery"
      @close="close(false)"
      @export="submit"
    />
    <mp-loading-path
      v-if="loadingPath"
      :visible="loadingPath"
      :hiddenButton="true"
      @hide="close(true)"
      @interrupt="interruptExport"
    />
  </div>
</template>

<script lang="ts">
/**
 * @description обертка модального окна экспора.
 *              работает совместно с: MpLoadingPath, MpExportExcel
 */
import Vue from 'vue'
import MpExportExcel from '@/components/MpExportExcel/MpExportExcelDialog.vue'
import MpLoadingPath from '@/components/MpLoadingPath/index.vue'
import { downloadFile } from '@/api_services/downloadFile/downloadFile'
import { exportTypes } from '@/api_services/downloadFile/downloadFile.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { methods } from '@/api_services/httpMethods.enum'
import { EXPORT_TIMEOUT } from '@/constant'
import axios, { CancelTokenSource } from 'axios'
import { mapActions, mapGetters } from 'vuex'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export default Vue.extend({
  name: 'MpExportExelWrapper',
  components: {
    MpLoadingPath,
    MpExportExcel,
  },
  props: {
    visible: {
      type: Boolean as () => boolean,
      default: false,
    },
    listQuery: {
      type: Object as () => TableDto,
      default: (): TableDto =>
        DtoFactory.create(DtoName.table, {
          tableName: TableName.unknown,
        }),
    },
  },
  computed: {
    ...mapGetters({
      getHidden: 'exportFile/getHidden',
      getCancelTokenSource: 'exportFile/getCancelTokenSource',
    }),
    source(): CancelTokenSource {
      return this.getCancelTokenSource(this.listQuery.tableName)
    },
    hidden(): boolean {
      return this.getHidden(this.listQuery.tableName)
    },
  },
  data() {
    return {
      loadingPath: false as boolean,
      exportVisible: false as boolean,
    }
  },
  watch: {
    visible(value) {
      if (!value) {
        this.exportVisible = false
        this.loadingPath = false
      }
      if (this.hidden && value) {
        this.loadingPath = true
      }
      if (!this.hidden && value) {
        this.exportVisible = true
      }
    },
  },
  methods: {
    ...mapActions({
      setHidden: 'exportFile/setHidden',
      setCancelTokenSource: 'exportFile/setCancelTokenSource',
      deleteCancelTokenSource: 'exportFile/deleteCancelTokenSource',
    }),
    interruptExport() {
      try {
        this.source.cancel()
        this.close(false)
      } catch (e) {
        console.warn('catch export interruption', e)
        this.$message.error(
          this.$t('notifications.error.exportInterruption') as string,
        )
      }
    },
    async submit(data) {
      try {
        this.loadingPath = true
        this.exportVisible = false
        this.setCancelTokenSource({
          tableName: this.listQuery.tableName,
          token: axios.CancelToken.source(),
        })
        const res = await this.$api.export({
          method: methods.post,
          timeout: EXPORT_TIMEOUT,
          cancelToken: this.source?.token,
          data: data,
        })
        if (res) {
          await downloadFile(res.filename, exportTypes.excel)
          this.deleteCancelTokenSource({
            tableName: this.listQuery.tableName,
          })
          this.close(false)
        }
      } catch (e) {
        this.$message.info(this.$t('notifications.cancel.export') as string)
      }
    },
    close(isHidden) {
      this.setHidden({
        tableName: this.listQuery.tableName,
        isHidden: isHidden,
      })
      this.$emit('close')
    },
  },
})
</script>

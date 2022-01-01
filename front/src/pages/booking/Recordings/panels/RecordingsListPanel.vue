<template>
  <mp-table-layout :title="$t('panels.title.recordings')">
    <mp-loading-path
      v-if="loadingPath"
      :visible="loadingPath"
      :hiddenButton="false"
      @interrupt="interruptDownloading"
    />
    <form-layout>
      <mp-video-player
        v-if="visibleVideoPlayer"
        :visible="visibleVideoPlayer"
        :src="videoSrc"
        @close="visibleVideoPlayer = false"
      />
      <share-record-modal
        v-if="visibleShareModal"
        :selected-entity="selectedEntity"
        :visible-edit-modal="visibleShareModal"
        @close="visibleShareModal = false"
      />
    </form-layout>

    <template #select>
      <MpSavingFilter
        :list-query="listQuery"
        :disable-button="filterActive"
        @onSelectFilterChange="savedFilterChange"
      />
    </template>

    <template #search>
      <mp-search
        ref="search"
        :extension-search-string="listQuery.TableSearchBy"
        @search="search"
      />
    </template>

    <template #pagination-top>
      <mp-pagination
        :position="paginationPosition.top"
        :total="paginationTotal"
        :list-query="listQuery"
        @pagination-state="paginationChanged"
      />
    </template>

    <template #table="propsSlotTable">
      <mp-table
        :table-name="listQuery.tableName"
        :column-defs="columnDefs"
        :row-data="rowData"
        :height="propsSlotTable.tableHeight"
        :loading="loading"
        :list-query="listQuery"
        :disabled="false"
        @sort-by="sortChanged"
        @check-row="checkRow"
        @onRowSelected="rowSelected"
        @action-button-click="actionsHandler"
        @columns-settings-changed="applyColumnRender"
        @onFilterChanged="filterChanged"
      />
    </template>

    <template #pagination-bottom>
      <mp-pagination
        :position="paginationPosition.bottom"
        :total="paginationTotal"
        :list-query="listQuery"
        @pagination-state="paginationChanged"
      />
    </template>
    <mp-confirm-modal
      :title="$t('confirm.recording.delete.title')"
      :text="[$t('confirm.recording.delete.text', [selectedEntity.name])]"
      :visible-confirm-modal.sync="visibleConfirmDeleteModal"
      :loading="loadingDelete"
      @confirm="deleteEntity"
      @close="visibleConfirmDeleteModal = false"
    />
  </mp-table-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import MpLoadingPath from '@/components/MpLoadingPath/index.vue'
import MpVideoPlayer from '@/components/MpVideoPlayer/MpVideoPlayer.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import { paginationParams } from '@/components/MpPagination/type'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import { PaginationPosition } from '@/modules/pagination/Pagination.const.ts'
import MpSavingFilter from '@/components/MpSavingFilter/MpSavingFilter.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender.ts'
import { mapActions, mapGetters } from 'vuex'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import {
  IComputedRecordingsListPanel,
  IDataRecordingsListPanel,
  IMethodsRecordingsListPanel,
  IPropsRecordingsListPanel,
} from '@/pages/booking/Recordings/config/recordingsListPanel.interface'
import { recordingsListColumns } from '@/pages/booking/Recordings/config/recordingsListPanel.const'
import ShareRecordModal from '@/pages/booking/Recordings/modals/ShareRecord.modal.vue'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'
import { methods } from '@/api_services/httpMethods.enum'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { downloadBlob } from '@/api_services/downloadFile/downloadFile'
import {
  downloadTypes,
  exportTypes,
} from '@/api_services/downloadFile/downloadFile.const'
import { TimeConverting } from '@/modules/TimeConverting/TimeConverting'
import axios from 'axios'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

export default Vue.extend<
  IDataRecordingsListPanel,
  IMethodsRecordingsListPanel,
  IComputedRecordingsListPanel,
  IPropsRecordingsListPanel
>({
  name: 'RecordingsListPanel',
  components: {
    MpVideoPlayer,
    MpTable,
    MpSearch,
    FormLayout,
    MpPagination,
    MpTableLayout,
    MpLoadingPath,
    ShareRecordModal,
    MpConfirmModal,
    MpSavingFilter,
  },
  computed: {
    ...mapGetters({
      getCancelTokenSource: 'exportFile/getCancelTokenSource',
    }),
    source() {
      return this.getCancelTokenSource(this.listQuery.tableName)
    },
    filterActive() {
      return !this.listQuery.filters?.length
    },
  },
  data() {
    return {
      loading: false,
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.recordings,
      }),
      rowData: [],
      columnDefs: [],
      // пока что свойства не нужны
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.recordings,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.recordings, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
      visibleShareModal: false,
      selectedEntity: new VksRecordingsEntity(),
      visibleVideoPlayer: false,
      videoSrc: '',
      loadingPath: false,
    }
  },
  async mounted() {
    await this.applyColumnRender()
    await this.getList()
  },
  methods: {
    ...mapActions({
      setActiveFiltersForTable: 'tableHeaderEffects/changeColumnActiveEffects',
      setCancelTokenSource: 'exportFile/setCancelTokenSource',
      deleteCancelTokenSource: 'exportFile/deleteCancelTokenSource',
    }),
    async getList() {
      this.loading = true
      const result = await this.$api.fetchData({ data: this.listQuery })
      if (result) {
        this.rowData = result.items.map(row => {
          row.duration = new TimeConverting().fromSeconds(row.duration)
          const disabled: Array<MpTypeButton> = []
          !row.isPlay && disabled.push(MpTypeButton.play)
          !row.isDownload && disabled.push(MpTypeButton.download)
          !row.isShare && disabled.push(MpTypeButton.link)
          !row.isDelete && disabled.push(MpTypeButton.remove)
          row.action = new ActionButtons(
            [
              MpTypeButton.play,
              MpTypeButton.download,
              MpTypeButton.link,
              MpTypeButton.remove,
            ],
            disabled,
          )
          return row
        })
        this.paginationTotal = result.total
      } else {
        this.rowData = []
      }
      this.loading = false
    },
    search(value) {
      this.listQuery.page = paginationParams.defaultPage
      this.listQuery.tableSearchBy = value.searchString
      this.getList()
    },
    paginationChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    async applyColumnRender() {
      this.columnDefs = []
      const columnRender = recordingsListColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    savedFilterChange(filters) {
      this.listQuery.filters = filters
      this.setActiveFiltersForTable({
        tableName: this.listQuery.tableName,
        effects: { filters: this.listQuery.filters },
      })
      this.getList()
    },
    addEntity() {},
    editEntity() {},
    async actionsHandler(data) {
      if (data.buttonType === MpTypeButton.play) {
        await this.downloadRecord(data.rowData.id, downloadTypes.show)
        return
      } else if (data.buttonType === MpTypeButton.download) {
        await this.downloadRecord(data.rowData.id, downloadTypes.download)
        return
      } else if (data.buttonType === MpTypeButton.link) {
        this.selectedEntity = new VksRecordingsEntity(data.rowData)
        this.visibleShareModal = true
      } else if (data.buttonType === MpTypeButton.remove) {
        this.selectedEntity = new VksRecordingsEntity(data.rowData)
        this.visibleConfirmDeleteModal = true
      } else {
        return
      }
    },
    async downloadRecord(id, type) {
      this.loadingPath = true
      this.setCancelTokenSource({
        tableName: this.listQuery.tableName,
        token: axios.CancelToken.source(),
      })
      const res = await downloadBlob(
        id,
        exportTypes.video,
        type,
        this.source.token,
      )
      this.deleteCancelTokenSource({
        tableName: this.listQuery.tableName,
      })
      this.loadingPath = false
      if (res && type === 'show') {
        this.videoSrc = res
        this.visibleVideoPlayer = true
      }
    },
    interruptDownloading() {
      try {
        if (this.source.cancel) {
          this.source.cancel()
          this.$message.info(this.$t('notifications.cancel.download') as string)
          this.loadingPath = false
        }
      } catch (e) {
        console.warn('catch export interruption', e)
      }
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    async deleteEntity() {
      const res = await this.$api.recordings({
        method: methods.delete,
        params: {
          id: this.selectedEntity.id,
        },
      })
      if (res && !res.validation) {
        this.$message.success(this.$t('notifications.data.deleted') as string)
        await this.getList()
      } else {
        const validationWithoutField = FormValidation.backValidationWithoutField(
          res.validation,
        )
        if (validationWithoutField) {
          this.$message.error(validationWithoutField)
        }
      }
      this.visibleConfirmDeleteModal = false
    },
    checkRow() {},
    checkAllRow() {},
    prepareCheckedToDelete() {},
    rowSelected(params) {
      this.selectedEntity = new VksRecordingsEntity(params.data)
    },
    emitTableDto() {},
    handlerRemove() {},
  },
})
</script>

<template>
  <div>
    <div class="columns-settings-menu">
      <div
        v-for="(option, index) in settingMenuOptions"
        :key="index"
        class="columns-settings-menu__option"
        @click="settingHandler(option.type)"
      >
        {{ option.title }}
      </div>
    </div>
    <columns-setting-dialog
      :listForDraggable="listForDraggable"
      :visible="setUpVisible"
      @save="saveColumns"
      @close="closeSetUpDialog"
    />
  </div>
</template>
<script lang="ts">
/**
 * @description обертка настроек колонок, включает в себя лист возможных операций над колонками
 * и модальное окно непосредственно настроек колонок определенной таблицы.
 * Работает совместно с: mpCellHeader
 */
import Vue from 'vue'
import ColumnsSettingDialog from './ColumnsSettingsDialog.vue'
import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import { IDataMpWrapperColumnsSettings } from '@/components/MpTable/columnSettings/MpWrapperColumnsSettings.interface'
import { SettingMenuOptions } from '@/components/MpTable/columnSettings/MpWrapperColumnsSettings.const'
import { methods } from '@/api_services/httpMethods.enum'

export default Vue.extend({
  name: 'MpWrapperColumnsSettings',
  components: {
    ColumnsSettingDialog,
  },
  props: {
    listForDraggable: {
      type: Array as () => Array<IColumnDefs>,
      default: (): Array<IColumnDefs> => [],
    },
    columnName: {
      type: String as () => string,
      default: '',
    },
    tableName: {
      type: String as () => string,
      default: '',
    },
  },
  data(): IDataMpWrapperColumnsSettings {
    return {
      setUpVisible: false,
      settingMenuOptions: [
        {
          title: this.$t('general.deleteColumn'),
          type: SettingMenuOptions.delete,
        },
        {
          title: this.$t('general.columnsSetup'),
          type: SettingMenuOptions.setup,
        },
      ],
    }
  },
  methods: {
    settingHandler(type: string): void {
      if (type === SettingMenuOptions.delete) {
        this.deleteColumn()
      } else {
        this.openSetUpDialog()
      }
    },
    deleteColumn() {
      const currentList = this.listForDraggable.filter(
        (column: IColumnDefs) => !column.hide,
      )
      if (currentList.length < 2) {
        this.$message.error(
          this.$t('notifications.error.deleteLastColumn') as string,
        )
        return
      } else {
        const editedList: Array<string> = this.getCurrentListForSave(
          currentList,
        )
        this.saveColumns(editedList)
      }
    },
    getCurrentListForSave(currentList: Array<IColumnDefs>) {
      const arr: Array<string> = []
      currentList.forEach((column: IColumnDefs) => {
        if (column.field !== this.columnName) {
          arr.push(column.field)
        }
      })
      return arr
    },
    async saveColumns(list: Array<string>) {
      const requestBody = {
        [this.tableName]: list,
      }
      const res = await this.$api.postSettingVisibleColumnsAgGridVue({
        method: methods.post,
        data: requestBody,
      })
      if (res) {
        this.$emit('update')
        this.$message.success(this.$t('notifications.data.saved') as string)
      }
      this.$emit('close-popup')
    },
    openSetUpDialog() {
      this.setUpVisible = true
    },
    closeSetUpDialog() {
      this.setUpVisible = false
      this.$message.info(this.$t('notifications.cancel.edit') as string)
      this.$emit('close-popup')
    },
  },
})
</script>

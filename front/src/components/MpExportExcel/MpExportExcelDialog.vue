<template>
  <FormLayout>
    <el-dialog
      width="40%"
      top="8vh"
      class="export-excel export-columns"
      :visible="visible"
      :title="$t('dialogs.export.title')"
      @close="$emit('close')"
    >
      <el-form ref="export" label-width="14rem" :model="form" :rules="rules">
        <el-form-item
          prop="fileName"
          label-width="10rem"
          :label="$t('general.specifyFileName')"
        >
          <el-input
            v-model="form.fileName"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                $t('forms.placeholders.entities.fileName'),
              ])
            "
            prefix-icon="el-icon-document"
          />
        </el-form-item>

        <el-form-item label-width="0" prop="exportType">
          <el-radio-group v-model="form.exportType" size="small">
            <el-radio :label="exportDataQuantity.currentPage">
              {{ $t('dialogs.export.currentPage') }}
            </el-radio>
            <el-radio :label="exportDataQuantity.allData">
              {{ $t('dialogs.export.allData') }}
            </el-radio>
          </el-radio-group>
        </el-form-item>
        <mp-table-layout :title="$t('dialogs.export.exportFields')">
          <template #table="propsSlotTable">
            <mp-table
              :table-name="tableName"
              :column-defs="columnDefs"
              :row-data="rowData"
              :height="propsSlotTable.tableHeight"
              :loading="loading"
              @check-row="checkRow"
              @check-all-row="checkAllRow"
              @structure="handlerStructure"
            />
          </template>
        </mp-table-layout>
      </el-form>
      <template slot="footer">
        <mp-button
          type="primary"
          mp-status="normal"
          mp-type="apply"
          @click="submit"
        >
          {{ $t('button.title.perform') }}
        </mp-button>

        <mp-button type="" mp-status="normal" mp-type="close" @click="close">
          {{ $t('button.title.close') }}
        </mp-button>
      </template>
    </el-dialog>
  </FormLayout>
</template>
<script lang="ts">
/**
 * @description общий диалог выгрузки в excel определенных таблиц и их колонок.
 */
import Vue from 'vue'
import { TableName } from '@/modules/table_grid/TableName.const'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { methods } from '@/api_services/httpMethods.enum'
import { GroupingRows } from '@/modules/GroupingRows/GroupingRows'
import { TreeStructure } from '@/modules/TreeStructure/TreeStructure'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { VForm } from '@/modules/Form/Form.interface'
import { exportDataQuantity, IDataExportExcel } from './MpExportExcel.interface'
import { ExportTableColumns } from '@/components/MpExportExcel/MpExportExcel.const'
import { columnProcessingRules } from '@/modules/Export/ProcessingRules/ColumnProcessingRules.const'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import { errorMessage } from '@/modules/Messages/Messages.plugin'
import {
  IStructureItem,
  TreeStructureEvent,
} from '@/modules/TreeStructure/TreeStructure.types'
import { ExportColumn } from '@/modules/Export/Export'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export default Vue.extend({
  name: 'MpExportExcel',
  components: {
    MpTableLayout,
    FormLayout,
    MpButton,
    MpTable,
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
  data(): IDataExportExcel {
    return {
      tableName: TableName.exportExcel,
      exportDataQuantity: exportDataQuantity,
      structure: new TreeStructure({}),
      rowDataChecked: new RowDataChecked([], TableName.exportExcel, 0),
      columnDefs: ExportTableColumns,
      rowData: [],
      checked: [],
      loading: false,
      form: {
        fileName: '',
        exportType: exportDataQuantity.currentPage,
      },
      rules: {
        fileName: [
          {
            required: true,
            message: this.$t('forms.validationError.pleaseEnterEntity', [
              (this.$t(
                'forms.placeholders.entities.fileName',
              ) as string).toLowerCase(),
            ]),
            trigger: TriggerType.blur,
          },
        ],
      },
    }
  },
  mounted() {
    this.getExportList()
  },
  methods: {
    async getExportList() {
      this.loading = true
      const res = await this.$api.filtrationPanelList({
        method: methods.get,
        data: { tableName: this.listQuery.tableName },
      })
      if (res) {
        /**
         * Бизнес логика:
         * 1. добавление propertyGroupId и propertyGroupName для основной таблицы,
         *    так как приходит null
         * 2. добавление постфикса для propertyGroupId подчиненной таблицы,
         *    чтобы не задваивались id после группировки
         *    (добавляестся тут, чтобы ничего не сломать в других таблицах)
         */
        const data = res.map(row => {
          if (row.isMainTable) {
            row.name = this.$t(`tables.${row.tableName}.${row.columnName}`)
            row.propertyGroupName = this.$t('tables.Inv_Excel_Export.mainTable')
            row.propertyGroupId = -1
          } else {
            row.propertyGroupId = `groupId_${row.propertyGroupId}`
            row.name = this.$t(`filter.dependentPanel.${row.privateName}`)
          }
          return row
        })
        /**
         * 1. Создаем группу;
         * 2. Переделываем данные группы в структуру;
         * 3. Создаем структуру;
         * 4. Добавляемся чекбоксы в структуру;
         */
        const groupRow = new GroupingRows(data)
        const groupedRows = groupRow.getGroupingRows()
        this.structure.updateData(
          (groupRow.convertToStructure(groupedRows, false) as unknown) as Array<
            IStructureItem
          >,
        )
        this.rowDataChecked.init(
          this.structure.getRowData(),
          groupedRows.length,
        )
        this.setInitialChecked()
      } else {
        this.rowData = []
      }
      this.loading = false
    },
    setInitialChecked() {
      const mainTableGroupTitle = this.rowDataChecked.rowData.find(
        row => row.id === -1,
      )
      if (mainTableGroupTitle) {
        this.rowDataChecked.setRowCheckboxDisabled(mainTableGroupTitle, true)
        mainTableGroupTitle.checkbox = true
        this.checkRow(mainTableGroupTitle)
      } else {
        this.rowData = this.rowDataChecked.rowData
      }
    },
    close() {
      this.$emit('close')
    },
    checkRow(data) {
      this.rowData = this.rowDataChecked.toggleCheck(
        data.row,
        data.row.checkbox,
      )
      this.setChecked()
    },
    checkAllRow(event) {
      this.rowData = this.rowDataChecked.checkAllRow(event.checkedAll)
      this.setInitialChecked()
      this.setChecked()
    },
    setChecked() {
      this.checked = this.rowDataChecked.checkedRowsList
    },
    handlerStructure(event: TreeStructureEvent) {
      this.rowDataChecked.setRowData(this.structure[event.type](event.data))
      this.rowData = this.structure[event.type](event.data)
    },
    async submit() {
      const form: VForm = this.$refs['export'] as VForm
      form.validate(async valid => {
        if (valid) {
          const columns = this.setColumnsForRequest()
          if (columns.length) {
            this.$emit('export', {
              dto: this.formListQueryToAssign(),
              fileName: this.form.fileName,
              exportColumns: columns,
            })
          } else {
            errorMessage(
              '',
              this.$t('forms.validationError.pleaseChooseEntity', [
                (this.$t(
                  'forms.placeholders.entities.columns',
                ) as string).toLowerCase(),
              ]) as string,
            )
          }
        }
      })
    },
    formListQueryToAssign() {
      if (this.form.exportType === exportDataQuantity.allData) {
        return { ...this.listQuery, Limit: null, Page: null }
      }
      return this.listQuery
    },
    setColumnsForRequest() {
      const columnList: Array<ExportColumn> = []
      this.checked.forEach(checkedRow => {
        if (!checkedRow.isTitleGroup) {
          columnList.push(this.getColumnSettings(checkedRow))
        }
      })
      return columnList
    },
    getColumnSettings(row): ExportColumn {
      const config = columnProcessingRules[row.privateName || row.columnName]
      return new ExportColumn(row, config)
    },
  },
})
</script>

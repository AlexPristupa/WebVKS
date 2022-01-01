import { TreeStructure } from '@/modules/TreeStructure/TreeStructure'
import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'
import { TableName } from '@/modules/table_grid/TableName.const'

export enum exportDataQuantity {
  allData = 'allData',
  currentPage = 'currentPage',
}

export interface IDataExportExcel {
  exportDataQuantity: Record<string, exportDataQuantity>
  tableName: TableName
  loading: boolean
  structure: TreeStructure
  rowDataChecked: RowDataChecked
  columnDefs: Array<IColumnDefs>
  rowData: Array<ISettingExportRow>
  checked: Array<ISettingExportRow>
  form: IExportColumnsForm
  rules: IFormRules
}

export interface ISettingExportRow {
  id: number | string
  name?: string
  tableName?: string
  columnName?: string
  isMainTable?: boolean
  privateName?: string | null
  isTitleGroup?: boolean
}

interface IExportColumnsForm {
  fileName: string
  exportType: exportDataQuantity
}

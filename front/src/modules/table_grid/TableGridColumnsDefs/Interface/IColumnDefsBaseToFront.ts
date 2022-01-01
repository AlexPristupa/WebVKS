/**
 * @description Базовый интерфрейс определяющий колонку на фронте
 */
import { FilterType } from '@/modules/Filters/Filters.const'
import { sortType } from '@/modules/Sort/Sort.const'

export default interface IColumnDefsBaseToFront {
  headerName: string
  field: string
  sortable?: boolean
  filter?: boolean
  width?: number
  minWidth?: number
  maxWidth?: number
  flex?: number
  filterFramework?: string
  hide: boolean
  sort?: sortType | null
  resizable?: boolean
  headerTooltip?: string
  checkboxSelection?: boolean
  cellClass?: string | Array<string>
  cellRenderer?: string
  filterParams?: FilterType
  userOrderColumn: number
  tooltipComponent: string
  tooltipField?: string
  isVisibleHint?: boolean
  cellRendererParams: { tableName: string }
}

import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'
import { IEventSearch } from '@/components/basic/MpSearch/MpSearch.interface'
import IColumnDefsBaseToFront from '@/modules/table_grid/TableGridColumnsDefs/Interface/IColumnDefsBaseToFront'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { IMpCellActionButtonEmitted } from '@/components/MpTable/cell/config/MpCellActionButton.interface'

/**
 * @description интерфейсы для компонента пенели с встроенной таблицей
 */
export interface IDataPanelTable {
  loading: boolean
  loadingDelete: boolean
  listQuery: TableDto
  listQueryToDelete: TableDto
  paginationPosition: typeof PaginationPosition
  paginationTotal: number
  rowData: any[]
  columnDefs: Array<IColumnDefsBaseToFront>
  checked: Array<string | number>
  checkedType: HeaderCheckboxState
  rowDataChecked: RowDataChecked
  visibleEditModal: boolean
  visibleConfirmDeleteModal: boolean
  selectedEntity: any
}

export interface IMethodsPanelTable {
  getList(): void

  applyColumnRender(): void

  search(value: IEventSearch): void

  sortChanged(data): void

  paginationChanged(listQuery: TableDto): void

  filterChanged(listQuery: TableDto): void

  savedFilterChange(filters: Array<IFilterState>): void

  checkRow(row): void

  checkAllRow(event): void

  prepareCheckedToDelete(type): void

  rowSelected(params): void

  emitTableDto(): void

  handlerRemove(): void

  actionsHandler(data: IMpCellActionButtonEmitted): void

  addEntity(setting?: { permanent: boolean }): void

  editEntity(row): void

  deleteEntity(row): void
}

export interface IComputedPanelTable {
  filterActive: boolean
}

export interface IPropsPanelTable {}

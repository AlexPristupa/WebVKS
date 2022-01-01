import { sortType } from '@/modules/Sort/Sort.const'
import { FilterType } from '@/modules/Filters/Filters.const'

export interface IColumnDefs {
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
  tooltipField?: string
  sort?: sortType | null
  resizable?: boolean
  showCheckboxPopover?: boolean
  tableName?: string
  headerTooltip?: string
  checkboxSelection?: boolean
  cellClass?: string | Array<string>
  cellRenderer?: nameImportComponent
  autoHeight?: boolean
  wrapText?: boolean
  filterParams?: FilterType
}

type nameImportComponent =
  | 'MpCellActionButton'
  | 'MpCellStatus'
  | 'agColumnHeader'
  | 'MpCellOpen'
  | 'MpCellInOut'
  | 'MpCellPhoneMask'
  | 'MpCellMatching'
  | 'MpCellDateTime'
  | 'MpCellStructure'
  | 'MpCellGroup'
  | 'MpCellBoolean'
  | 'MpCellSelect'
  | 'MpCellCheckboxes'
  | 'MpCellTranslate'
  | 'MpCellCheckbox'
  | 'MpCellContextMenu'
  | 'MpCellEditValue'
  | 'MpCellTimer'

import { IColumnDefs } from '@/components/MpTable/MpTable.interface.ts'
import { FilterType } from '@/modules/Filters/Filters.const'

export const COLUMN_DEFS_EDITING_HISTORY_TABLE: Array<IColumnDefs> = [
  {
    headerName: 'tables.Inv_Extension_Logs.dateRecord',
    field: 'dateRecord',
    sortable: true,
    filter: true,
    hide: false,
    resizable: true,
    width: 140,
    filterParams: FilterType.date,
  },
  {
    headerName: 'tables.Inv_Extension_Logs.userName',
    field: 'userName',
    sortable: true,
    filter: true,
    hide: false,
    resizable: true,
    width: 100,
    filterParams: FilterType.select,
  },
  {
    headerName: 'tables.Inv_Extension_Logs.action',
    field: 'action',
    sortable: true,
    filter: true,
    hide: false,
    resizable: true,
    width: 120,
    filterParams: FilterType.select,
  },
  {
    headerName: 'tables.Inv_Extension_Logs.description',
    field: 'description',
    sortable: true,
    autoHeight: true,
    wrapText: true,
    filter: true,
    hide: false,
    resizable: true,
    width: 100,
    flex: 1,
    filterParams: FilterType.select,
  },
]

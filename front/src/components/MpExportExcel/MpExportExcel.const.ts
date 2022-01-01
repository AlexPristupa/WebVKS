import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import i18n from '@/i18n'

export const ExportTableColumns: Array<IColumnDefs> = [
  {
    headerName: i18n.t('tables.Inv_Excel_Export.name') as string,
    field: 'name',
    sortable: false,
    filter: false,
    hide: false,
    resizable: false,
    width: 100,
    cellRenderer: 'MpCellStructure',
    flex: 1,
  },
  {
    headerName: '',
    field: 'checkbox',
    showCheckboxPopover: false,
    sortable: false,
    filter: false,
    hide: false,
    resizable: false,
    minWidth: 38,
    width: 38,
    maxWidth: 38,
    cellRenderer: 'MpCellCheckbox',
  },
]

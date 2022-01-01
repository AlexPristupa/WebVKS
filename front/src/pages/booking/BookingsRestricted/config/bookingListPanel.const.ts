import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const bookingListColumns: Array<IColumnRender> = [
  {
    nameField: 'currentStatus',
    cellRenderer: 'MpCellTranslate',
  },
  {
    nameField: 'nextRun',
    cellRenderer: 'MpCellDateTime',
  },
  {
    nameField: 'counter',
    cellRenderer: 'MpCellTimer',
  },
  {
    nameField: 'actions',
    cellRenderer: 'MpCellActionButton',
  },
]

import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const serverListColumns: Array<IColumnRender> = [
  {
    nameField: 'actions',
    cellRenderer: 'MpCellActionButton',
  },
]

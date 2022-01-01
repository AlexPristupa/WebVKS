import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const serverGroupListColumns: Array<IColumnRender> = [
  {
    nameField: 'actions',
    minWidth: 150,
    cellRenderer: 'MpCellActionButton',
  },
]

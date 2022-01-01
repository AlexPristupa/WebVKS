import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const userProfileListColumns: Array<IColumnRender> = [
  {
    nameField: 'actions',
    cellRenderer: 'MpCellActionButton',
  },
]

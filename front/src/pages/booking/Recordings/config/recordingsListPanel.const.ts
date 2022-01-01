import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const recordingsListColumns: Array<IColumnRender> = [
  {
    nameField: 'dateStart',
    cellRenderer: 'MpCellDateTime',
  },
  {
    nameField: 'dateEnd',
    cellRenderer: 'MpCellDateTime',
  },
  {
    nameField: 'actions',
    cellRenderer: 'MpCellActionButton',
  },
]

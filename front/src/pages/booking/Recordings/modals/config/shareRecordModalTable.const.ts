import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const shareRecordModalTableColumns: Array<IColumnRender> = [
  {
    nameField: 'user',
  },
  {
    nameField: 'isPlay',
    cellRenderer: 'MpCellCheckbox',
    cellClass: ['mp-cell--flex-centre'],
  },
  {
    nameField: 'isDownload',
    cellRenderer: 'MpCellCheckbox',
    cellClass: ['mp-cell--flex-centre'],
  },
  {
    nameField: 'dateRecord',
    cellRenderer: 'MpCellDateTime',
  },
  {
    nameField: 'actions',
    cellRenderer: 'MpCellActionButton',
  },
]

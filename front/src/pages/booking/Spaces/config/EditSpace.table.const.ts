import IColumnRender from '@/modules/table_grid/TableGridColumnsDefs/Interface/ColumnRender/IColumnRender'

export const editSpaceBookingTableColumns: Array<IColumnRender> = [
  {
    nameField: 'name',
    sortable: false,
    filter: false,
    flex: 1,
  },
  {
    nameField: 'description',
    sortable: false,
    filter: false,
    flex: 1,
  },
  {
    nameField: 'currentStatus',
    cellRenderer: 'MpCellTranslate',
    sortable: false,
    filter: false,
    flex: 1,
  },
  {
    nameField: 'nextRun',
    cellRenderer: 'MpCellDateTime',
    sortable: false,
    filter: false,
    flex: 1,
  },
]

export const editSpaceParticipantsTableColumns: Array<IColumnRender> = [
  {
    nameField: 'vksUser',
    cellRenderer: 'MpCellSelect',
    tooltipField: 'vksUserName',
  },
  {
    nameField: 'callLegProfileGuid',
    cellRenderer: 'MpCellSelect',
  },
  {
    nameField: 'rights',
    cellRenderer: 'MpCellCheckboxes',
    minWidth: 400,
    autoHeight: true,
    wrapText: true,
  },
  {
    nameField: 'actions',
    cellClass: ['mp-cell--flex-centre'],
    cellRenderer: 'MpCellActionButton',
  },
]

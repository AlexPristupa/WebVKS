import { TableName } from '@/modules/table_grid/TableName.const'

export default {
  mpCellHeaderCheckbox: {
    selectedEntries: 'Selected entries',
    entriesPage: 'Only records per page',
    entriesAll: 'All records by filter',
  },
  MpCellContextMenu: {
    follow: 'Follow:',
    numbers: 'Numbers',
  },
  MpCellTranslate: {
    [TableName.booking]: {
      STOP: 'Stopped',
      START: 'Launched',
    },
    [TableName.editSpaceBooking]: {
      STOP: 'Stopped',
      START: 'Launched',
    },
  },
  MpCellCheckboxes: {
    canDestroy: 'Room deleting',
    canChangeName: 'The room name changing',
    canChangeCallId: 'The conference ID changing',
    canAddRemoveMember: 'Adding/removing participants',
    canChangeUri: 'The URI changing',
    canChangePassCode: 'The conference password changing',
    canRemoveSelf: 'Removing yourself (from the participants)',
    canChangeNonMemberAccessAllowed: 'Room access changing',
  },
  MpCellTimer: {
    [TableName.booking]: {
      nextRun: 'before the start',
      dateEnd: 'until the end',
    },
  },
}

import { TableName } from '@/modules/table_grid/TableName.const'

export default {
  mpCellHeaderCheckbox: {
    selectedEntries: 'Выбрано записей',
    entriesPage: 'Только записи на странице',
    entriesAll: 'Все записи по фильтру',
  },
  MpCellContextMenu: {
    follow: 'Перейти:',
    numbers: 'Номера',
  },
  MpCellTranslate: {
    [TableName.booking]: {
      STOP: 'Остановлена',
      START: 'Запущена',
    },
    [TableName.editSpaceBooking]: {
      STOP: 'Остановлена',
      START: 'Запущена',
    },
  },
  MpCellCheckboxes: {
    canDestroy: 'Удаление комнаты',
    canChangeName: 'Изменение имени комнаты',
    canChangeCallId: 'Изменение идентификатора конференции',
    canAddRemoveMember: 'Добавление/удаление участников',
    canChangeUri: 'Изменение URI',
    canChangePassCode: 'Изменение пароля конференции',
    canRemoveSelf: 'Удаление себя (из участников)',
    canChangeNonMemberAccessAllowed: 'Может редактировать доступ к комнате',
  },
  MpCellTimer: {
    [TableName.booking]: {
      nextRun: 'до начала',
      dateEnd: 'до завершения',
    },
  },
}

import i18n from '@/i18n'
import { weekdaysEnum } from '@/domain'

export const monthStringFormators = {
  days: 'days:',
  daysMinus: 'days:-1',
}

export const list = [
  { id: 1, name: i18n.t(`forms.editBooking.texts.every`) },
  {
    id: 2,
    name: i18n.t(`forms.editBooking.texts.at`),
  },
]

export const weekdaysButtons = [
  // monday: 'Пн',
  // tuesday: 'Вт',
  // wednesday: 'Ср',
  // thursday: 'Чт',
  // friday: 'Пт',
  // saturday: 'Сб',
  // sunday: 'Вс',
  { id: 1, name: i18n.t(`button.weekdaysShortcuts.monday`) },
  {
    id: 2,
    name: i18n.t(`button.weekdaysShortcuts.tuesday`),
  },
  { id: 3, name: i18n.t(`button.weekdaysShortcuts.wednesday`) },
  {
    id: 4,
    name: i18n.t(`button.weekdaysShortcuts.thursday`),
  },
  {
    id: 5,
    name: i18n.t(`button.weekdaysShortcuts.friday`),
  },
  { id: 6, name: i18n.t(`button.weekdaysShortcuts.saturday`) },
  {
    id: 7,
    name: i18n.t(`button.weekdaysShortcuts.sunday`),
  },
]

export const weekdays = {
  [i18n.t(`button.weekdaysShortcuts.monday`).toString()]: weekdaysEnum.monday,
  [i18n.t(`button.weekdaysShortcuts.tuesday`).toString()]: weekdaysEnum.tuesday,
  [i18n
    .t(`button.weekdaysShortcuts.wednesday`)
    .toString()]: weekdaysEnum.wednesday,
  [i18n
    .t(`button.weekdaysShortcuts.thursday`)
    .toString()]: weekdaysEnum.thursday,
  [i18n.t(`button.weekdaysShortcuts.friday`).toString()]: weekdaysEnum.friday,
  [i18n
    .t(`button.weekdaysShortcuts.saturday`)
    .toString()]: weekdaysEnum.saturday,
  [i18n.t(`button.weekdaysShortcuts.sunday`).toString()]: weekdaysEnum.sunday,
  [i18n.t(`button.weekdays.monday`).toString()]: weekdaysEnum.monday,
  [i18n.t(`button.weekdays.tuesday`).toString()]: weekdaysEnum.tuesday,
  [i18n.t(`button.weekdays.wednesday`).toString()]: weekdaysEnum.wednesday,
  [i18n.t(`button.weekdays.thursday`).toString()]: weekdaysEnum.thursday,
  [i18n.t(`button.weekdays.friday`).toString()]: weekdaysEnum.friday,
  [i18n.t(`button.weekdays.saturday`).toString()]: weekdaysEnum.saturday,
  [i18n.t(`button.weekdays.sunday`).toString()]: weekdaysEnum.sunday,
}

import i18n from '@/i18n'

export const weekStringFormators = {
  days: 'days:',
}

export const list = [
  {
    id: 1,
    name: i18n.t(`forms.editBooking.texts.everyDate`),
  },
  {
    id: 2,
    name: i18n.t(`forms.editBooking.texts.lastWeekday`),
  },
  {
    id: 3,
    name: i18n.t(`forms.editBooking.texts.lastDay`),
  },
  {
    id: 4,
    name: i18n.t(`forms.editBooking.texts.in`),
  },
]

export const weekdaysList = [
  { id: 1, name: i18n.t(`button.weekdays.monday`) },
  {
    id: 2,
    name: i18n.t(`button.weekdays.tuesday`),
  },
  { id: 3, name: i18n.t(`button.weekdays.wednesday`) },
  {
    id: 4,
    name: i18n.t(`button.weekdays.thursday`),
  },
  {
    id: 5,
    name: i18n.t(`button.weekdays.friday`),
  },
  { id: 6, name: i18n.t(`button.weekdays.saturday`) },
  {
    id: 7,
    name: i18n.t(`button.weekdays.sunday`),
  },
]

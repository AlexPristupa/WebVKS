import i18n from '@/i18n'

export const dayStringFormators = {
  day: 'day',
  hour: 'hour',
  minute: 'minute',
  days: 'days:Monday,Tuesday,Wednesday,Thursday,Friday',
}

export const list = [
  { id: 1, name: i18n.t(`forms.editBooking.texts.every`) },
  {
    id: 2,
    name: i18n.t(`forms.editBooking.texts.every`),
  },
  {
    id: 3,
    name: i18n.t(`forms.editBooking.texts.everyWeekday`),
  },
]

export const frequencyList = [
  { id: 1, name: i18n.t(`forms.editBooking.texts.hour`) },
  {
    id: 2,
    name: i18n.t(`forms.editBooking.texts.minutes`),
  },
]

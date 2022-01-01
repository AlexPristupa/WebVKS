import i18n from '@/i18n'
import { TranslateResult } from 'vue-i18n'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { IFormLayoutColumn } from '@/layouts/formLayout/formColumnLayout.interface'

export enum periodicTypes {
  constantRU = 'Постоянная',
  periodic = 'PERIODIC',
  constant = 'CONSTANT',
  onetime = 'ONETIME',
}

export const intersectionFields = [
  'hour',
  'minute',
  'dateStart',
  'dateEnd',
  'timeZone',
  'schedule',
  'repeatCount',
  'openConferenceBefore',
  'periodic',
  'spaceId',
]

export const modalColumns: IFormLayoutColumn = {
  number: 3,
  equals: true,
  buttons: [
    [],
    [],
    [
      {
        type: MpTypeButton.add,
        title: i18n.t('dialogs.titles.addBookingListParticipant').toString(),
      },
    ],
  ],
  titles: [
    'forms.editBooking.titles.settings',
    'forms.editBooking.titles.cmsSpace',
    'forms.editBooking.titles.participants',
  ],
}

export const modalPermanentsColumns: IFormLayoutColumn = {
  number: 2,
  equals: true,
  titles: [
    'forms.editBooking.titles.settings',
    'forms.editBooking.titles.cmsSpace',
  ],
}

export const durationSelectHours: Array<{
  label: string
  value: number
}> = Array.from({ length: 25 }, (_, i) => ({
  label: i + ` ${i18n.t('general.time.hourShortcut')}`,
  value: i * 60,
}))

export const durationSelectMinutes: Array<{
  label: string
  value: number
}> = [
  { label: `0 ${i18n.t('general.time.minuteShortcut')}`, value: 0 },
  { label: `15 ${i18n.t('general.time.minuteShortcut')}`, value: 15 },
  { label: `30 ${i18n.t('general.time.minuteShortcut')}`, value: 30 },
  { label: `45 ${i18n.t('general.time.minuteShortcut')}`, value: 45 },
]

export const placeholders: { [key: string]: string | TranslateResult } = {
  name: i18n.t('forms.placeholders.enterEntity', [
    FirstLetterToCase.placeholderToLower('name', 'editBooking'),
  ]),
  description: i18n.t('forms.placeholders.enterEntity', [
    FirstLetterToCase.placeholderToLower('description', 'editBooking'),
  ]),
  location: i18n.t('forms.placeholders.enterEntity', [
    FirstLetterToCase.placeholderToLower('location', 'editBooking'),
  ]),
  ownerId: i18n.t('forms.placeholders.chooseEntity', [
    i18n.t('forms.editBooking.placeholders.owner'),
  ]),
  dateStart: i18n.t('forms.editBooking.placeholders.dateStart'),
  timeZone: i18n.t('forms.placeholders.chooseEntity', [
    i18n.t('forms.editBooking.placeholders.timeZone'),
  ]),
  pinCode: i18n.t('forms.placeholders.enterEntity', [
    i18n.t('forms.editBooking.pinCode'),
  ]),
  dateEnd: FirstLetterToCase.placeholderToLower('dateEnd', 'editBooking'),
  connectionType: i18n.t('forms.placeholders.chooseEntity', [
    FirstLetterToCase.placeholderToLower('connectionType', 'editBooking'),
  ]),
}

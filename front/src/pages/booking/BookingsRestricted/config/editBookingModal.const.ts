import { selectType } from '@/pages/booking/Bookings/modals/config/editBookingList.interface'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { IFormLayoutColumn } from '@/layouts/formLayout/formColumnLayout.interface'
import i18n from '@/i18n'

export const selectTypes = {
  single: selectType.single,
  multiple: selectType.multiple,
}

export const modalColumns: IFormLayoutColumn = {
  number: 2,
  equals: true,
  buttons: [
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
    'forms.editBooking.titles.participants',
  ],
}

import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import {
  datePickerType,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import {
  BookingModalListType,
  listClass,
  selectType,
} from '@/pages/booking/Bookings/modals/config/editBookingList.interface'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity.ts'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import { VksSelectOptionWithPrivateEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOptionWithPrivate.entity'
import { IFormLayoutColumn } from '@/layouts/formLayout/formColumnLayout.interface'
import { VksTimeZoneEntity } from '@/modules/ApiDataValidation/ResponseDto/TimeZone/VksTimeZone.entity.ts'
import { TranslateResult } from 'vue-i18n'
import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'

export interface IComputedEditBookingModal extends IComputedEditModal {
  columns: IFormLayoutColumn
  spacesChecked: Array<VksSelectOptionEntity>
}

export interface IDataEditBookingModal extends IDataEditModal {
  placeholders: { [key: string]: string | TranslateResult }
  selectTypes: {
    [key: string]: selectType
  }
  datePickerTypes: {
    [key: string]: datePickerType
  }
  timeFormats: {
    [key: string]: timeFormat
  }
  text: string
  rules: IFormRules
  participantsCheckedCopy: Array<BookingModalListType>
  participantsChecked: Array<BookingModalListType>
  listClass: {
    spaces: listClass.spaces
    participants: listClass.participants
  }
  optionsParticipantsLoading: boolean
  optionsSpacesLoading: boolean
  hasIntersection: boolean
  entityModel: VksBookingEntity
  connectionTypeOptions: Array<VksSelectOptionWithPrivateEntity>
  ownerOptions: Array<VksSelectOptionEntity>
  spacesOptions: Array<VksSelectOptionEntity>
  participantsOptions: Array<BookingModalListType>
  timeZoneOptions: Array<VksTimeZoneEntity>
}

export interface IPropsEditBookingModal extends IPropsEditModal {
  permanent: boolean
}

export interface IMethodsEditBookingModal extends IMethodsEditModal {
  getOptions(): void

  entityChanged(entity: string, value: string | boolean | number): void

  dateEndValidation(rule: any, value: any, callback: any): void

  durationValidation(rule: any, value: any, callback: any): void

  updateText(text?: string): void

  getSelectedBooking(id: number): void

  getOptionsSpaces(search?: string): void

  getBookingTypeOptions(): void

  getOptionsOwner(search?: string): void

  getOptionsParticipants(search?: string): void

  findIntersection(): void

  getConnectionTypeOptions(search?: string): void

  getTimeZoneOptions(search?: string): void

  updateSpace(spaces: Array<{ id: number }>): void

  addLinkBookingToParticipants(): void

  updateParticipants(participants: Array<BookingModalListType>): void

  updateParticipant(updatedParticipant: BookingModalListType): void

  createParticipant(createdParticipant: BookingModalListType): void

  setParticipantsChecked(): void

  setParticipantsCheckedCopy(participant: BookingModalListType): void
}

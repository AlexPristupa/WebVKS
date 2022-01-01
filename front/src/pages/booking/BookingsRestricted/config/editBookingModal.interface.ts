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
  selectType,
} from '@/pages/booking/Bookings/modals/config/editBookingList.interface'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity.ts'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import { VksSelectOptionWithPrivateEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOptionWithPrivate.entity'
import { IFormLayoutColumn } from '@/layouts/formLayout/formColumnLayout.interface'
import { VksTimeZoneEntity } from '@/modules/ApiDataValidation/ResponseDto/TimeZone/VksTimeZone.entity.ts'
import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'

export interface IComputedEditBookingModal extends IComputedEditModal {
  columns: IFormLayoutColumn
}

export interface IDataEditBookingModal extends IDataEditModal {
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
  optionsParticipantsLoading: boolean
  hasIntersection: boolean
  participantsCheckedCopy: Array<BookingModalListType>
  participantsChecked: Array<BookingModalListType>
  entityModel: VksBookingEntity
  connectionTypeOptions: Array<VksSelectOptionWithPrivateEntity>
  ownerOptions: Array<VksSelectOptionEntity>
  participantsOptions: Array<BookingModalListType>
  timeZoneOptions: Array<VksTimeZoneEntity>
}

export interface IPropsEditBookingModal extends IPropsEditModal {
  permanent: boolean
}

export interface IMethodsEditBookingModal extends IMethodsEditModal {
  entityChanged(entity: string, value: string | boolean): void

  updateText(text?: string): void

  getSelectedBooking(id: number): void

  toTimeZone(date: string): string

  dateEndValidation(rule: any, value: any, callback: any): void

  durationValidation(rule: any, value: any, callback: any): void

  getOptionsParticipants(search?: string): void

  findIntersection(): void

  getOptionsOwnerByLogin(search?: string): void

  getBookingTypeOptions(): void

  getTimeZoneOptions(search?: string): void

  updateParticipants(participants: Array<BookingModalListType>): void

  updateParticipant(updatedParticipant: BookingModalListType): void

  createParticipant(createdParticipant: BookingModalListType): void

  getOptionsSpaces(search?: string): void

  setParticipantsChecked(): void

  addLinkBookingToParticipants(): void

  setParticipantsCheckedCopy(participant: BookingModalListType): void
}

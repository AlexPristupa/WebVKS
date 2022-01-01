import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity.ts'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { ElementUIIcons } from '@/components/MpTable/cell/config/MpCellActionButton.interface'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity.ts'
import { VksUserEntity } from '@/modules/ApiDataValidation/ResponseDto/User/User.entity.ts'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'
import { VksUserOtherEntity } from '@/modules/ApiDataValidation/ResponseDto/UserOthers/UserOther.entity'

export enum selectType {
  single = 'single',
  multiple = 'multiple',
}

export enum listClass {
  spaces = 'spaces',
  participants = 'participants',
}

export type BookingModalListType =
  | VksSelectOptionEntity
  | VksUserOtherEntity
  | VksBookingLinkToParticipant
  | VksBookingLinkBookingToVksUsersOthers

export interface IDataBookingList {
  list: Array<BookingModalListType>
  search: string
  classes: {
    spaces: listClass.spaces
    participants: listClass.participants
  }
  selectedItem: BookingModalListType
  buttonType: MpTypeButton
  buttonIcon: ElementUIIcons
  isVisibleParticipantDialog: boolean
}

export interface IPropsBookingList {
  listClass: listClass
  selectType: selectType
  options: Array<BookingModalListType>
  checked: Array<BookingModalListType>
  checkedCopy: Array<BookingModalListType>
  spaceId: number
  selectedOwner: number
  loading: boolean
}

export interface IComputedBookingList {
  searchDisabled: boolean
  checkedIdList: Array<number | string>
}

export interface IMethodsBookingList {
  toggleCheckParticipants(
    item: BookingModalListType,
  ): Promise<Array<BookingModalListType>>

  getLabel(item: BookingModalListType): string

  getUser(
    id: number | string,
  ): Promise<VksUserEntity | VksUserOtherEntity | undefined>

  editList(editedUser: BookingModalListType): void

  createUser(createdUser: BookingModalListType): void

  searchOptions(): void

  setEntity(item: BookingModalListType): void

  setCheckboxes(): void

  openParticipantDialog(item: BookingModalListType): void
}

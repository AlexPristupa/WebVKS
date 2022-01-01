import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'
import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface.ts'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity.ts'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import { SpacesCaLegProfilesProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/SpacesCaLegProfilesProxy.entity'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'

export interface IComputedEditBookingListParticipantModal
  extends IComputedEditModal {
  rules: IFormRules
}

export interface IDataEditBookingListParticipantModal extends IDataEditModal {
  callLegProfileGuidOptions: Array<SpacesCaLegProfilesProxyEntity>
  FirstLetterToCase: FirstLetterToCase
}

export interface IPropsEditBookingListParticipantModal extends IPropsEditModal {
  selectedEntity:
    | VksSelectOptionEntity
    | VksBookingLinkToParticipant
    | VksBookingLinkBookingToVksUsersOthers
  spaceId: number
}

export interface IMethodsEditBookingListParticipantModal
  extends IMethodsEditModal {
  entityChanged(entity: string, value: string): void
  uriValidator(rule: any, value: any, callback: any): void
  getCallLegProfileGuidOptions(): void
  getSpaceId(id: number): Promise<number> | void
}

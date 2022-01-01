import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'

export interface IComputedBookingPinSettingsModal extends IComputedEditModal {}

export interface IDataBookingPinSettingsModal extends IDataEditModal {
  shiftSettingList: Array<IShiftSetting>
  entityModel: IEntityModelBookingPinSettingsModal
}

export interface IPropsBookingPinSettingsModal extends IPropsEditModal {}

export interface IMethodsBookingPinSettingsModal extends IMethodsEditModal {
  changeShift(value: number): void
  getOptions(search?: string): void
}

interface IEntityModelBookingPinSettingsModal {
  pinPoliticsId: number
}

interface IShiftSetting {
  id: number
  name: string
}

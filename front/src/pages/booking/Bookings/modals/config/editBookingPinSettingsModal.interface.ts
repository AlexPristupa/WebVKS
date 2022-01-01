import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { VksSelectOptionWithPrivateEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOptionWithPrivate.entity'

export interface IComputedBookingPinSettingsModal extends IComputedEditModal {}

export interface IDataBookingPinSettingsModal extends IDataEditModal {
  text: string
  shiftSettingList: Array<VksSelectOptionWithPrivateEntity>
  entityModel: PinSettingsEntity
}

export interface IPropsBookingPinSettingsModal extends IPropsEditModal {
  selectedEntity: PinSettingsEntity
}

export interface IMethodsBookingPinSettingsModal extends IMethodsEditModal {
  getOptions(search?: string): void
  setPoliticsId(id: number): void
  updateText(text?: string): void
}

export class PinSettingsEntity {
  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
    }
    return this
  }

  public id: number = 0
  public pinScheduleTab: string = '2'
  public pinPoliticsId: number = 1
  public pinSchedule: string = ''
  public pinDateStart: string | null = null
}

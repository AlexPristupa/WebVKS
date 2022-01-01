import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'

export interface IDataShareRecordAddUserModal extends IDataEditModal {
  FirstLetterToCase: FirstLetterToCase
  userOptions: Array<any>
}
export interface IMethodsShareRecordAddUserModal extends IMethodsEditModal {
  getUserOptions(search: string): void
  userChanged(value: number): void
}
export interface IComputedShareRecordAddUserModal extends IComputedEditModal {
  rules: IFormRules
}
export interface IPropsShareRecordAddUserModal extends IPropsEditModal {
  selectedEntity: VksRecordingsEntity
}

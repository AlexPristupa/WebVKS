import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'

export interface IDataShareRecordModal extends IDataEditModal {}
export interface IMethodsShareRecordModal extends IMethodsEditModal {}
export interface IComputedShareRecordModal extends IComputedEditModal {}
export interface IPropsShareRecordModal extends IPropsEditModal {}

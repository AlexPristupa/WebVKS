import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
/**
 * @description интерфейсы компонента ServerListPanel
 */
export interface IDataEditServerModal extends IDataEditModal {
  entityModel: VksServerEntity
  FirstLetterToCase: FirstLetterToCase
}

export interface IMethodsEditServerModal extends IMethodsEditModal {}

export interface IComputedEditServerModal extends IComputedEditModal {}

export interface IPropsEditServerModal extends IPropsEditModal {
  selectedEntity: VksServerEntity
}

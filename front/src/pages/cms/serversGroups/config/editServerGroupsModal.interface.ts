import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { BalancingAlgorithm } from '@/domain/BalancingAlgorithm/BalancingAlgorithm.entity'
import { VksServersGroupEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServersGroup.entity'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'

/**
 * @description интерфейсы компонента ServerListPanel
 */
export interface IDataEditServerModal extends IDataEditModal {
  FirstLetterToCase: FirstLetterToCase
  entityModel: VksServersGroupEntity
  algorithmOptions: Array<BalancingAlgorithm>
}

export interface IMethodsEditServerModal extends IMethodsEditModal {
  entityChanged(entity: string, value: string | Array<any> | number): void
}

export interface IComputedEditServerModal extends IComputedEditModal {}

export interface IPropsEditServerModal extends IPropsEditModal {
  selectedEntity: VksServersGroupEntity
}

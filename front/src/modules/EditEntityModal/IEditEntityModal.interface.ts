/**
 * @description интерфейсы компонента абстрактной модапки создания/изменения
 *              сущности
 */
import { TranslateResult } from 'vue-i18n'
import { IFormLayoutColumn } from '@/layouts/formLayout/formColumnLayout.interface'
import { FormValidationError } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidation.entity'
import { ApiFormValidationResponse } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/ApiFormValidationResponse'

export const nonCopiedValues = ['action', 'settings']

export interface IDataEditModal {
  nonCopiedValues: Array<string>
  entityModel: any // any Entity
  validationFromBackData?: Array<FormValidationError>
  savingLoading: boolean
  columns?: IFormLayoutColumn
}

export interface IMethodsEditModal {
  submit(): void

  close(): void

  validation?(
    validationArray: Array<ApiFormValidationResponse>,
    rule: any,
    value: any,
    callback: any,
  ): void
}

export interface IComputedEditModal {
  title: TranslateResult | string
  disabledSaveButton: boolean
}

export interface IPropsEditModal {
  selectedEntity: any // any Entity
  visibleEditModal: boolean
}

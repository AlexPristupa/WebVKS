import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'
import { FormValidationError } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidation.entity'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export interface IComputedLoginPage {
  rules: IFormRules
}

export interface IDataLoginPage {
  validationFromBackData: Array<FormValidationError>
  mpTypeLogin: MpTypeButton
  formModel: {
    provider: 'Integrated' | 'Ldap'
    login: string
    password: string
  }
  loading: boolean
}

export interface IPropsLoginPage {}

export interface IMethodsLoginPage {
  onSubmit(): void
  validation(rule: any, value: any, callback: any): void
}

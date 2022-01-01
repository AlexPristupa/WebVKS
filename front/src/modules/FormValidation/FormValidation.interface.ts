import { TranslateResult } from 'vue-i18n'
import { TriggerType } from './FormValidation.const'

interface IFormRule {
  required?: boolean
  message?: string | TranslateResult
  trigger: TriggerType
}

interface IFormRuleWithValidator {
  validator: (rule: any, value: any, callback: any) => void
  trigger?: TriggerType
}

export interface IFormRules {
  [key: string]: Array<IFormRule | IFormRuleWithValidator>
}

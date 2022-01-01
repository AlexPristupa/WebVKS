import { TranslateResult } from 'vue-i18n'

export interface IRowWithLinks {
  id: number
  numberLinked?: string
  links?: Array<IConnectedNumbersLink>
}

export interface IConnectedNumbersLink {
  value: string
  label: TranslateResult
}

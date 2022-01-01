import { TranslateResult } from 'vue-i18n'

export enum formLayoutColumnWidth {
  auto = 'auto',
  full = 'full',
  half = 'half',
  oneThird = 'oneThird',
}

export interface IFormLayoutColumn {
  number: number
  equals: boolean
  buttons?: Array<
    Array<{
      type: string
      title: string
    }>
  >
  titles: Array<string | TranslateResult>
}

export interface IComputedFormColumnLayout {
  slots: Array<number>
  width: formLayoutColumnWidth
}

export interface IDataFormColumnLayout {}

export interface IPropsFormColumnLayout {
  columns: IFormLayoutColumn
}

export interface IMethodsFormColumnLayout {}

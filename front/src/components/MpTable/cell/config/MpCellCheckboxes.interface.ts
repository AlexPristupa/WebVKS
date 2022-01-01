import { TranslateResult } from 'vue-i18n'

export interface ICheckboxesItem {
  label: string | TranslateResult
  field: string
  checked: boolean
}

export interface IMethodsMpCellCheckboxes {
  change(field: string): void
}

export interface IComputedMpCellCheckboxes {
  checkboxes: Array<ICheckboxesItem>
}

export interface IDataMpCellCheckboxes {}
export interface IPropsMpCellCheckboxes {}

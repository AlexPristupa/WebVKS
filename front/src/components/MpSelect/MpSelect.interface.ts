import { MP_SELECT_SIZE } from '@/components/MpSelect/MpSelect.const'
import { QueryChangeHandler } from 'element-ui/types/select'

export interface IDataMpSelect {}

export interface IMethodsMpSelect {
  optionValueField(item): any
  blur(payload: any): void
  change(payload: any): void
  clear(payload: any): void
  focus(payload: any): void
  removeTag(payload: any): void
  visibleChange(payload: any): void
}

export interface IComputedMpSelect {
  optionListField: Array<any>
  valueData: any
}

export interface IPropsMpSelect {
  optionList: Array<any>
  optionKey: string
  optionLabel: string
  optionValue: string

  autocomplete: string
  automaticDropdown: boolean
  allowCreate: boolean
  clearable: boolean
  collapseTags: boolean
  defaultFirstOption: boolean
  disabled: boolean
  filterable: boolean
  filterMethod: QueryChangeHandler
  loading: boolean
  loadingText: string
  multiple: boolean
  multipleLimit: number
  name: string
  noMatchText: string
  noDataText: string
  placeholder: string
  popperClass: string
  popperAppendToBody: boolean
  remote: boolean
  remoteMethod: QueryChangeHandler
  reserveKeyword: boolean
  size: MP_SELECT_SIZE
  value: any
  valueKey: string
}

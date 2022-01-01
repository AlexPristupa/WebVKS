import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { IHeaderEffect } from '@/store/modules/table_header_effects_store/types'

export interface IDataMpCellHeaderCheckbox {
  submenuVisible: boolean
  fullItems: HeaderCheckboxState
  checkboxState: {
    page: HeaderCheckboxState.page
    full: HeaderCheckboxState.full
  }
}

export type IMethodsMpCellHeaderCheckbox = any

export interface IComputedMpCellHeaderCheckbox {
  checkAll: () => boolean
  selectedEntries: () => number
  showCheckboxPopover: () => void
  findActiveColumnsEffects: (tableName: string) => IHeaderEffect
}

export interface IPropsMpCellHeaderCheckbox {
  params: any
}

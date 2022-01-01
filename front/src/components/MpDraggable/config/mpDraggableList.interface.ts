import { TranslateResult } from 'vue-i18n'
import { IDragEvent } from '@/components/MpDraggable/config/mpDraggable.interface'

export interface IDataMpDraggableList {
  search: string
  copiedList: Array<any>
}

export interface IMethodsMpDraggableList {
  moved(event: IDragEvent): void

  add(item: any): void

  remove(item: any): void

  setActive(item: any): void

  update(): void

  activeSearch(): void
}

export interface IComputedMpDraggableList {}

export interface IPropsMpDraggableList {
  withSearch: boolean
  disabled: boolean
  list: Array<any>
  selected: any
  tooltip: string | TranslateResult
}

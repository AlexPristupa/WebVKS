import { TranslateResult } from 'vue-i18n'
import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import { buttonPositions } from '@/components/MpDraggable/config/mpDraggableButtons.interface'

export interface IDataMpDraggable {
  active: null | any
  selectedIn: boolean
  availableList: Array<any>
  currentList: Array<any>
}

export interface IMethodsMpDraggable {
  changePositionInList(position: string): void

  moveHorizontal(position: string): void

  moveVertical(index: number, position: string): void

  update(): void

  setLists(): void

  setCurrentList(list: any): void

  setAvailableList(list: any): void

  setActive(item: any): void
}

export interface IComputedMpDraggable {
  disabledCurrent: boolean
}

export interface IPropsMpDraggable {
  settings: IMpDraggableSettings
}

interface IMpDraggableSettings {
  withSearch: boolean
  disabled: boolean
  titles: Array<string | TranslateResult>
  listForDraggable: Array<any>
  buttons: {
    position: buttonPositions
    isHorizontal: boolean
  }
}

export interface IDragEvent {
  added: {
    element: IColumnDefs | any
    newIndex: number
  }
}

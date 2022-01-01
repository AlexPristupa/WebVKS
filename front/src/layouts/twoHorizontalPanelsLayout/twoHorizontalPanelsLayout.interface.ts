import { ActionMethod } from 'vuex'
import {
  ILayoutItemData,
  ILayoutItemDataSizes,
  ILayoutItemDataIsOpen,
} from '@/store/modules/user_settings/types.ts'

export interface ITwoHorizontalPanelsLayoutHeight {
  height: string
}

export interface ITwoHorizontalPanelsLayoutNewHeights {
  top: number
  bottom: number
  isAddPixels: boolean
}

export interface IDataTwoHorizontalPanelsLayout {
  parent: null | Element | Vue | Vue[] | Element[] | HTMLElement
  top: null | Element | Vue | Vue[] | Element[] | HTMLElement
  line: null | Element | Vue | Vue[] | Element[] | HTMLElement
  bottom: null | Element | Vue | Vue[] | Element[] | HTMLElement
  currHeight: number
  unlock: boolean
}

export interface IMethodsTwoHorizontalPanelsLayout {
  setLayoutItem: ActionMethod
  getOffsetHeight: (HTMLElement) => number
  getHeight: (string) => ITwoHorizontalPanelsLayoutHeight
  getBoundingClientRectFromTop: (HTMLElement) => number
  initialLayoutDataStyle: (string) => string
  updateLayoutDataSizes: (ILayoutItemDataSizes) => void
  updateLayoutDataIsOpen: (ILayoutItemDataIsOpen) => void
  initiateRecalculation: () => void
  updateLayoutDataSizesAndInitiateRecalculation: () => void
  setNewHeightToElement: (HTMLElement, number, boolean) => void
  setNewHeights: (ITwoHorizontalPanelsLayoutNewHeights) => void
  mousedownLineHandler: (EventTarget) => void
  mousedownWindowHandler: (EventTarget) => void
  mousemoveHandler: (EventTarget) => void
  mouseupHandler: (EventTarget) => void
  checkMaxMinValues: (top: number, bottom: number) => void
  dblclickHandler: (EventTarget) => void
  getInitialHeightNumber: (string) => number
  expandCollapseHandler: (string) => void
  addListener: (HTMLElement, string, Function) => void
  removeListener: (HTMLElement, string, Function) => void
  addListeners: () => void
  removeListeners: () => void
}

export interface IComputedTwoHorizontalPanelsLayout {
  layoutItemData: Function
  layoutData: ILayoutItemData
  sizes: ILayoutItemDataSizes
  isOpen: ILayoutItemDataIsOpen
  initialStyleTop: ITwoHorizontalPanelsLayoutHeight
  initialStyleBottom: ITwoHorizontalPanelsLayoutHeight
}

export interface IPropsTwoHorizontalPanelsLayout {
  layoutName: string
  report: boolean
  initialHeightTop: string
  resizeLineSize: number
  initialHeightBottom: string
  minHeight: number
  isCollapsibleTop: boolean
  isCollapsibleBottom: boolean
  smallestSizes: ISmallestSizes
}

export interface ISmallestSizes {
  top: number
  bottom: number
}

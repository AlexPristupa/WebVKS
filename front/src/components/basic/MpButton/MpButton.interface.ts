import {
  MpStatusButton,
  MpTypeButton,
  NativeTypeButton,
  SizeButton,
  TypeButton,
} from '@/components/basic/MpButton/MpButton.const'
import { TranslateResult } from 'vue-i18n'

export interface IDataMpButton {}

export interface IMethodsMpButton {}

export interface IComputedMpButton {
  buttonClass: string
  buttonTitle: string | TranslateResult
}

export interface IPropsMpButton {
  mpType: MpTypeButton
  mpStatus: MpStatusButton
  size: SizeButton
  type: TypeButton
  plain: boolean
  round: boolean
  circle: boolean
  loading: boolean
  disabled: boolean
  autofocus: boolean
  icon: string
  nativeType: NativeTypeButton
  isIcon: boolean
  title: string
}

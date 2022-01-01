import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export class ActionButtons {
  readonly show: Array<MpTypeButton>
  readonly disabled: Array<MpTypeButton>

  constructor(
    show: Array<MpTypeButton> = [],
    disabled: Array<MpTypeButton> = [],
  ) {
    this.show = show
    this.disabled = disabled
  }
}

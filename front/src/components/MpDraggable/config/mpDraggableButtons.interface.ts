import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import {
  MovingButtonButtonsHorizontalArgs,
  MovingButtonButtonsVerticalArgs,
  MovingButtonContentHorizontal,
  MovingButtonContentVertical,
} from '@/components/MpDraggable/config/mpDraggable.enum'

export enum buttonPositions {
  middle = 'middle',
  right = 'right',
}

export interface IMovingButton {
  type: MpTypeButton.columnSetting
  content: MovingButtonContentVertical | MovingButtonContentHorizontal
  arg: MovingButtonButtonsVerticalArgs | MovingButtonButtonsHorizontalArgs
}

export interface IDataMpDraggableButtons {
  buttonPosition: {
    middle: buttonPositions
    right: buttonPositions
  }
}

export interface IMethodsMpDraggableButtons {
  buttonHandler(
    buttonArg:
      | MovingButtonButtonsHorizontalArgs
      | MovingButtonButtonsVerticalArgs,
  ): void
}

export interface IComputedMpDraggableButtons {
  buttons: Array<IMovingButton>
}

export interface IPropsMpDraggableButtons {
  settings: {
    position: buttonPositions
    isHorizontal: boolean
  }
}

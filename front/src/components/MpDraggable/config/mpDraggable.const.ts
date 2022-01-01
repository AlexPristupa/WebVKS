import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import {
  MovingButtonContentVertical,
  MovingButtonButtonsHorizontalArgs,
  MovingButtonButtonsVerticalArgs,
  MovingButtonContentHorizontal,
} from '@/components/MpDraggable/config/mpDraggable.enum'
import { IMovingButton } from '@/components/MpDraggable/config/mpDraggableButtons.interface'

export const movingButtonsVertical: Array<IMovingButton> = [
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentVertical.first,
    arg: MovingButtonButtonsVerticalArgs.first,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentVertical.up,
    arg: MovingButtonButtonsVerticalArgs.up,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentVertical.down,
    arg: MovingButtonButtonsVerticalArgs.down,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentVertical.last,
    arg: MovingButtonButtonsVerticalArgs.last,
  },
]

export const movingButtonContentHorizontal: Array<IMovingButton> = [
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentHorizontal.leftAll,
    arg: MovingButtonButtonsHorizontalArgs.leftAll,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentHorizontal.right,
    arg: MovingButtonButtonsHorizontalArgs.right,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentHorizontal.left,
    arg: MovingButtonButtonsHorizontalArgs.left,
  },
  {
    type: MpTypeButton.columnSetting,
    content: MovingButtonContentHorizontal.rightAll,
    arg: MovingButtonButtonsHorizontalArgs.rightAll,
  },
]

import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'

export enum ElementUIIcons {
  plus = 'el-icon-plus',
  delete = 'el-icon-delete',
  edit = 'el-icon-edit',
  download = 'el-icon-download',
  link = 'el-icon-share',
}

export interface IDataMpCellActionButton {
  default: ActionButtons
}

export interface IComputedMpCellActionButton {
  cellData: Array<MpTypeButton>
  disabledList: Array<MpTypeButton>
}

export interface IMethodsMpCellActionButton {
  setIcon(name: MpTypeButton): ElementUIIcons | ''
  setDisabled(buttonName: MpTypeButton): boolean
  messageToolTip(buttonName: MpTypeButton): string
  emitButtonClick(buttonName: MpTypeButton): void
}

export interface IPropsMpCellActionButton {}

export interface IMpCellActionButtonEmitted {
  buttonType: MpTypeButton
  rowData: any
}

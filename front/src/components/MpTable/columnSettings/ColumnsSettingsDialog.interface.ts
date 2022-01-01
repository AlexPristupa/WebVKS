import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import { IMovingButton } from '@/components/MpDraggable/config/mpDraggableButtons.interface'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export interface IColumnsSettingsData {
  buttons: Array<IMovingButton>
  activeColumn: IColumnDefs | null
  availableList: Array<IColumnDefs>
  currentList: Array<IColumnDefs>
  typeButton: { [key: string]: MpTypeButton }
}

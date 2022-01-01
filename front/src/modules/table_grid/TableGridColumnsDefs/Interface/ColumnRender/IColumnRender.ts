/**
 * @description Базовый интерфейс для cellRenderer
 */
export default interface IColumnRender {
  nameField: string
  sortable?: boolean
  resizable?: boolean
  showCheckboxPopover?: boolean
  cellClass?: Array<string>
  cellRenderer?: string
  filter?: boolean
  flex?: number
  minWidth?: number
  tooltipField?: string
  width?: number
  maxWidth?: number
  autoHeight?: boolean
  wrapText?: boolean
}

export interface IRowDataChecked {
  id: number | string
  checkbox?: boolean
  total?: number
  disabled?: boolean
  children?: Array<IRowDataChecked>
  propertyGroupId?: number
  checkboxChecked?: number
  checkboxAllChecked?: boolean
  checkboxAllDisabled?: boolean
}

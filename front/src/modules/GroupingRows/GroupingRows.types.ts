export interface IRow {
  id: number
  propertyGroupId: number
  propertyGroupName: string
  sortOrder: number
  isTitleGroup?: boolean
  editValue?: boolean
  children?: Array<IRow | IGroupingRow>
  collapsed?: boolean
  level?: number
}

export interface IGroupingRow {
  id: string | number
  name: string
  propertyGroupId?: number
  isTitleGroup: boolean
  collapsed?: boolean
  children?: Array<IGroupingRow | IRow>
  level?: number
}

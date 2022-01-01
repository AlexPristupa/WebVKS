export enum TreeStructureEventType {
  closeBranch = 'closeBranch',
  openBranch = 'openBranch',
}

export interface TreeStructureEvent {
  type: TreeStructureEventType
  data: IStructureItem
}

export interface IStructureItem {
  id: number
  collapsed: boolean
  level: number
  children: Array<IStructureItem>
  isFound: boolean
}

export type AddedFields = {} | null

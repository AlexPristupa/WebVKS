import { IStructureItem, AddedFields } from './TreeStructure.types'

export class TreeStructure {
  private _data: Array<IStructureItem> = []
  private readonly _addedFields: AddedFields = null
  private _openedNodes: Array<IStructureItem['id']> = []
  private _rowData: Array<IStructureItem> = []

  constructor(addedFields?: AddedFields) {
    if (addedFields) {
      this._addedFields = addedFields
    }
  }

  get opened() {
    return this._openedNodes
  }

  public openBranch(item: IStructureItem) {
    this._openedNodes.push(item.id)
    return this.getRowData()
  }

  public closeBranch(item: IStructureItem) {
    this._openedNodes = this._openedNodes.filter(value => value !== item.id)
    return this.getRowData()
  }

  public getRowData() {
    this._rowData = []
    this._setItemIsOpen(this._data)
    return this._rowData
  }

  public updateData(data: Array<IStructureItem>): void {
    this._data = this._addedFields ? this._addFields(data) : data
  }

  public setOpened(openedNodes: Array<IStructureItem['id']>) {
    this._openedNodes = openedNodes
  }

  private _setItemIsOpen(data: Array<IStructureItem>): void {
    data.forEach(item => {
      if (this._openedNodes.includes(item.id)) {
        item.collapsed = true
        this._rowData.push(item)
        this._setItemIsOpen(item.children)
      } else {
        item.collapsed = false
        this._rowData.push(item)
      }
    })
  }

  private _addFields(data: Array<IStructureItem>): Array<IStructureItem> {
    return data.map(item => {
      if (item.children.length) {
        item.children = this._addFields(item.children)
      }
      if (item.collapsed) {
        this._openedNodes.push(item.id)
        item.collapsed = true
      }
      return {
        ...item,
        ...this._addedFields,
      }
    })
  }
}

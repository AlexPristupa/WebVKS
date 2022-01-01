import { IRow, IGroupingRow } from './GroupingRows.types'
import { IStructureItem } from '@/modules/TreeStructure/TreeStructure.types'

export class GroupingRows {
  private _rows: Array<IRow> = []
  private groupingRows: Array<IGroupingRow | IRow> = []
  private groupNames: Array<string> = []

  constructor(rows: Array<IRow>) {
    this._rows = rows
    this.sortRows()
    return this
  }

  sortRows() {
    this._rows = this._rows.sort((a: IRow, b: IRow) => {
      if (a.propertyGroupId > b.propertyGroupId) return 1
      if (a.propertyGroupId < b.propertyGroupId) return -1
      if (a.propertyGroupId === b.propertyGroupId) {
        if (a.sortOrder > b.sortOrder) return 1
        if (a.sortOrder < b.sortOrder) return -1
      }
      return 0
    })
  }

  setRowTitleGroup() {
    this._rows.forEach((row: IRow) => {
      if (!this.groupNames.includes(row.propertyGroupName)) {
        this.groupingRows.push({
          name: row.propertyGroupName,
          id: row.propertyGroupId,
          isTitleGroup: true,
        })
        this.groupNames.push(row.propertyGroupName)
      }
      this.groupingRows.push(row)
    })
    return this
  }

  setStateEditValue(state: boolean) {
    this._rows = this._rows.map(row => {
      return {
        ...row,
        editValue: state,
      }
    })
    return this
  }

  getGroupingRows(): Array<IGroupingRow | IRow> {
    this.setRowTitleGroup()
    return this.groupingRows
  }

  convertToStructure(
    rows: Array<IGroupingRow | IRow>,
    collapsed: boolean,
  ): Array<IGroupingRow | IRow> {
    const finalArr: Array<IGroupingRow | IRow> = []
    rows.forEach(row => {
      row.children = []
      row.collapsed = collapsed
      row.level = 0
      if (row.propertyGroupId) {
        const parent = rows.find(parent => {
          return parent.isTitleGroup && parent.id === row.propertyGroupId
        })
        if (parent) {
          parent.children?.push({ ...row, level: 1, children: [] })
        }
      } else {
        finalArr.push(row)
      }
    })
    return finalArr
  }
}

import { IRowDataChecked } from '@/modules/table_grid/RowDataChecked/IRowDataChecked.interface'
import store from '@/store'

/**
 * @description Обрабатывает таблицы с чекбоксами в строках и в заголовке.
 *              Работает совместно с:
 *              - MpCellHeaderCheckbox
 *              - MpCellCheckbox
 */
export class RowDataChecked {
  private _checkedRowsList: Array<IRowDataChecked> = []
  private readonly _tableName: string
  private _pageLength: number = 0
  private _rowData: Array<IRowDataChecked> = []

  constructor(
    private readonly rowDataIn: Array<IRowDataChecked> = [],
    tableName: string,
    pageLength?: number,
    total?: number,
  ) {
    this._tableName = tableName
    this.init(rowDataIn, pageLength, total)
    return this
  }

  init(rowDataIn: Array<IRowDataChecked>, pageLength?: number, total?: number) {
    this.checkAllRow(false)
    this._pageLength = pageLength || rowDataIn.length
    this._rowData = rowDataIn.map(row => {
      row.checkboxAllChecked = false
      row.checkbox = false
      row.total = total || 0
      return row
    })
  }

  get rowData(): Array<IRowDataChecked> {
    return this._rowData
  }

  get checkedRowsList(): Array<IRowDataChecked> {
    return this._checkedRowsList
  }

  get checkedRowIdList(): Array<IRowDataChecked['id']> {
    return this._checkedRowsList.map(row => row.id)
  }

  setRowData(rows: Array<IRowDataChecked>) {
    this._rowData = rows
  }

  setAllCheckboxDisabled(value: boolean) {
    this._rowData = this._rowData.map(row => {
      return {
        ...row,
        checkboxAllDisabled: value,
      }
    })
    return this
  }

  setRowCheckboxDisabled(
    currentRow: IRowDataChecked,
    disabled: boolean,
    rows: Array<IRowDataChecked> = this._rowData,
  ) {
    const row = rows.find(row => row.id === currentRow.id)
    if (row) {
      row.disabled = disabled
      if (row.children?.length) {
        row.children.forEach(child => {
          this.setRowCheckboxDisabled(child, disabled, row.children)
        })
      }
    }
  }

  toggleCheck(currentRow: IRowDataChecked, isChecked: boolean) {
    const row = this._rowData.find(row => row.id === currentRow.id)
    if (row) {
      this._setCheckedRowList(currentRow, isChecked)
      if (row.children?.length) {
        this._toggleChildrenCheck(currentRow, isChecked)
      }
      if (row.propertyGroupId) {
        this._toggleParentCheck(currentRow, isChecked)
      }
    }
    return this._setCheckedSize()
  }

  private _toggleChildrenCheck(row: IRowDataChecked, check: boolean) {
    row.children?.forEach(child => {
      this._setCheckedRowList(child, check)
      if (child.children?.length) {
        this._toggleChildrenCheck(child, check)
      }
    })
  }

  private _toggleParentCheck(row: IRowDataChecked, check: boolean) {
    const parent = this._rowData.find(
      parent => parent.id === row.propertyGroupId,
    )
    if (parent) {
      const areAllChildrenUnchecked = parent.children?.every(
        child => !child.checkbox,
      )
      this._setCheckedRowList(parent, !areAllChildrenUnchecked)
      if (parent.propertyGroupId) {
        this._toggleParentCheck(parent, check)
      }
    }
  }

  private _setCheckedRowList(row: IRowDataChecked, check: boolean): void {
    row.checkbox = check
    if (
      check &&
      !this._checkedRowsList.find(checked => checked.id === row.id)
    ) {
      this._checkedRowsList.push(row)
    }
    if (!check) {
      this._checkedRowsList = this._checkedRowsList.filter(
        checked => checked.id !== row.id,
      )
    }
  }

  checkAllRow(checkedAll: boolean): Array<IRowDataChecked> {
    this._rowData.forEach(row => {
      row.checkboxAllChecked = checkedAll
      this.toggleCheck(row, checkedAll)
    })
    return this._setCheckedSize()
  }

  private _setCheckedSize() {
    const allChecked =
      this._checkedRowsList.length === this._pageLength &&
      !!this._rowData.length
    this.setStoreEffectCheckAll(allChecked)
    return this._rowData.map(row => {
      row.checkboxChecked = this._checkedRowsList.length
      row.checkboxAllChecked = allChecked
      return row
    })
  }

  setStoreEffectCheckAll(isChecked: boolean) {
    store.dispatch('tableHeaderEffects/changeColumnActiveEffects', {
      tableName: this._tableName,
      effects: { checkAll: isChecked },
    })
  }
}

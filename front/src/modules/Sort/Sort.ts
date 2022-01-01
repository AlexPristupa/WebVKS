import { sortType } from '@/modules/Sort/Sort.const'
import { FieldName, SortResult, TableRow } from '@/modules/Sort/Sort.interface'
import { DateTime } from '@/modules/DateTime/DateTime'

export class Sort {
  private readonly _rows: Array<TableRow> = []
  private readonly _keys: Array<FieldName> = []
  private readonly _sortOrder: sortType
  private _actualKey = 0
  private readonly _fieldTypeDateTime: Array<string> = ['dateTo', 'dateFrom']

  constructor(
    rows: Array<TableRow>,
    fieldName: Array<FieldName>,
    sortOrder: sortType,
    fieldTypeDateTime?: Array<string>,
  ) {
    this._rows = rows
    this._keys = fieldName
    this._sortOrder = sortOrder
    if (fieldTypeDateTime && fieldTypeDateTime.length) {
      this._fieldTypeDateTime = fieldTypeDateTime
    }
    this.sortRows()
    return this
  }

  get rows(): Array<TableRow> {
    return this._rows
  }

  private sortRows() {
    if (this._sortOrder === sortType.asc) {
      this._rows.sort(this.sortAsc(this))
    } else {
      this._rows.sort(this.sortDesc(this))
    }
  }

  private sortAsc(self: Sort) {
    return (a: TableRow, b: TableRow): SortResult => {
      if (this._fieldTypeDateTime.includes(self._keys[self._actualKey])) {
        const dateA = new DateTime({
          dateTime: a[self._keys[self._actualKey]],
        }).getJsDate()
        const dateB = new DateTime({
          dateTime: b[self._keys[self._actualKey]],
        }).getJsDate()
        if (dateA && dateB) {
          if (dateA > dateB) {
            return 1
          }
          if (dateA < dateB) {
            return -1
          }
        }
      } else {
        const valueA = a[self._keys[self._actualKey]]
        const valueB = b[self._keys[self._actualKey]]

        if (valueA > valueB) {
          return 1
        }
        if (valueA < valueB) {
          return -1
        }
      }
      if (self._actualKey < self._keys.length - 1) {
        self._actualKey = self._actualKey + 1
        self.sortAsc(self)
      }
      return 0
    }
  }

  private sortDesc(self: Sort) {
    return (a: TableRow, b: TableRow): SortResult => {
      if (this._fieldTypeDateTime.includes(self._keys[self._actualKey])) {
        const dateA = new DateTime({
          date: a[self._keys[self._actualKey]],
        }).getJsDate()
        const dateB = new DateTime({
          date: b[self._keys[self._actualKey]],
        }).getJsDate()
        if (dateA && dateB) {
          if (dateA < dateB) {
            return 1
          }
          if (dateA > dateB) {
            return -1
          }
        }
      } else {
        const valueA = a[self._keys[self._actualKey]]
        const valueB = b[self._keys[self._actualKey]]
        if (valueA < valueB) {
          return 1
        }
        if (valueA > valueB) {
          return -1
        }
      }
      if (self._actualKey < self._keys.length - 1) {
        self._actualKey = self._actualKey + 1
        self.sortDesc(self)
      }
      return 0
    }
  }
}

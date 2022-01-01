/**
 * @description Обрабатывает таблицы с контекстным меню ссылок.
 *              Работает совместно с:
 *              - MpCellContextMenu;
 */
import { IRowWithLinks } from '@/modules/ContextMenu/ContextMenu.interface'
import { contextMenuConfig } from './ContextMenu.config'
import i18n from '@/i18n'

export class ContextMenu {
  private readonly _tableName: string
  private readonly _rows: Array<IRowWithLinks>

  constructor(tableName, rows) {
    this._tableName = tableName
    this._rows = this.setRowsLinks(rows)
    return this
  }

  get rows() {
    return this._rows
  }

  setRowsLinks(rows: Array<IRowWithLinks>): Array<IRowWithLinks> {
    const linkConfig = contextMenuConfig[this._tableName]
    return rows.map((row: IRowWithLinks) => {
      row.links = []
      if (linkConfig && row[linkConfig.value]) {
        row.links.push({
          value: row[linkConfig.value],
          label: i18n.t(linkConfig.label),
        })
      }
      return row
    })
  }
}

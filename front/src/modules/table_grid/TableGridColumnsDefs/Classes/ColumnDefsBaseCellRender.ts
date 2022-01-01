import ColumnDefsBase from './ColumnDefsBase'
import IColumnDefsBaseToFront from '../Interface/IColumnDefsBaseToFront'
import IColumnRender from '../Interface/ColumnRender/IColumnRender'
import IColumnDefsBaseCellRender from '../Interface/ColumnRender/IColumnDefsBaseColumnRender'

/**
 * @description Применить к колонкам columnRenderer
 */
class ColumnDefsBaseCellRender extends ColumnDefsBase
  implements IColumnDefsBaseCellRender {
  private listColumns: Array<IColumnDefsBaseToFront> = []
  public Ready!: Promise<any>

  constructor(listColumnsIN: Array<IColumnDefsBaseToFront>, tableName: string) {
    super()

    if (!listColumnsIN || (listColumnsIN && !listColumnsIN.length)) {
      this.Ready = new Promise(resolve => {
        this.getListColumnsToFront(tableName)
          .then(result => {
            this.listColumns = result || []
            resolve(this.listColumns)
          })
          .catch(() => {
            this.listColumns = []
          })
      })
    } else {
      this.listColumns = listColumnsIN
    }
  }

  /**
   * @description Проверить и применить к каждой колонке cellRenderer
   * @param columnRender {Array<IColumnRender>} дополнение полей из кастомной
   *   конфигурации
   */
  applyColumnRender(columnRender: Array<IColumnRender>) {
    // ищем пересечение массивов по имени поля
    const intersection = this.listColumns?.filter(itemListColumns =>
      columnRender.some(
        itemColumnRender =>
          itemColumnRender.nameField === itemListColumns.field,
      ),
    )

    // применить конфу к элементам пересечения
    intersection?.forEach((v: IColumnDefsBaseToFront) => {
      const itemColumnRender = columnRender.find(x => x.nameField === v.field)
      // примешивем недостающие настройки
      for (const key in itemColumnRender) {
        v[key] = itemColumnRender[key]
      }
    })

    /**  Дабавляет к последней видимой колонке свойсво flex, тем самым скрываем
     *  белое поле слева в таблице, которое появляется если суммарная ширина
     *  колонок таблицы меньше ширины лайаута таблицы */
    const visibleColumns = this.listColumns.filter(column => !column.hide)
    if (
      visibleColumns.length &&
      visibleColumns[visibleColumns.length - 1].field === 'actions'
    ) {
      visibleColumns[visibleColumns.length - 1].flex = 1
    }

    return this.listColumns || []
  }
}

export default ColumnDefsBaseCellRender

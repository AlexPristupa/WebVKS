/**
 * @description Базовый класс реализующий базовый интерфейс IColumnDefsBase
 */
import IColumnDefsBase from '@/modules/table_grid/TableGridColumnsDefs/Interface/IColumnDefsBase'
import IColumnDefsBaseToFront from '@/modules/table_grid/TableGridColumnsDefs/Interface/IColumnDefsBaseToFront'
import api from '@/api_services'
import i18n from '@/i18n'
import listExcludedFields from '@/modules/table_grid/Const/listExcludedFields.const'
import { methods } from '@/api_services/httpMethods.enum'
import { ColumnTableServer } from '@/modules/ApiDataValidation/ResponseDto/ColumnTableServer/ColumnTableServer'

class ColumnDefsBase implements IColumnDefsBase {
  /**
   * @description Список базовых колонок используемых фронтом
   */
  private listColumnsToFront: Array<IColumnDefsBaseToFront> | undefined

  /**
   * @description Получить настройки колонок с бэка
   * @param tableName {string} - имя таблицы, для которой определяем колонки
   */
  private async getNameFieldAgGridVue(tableName: string) {
    const res = await api.fetchNameFieldAgGridVue({
      method: methods.get,
      data: {
        TableName: tableName,
      },
    })
    if (res) {
      this.listColumnsToFront = res.map((v: ColumnTableServer) => {
        const columnToFront: IColumnDefsBaseToFront = {
          headerName: i18n.t(`tables.${tableName}.${v.nameField}`).toString(),
          field: v.nameField,
          sortable: v.sortable,
          hide: !v.chkbD,
          width: v.width || 100,
          cellRendererParams: {
            tableName: tableName,
          },
          userOrderColumn: v.userOrderColumn,
          filter: v.filter,
          filterParams: v.filterType,
          minWidth: v.minWidth,
          isVisibleHint: v.cellsWithoutHint,
          tooltipField: v.cellsWithoutHint ? v.nameField : '',
          tooltipComponent: 'CustomTooltip',
          resizable: this.getResizable(v.nameField),
        }
        return columnToFront
      })
    }
  }

  /**
   * @description Получить список колонок для фронта
   */
  public getResizable(nameField: string): boolean {
    const listValues: Array<string> = Object.values(listExcludedFields)
    return !listValues.includes(nameField)
  }

  /**
   * @description Отсортировать в соответствие с установками сервера
   */
  public sortOrderColumns(): Array<IColumnDefsBaseToFront> | undefined {
    return this.listColumnsToFront?.sort(
      (prev, next) => prev.userOrderColumn - next.userOrderColumn,
    )
  }

  /**
   * @description Получить список колонок для фронта
   */
  public async getListColumnsToFront(
    tableName: string,
  ): Promise<IColumnDefsBaseToFront[] | undefined> {
    await this.getNameFieldAgGridVue(tableName)
    this.listColumnsToFront = this.sortOrderColumns()
    return this.listColumnsToFront
  }
}

export default ColumnDefsBase

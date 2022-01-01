/**
 * @description функция замены фильтров таблицы, для отправки запросов и установки в стор.
 */
import store from '@/store'
import { paginationParams } from '@/components/MpPagination/type'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'
import { TableName } from '@/modules/table_grid/TableName.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'

export function setFilter(
  dto: TableDto,
  tableName: TableName,
  filterState: IFilterState,
): TableDto {
  const filters = dto.filters.filter(
    currentFilter => currentFilter.nameField !== filterState.nameField,
  )
  if (filterState.valuesField.length) {
    filters.push(filterState)
  }
  store.dispatch('tableHeaderEffects/changeColumnActiveEffects', {
    tableName: tableName,
    effects: { filters: filters },
  })
  return {
    ...dto,
    filters: filters,
    page: paginationParams.defaultPage,
  }
}

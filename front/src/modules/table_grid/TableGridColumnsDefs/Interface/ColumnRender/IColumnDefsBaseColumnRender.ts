/**
 * @description DIP CellRender в компонент
 */
import IColumnRender from './IColumnRender'
import IColumnDefsBaseToFront from '@/modules/table_grid/TableGridColumnsDefs/Interface/IColumnDefsBaseToFront'

export default interface IColumnDefsBaseCellRender {
  applyColumnRender(
    columnRender: Array<IColumnRender>,
  ): void | Array<IColumnDefsBaseToFront>
}

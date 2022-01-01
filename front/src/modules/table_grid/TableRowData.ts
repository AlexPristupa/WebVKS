import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { Expose } from 'class-transformer'
import { IsInt } from 'class-validator'

export class TableRowData implements ITableRowData {
  @Expose()
  @IsInt()
  public id: number = 0
}

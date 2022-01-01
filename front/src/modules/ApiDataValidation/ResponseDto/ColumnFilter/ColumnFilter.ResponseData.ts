import { Type, Expose } from 'class-transformer'
import { IsEnum, IsInt, IsDefined } from 'class-validator'
import { TableName } from '@/modules/table_grid/TableName.const'
import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'
import { TableRowData } from '@/modules/table_grid/TableRowData'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class ColumnFilterResponseData extends ApiResponseData
  implements IEntity {
  toValidationName(): string {
    return 'ColumnFilterResponseData'
  }

  @IsDefined()
  @Type(() => TableRowData)
  items: TableRowData[] = []

  @Expose()
  @IsDefined()
  @IsInt()
  public pageIndex: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public pageSize: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public total: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public totalPages: number = 0

  @Expose()
  @IsDefined()
  @IsEnum(TableName)
  public tableName: string = TableName.unknown
}

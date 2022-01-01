import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'
import { Expose } from 'class-transformer'
import {
  IsBoolean,
  IsDefined,
  IsEnum,
  IsInt,
  IsPositive,
  IsString,
  Max,
  Min,
  MinLength,
} from 'class-validator'
import { FilterType } from '@/modules/Filters/Filters.const'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class ColumnTableServer extends ApiResponseData implements IEntity {
  toValidationName(): string {
    return 'ColumnTableServer'
  }

  @Expose()
  @IsDefined()
  @IsBoolean()
  public actionsColumnsNames: boolean = false

  @Expose()
  @IsDefined()
  @IsString()
  public cellRenderer: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public cellsWithoutHint: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public chkbD: boolean = true

  @Expose()
  @IsDefined()
  @IsBoolean()
  public filter: boolean = true

  @Expose()
  @IsDefined()
  @IsEnum(FilterType)
  filterType: FilterType = FilterType.select

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isVisibleHint: boolean = true

  @Expose()
  @IsDefined()
  @IsString()
  @MinLength(3)
  public lngName: string = ''

  @Expose()
  @IsDefined()
  @IsInt()
  @IsPositive()
  @Min(40)
  @Max(2000)
  public minWidth: number = 40

  @Expose()
  @IsDefined()
  @IsString()
  @MinLength(3)
  public nameField: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public resizable: boolean = true

  @Expose()
  @IsDefined()
  @IsBoolean()
  public sortable: boolean = true

  @Expose()
  @IsDefined()
  @IsInt()
  public userOrderColumn: number = 0

  @Expose()
  @IsDefined()
  @IsBoolean()
  public visibleCheckBox: boolean = false

  @Expose()
  @IsDefined()
  @IsInt()
  @IsPositive()
  @Min(40)
  @Max(2000)
  public width: number = 100
}

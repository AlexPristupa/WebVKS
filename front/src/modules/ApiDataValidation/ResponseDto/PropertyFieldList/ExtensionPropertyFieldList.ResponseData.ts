import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'
import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { Expose } from 'class-transformer'
import {
  IsBoolean,
  IsDefined,
  IsEnum,
  IsInt,
  IsOptional,
  IsString,
} from 'class-validator'
import { propertyPrivateName } from '@/domain'
import { invProfileId } from '@/domain'
import { invProfileName } from '@/domain'
import { FilterType } from '@/modules/Filters/Filters.const'
import { propertyGroupId } from '@/domain'
import { propertyGroupName } from '@/domain'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class ExtensionPropertyFieldListResponseData extends ApiResponseData
  implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'ExtensionPropertyFieldListResponseData'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public columnName: string = ''

  @Expose()
  @IsOptional()
  @IsEnum(propertyPrivateName)
  public filterTitle: propertyPrivateName | null = null

  @Expose()
  @IsDefined()
  @IsEnum(FilterType)
  filterType: FilterType = FilterType.select

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isCommon: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isMainTable: boolean = false

  @Expose()
  @IsOptional()
  @IsEnum(propertyPrivateName)
  public privateName: propertyPrivateName | null = null

  @Expose()
  @IsOptional()
  @IsEnum(invProfileId)
  public profileId: invProfileId = invProfileId.undefined

  @Expose()
  @IsOptional()
  @IsEnum(invProfileName)
  public profileName: invProfileName | null = null

  @Expose()
  @IsOptional()
  @IsEnum(propertyGroupId)
  public propertyGroupId: propertyGroupId | null = null

  @Expose()
  @IsOptional()
  @IsEnum(propertyGroupName)
  public propertyGroupName: propertyGroupName | null = null

  @Expose()
  @IsOptional()
  @IsString()
  public propertyName: string | null = null

  @Expose()
  @IsDefined()
  @IsString()
  public tableName: string = ''
}

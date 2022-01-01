import { Expose } from 'class-transformer'
import {
  IsDefined,
  IsEnum,
  IsInt,
  IsOptional,
  IsString,
  Min,
} from 'class-validator'
import { propertyPrivateName } from '@/domain'
import { catalogIdType } from '@/domain'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class PropertyCatalogEntity implements IEntity {
  toValidationName(): string {
    return 'PropertyCatalogEntity'
  }

  @Expose()
  @IsDefined()
  @IsInt()
  @Min(0)
  public catalogId: number = 0

  @Expose()
  @IsOptional()
  @IsString()
  public catalogValue: string | null = null

  @Expose()
  @IsOptional()
  @IsEnum(propertyPrivateName)
  public privateName: propertyPrivateName = propertyPrivateName.unknown

  @Expose()
  @IsDefined()
  @IsEnum(catalogIdType)
  public typeId: catalogIdType = catalogIdType.string
}

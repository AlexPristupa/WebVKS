import { Expose, Type } from 'class-transformer'
import { IsDefined, IsEnum } from 'class-validator'
import { invProfileId } from '@/domain'
import { PropertyCatalogEntity } from '@/modules/ApiDataValidation/ResponseDto/PropertyCatalog/PropertyCatalog.Entity'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class ExtensionPropertyCatalogResponseData implements IEntity {
  toValidationName(): string {
    return 'ExtensionPropertyCatalogResponseData'
  }

  @Expose()
  @IsDefined()
  @IsEnum(invProfileId)
  public profileId: invProfileId = invProfileId.common

  @IsDefined()
  @Type(() => PropertyCatalogEntity)
  public values: PropertyCatalogEntity[] = []
}

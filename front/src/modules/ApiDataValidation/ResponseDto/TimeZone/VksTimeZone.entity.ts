import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsInt, IsString } from 'class-validator'

export class VksTimeZoneEntity implements IEntity {
  toValidationName(): string {
    return 'VksTimeZoneEntity'
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public privateName: string = ''

  @Expose()
  @IsDefined()
  @IsInt()
  public offsetMinute: number = 0
}

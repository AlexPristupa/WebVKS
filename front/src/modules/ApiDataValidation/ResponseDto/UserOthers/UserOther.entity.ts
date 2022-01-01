import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsOptional, IsString } from 'class-validator'
import { Expose, Transform } from 'class-transformer'

export class VksUserOtherEntity implements IEntity {
  toValidationName(): string {
    return 'VksUserOtherEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.email = data.email || ''
      this.uri = data.uri || ''
      this.privateName = data.privateName || ''
    }
  }

  @Expose()
  @Transform(({ value }) => {
    return value + 'other'
  })
  public id: number | string = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public email: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public uri: string = ''

  @Expose()
  @IsOptional()
  public privateName: string | null = null

  public checked: boolean = false
  public isFromOtherList: boolean = true
}

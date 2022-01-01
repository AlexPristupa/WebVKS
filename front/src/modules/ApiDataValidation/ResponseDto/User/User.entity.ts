import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsNumber, IsOptional, IsString } from 'class-validator'
import { Expose } from 'class-transformer'

export class VksUserEntity implements IEntity {
  toValidationName(): string {
    return 'VksUserEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.email = data.email || ''
      this.uri = data.uri || ''
      this.vksUserName = data.vksUserName || ''
      this.name = data.vksUserName || ''
    }
  }

  @Expose()
  @IsOptional()
  @IsNumber()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public email: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public uri: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public vksUserName: string = ''

  public name: string = ''
}

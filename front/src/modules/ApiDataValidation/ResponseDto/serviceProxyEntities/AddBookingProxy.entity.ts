import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsString } from 'class-validator'

export class AddBookingProxyEntity implements IEntity {
  toValidationName(): string {
    return 'AddBookingProxyEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public guid: string = ''

  // TODO Попросить исправить регистр полей callid, passwordguest

  @Expose()
  @IsDefined()
  @IsString()
  public callid: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public passwordguest: string = ''
}

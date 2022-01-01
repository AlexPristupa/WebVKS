import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { TranslateResult } from 'vue-i18n'

export class VksSelectOptionWithPrivateEntity implements IEntity {
  toValidationName(): string {
    return 'VksSelectOptionWithPrivateEntity'
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string | TranslateResult = ''

  @Expose()
  @IsDefined()
  @IsString()
  public privateName: string = ''

  public checked: boolean = false
}

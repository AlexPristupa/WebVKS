import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { TranslateResult } from 'vue-i18n'

export class VksSelectOptionEntity implements IEntity {
  toValidationName(): string {
    return 'VksSelectOptionEntity'
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string | TranslateResult = ''

  isFromOtherList: boolean = false
  checked: boolean = false
}

import { Expose } from 'class-transformer'
import {
  IsDefined,
  IsInt,
  IsPositive,
  IsString,
  MaxLength,
} from 'class-validator'

export class BalancingAlgorithm {
  @Expose()
  @IsDefined()
  @IsInt()
  @IsPositive()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  @MaxLength(128)
  public privateName: string = ''

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.privateName = data.privateName || ''
    }
    return this
  }
}

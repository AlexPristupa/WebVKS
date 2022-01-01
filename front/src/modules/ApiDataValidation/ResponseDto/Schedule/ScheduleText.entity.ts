import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsString } from 'class-validator'
import { Expose } from 'class-transformer'

export class ScheduleTextEntity implements IEntity {
  toValidationName(): string {
    return 'ScheduleTextEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public scheduleText: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public dateStart: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public nextRun: string = ''
}

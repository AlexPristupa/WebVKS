import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { IMpCellDateTimeSettings } from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export class VksSpaceEditTableBookingEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksSpaceEditTableBookingEntity'
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
  public description: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public currentStatus: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public nextRun: string = ''

  @IsDefined()
  public fieldsSetting: IMpCellDateTimeSettings = {
    nextRun: {
      showType: datePickerType.dateTime,
    },
  }
}

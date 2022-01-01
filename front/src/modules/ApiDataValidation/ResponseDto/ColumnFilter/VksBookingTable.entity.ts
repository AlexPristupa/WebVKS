import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsOptional, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { IMpCellDateTimeSettings } from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export class VksBookingTableEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksBookingTableEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.description = data.description || ''
      this.spaceName = data.spaceName || ''
      this.schedule = data.schedule || ''
      this.owner = data.owner || ''
      this.type = data.type || ''
      this.spaceUri = data.spaceUri || ''
      this.currentStatus = data.currentStatus || ''
      this.nextRun = data.nextRun || ''
    }
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
  public spaceName: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public schedule: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public type: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public owner: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public spaceUri: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public currentStatus: string = ''

  @Expose()
  public nextRun: string | null = ''

  @Expose()
  public dateEnd: string | null = ''

  public fieldsSetting: IMpCellDateTimeSettings = {
    counter: {
      display: false,
      fieldTo: 'nextRun',
    },
    nextRun: {
      showType: datePickerType.dateTime,
    },
  }
}

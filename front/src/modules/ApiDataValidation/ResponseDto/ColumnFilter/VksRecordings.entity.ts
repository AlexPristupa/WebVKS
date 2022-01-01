import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsBoolean, IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { IMpCellDateTimeSettings } from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export class VksRecordingsEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksRecordingsEntity'
  }

  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
    }
    return this
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public dateEnd: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public dateStart: string = ''

  @Expose()
  @IsDefined()
  @IsInt()
  public duration: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public owner: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public serversGroupsName: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public spaceUri: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isDelete: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isDownload: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isPlay: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isShare: boolean = false

  @IsDefined()
  public fieldsSetting: IMpCellDateTimeSettings = {
    dateStart: {
      showType: datePickerType.dateTime,
    },
    dateEnd: {
      showType: datePickerType.dateTime,
    },
    duration: {
      showType: datePickerType.time,
    },
  }
}

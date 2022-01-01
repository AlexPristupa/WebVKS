import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsBoolean, IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { IMpCellDateTimeSettings } from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export class VksRecordingsShareEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksRecordingsShareEntity'
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public recordingVksUsersId: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public userId: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public user: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isPlay: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isDownload: boolean = false

  @Expose()
  @IsDefined()
  @IsString()
  public dateRecord: string = ''

  @IsDefined()
  public action: ActionButtons = new ActionButtons([MpTypeButton.remove])

  @IsDefined()
  public fieldsSetting: IMpCellDateTimeSettings = {
    dateRecord: {
      showType: datePickerType.date,
    },
  }
}

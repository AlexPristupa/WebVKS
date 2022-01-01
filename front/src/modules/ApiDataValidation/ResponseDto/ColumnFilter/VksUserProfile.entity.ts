import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export class VksUserProfileEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksUserProfileEntity'
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
  public serversGroupsName: string = ''

  public action: ActionButtons = new ActionButtons(
    [MpTypeButton.edit, MpTypeButton.remove],
    [MpTypeButton.edit, MpTypeButton.remove],
  )
}

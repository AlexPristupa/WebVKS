import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export class VksSpaceTableEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksSpaceTableEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.serversGroupsName = data.serversGroupsName || ''
      this.uri = data.uri || ''
      this.serversGroupsName = data.serversGroupsName || ''
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
  public serversGroupsName: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public uri: string = ''

  public action: ActionButtons = new ActionButtons([
    MpTypeButton.edit,
    MpTypeButton.remove,
  ])
}

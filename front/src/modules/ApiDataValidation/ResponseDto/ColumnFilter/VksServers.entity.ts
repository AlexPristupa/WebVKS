import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export class VksServerEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksServerEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.basicPath = data.basicPath || ''
      this.serversGroupsId = data.serversGroupsId || -1
      this.remoteIpAddress = data.remoteIpAddress || ''
      this.login = data.login || ''
      this.password = data.password || ''
    }
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsInt()
  public serversGroupsId: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public basicPath: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public remoteIpAddress: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public login: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public password: string = ''

  public action: ActionButtons = new ActionButtons([
    MpTypeButton.edit,
    MpTypeButton.remove,
  ])
}

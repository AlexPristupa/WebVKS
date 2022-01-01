import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export class VksServersGroupEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksServersGroupEntity'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.description = data.description || ''
      this.isUseBalancer = data.isUseBalancer || false
      this.balancerAlgId = data.balancerAlgId || 1
      this.servers = data.servers || []
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
  public isUseBalancer: boolean = false

  @Expose()
  public balancerAlgId: number = 1

  @Expose()
  public servers: Array<any> = []

  public action: ActionButtons = new ActionButtons([
    MpTypeButton.edit,
    MpTypeButton.remove,
  ])
}

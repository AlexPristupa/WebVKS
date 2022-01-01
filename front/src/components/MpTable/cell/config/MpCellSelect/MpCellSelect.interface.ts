import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { SpacesCaLegProfilesProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/SpacesCaLegProfilesProxy.entity'
import { TableName } from '@/modules/table_grid/TableName.const'
import { methods } from '@/api_services/httpMethods.enum'

export interface IMethodsMpCellSelect {
  getOptions(search?: string): void
  change(change: string | number): void
  focus(): void
}

export interface IMpCellSelectSettings {
  display: {
    id: string
    name: string
  }
  search: boolean
  clearable?: boolean
  request(IRequestConfig): any
  method: methods
  additionalParams: Array<string>
  defaultParams: { [key: string]: string | number }
}

export interface IComputedMpCellSelect {
  settings: IMpCellSelectSettings
  tableName: TableName
  field: string
  data: any
}

export interface IDataMpCellSelect {
  options: Array<VksSelectOptionEntity | SpacesCaLegProfilesProxyEntity>
  value: number | string | null
}
export interface IPropsMpCellSelect {}

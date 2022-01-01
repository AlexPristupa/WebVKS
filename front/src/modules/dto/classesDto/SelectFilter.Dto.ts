import { TableName } from '@/modules/table_grid/TableName.const'

export class SelectFilterDto {
  public filterId: number | null = null
  public columnName: string = ''
  public tableName: TableName = TableName.unknown
  public search: string = ''
  public checkList: Array<string> = []
  public addParameters: Array<any> = []
}

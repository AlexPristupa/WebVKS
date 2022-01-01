export interface IComputedMpCellStatus {
  cellData: IStatusCellData
  type: IStatusCellData['type']
  disabled: IStatusCellData['disabled']
}

interface IStatusCellData {
  type: string
  disabled: boolean
}

export interface IMethodsMpCellStatus {}
export interface IDataMpCellStatus {}
export interface IPropsMpCellStatus {}

export enum MpMatching {
  match = 'Соответствует',
  noMatch = 'Не соответствует',
}
export interface IComputedMpCellMatching {
  numberMap: MpMatching
}
export interface IDataMpCellMatching {
  mpMatching: {
    match: MpMatching.match
    noMatch: MpMatching.noMatch
  }
}
export interface IMethodsMpCellMatching {}
export interface IPropsMpCellMatching {}

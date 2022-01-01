export enum FilterType {
  string = 'string',
  select = 'select',
  integer = 'integer',
  date = 'date',
  // tree = 'tree',
  // time = 'time',
  // selectFts = 'selectFts',
  // stringFts = 'stringFts',
  // treeFts = 'structureFts',
  // boolean = 'boolean',
}

// export type FilterId = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10
export type FilterId = 1 | 2 | 3 | 4

export const FilterTypeById = {
  1: FilterType.select,
  2: FilterType.date,
  3: FilterType.integer,
  4: FilterType.string,
  // 5: FilterType.time,
  // 6: FilterType.tree,
  // 7: FilterType.selectFts,
  // 8: FilterType.stringFts,
  // 9: FilterType.treeFts,
  // 10: FilterType.boolean,
}

export const FiltersEndpoints = {
  [FilterType.string]: 'getActiveStringFilter',
  [FilterType.date]: 'getActiveDateFilter',
  [FilterType.select]: 'getActiveSelectFilter',
  [FilterType.integer]: 'getActiveIntegerFilter',
}

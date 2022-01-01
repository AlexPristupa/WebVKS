import { RowNode } from 'ag-grid-community'
import { propertyPrivateName } from '@/domain'

/**
 * @description Интерфейс для классов реализующих особые бизнес правила при
 *              редактировании полей в таблице с данными при применении компонента
 *              MpCellEditValue
 */
export interface ISettingPropertyEditValue {
  _nodeList: Array<RowNode>
  getDisabledStatus(privateName: propertyPrivateName): boolean
  getCatalogList(
    privateName: propertyPrivateName,
    catalogList: Array<PropertyEditValueCatalogList>,
  ): Array<PropertyEditValueCatalogList>
}

export interface PropertyEditValueCatalogList {
  catalogId: number
  catalogValue: string
}

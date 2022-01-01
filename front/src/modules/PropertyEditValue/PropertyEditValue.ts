import { propertyPrivateName } from '@/domain'

/**
 * @description Сущность значения свойства возвращаемая в результате ее редактирования
 */
export class PropertyEditValue {
  public privateName: propertyPrivateName = propertyPrivateName.unknown
  public valueId: number = 0
  public valueName: string = ''

  constructor(
    privateName: propertyPrivateName,
    valueName: string,
    valueId: number = 0,
  ) {
    this.privateName = privateName
    this.valueName = valueName
    this.valueId = valueId
    return this
  }
}

import { propertyPrivateName } from '@/domain'
import { SubscriberPropertyPrivateName } from '@/domain'

export class PropertyEditValue {
  privateName: propertyPrivateName | SubscriberPropertyPrivateName =
    propertyPrivateName.unknown
  valueName: string = ''
  valueId?: number

  constructor(privateName, valueName, valueId?) {
    this.privateName = privateName
    this.valueName = valueName
    if (valueId) {
      this.valueId = valueId
    }
  }
}

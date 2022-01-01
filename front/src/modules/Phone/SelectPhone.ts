import { invProfileId } from '@/domain'
import { invTypeId } from '@/domain'

export class SelectPhone {
  checkbox: boolean = true
  ext: string = ''
  id: number = 0
  invProfileId: invProfileId = invProfileId.undefined
  invTypeId: invTypeId = invTypeId.number
  typeName: string = ''

  constructor(selectPhone: SelectPhone) {
    this.checkbox = selectPhone.checkbox
    this.ext = selectPhone.ext
    this.id = selectPhone.id
    this.invProfileId = selectPhone.invProfileId
    this.invTypeId = selectPhone.invTypeId
    this.typeName = selectPhone.typeName
  }
}

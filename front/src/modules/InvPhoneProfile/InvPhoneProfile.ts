import { InvPhoneProfileItem } from '@/modules/InvPhoneProfile/InvPhoneProfile.interface'

export class InvPhoneProfile {
  static inner: InvPhoneProfileItem = {
    name: 'inner',
    id: 2,
  }
  static outer: InvPhoneProfileItem = {
    name: 'outer',
    id: 3,
  }
  static common: InvPhoneProfileItem = {
    name: 'common',
    id: 4,
  }
}

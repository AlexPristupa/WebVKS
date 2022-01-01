/**
 * @description Интерфейсы
 */
import { ITableHeaderEffectsState } from '@/store/modules/table_header_effects_store/types'
import { IUser } from '@/store/modules/user/types'
import { IUserSettings } from '@/store/modules/user_settings/types'

export interface IRootState {
  version: string
  columnFilter?: ITableHeaderEffectsState
  user?: IUser
  userSettings?: IUserSettings
}

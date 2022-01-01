import { TranslateResult } from 'vue-i18n'

export interface ISettingMenuOptions {
  title: TranslateResult
  type: string
}

export interface IDataMpWrapperColumnsSettings {
  setUpVisible: boolean
  settingMenuOptions: Array<ISettingMenuOptions>
}

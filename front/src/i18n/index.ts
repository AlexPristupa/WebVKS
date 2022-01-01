import Vue from 'vue'
import VueI18n from 'vue-i18n'
import elementEnLocale from 'element-ui/lib/locale/lang/en' // element-ui lang
import elementRuLocale from 'element-ui/lib/locale/lang/ru-RU' // element-ui lang

import enLocale from './en'
import ruLocale from './ru'

import store from 'store'

Vue.use(VueI18n)

const messages = {
  en: {
    ...enLocale,
    ...elementEnLocale,
  },
  ru: {
    ...ruLocale,
    ...elementRuLocale,
  },
}

export function getLanguage() {
  const chooseLanguage = store.get('language')
  if (chooseLanguage) return chooseLanguage
  const navigatorNew = window.navigator as any

  // if has not choose language
  const language = (
    navigatorNew.language || navigatorNew.browserLanguage
  ).toLowerCase()
  const locales = Object.keys(messages)
  for (const locale of locales) {
    if (language.indexOf(locale) > -1) {
      return locale
    }
  }
  return 'ru'
}

const i18n = new VueI18n({
  // set locale
  locale: getLanguage(),
  silentTranslationWarn: true,
  messages,
})

export default i18n

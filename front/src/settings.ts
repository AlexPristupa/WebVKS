/**
 * @description Модуль - глобальных переменных, можно использовать всеми компонентами
 */

const browserLanguage = (): string => {
  let lang = window.navigator ? window.navigator.language : 'ru'
  lang.substr(0, 2).toLowerCase()
  if (lang !== 'ru' && lang !== 'en') {
    lang = 'en'
  }
  return lang
}

export default {
  title: 'MENTOL PRO',

  /**
   * @type {string | array} 'production' | ['production', 'development']
   * @description Показать ошибочные логи компонентов.
   * По умолчанию используется только в рабочей среде
   * Для использования в dev, нужно передать ['production', 'development']
   */
  errorLog: 'production',
  /**
   * @description Настройки кнопки BackToTop
   */
  myBackToTopStyle: {
    right: '50px',
    bottom: '50px',
    width: '40px',
    height: '40px',
    'border-radius': '4px',
    'line-height': '45px',
    background: '#e7eaf1',
  },
  /**
   * @description Язык браузера
   */
  browserLanguage,

  /**
   * @description Инициализация темы
   */
  theme: process.env.APP_THEME,
}

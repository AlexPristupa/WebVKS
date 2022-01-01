/**
 * @description Маски для даты и времени.
 */
export const dateMasks: any = {
  withoutTime: {
    ru: 'DD.MM.YYYY',
    en: 'YYYY-MM-DD',
  },
  withTime: {
    withoutSeconds: {
      ru: 'DD.MM.YYYY HH:mm',
      en: 'YYYY-MM-DDTHH:mm',
    },
    withSeconds: {
      ru: 'DD.MM.YYYY HH:mm:ss',
      en: 'YYYY-MM-DDTHH:mm:ss',
    },
  },
  forParseTimeFunc: {
    withoutTime: {
      ru: '{d}.{m}.{y}',
      en: '{y}-{m}-{d}',
    },
    withTime: {
      withoutSeconds: {
        ru: '{d}.{m}.{y} {h}:{i}',
        en: '{y}-{m}-{d} {h}:{i}',
      },
      withSeconds: {
        ru: '{d}.{m}.{y} {h}:{i}:{s}',
        en: '{y}-{m}-{d} {h}:{i}:{s}',
      },
    },
  },
}

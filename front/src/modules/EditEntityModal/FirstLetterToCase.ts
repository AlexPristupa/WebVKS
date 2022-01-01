import i18n from '@/i18n'

export class FirstLetterToCase {
  constructor() {}

  static placeholderToLower(key, entity): string {
    const string = i18n.t(`forms.${entity}.${key}`).toString()
    return FirstLetterToCase.firstToLower(string)
  }

  static firstToLower(string: string): string {
    return string[0].toLowerCase() + string.slice(1)
  }

  static firstToUpper(string: string): string {
    return string[0].toUpperCase() + string.slice(1)
  }
}

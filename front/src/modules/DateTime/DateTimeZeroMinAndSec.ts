/**
 * @description Дочерний класс модуля DateTime для работы с сущностями минут и секунд
 *              времени.
 */
export class DateTimeZeroMinAndSec {
  public getWithZero(value): string {
    if (value || value === 0) {
      if (value === 0) {
        return '00'
      }
      return value < 10 ? `0${value}` : value.toString()
    }
    return '00'
  }
}

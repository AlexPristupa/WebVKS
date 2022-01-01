import { DateTimeZeroMinAndSec } from '@/modules/DateTime/DateTimeZeroMinAndSec'

export class DateTimeString extends DateTimeZeroMinAndSec {
  constructor() {
    super()
  }

  public toStringDDMMYYYYHHMMSS(day, month, year, hour, minute, second) {
    return `${this.toStringDDMMYYYY(day, month, year)} ${this.toStringHHMMSS(
      hour,
      minute,
      second,
    )}`
  }

  public toStringDDMMYYYY(day, month, year): string {
    month = this.getWithZero((month || 0) + 1)
    return `${this.getWithZero(day)}.${month}.${year}`
  }

  public toStringHHMMSS(hour, minute, second): string {
    return `${this.getWithZero(hour)}:${this.getWithZero(
      minute,
    )}:${this.getWithZero(second)}`
  }

  public toISOString(day, month, year, hour, minute, second) {
    month = this.getWithZero((month || 0) + 1)
    const date = `${year}-${month}-${this.getWithZero(day)}`
    return `${date}T${this.toStringHHMMSS(hour, minute, second)}.000Z`
  }
}

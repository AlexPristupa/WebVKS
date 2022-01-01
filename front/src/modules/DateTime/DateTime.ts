import { constructorData } from './DateTime.types'
import { DateTimeString } from '@/modules/DateTime/DateTimeStrings'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

/**
 * @description Основной класс модуля DateTime для работы с сущностями даты и
 *              времени.
 *              Использует вспомогательные классы:
 *              - DateTimeBack() принимает в конструктор дату в формате строки DateTime
 *
 *              Есть методы для конвертации даты и времени между форматами.
 *
 *              Имеет набор статических методов для получения псевдо константных
 *              значений, таких как начало текущего месяца и подобных.
 */
export class DateTime extends DateTimeString {
  private readonly _day: number | null = null
  private readonly _month: number | null = null
  private readonly _year: number | null = null
  private readonly _hour: number | null = null
  private readonly _minute: number | null = null
  private readonly _second: number | null = null
  private readonly _jsDate: Date = new Date()

  constructor(data: constructorData) {
    super()
    if (data.dateTime && !data.date) {
      this._day = Number(data.dateTime.slice(0, 2))
      this._month = Number(data.dateTime.slice(3, 5)) - 1
      this._year = Number(data.dateTime.slice(6, 10))
      this._hour = Number(data.dateTime.slice(11, 13))
      this._minute = Number(data.dateTime.slice(14, 16))
      this._second = Number(data.dateTime.slice(17, 19))
      this._jsDate = new Date(
        this._year,
        this._month,
        this._day,
        this._hour,
        this._minute,
        this._second,
      )
    }
    if (!data.dateTime && data.date) {
      this._jsDate = data.date
      this._day = data.date.getDate()
      this._month = data.date.getMonth()
      this._year = data.date.getFullYear()
      this._hour = data.date.getHours()
      this._minute = data.date.getMinutes()
      this._second = data.date.getSeconds()
    }
    return this
  }

  getJsDate(): Date {
    return this._jsDate
  }

  getDateToString(): string {
    return this.toStringDDMMYYYY(this._day, this._month, this._year)
  }

  getTimeToString(): string {
    return this.toStringHHMMSS(this._hour, this._minute, this._second)
  }

  getDateAndTimeToString(): string {
    return this.toStringDDMMYYYYHHMMSS(
      this._day,
      this._month,
      this._year,
      this._hour,
      this._minute,
      this._second,
    )
  }

  toISO(): string {
    return this.toISOString(
      this._day,
      this._month,
      this._year,
      this._hour,
      this._minute,
      this._second,
    )
  }

  isTodayDate(): boolean {
    return (
      this.getDateToString() ===
      new DateTime({ date: new Date() }).getDateToString()
    )
  }

  public toChosenTimeZone(offset: number, type?: datePickerType): string {
    if (this._jsDate) {
      this._jsDate.setTime(this._jsDate.getTime() + (offset / 60) * 3600000)
      if (type) {
        return this.getToString(type)
      }
    }
    return ''
  }

  public toCurrentTimeZone(type?: datePickerType): string | Date {
    if (this._jsDate) {
      const offset = -new Date().getTimezoneOffset()
      this._jsDate.setTime(this._jsDate.getTime() + (offset / 60) * 3600000)
      if (type) {
        return this.getToString(type)
      }
      return this._jsDate
    }
    return ''
  }

  public toGlobalTime(type?: datePickerType, initOffset?: number): string {
    if (this._jsDate) {
      let offset = -new Date().getTimezoneOffset()
      if (initOffset) {
        offset = initOffset
      }
      this._jsDate.setTime(this._jsDate.getTime() - (offset / 60) * 3600000)
      if (type) {
        return this.getToString(type)
      }
    }
    return ''
  }

  private getToString(type: datePickerType): string {
    if (this._jsDate) {
      switch (type) {
        case datePickerType.date:
          return new DateTime({ date: this._jsDate }).getDateToString()
        case datePickerType.time:
          return new DateTime({ date: this._jsDate }).getTimeToString()
        case datePickerType.iso:
          return new DateTime({ date: this._jsDate }).toISO()
        default:
          return new DateTime({ date: this._jsDate }).getDateAndTimeToString()
      }
    }
    return ''
  }

  /**
   * @description Возвращает первое число текущего месяца 00:00:00
   */
  static beginningCurrentMonthDateTime() {
    const date = new Date()
    date.setDate(1)
    date.setHours(0)
    date.setMinutes(0)
    date.setSeconds(0)
    date.setMinutes(0)
    return new DateTime({ date: date }).getDateAndTimeToString()
  }

  /**
   * @description Возвращает конец текущего года
   */
  static endingCurrentYearDateTime() {
    const date = new Date()
    date.setDate(31)
    date.setMonth(11)
    date.setHours(23)
    date.setMinutes(59)
    date.setSeconds(59)
    return new DateTime({ date: date }).getDateAndTimeToString()
  }
}

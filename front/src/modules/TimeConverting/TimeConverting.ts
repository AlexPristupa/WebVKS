import { timeParts } from '@/modules/TimeConverting/TimeConverting.interface'
import { DateTimeString } from '@/modules/DateTime/DateTimeStrings'
import i18n from '@/i18n'

export class TimeConverting extends DateTimeString {
  constructor() {
    super()
  }
  static splitTimeString(time: string): timeParts {
    const timeParts: timeParts = {
      hours: 0,
      minutes: 0,
      seconds: 0,
    }
    const parts: RegExpMatchArray | null =
      time.length > 5
        ? time.match(/(\d{2}):(\d{2}):(\d{2})/)
        : time.match(/(\d{2}):(\d{2})/)
    if (parts && parts.length) {
      timeParts.hours = +parts[1]
      timeParts.minutes = +parts[2]
      timeParts.seconds = parts[3] ? +parts[3] : 0
    }
    return timeParts
  }
  static toHours(time: string): number {
    return Math.floor(TimeConverting.toSeconds(time) / 3600)
  }
  static toMinutes(time: string): number {
    return Math.floor(TimeConverting.toSeconds(time) / 60)
  }
  static toSeconds(time: string): number {
    const timeValues = TimeConverting.splitTimeString(time)
    return (
      timeValues.hours * 3600 + timeValues.minutes * 60 + timeValues.seconds
    )
  }
  static fromMinutes(minutes: number): string {
    return new TimeConverting().fromSeconds(minutes * 60)
  }

  public fromSeconds(seconds: number): string {
    const day = Math.floor(seconds / (3600 * 24))
    const hour = Math.floor((seconds % (3600 * 24)) / 3600)
    const minute = Math.floor((seconds % 3600) / 60)
    const second = Math.floor(seconds % 60)
    const time = this.toStringHHMMSS(hour, minute, second)
    if (day) {
      return `${this.getWithZero(day)} ${i18n.t('general.time.days')} ${time}`
    }
    return time
  }
}

/**
 * @description DateTimeProcessingRule возвращает инстанс дополнительных
 *              настроек экпортируемой колонки, если она date типа,
 *              работает с: ExportColumn, columnProcessingRules.
 */
import { dateTimeTypes } from '@/modules/Export/ProcessingRules/dateTime/DateTimeProcessingRule.const'

export class DateTimeProcessingRule {
  dateTime: dateTimeTypes

  constructor(dateTimeType) {
    this.dateTime = dateTimeType
  }
}

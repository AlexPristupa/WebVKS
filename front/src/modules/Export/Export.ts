/**
 * @description ExportColumn возвращает инстанс экспортируемой колонки,
 *              работает с: BooleanProcessingRule, DateTimeProcessingRule.
 */
import { DateTimeProcessingRule } from '@/modules/Export/ProcessingRules/dateTime/DateTimeProcessingRule'
import { BooleanProcessingRule } from '@/modules/Export/ProcessingRules/boolean/BooleanProcessingRule'

export class ExportColumn {
  fieldName: string
  fieldDisplayName: string
  isMainTable: boolean
  processingRule?: DateTimeProcessingRule | BooleanProcessingRule

  constructor(column, processingRules) {
    this.fieldName = column.isMainTable ? column.columnName : column.privateName
    this.fieldDisplayName = column.name
    this.isMainTable = column.isMainTable
    this.processingRule = processingRules
  }
}

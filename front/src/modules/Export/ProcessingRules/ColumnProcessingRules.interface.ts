import { DateTimeProcessingRule } from '@/modules/Export/ProcessingRules/dateTime/DateTimeProcessingRule'
import { BooleanProcessingRule } from '@/modules/Export/ProcessingRules/boolean/BooleanProcessingRule'

export interface IColumnProcessingRules {
  [key: string]: DateTimeProcessingRule | BooleanProcessingRule
}

/**
 * @description объект дополнительных настроек экспортируемых колонок
 */
import { IColumnProcessingRules } from '@/modules/Export/ProcessingRules/ColumnProcessingRules.interface'
// import { BooleanProcessingRule } from "@/modules/Export/ProcessingRules/boolean/BooleanProcessingRule";
// import { DateTimeProcessingRule } from "@/modules/Export/ProcessingRules/dateTime/DateTimeProcessingRule";
// import {dateTimeTypes} from "@/modules/Export/ProcessingRules/dateTime/DateTimeProcessingRule.const";

export const columnProcessingRules: IColumnProcessingRules = {
  // EXTENSION_O_invExtensionPContractDate: new DateTimeProcessingRule(dateTimeTypes.date),
}

/**
 * @description BooleanProcessingRule возвращает инстанс дополнительных
 *              настроек экпортируемой колонки, если она boolean типа,
 *              работает с: ExportColumn, columnProcessingRules.
 */
import { IProcessingBooleanRule } from '@/modules/Export/ProcessingRules/boolean/BooleanProcessingRule.interface'

export class BooleanProcessingRule {
  boolean: IProcessingBooleanRule

  constructor(config) {
    this.boolean = {
      true: config.true,
      false: config.false,
    }
  }
}

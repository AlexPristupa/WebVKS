import { TableName } from '@/modules/table_grid/TableName.const'

/**
 * @description Справочник возвращающий особые правила для компонента MpCellEditValue
 *              При необходимости введения дополнительных правил, написать класс
 *              по примеру EditValuePropertyPanel, которым имплементировать интерфейс
 *              ISettingPropertyEditValue. В поле settings добавить запись по схеме:
 *              [%имя_таблицы%]: класс реализующий правила
 */
export class SettingPropertyEditValueDictionary {
  static settings = {
    // [TableName.phonePropertyPanel]: EditValuePropertyPanel,
  }

  static getSettings(tableName: TableName) {
    return SettingPropertyEditValueDictionary.settings[tableName] || false
  }
}

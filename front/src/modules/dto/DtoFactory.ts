import { TableDto } from '@/modules/dto/classesDto/Table.Dto'
import { TableLimitDto } from '@/modules/dto/classesDto/TableLimit.Dto'
import { SelectFilterDto } from '@/modules/dto/classesDto/SelectFilter.Dto'
import { DtoName } from '@/modules/dto/DtoName.const'

/**
 * @description Фабрика создающее dto на по его имени, при необходимости может
 *              принять и применить новое состояние для dto.
 *
 *              Рекомендации:
 *              - новые классы dto создаются в ../classesDto
 *              - имена файлов классов dto именуются %DtoName%.Dto.ts
 *              - имена dto разпологаются в ./DtoName.const.ts
 */
export class DtoFactory {
  private static dtoList = {
    [DtoName.table]: TableDto,
    [DtoName.tableLimit]: TableLimitDto,
    [DtoName.selectFilter]: SelectFilterDto,
  }

  /**
   * @description создает Dto по его имени, если необходимо скорректировать
   *              параметры dto создаваемые по умолчанию, то передайте объект
   *              со спискос свойств и их значениями которые необходимо заменить
   */
  static create<T>(dtoType: DtoName = DtoName.table, state?: Partial<T>) {
    const Dto = DtoFactory.dtoList[dtoType] || DtoFactory.dtoList.tableDto
    if (state) {
      return DtoFactory.setState<typeof Dto>(new Dto(), state)
    }
    return new Dto()
  }

  /**
   * @description Метод применяет состояние к dto. Если dto не сожержит переданного
   *              имени свойства, то будет выброшено предупреждение в консоль.
   *              На прямую методом пользоваться не рекомендуется, рекомендованное
   *              использование через метод create
   */
  static setState<T>(Dto, state: Partial<T>) {
    Object.entries(state).forEach(property => {
      if (Object.keys(Dto).includes(property[0])) {
        Dto[property[0]] = property[1]
      } else {
        const getObjectClass = obj => {
          if (typeof obj !== 'object' || obj === null) {
            return false
          } else {
            // @ts-ignore
            return /(\w+)\(/.exec(obj.constructor.toString())[1]
          }
        }
        console.error(
          `[MentolPro DtoFactory] Нет свойства ${
            property[0]
          } в DTO: ${getObjectClass(Dto)}`,
        )
      }
    })
    return Dto
  }
}

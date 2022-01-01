/**
 * @description Класс валидатор содержити методы для валидации данных
 */
export class Validation {
  /**
   * @description Функция проверяет является ли строка процентами (от 0 до 100)
   */
  public static percentage(str: string): boolean {
    const number = parseFloat(str)
    return !(isNaN(number) || number < 0 || number > 100)
  }
}

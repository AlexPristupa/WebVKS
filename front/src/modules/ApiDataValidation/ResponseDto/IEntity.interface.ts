export interface IEntity {
  /**
   * Возвращает имя класса для отображения в функции вывода ошибок
   */
  toValidationName(): string
}

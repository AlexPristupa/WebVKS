/**
 * @description Задает тип проверки разрешений
 *              single - пользователь должне иметь хотябы одно разрешение
 *              multi - пользователь должне иметь Все разрешения
 */
export enum PermissionsType {
  single = 'single',
  multi = 'multi',
}

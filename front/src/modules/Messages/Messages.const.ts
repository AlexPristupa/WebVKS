/**
 * @description Тип сообщений.
 */
export enum typeMessage {
  warning = 'warning',
  info = 'info',
  error = 'error',
  success = 'success',
}

/**
 * @description Продолжительность показа сообщения.
 */
export enum durationMessage {
  short = 1500, // использовать для уведомлений типа typeMessage.success
  medium = 3000, // использовать для уведомлений типа typeMessage.info и typeMessage.warning
  long = 5000, // использовать для уведомлений типа typeMessage.error
}

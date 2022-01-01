/**
 * @description todo поразмышлять над неймингом и структурой. для установки значений
 * не нравится то, что часть данных в куках, часть в localStorage.
 * опять-таки верное решение - хранить в localStorage JWT-token, а не расшифрованные данные из него
 */

export const userConstants = {
  COOKIE_NAME_USER_NAME: 'userName',
  COOKIE_NAME_USER_FULLNAME: 'userFullName',
}

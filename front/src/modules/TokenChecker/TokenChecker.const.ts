export const tokenCheckerConstants = {
  checkInterval: 60_000, // интервал срабатывания события на проверку токена
  defaultExpireTimeMin: 60, // время протухания токена по умолчанию
  tokenActivityThreshold: 0.85, // порог в процентах "время последней активности" / "время жизни токена"
  cookieNameUserActivityTimestamp: 'lastUserActivity',
  cookieNameUserExpireTokenIntervalMin: 'expireInterval',
}

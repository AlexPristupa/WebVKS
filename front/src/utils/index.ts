export function debounce(func: () => void, wait: number, immediate?: boolean) {
  let timeout: any = null
  let args: any = null
  let context: any = null
  let timestamp = 0
  let result: any = null

  const later = () => {
    // В соответствии с последним интервалом переключения
    const last = +new Date() - timestamp

    if (last < wait && last > 0) {
      timeout = setTimeout(later, wait - last)
    } else {
      timeout = null
      if (!immediate) {
        result = func.apply(context, args)
        if (!timeout) context = args = null
      }
    }
  }

  return function(this: any, ...args: any) {
    context = this
    timestamp = +new Date()
    const callNow = immediate && !timeout
    if (!timeout) timeout = setTimeout(later, wait)
    if (callNow) {
      result = func.apply(context, args)
      context = args = null
    }

    return result
  }
}

export function parseTime(
  time: string | number | Date,
  cFormat: string,
): string | null {
  if (arguments.length === 0) {
    return null
  }
  const format = cFormat || '{y}-{m}-{d} {h}:{i}:{s}'
  let date
  if (typeof time === 'object') {
    date = time
  } else {
    if (typeof time === 'string' && /^[0-9]+$/.test(time)) {
      time = parseInt(time, 10)
    }
    if (typeof time === 'number' && time.toString().length === 10) {
      time = time * 1000
    }
    date = new Date(time)
  }
  const formatObj: { [key: string]: any } = {
    y: date.getFullYear(),
    m: date.getMonth() + 1,
    d: date.getDate(),
    h: date.getHours(),
    i: date.getMinutes(),
    s: date.getSeconds(),
    a: date.getDay(),
  }
  const timeStr = format.replace(/{(y|m|d|h|i|s|a)+}/g, (result, key) => {
    let value = formatObj[key]
    // Примечание: getDay() возвращает 0 в воскресенье
    if (key === 'a') {
      return ['день', 'час', 'один', 'два', 'три', 'четыре', 'пять'][value]
    }
    if (result.length > 0 && value < 10) {
      value = '0' + value
    }
    return value || 0
  })

  return timeStr
}

export function formatTime(time: string | number | Date, option: string) {
  time = +time * 1000
  const d: any = new Date(time)
  const now = Date.now()

  const diff = (now - d) / 1000

  if (diff < 30) {
    return 'только что'
  } else if (diff < 3600) {
    // меньше 1 часа
    return Math.ceil(diff / 60) + 'Несколько минут назад'
  } else if (diff < 3600 * 24) {
    return Math.ceil(diff / 3600) + 'Несколько часов назад'
  } else if (diff < 3600 * 24 * 2) {
    return '1 день назад'
  }
  if (option) {
    return parseTime(time, option)
  } else {
    return (
      d.getMonth() +
      1 +
      'месяц' +
      d.getDate() +
      'день' +
      d.getHours() +
      'час' +
      d.getMinutes() +
      'минута'
    )
  }
}

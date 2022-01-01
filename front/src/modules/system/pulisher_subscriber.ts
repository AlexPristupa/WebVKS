/**
 * @description модуль Издатель/Подписчик
 */

const subscribers: { [key: string]: any } = {}

/**
 * @description издатель события
 * @param {String} event имя события
 * @param {Object} data данные callback
 */
export function publish(event: string, data: any) {
  if (!subscribers[event]) {
    return
  }

  subscribers[event].forEach((subscriberCallback: (arg0: any) => any) =>
    subscriberCallback(data),
  )
}

/**
 * @description Подписчик на событие
 * @param {String} event имя события
 * @param {Function} callback
 */
export function subscribe(event: string, callback: any) {
  if (!subscribers[event]) {
    subscribers[event] = []
  }

  const index = subscribers[event].push(callback) - 1

  return {
    unsubscribe() {
      subscribers[event].splice(index, 1)
    },
  }
}

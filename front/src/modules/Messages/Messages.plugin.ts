import { Message } from 'element-ui'
import Vue, { CreateElement, VNode } from 'vue'
import { durationMessage, typeMessage } from '@/modules/Messages/Messages.const'
import {
  IPluginMessage,
  MessageSubMessage,
  MessageTitle,
  RenderMessage,
  ShowMessage,
  WrapMessage,
} from '@/modules/Messages/Messages.interface'

const h: CreateElement = new Vue().$createElement

export const renderMessage: RenderMessage = ({
  message = '',
  caption = [''],
  isShowCaption = false,
}) => {
  if (isShowCaption) {
    return h(
      'div',
      { class: 'el-message__content' },
      [h('div', undefined, message)].concat(
        caption.map(item => {
          return h('div', { class: 'caption' }, [h('small', item.toString())])
        }),
      ),
    )
  } else {
    return h('div', { class: 'el-message__content' }, [
      h('div', undefined, message),
    ])
  }
}

export const wrapper: WrapMessage = (title, type, duration, subMessage) => {
  let message: string | VNode = title
  if (
    (typeof subMessage === 'string' || Array.isArray(subMessage)) &&
    subMessage.length
  ) {
    message = renderMessage({
      message: title,
      caption: typeof subMessage === 'string' ? [subMessage] : subMessage,
      isShowCaption: true,
    })
  }
  setTimeout(() => {
    return Message({
      showClose: true,
      type,
      message: message,
      duration,
    })
  }, 1)
}

export const successMessage: ShowMessage = (title, subMessage) => {
  return wrapper(title, typeMessage.success, durationMessage.short, subMessage)
}

export const infoMessage: ShowMessage = (title, subMessage) => {
  return wrapper(title, typeMessage.info, durationMessage.medium, subMessage)
}

export const warningMessage: ShowMessage = (title, subMessage) => {
  return wrapper(title, typeMessage.warning, durationMessage.medium, subMessage)
}

export const errorMessage: ShowMessage = (title, subMessage) => {
  return wrapper(title, typeMessage.error, durationMessage.long, subMessage)
}

/**
 * @description Подключение функциональности плагина в прототип Vue
 */
export default {
  install(Vue: IPluginMessage) {
    Vue.prototype.$mes = {
      success: (title: MessageTitle, subMessage: MessageSubMessage = '') =>
        successMessage(title, subMessage),
      info: (title: MessageTitle, subMessage: MessageSubMessage = '') =>
        infoMessage(title, subMessage),
      warning: (title: MessageTitle, subMessage: MessageSubMessage = '') =>
        warningMessage(title, subMessage),
      error: (title: MessageTitle, subMessage: MessageSubMessage = '') =>
        errorMessage(title, subMessage),
    }
    Vue.prototype.$message = {
      ...Vue.prototype.$message,
      ...Vue.prototype.$mes,
    }
  },
}

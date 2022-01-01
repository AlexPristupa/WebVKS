import { VNode } from 'vue'
import { durationMessage, typeMessage } from '@/modules/Messages/Messages.const'
import { Vue } from 'vue/types/vue'

export interface IPayloadRenderMessage {
  message: string
  caption: Array<string>
  isShowCaption: boolean
}

export type RenderMessage = (payload: IPayloadRenderMessage) => VNode

export interface IPluginMessage<V extends Vue = Vue> {
  prototype: {
    $mes: {
      success: ShowMessage
      info: ShowMessage
      warning: ShowMessage
      error: ShowMessage
    }
    $message: any
  }
}

export type MessageTitle = string
export type MessageSubMessage = string | Array<string> | null

export type ShowMessage = (
  title: MessageTitle,
  subMessage: MessageSubMessage,
) => void

export type WrapMessage = (
  title: MessageTitle,
  type: typeMessage,
  duration: durationMessage,
  subMessage: MessageSubMessage,
) => void

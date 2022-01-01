<template>
  <div class="mp-text-message" :class="cssClassMessage">
    <ul class="mp-text-message__list">
      <li
        v-for="(message, index) in messageList"
        :key="index"
        class="mp-text-message__item"
      >
        {{ message }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { TextMessageType } from '@/components/MpTextMessage/MpTextMessage.enum'

export default Vue.extend<
  {},
  {},
  {
    cssClassMessage: string
  },
  {
    messageType: TextMessageType
    messageList: Array<string>
  }
>({
  name: 'MpTextMessage',
  props: {
    messageType: {
      type: String as () => TextMessageType,
      default: () => {
        return TextMessageType.info
      },
    },
    messageList: {
      type: Array as () => Array<string>,
      default: () => {
        return []
      },
    },
  },
  computed: {
    cssClassMessage() {
      switch (this.messageType) {
        case TextMessageType.info:
          return 'mp-text-message--info'
        case TextMessageType.warning:
          return 'mp-text-message--warning'
        case TextMessageType.success:
          return 'mp-text-message--success'
        case TextMessageType.error:
          return 'mp-text-message--error'
        default:
          return TextMessageType.info
      }
    },
  },
})
</script>

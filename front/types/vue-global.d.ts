import Vue from 'vue'

declare module 'vue/types/vue' {
  // Можно использовать `VueConstructor`
  interface Vue {
    readonly $api: any
  }
}

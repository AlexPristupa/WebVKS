<template>
  <div :class="'resize-line resize-line--' + type">
    <div :class="'resize-line__area resize-line__area--' + type"></div>
  </div>
</template>

<script lang="ts">
/**
 * @description компонент линии для ресайза в лэйаутах
 * используется в TwoHorizontalPanelsLayout
 */
import Vue from 'vue'
import { MpResizeLineType } from '@/components/basic/MpResizeLine/MpResizeLine.const.ts'
import {
  IPropsMpResizeLine,
  IDataMpResizeLine,
  IComputedMpResizeLine,
  IMethodsMpResizeLine,
} from '@/components/basic/MpResizeLine/MpResizeLine.interface.ts'

export default Vue.extend<
  IDataMpResizeLine,
  IMethodsMpResizeLine,
  IComputedMpResizeLine,
  IPropsMpResizeLine
>({
  name: 'MpResizeLine',
  props: {
    type: {
      type: String as () => MpResizeLineType,
      default: MpResizeLineType.horizontal,
      validator: value => {
        return Object.values(MpResizeLineType).includes(value)
      },
    },
  },
})
</script>

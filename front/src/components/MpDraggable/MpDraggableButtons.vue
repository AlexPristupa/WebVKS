<template>
  <div
    class="mp-draggable__buttons"
    :class="{
      middle: settings.position === buttonPosition.middle,
      horizontal: settings.isHorizontal,
    }"
  >
    <mp-button
      v-for="(button, index) in buttons"
      :key="index"
      :mp-type="button.type"
      @click="buttonHandler(button.arg)"
    >
      {{ button.content }}
    </mp-button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import {
  movingButtonContentHorizontal,
  movingButtonsVertical,
} from '@/components/MpDraggable/config/mpDraggable.const'
import {
  buttonPositions,
  IComputedMpDraggableButtons,
  IDataMpDraggableButtons,
  IMethodsMpDraggableButtons,
  IPropsMpDraggableButtons,
} from '@/components/MpDraggable/config/mpDraggableButtons.interface'

export default Vue.extend<
  IDataMpDraggableButtons,
  IMethodsMpDraggableButtons,
  IComputedMpDraggableButtons,
  IPropsMpDraggableButtons
>({
  name: 'MpDraggable',
  components: {
    MpButton,
  },
  props: {
    settings: {
      type: Object,
      default: () => ({
        position: buttonPositions.middle,
        isHorizontal: false,
      }),
    },
  },
  data() {
    return {
      activeColumn: {},
      buttonPosition: {
        middle: buttonPositions.middle,
        right: buttonPositions.right,
      },
    }
  },
  computed: {
    buttons() {
      return this.settings.isHorizontal
        ? movingButtonContentHorizontal
        : movingButtonsVertical
    },
  },
  methods: {
    buttonHandler(buttonArg) {
      this.$emit('click', buttonArg)
    },
  },
})
</script>

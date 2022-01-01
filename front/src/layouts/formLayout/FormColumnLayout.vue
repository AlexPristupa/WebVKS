<template>
  <div class="layout_form_columns">
    <div
      v-for="(slot, index) in slots"
      :key="index"
      class="layout_form_column"
      :class="`layout_form_column--${width}`"
    >
      <div class="layout_form_column__title">
        <p v-if="columns.titles.length">
          {{ $t(`${columns.titles[slot]}`) }}
        </p>
        <div v-if="columns.buttons">
          <mp-button
            v-for="(button, index) in columns.buttons[slot]"
            :key="index"
            is-icon
            :mp-type="button.type"
            :title="button.title"
            mp-status="normal"
            @click="$emit(button.type)"
          />
        </div>
      </div>
      <slot :name="`column_${slot}`" />
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import {
  formLayoutColumnWidth,
  IComputedFormColumnLayout,
  IDataFormColumnLayout,
  IFormLayoutColumn,
  IMethodsFormColumnLayout,
  IPropsFormColumnLayout,
} from '@/layouts/formLayout/formColumnLayout.interface'
import MpButton from '@/components/basic/MpButton/MpButton.vue'

export default Vue.extend<
  IDataFormColumnLayout,
  IMethodsFormColumnLayout,
  IComputedFormColumnLayout,
  IPropsFormColumnLayout
>({
  name: 'FormColumnLayout',
  components: { MpButton },
  props: {
    columns: {
      type: Object as () => IFormLayoutColumn,
      default: () => ({
        number: 1,
        equals: true,
        buttons: [],
        titles: [],
      }),
    },
  },
  computed: {
    slots() {
      return Array.from(Array(this.columns.number).keys())
    },
    width() {
      if (this.columns.equals) {
        if (this.columns.number === 2) {
          return formLayoutColumnWidth.half
        } else if (this.columns.number === 3) {
          return formLayoutColumnWidth.oneThird
        }
        return formLayoutColumnWidth.full
      } else {
        return formLayoutColumnWidth.auto
      }
    },
  },
})
</script>

<template>
  <el-tooltip
    :content="buttonTitle"
    :disabled="!buttonTitle"
    popper-class="button__custom-tooltip"
    placement="bottom"
    :open-delay="1000"
    :visible-arrow="false"
  >
    <el-button
      :type="type"
      :size="size"
      :plain="plain"
      :round="round"
      :circle="circle"
      :loading="loading"
      :disabled="disabled"
      :autofocus="autofocus"
      :icon="icon"
      class="mp-button"
      :class="buttonClass"
      @click="$emit('click')"
    >
      <div v-if="isIcon" class="mp-button__icon"></div>
      <slot />
    </el-button>
  </el-tooltip>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  buttonsWithoutHint,
  MpStatusButton,
  MpTypeButton,
  NativeTypeButton,
  SizeButton,
  TypeButton,
} from '@/components/basic/MpButton/MpButton.const'
import {
  IComputedMpButton,
  IDataMpButton,
  IMethodsMpButton,
  IPropsMpButton,
} from '@/components/basic/MpButton/MpButton.interface'

export default Vue.extend<
  IDataMpButton,
  IMethodsMpButton,
  IComputedMpButton,
  IPropsMpButton
>({
  name: 'MpButton',
  props: {
    mpType: {
      type: String as () => MpTypeButton,
      default: MpTypeButton.unknown,
      validator: (value: MpTypeButton) => {
        return Object.values(MpTypeButton).includes(value)
      },
    },
    mpStatus: {
      type: String as () => MpStatusButton,
      default: MpStatusButton.normal,
      validator: (value: MpStatusButton) => {
        return Object.values(MpStatusButton).includes(value)
      },
    },
    size: {
      type: String as () => SizeButton,
      default: SizeButton.unknown,
      validator: (value: SizeButton) => {
        return Object.values(SizeButton).includes(value)
      },
    },
    type: {
      type: String as () => TypeButton,
      default: TypeButton.unknown,
      validator: (value: TypeButton) => {
        return Object.values(TypeButton).includes(value)
      },
    },
    plain: {
      type: Boolean,
      default: false,
    },
    round: {
      type: Boolean,
      default: false,
    },
    circle: {
      type: Boolean,
      default: false,
    },
    loading: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    autofocus: {
      type: Boolean,
      default: false,
    },
    icon: {
      type: String,
      default: '',
    },
    nativeType: {
      type: String as () => NativeTypeButton,
      default: NativeTypeButton.button,
      validator: (value: NativeTypeButton) => {
        return Object.values(NativeTypeButton).includes(value)
      },
    },
    isIcon: {
      type: Boolean,
      default: false,
    },
    title: {
      type: String,
      default: '',
    },
  },
  computed: {
    buttonClass() {
      return [
        `mp-button--${this.mpType}`,
        this.mpStatus === 'active' ? `mp-button--active` : '',
        this.mpStatus === 'disabled' ? `mp-button--disabled` : '',
        this.isIcon ? `mp-button--icon` : '',
      ].join(' ')
    },
    buttonTitle() {
      if (this.title) {
        return this.title
      }
      if (buttonsWithoutHint.includes(this.mpType)) {
        return ''
      }
      return this.$t(`general.${this.mpType}`)
    },
  },
})
</script>

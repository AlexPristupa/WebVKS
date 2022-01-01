<template>
  <div class="mp-select">
    <el-select
      ref="el-select"
      :allow-create="allowCreate"
      :autocomplete="autocomplete"
      :automatic-dropdown="automaticDropdown"
      :clearable="clearable"
      :collapse-tags="collapseTags"
      :default-first-option="defaultFirstOption"
      :disabled="disabled"
      :filter-method="filterMethod"
      :filterable="filterable"
      :loading="loading"
      :loading-text="loadingText"
      :multiple="multiple"
      :multiple-limit="multipleLimit"
      :name="name"
      :no-data-text="noDataText"
      :no-match-text="noMatchText"
      :placeholder="placeholder"
      :popper-append-to-body="popperAppendToBody"
      :popper-class="popperClass"
      :remote="remote"
      :reserve-keyword="reserveKeyword"
      :size="size"
      :value="valueData"
      :value-key="valueKey"
      @change="change"
      @visible-change="visibleChange"
      @remove-tag="removeTag"
      @clear="clear"
      @blur="blur"
      @focus="focus"
    >
      <template #prefix>
        <slot name="prefix" />
      </template>

      <template #empty>
        <div class="mp-select__dropdown__empty">
          {{ noDataText }}
        </div>
      </template>

      <template #default>
        <mp-loading-spinner v-if="loading" />
        <div v-else>
          <el-option
            v-for="item in optionListField"
            :key="item[optionKey]"
            :label="item[optionLabel]"
            :value="optionValueField(item)"
          />
        </div>
      </template>
    </el-select>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpLoadingSpinner from '@/components/MpLoadingSpinner/MpLoadingSpinner.vue'
import { MP_SELECT_SIZE } from '@/components/MpSelect/MpSelect.const'
import {
  IComputedMpSelect,
  IDataMpSelect,
  IMethodsMpSelect,
  IPropsMpSelect,
} from '@/components/MpSelect/MpSelect.interface'

/**
 * @description Компонент обертка над компонентом библиотеки element-ui для
 *              кастомизации и оверайда поведения
 */
export default Vue.extend<
  IDataMpSelect,
  IMethodsMpSelect,
  IComputedMpSelect,
  IPropsMpSelect
>({
  name: 'MpSelect',
  components: {
    MpLoadingSpinner,
  },
  data() {
    return {
      customValue: null,
    }
  },
  props: {
    optionList: {
      type: Array,
      default: () => [],
    },
    optionKey: {
      type: String,
      default: '',
    },
    optionLabel: {
      type: String,
      default: '',
    },
    optionValue: {
      type: String,
      default: '',
    },

    autocomplete: {
      type: String,
      default: 'off',
    },
    automaticDropdown: {
      type: Boolean,
      default: false,
    },
    allowCreate: {
      type: Boolean,
      default: false,
    },
    clearable: {
      type: Boolean,
      default: false,
    },
    collapseTags: {
      type: Boolean,
      default: false,
    },
    defaultFirstOption: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    filterable: {
      type: Boolean,
      default: false,
    },
    filterMethod: {
      type: Function,
    },
    loading: {
      type: Boolean,
      default: false,
    },
    loadingText: {
      type: String,
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    multipleLimit: {
      type: Number,
      default: 0,
    },
    name: {
      type: String,
    },
    noMatchText: {
      type: String,
      default: '',
    },
    noDataText: {
      type: String,
      default: 'нет данных',
    },
    placeholder: {
      type: String,
      default: '',
    },
    popperClass: {
      type: String,
      default: 'mp-select__dropdown',
    },
    popperAppendToBody: {
      type: Boolean,
      default: true,
    },
    remote: {
      type: Boolean,
      default: false,
    },
    remoteMethod: {
      type: Function,
    },
    reserveKeyword: {
      type: Boolean,
      default: false,
    },
    size: {
      type: String as () => MP_SELECT_SIZE,
      default: MP_SELECT_SIZE.small,
    },
    value: {
      required: true,
    },
    valueKey: {
      type: String,
      default: 'value',
    },
  },
  computed: {
    optionListField() {
      return this.optionList
      // if (this.optionList.length) {
      //   return this.optionList
      // } else {
      //   return [
      //     {
      //       [this.optionValue]: Math.random() * 10000000000000,
      //       [this.optionLabel]: this.$t('general.noData'),
      //     },
      //   ]
      // }
    },
    valueData() {
      return this.value
    },
  },
  methods: {
    optionValueField(item) {
      if (this.optionValue) {
        return item[this.optionValue]
      }
      return item
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    blur(payload) {
      this.$emit('blur', payload)
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    change(payload) {
      // if (payload[this.optionLabel] !== this.$t('general.noData')) {
      this.$emit('change', payload)
      // } else {
      //   this.value = this.customValue
      // }
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    clear(payload) {
      this.$emit('clear', payload)
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    focus(payload) {
      this.$emit('focus', payload)
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    removeTag(payload) {
      this.$emit('remove-tag', payload)
    },
    /**
     * @description up element-ui event
     * @param payload
     */
    visibleChange(payload) {
      this.$emit('visible-change', payload)
    },
  },
})
</script>

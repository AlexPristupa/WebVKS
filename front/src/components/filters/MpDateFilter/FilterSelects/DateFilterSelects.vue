<template>
  <div class="filter-select">
    <div class="filter-select--operand">
      <el-select
        :value="initialDate.operand"
        @change="emitOperand"
        placeholder="Select"
      >
        <el-option
          v-for="(item, index) in dateOperandList"
          :key="index"
          :label="item.name"
          :value="item.value"
        />
      </el-select>
    </div>
    <div class="filter-select--date">
      <date-picker
        :value="initialDate.date"
        type="date"
        :placeholder="
          $t('forms.placeholders.chooseEntity', [
            $t('forms.placeholders.entities.date'),
          ])
        "
        :format="dateFormat"
        :value-type="dateFormat"
        :lang="langDatePicker"
        :disabled="disabled"
        :append-to-body="false"
        @clear="clearDate"
        @change="emitDate"
      />
    </div>
    <div class="filter-select--time">
      <date-picker
        :value="initialDate.time"
        format="HH:mm:ss"
        type="time"
        value-type="HH:mm:ss"
        :disabled="disabled"
        :placeholder="$t('forms.placeholders.HHMMSS')"
        :lang="langDatePicker"
        :append-to-body="false"
        @clear="clearTime"
        @change="emitTime"
      />
    </div>
    <div class="filter-select--clear" @click="$emit('clear')" />
  </div>
</template>
<script lang="ts">
/**
 * @description селекты для фильтра типа 'дата', включают в себя поле выбора операнда,
 * поле выбора даты, поле выбора времени и иконку возвращения в начальное состояние.
 *  Работает совместно с: MpDateFilter
 */
import Vue from 'vue'
import DatePicker from 'vue2-datepicker'
// @ts-ignore
import ruDatePickerLocale from 'vue2-datepicker/locale/ru'
// @ts-ignore
import enDatePickerLocale from 'vue2-datepicker/locale/en'
import { IInitialDateFilterState } from '../DateFilter.interface'
import { IDataDateFilterSelects } from './DateFilterSelects.interface'

import dateOperandsConstants from '@/modules/Filters/Operands/DateOperands.const'
import { operandValue } from '@/modules/Filters/Operands/Operands.const'

export default Vue.extend({
  name: 'DateFilterSelects',
  components: {
    DatePicker,
  },
  props: {
    initialDate: {
      type: Object as () => IInitialDateFilterState,
      default: (): IInitialDateFilterState => {
        return {
          operand: operandValue.smallerOrEqual,
          date: '',
          time: '',
        }
      },
    },
    disabled: {
      type: Boolean as () => boolean,
      default: false,
    },
    dateFormat: {
      type: String as () => string,
      default: '',
    },
  },
  computed: {
    langDatePicker() {
      const lang = this.$i18n.locale
      return lang === 'ru' ? ruDatePickerLocale : enDatePickerLocale
    },
  },
  data(): IDataDateFilterSelects {
    return {
      dateOperandList: dateOperandsConstants,
    }
  },
  methods: {
    emitOperand(operand: string) {
      this.$emit('operandChanged', operand)
    },
    emitDate(date: string) {
      this.$emit('dateChanged', date)
    },
    emitTime(time: string) {
      this.$emit('timeChanged', time)
    },
    clearDate() {
      this.$emit('clearDate')
    },
    clearTime() {
      this.$emit('clearTime')
    },
  },
})
</script>

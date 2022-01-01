<template>
  <div class="mp-date-picker">
    <date-picker
      popup-class="mp-date-picker__popup"
      :value="value"
      :format="format"
      :value-type="format"
      :disabled-date="getDisabledDate"
      :disabled-time="getDisabledTime"
      :disabled="disabled"
      :editable="editable"
      :lang="langDatePicker"
      :type="datePickerType"
      :append-to-body="append"
      :time-picker-options="timePickerOptions"
      :placeholder="$t('forms.placeholders.enterEntity', [placeholder])"
      @clear="setDefaults"
      @change="changed"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import DatePicker from 'vue2-datepicker'
import ruDatePickerLocale from 'vue2-datepicker/locale/ru'
import enDatePickerLocale from 'vue2-datepicker/locale/en'
import {
  datePickerType,
  defaultTimeHhmm,
  defaultTimeHhmmss,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import {
  IComputedMpDatePicker,
  IDateMpDatePicker,
  IMethodsMpDatePicker,
  IPropsMpDatePicker,
} from '@/components/basic/MpDatePicker/MpDatePicker.interface'
import { dateMasks } from '@/constant'
import { DateOrder } from '@/modules/DateTime/DateOrder.const'
import CONSTANTS from '@/constants'
import { DateTime } from '@/modules/DateTime/DateTime'

export default Vue.extend<
  IDateMpDatePicker,
  IMethodsMpDatePicker,
  IComputedMpDatePicker,
  IPropsMpDatePicker
>({
  name: 'MpDatePicker',
  props: {
    value: {
      type: String,
      default: '',
    },
    timePickerOptions: {
      type: Object || null,
      default: null,
    },
    append: {
      type: Boolean,
      default: false,
    },
    datePickerType: {
      type: String as () => datePickerType,
      default: datePickerType.date,
    },
    placeholder: {
      type: String,
      default: '',
    },
    disablePreviousDates: {
      type: Boolean,
      default: false,
    },
    disableFromDate: {
      type: Object as () => DateTime,
      default: () => new DateTime({ date: new Date() }),
    },
    disableFromTime: {
      type: Object as () => DateTime,
      default: () => new DateTime({ date: new Date() }),
    },
    editable: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    timeFormat: {
      type: String as () => timeFormat,
      default: timeFormat.hhmmss,
    },
  },
  components: {
    DatePicker,
  },
  computed: {
    langDatePicker() {
      const lang = this.$i18n.locale
      return lang === 'ru' ? ruDatePickerLocale : enDatePickerLocale
    },
    format() {
      const lang = this.$i18n.locale
      if (this.datePickerType === datePickerType.date) {
        return dateMasks.withoutTime[lang]
      } else if (this.datePickerType === datePickerType.dateTime) {
        return dateMasks.withTime.withoutSeconds[lang]
      }
      return this.timeFormat
    },
    dateOrder() {
      return this.format === CONSTANTS.dateMasks.withoutTime.ru
        ? DateOrder.ru
        : DateOrder.en
    },
  },
  methods: {
    getDisabledDate(date) {
      const compareDate = new DateTime({ date: date })
      return this.disablePreviousDates
        ? compareDate.getJsDate() < this.disableFromDate.getJsDate() &&
            this.disableFromDate.getDateToString() !==
              compareDate.getDateToString()
        : false
    },
    getDisabledTime(date) {
      return false
    },
    setDefaults() {
      let value = ''
      if (this.datePickerType === datePickerType.date) {
        value = this.setDefaultDate()
      } else if (this.datePickerType === datePickerType.time) {
        value = this.setDefaultTime()
      } else if (this.datePickerType === datePickerType.dateTime) {
        value = this.setDefaultDate() + ` ${this.setDefaultTime()}`
      }
      this.changed(value)
    },
    setDefaultDate() {
      return new DateTime({
        date: new Date(),
      }).getDateToString()
    },
    setDefaultTime() {
      return this.timeFormat === timeFormat.hhmmss
        ? defaultTimeHhmmss
        : defaultTimeHhmm
    },
    changed(value) {
      this.$emit('change', value)
    },
  },
})
</script>

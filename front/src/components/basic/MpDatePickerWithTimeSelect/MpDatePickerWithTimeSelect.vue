<template>
  <div class="mp-date-picker--with-select">
    <mp-date-picker
      class="half"
      :value="entityModel.time"
      :time-format="timeFormat"
      :date-picker-type="datePickerTypes.time"
      :time-picker-options="{
        start: '00:00',
        step: '00:30',
        end: '23:30',
        format: timeFormat,
      }"
      :disable-previous-dates="datePickerSettings.disablePreviousDates"
      :disable-from-time="disableFromTime"
      :editable="datePickerSettings.editable"
      @change="entityChanged('time', $event)"
    />
    <mp-date-picker
      class="half"
      :value="entityModel.date"
      :date-picker-type="datePickerTypes.date"
      :placeholder="datePickerSettings.placeholder"
      :disable-previous-dates="datePickerSettings.disablePreviousDates"
      :disable-from-date="disableFromDate"
      :editable="datePickerSettings.editable"
      @change="entityChanged('date', $event)"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpDatePickerWithTimeSelect,
  IDateMpDatePickerWithTimeSelect,
  IMethodsMpDatePickerWithTimeSelect,
  IPropsMpDatePickerWithTimeSelect,
} from '@/components/basic/MpDatePickerWithTimeSelect/MpDatePickerWithTimeSelect.interface'
import {
  datePickerTypes,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import MpDatePicker from '@/components/basic/MpDatePicker/MpDatePicker.vue'
import { DateTime } from '@/modules/DateTime/DateTime'
import { TimeConverting } from '@/modules/TimeConverting/TimeConverting'

export default Vue.extend<
  IDateMpDatePickerWithTimeSelect,
  IMethodsMpDatePickerWithTimeSelect,
  IComputedMpDatePickerWithTimeSelect,
  IPropsMpDatePickerWithTimeSelect
>({
  name: 'MpDatePickerWithTimeSelect',
  components: {
    MpDatePicker,
  },
  props: {
    initial: {
      type: String,
      default: new DateTime({ date: new Date() }).getDateToString(),
    },
    datePickerSettings: {
      type: Object,
      default: () => ({
        placeholder: '',
        editable: false,
        disablePreviousDates: false,
        disableFromDate: new DateTime({
          date: new Date(),
        }).getDateAndTimeToString(),
        disableFromTime: new DateTime({
          date: new Date(),
        }).getDateAndTimeToString(),
      }),
    },
  },
  computed: {
    disableFromDate() {
      if (this.datePickerSettings.disableFromDate) {
        return new DateTime({
          dateTime: this.datePickerSettings.disableFromDate,
        })
      }
      return new DateTime({ date: new Date() })
    },
    disableFromTime() {
      if (this.datePickerSettings.disableFromTime) {
        return new DateTime({
          dateTime: this.datePickerSettings.disableFromTime,
        })
      }
      return new DateTime({ date: new Date() })
    },
  },
  watch: {
    initial(v) {
      if (v) {
        const dateToDivide = new DateTime({ dateTime: v })
        this.entityModel.time = dateToDivide.getTimeToString().substring(0, 5)
        this.entityModel.date = dateToDivide.getDateToString()
      }
    },
  },
  mounted() {
    this.setInitialData()
    this.emitChanges()
  },
  data() {
    return {
      datePickerTypes,
      timeFormat: timeFormat.hhmm,
      entityModel: {
        date: '',
        time: '',
      },
    }
  },
  methods: {
    entityChanged(entity, value) {
      this.entityModel[entity] = value
      if (this.entityModel.date === this.disableFromDate.getDateToString()) {
        this.compareTime(this.disableFromDate.getTimeToString())
      }
      this.emitChanges()
    },
    emitChanges() {
      this.$emit('change', `${this.entityModel.date} ${this.entityModel.time}`)
    },
    compareTime(now) {
      if (
        this.entityModel.time &&
        TimeConverting.toSeconds(this.entityModel.time) <
          TimeConverting.toSeconds(now)
      ) {
        this.entityModel.time = this.setMinutes(now)
      }
    },
    setInitialData() {
      const dateToDivide = new DateTime({ dateTime: this.initial })
      this.entityModel.time = this.setMinutes(
        dateToDivide.getTimeToString().substring(0, 5),
      )
      this.entityModel.date = dateToDivide.getDateToString()
    },
    setMinutes(time) {
      const hours = time.substring(0, 2)
      if (Number(hours) !== 23) {
        return `${Number(hours) + 1}:00`
      }
      return '00:00'
    },
  },
})
</script>

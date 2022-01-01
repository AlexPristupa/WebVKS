<template>
  <div class="mp-modal--edit-booking-periodic__frequency-settings--monthly">
    <el-radio-group :value="tab">
      <el-radio
        v-for="(item, index) in list"
        class="mp-radio"
        :key="index"
        :label="item.id"
        @change="setTab(item.id)"
      >
        <p class="title" :style="{ width: getWidthTitle(item.id) }">
          {{ item.name }}
        </p>
        <div
          class="settings-block"
          :style="{ width: getWidthSettings(item.id) }"
        >
          <el-input-number
            v-if="item.id === 1"
            v-model="inputModel.monthDay1"
            :min="1"
            :max="30"
            @change="entityChanged('monthDay1', item.id, $event)"
          />

          <el-input-number
            v-if="item.id === 4"
            v-model="inputModel.monthDay4"
            :min="1"
            :max="30"
            @change="entityChanged('monthDay4', item.id, $event)"
          />

          <mp-select
            v-if="item.id === 3"
            option-key="name"
            option-value="name"
            option-label="name"
            :option-list="weekdaysList"
            :value="inputModel.weekday3"
            :popper-append-to-body="false"
            @change="entityChanged('weekday3', item.id, $event)"
          />

          <mp-select
            v-if="item.id === 4"
            option-key="name"
            option-value="name"
            option-label="name"
            :option-list="weekdaysList"
            :value="inputModel.weekday4"
            :popper-append-to-body="false"
            @change="entityChanged('weekday4', item.id, $event)"
          />
          <div class="settings-block__date-picker">
            <span class="el-input-number__text" v-if="item.id === 1">
              {{ $t(`forms.editBooking.texts.monthDayAt`) }}
            </span>
            <span class="el-input-number__text" v-else>
              {{ $t(`forms.editBooking.texts.monthAt`) }}
            </span>
            <mp-date-picker
              v-if="item.id === 1"
              :append="true"
              :value="inputModel.duration1"
              :time-format="timeFormats.hhmmss"
              :date-picker-type="datePickerTypes.time"
              @change="entityChanged('duration1', item.id, $event)"
            />
            <mp-date-picker
              v-if="item.id === 2"
              :append="true"
              :value="inputModel.duration2"
              :time-format="timeFormats.hhmmss"
              :date-picker-type="datePickerTypes.time"
              @change="entityChanged('duration2', item.id, $event)"
            />
            <mp-date-picker
              v-if="item.id === 3"
              :append="true"
              :value="inputModel.duration3"
              :time-format="timeFormats.hhmmss"
              :date-picker-type="datePickerTypes.time"
              @change="entityChanged('duration3', item.id, $event)"
            />
            <mp-date-picker
              v-if="item.id === 4"
              :append="true"
              :value="inputModel.duration4"
              :time-format="timeFormats.hhmmss"
              :date-picker-type="datePickerTypes.time"
              @change="entityChanged('duration4', item.id, $event)"
            />
          </div>
        </div>
      </el-radio>
    </el-radio-group>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import MpDatePicker from '@/components/basic/MpDatePicker/MpDatePicker.vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import {
  list,
  weekdaysList,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.const.ts'
import {
  datePickerTypes,
  timeFormats,
} from '@/components/basic/MpDatePicker/MpDatePicker.const.ts'
import {
  EditPeriodicMonthlyEntity,
  IComputedBookingEditPeriodicMonthly,
  IDataBookingEditPeriodicMonthly,
  IMethodsBookingEditPeriodicMonthly,
  IPropsBookingEditPeriodicMonthly,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.interface'
import { DateTime } from '@/modules/DateTime/DateTime'
import {
  monthStringFormators,
  weekdays,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.const'

export default Vue.extend<
  IDataBookingEditPeriodicMonthly,
  IMethodsBookingEditPeriodicMonthly,
  IComputedBookingEditPeriodicMonthly,
  IPropsBookingEditPeriodicMonthly
>({
  name: 'EditPeriodicMonthly',
  props: {
    date: {
      type: String,
      default: new DateTime({
        date: new Date(),
      }).getDateToString(),
    },
    tabs: {
      type: String,
      default: '3,1',
    },
    initData: {
      type: Object as () => EditPeriodicMonthlyEntity,
      default: () => new EditPeriodicMonthlyEntity(),
    },
  },
  components: {
    MpDatePicker,
    MpSelect,
  },
  data(this) {
    return {
      list,
      weekdaysList,
      timeFormats,
      datePickerTypes,
      inputModel: new EditPeriodicMonthlyEntity(),
    }
  },
  computed: {
    tab() {
      return Number(this.tabs.substring(2))
    },
  },
  watch: {
    tab() {
      this.setModel()
    },
  },
  mounted() {
    this.inputModel = this.initData
  },
  methods: {
    setTab(tab) {
      this.$emit('setTab', `3,${tab}`)
      this.setModel()
    },
    entityChanged(entity, tab, value) {
      this.inputModel[entity] = value
      this.setTab(tab)
    },
    setModel() {
      const entityModel = {
        schedule: '',
        dateStart: '',
      }
      if (this.tab === 1) {
        entityModel.schedule =
          monthStringFormators.days + this.inputModel.monthDay1
        entityModel.dateStart = `${this.date} ${this.inputModel.duration1}`
      }
      if (this.tab === 2) {
        entityModel.schedule = monthStringFormators.daysMinus
        entityModel.dateStart = `${this.date} ${this.inputModel.duration2}`
      }
      if (this.tab === 3) {
        entityModel.schedule =
          monthStringFormators.daysMinus + weekdays[this.inputModel.weekday3]
        entityModel.dateStart = `${this.date} ${this.inputModel.duration3}`
      }
      if (this.tab === 4) {
        entityModel.schedule =
          monthStringFormators.days +
          this.inputModel.monthDay4 +
          weekdays[this.inputModel.weekday4]
        entityModel.dateStart = `${this.date} ${this.inputModel.duration4}`
      }
      this.$emit('update', entityModel)
    },
    getWidthTitle(id) {
      if (id === 1) {
        return '15%'
      }
      if (id === 2) {
        return '80%'
      }
      if (id === 4) {
        return '6%'
      }
      return '32%'
    },
    getWidthSettings(id) {
      if (id === 1) {
        return '85%'
      }
      if (id === 2) {
        return 'auto'
      }
      if (id === 4) {
        return '94%'
      }
      return '68%'
    },
  },
})
</script>

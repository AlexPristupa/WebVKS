<template>
  <div class="mp-modal--edit-booking-periodic__frequency-settings--weekly">
    <el-radio-group :value="tab">
      <el-radio
        v-for="(item, index) in list"
        class="mp-radio"
        :key="index"
        :label="item.id"
      >
        <p class="title" :style="{ width: getWidthTitle(item.id) }">
          {{ item.name }}
        </p>
        <div
          class="settings-block"
          :style="{ width: getWidthSettings(item.id) }"
        >
          <div v-if="item.id === 1">
            <mp-button
              v-for="(weekday, index) in weekdaysButtons"
              :key="index"
              :title="weekday.name"
              :class="{ included: isButtonIncluded(weekday.name) }"
              @click="setWeekdays(weekday.name)"
            >
              {{ weekday.name }}
            </mp-button>
          </div>

          <mp-date-picker
            v-if="item.id === 2"
            :append="true"
            :value="inputModel.duration"
            :time-format="timeFormats.hhmmss"
            :date-picker-type="datePickerTypes.time"
            @change="entityChanged('duration', 2, $event)"
          />
        </div>
      </el-radio>
    </el-radio-group>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import MpDatePicker from '@/components/basic/MpDatePicker/MpDatePicker.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import {
  list,
  weekdays,
  weekdaysButtons,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.const.ts'
import {
  datePickerTypes,
  timeFormats,
} from '@/components/basic/MpDatePicker/MpDatePicker.const.ts'
import {
  EditPeriodicWeeklyEntity,
  IComputedBookingEditPeriodicWeekly,
  IDataBookingEditPeriodicWeekly,
  IMethodsBookingEditPeriodicWeekly,
  IPropsBookingEditPeriodicWeekly,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.interface'
import { DateTime } from '@/modules/DateTime/DateTime'
import { weekStringFormators } from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.const'

export default Vue.extend<
  IDataBookingEditPeriodicWeekly,
  IMethodsBookingEditPeriodicWeekly,
  IComputedBookingEditPeriodicWeekly,
  IPropsBookingEditPeriodicWeekly
>({
  name: 'EditPeriodicWeekly',
  components: {
    MpButton,
    MpDatePicker,
  },
  props: {
    date: {
      type: String,
      default: new DateTime({
        date: new Date(),
      }).getDateToString(),
    },
    tabs: {
      type: String,
      default: '2',
    },
    initData: {
      type: Object as () => EditPeriodicWeeklyEntity,
      default: new EditPeriodicWeeklyEntity(),
    },
  },
  data() {
    return {
      list,
      weekdaysButtons,
      timeFormats,
      datePickerTypes,
      inputModel: new EditPeriodicWeeklyEntity(),
    }
  },
  computed: {
    tab() {
      return 1
    },
  },
  mounted() {
    this.inputModel = this.initData
  },
  methods: {
    entityChanged(entity, tab, value) {
      this.inputModel[entity] = value
      this.setModel()
    },
    setModel() {
      const entityModel = {
        schedule: '',
        dateStart: '',
      }
      entityModel.schedule =
        weekStringFormators.days + this.inputModel.weekdays.join()
      entityModel.dateStart = `${this.date} ${this.inputModel.duration}`
      this.$emit('update', entityModel)
    },
    setWeekdays(name) {
      let arr = this.inputModel.weekdays
      if (this.isButtonIncluded(name)) {
        if (this.inputModel.weekdays.length > 1) {
          arr = this.inputModel.weekdays.filter(
            weekday => weekday !== weekdays[name.toString()],
          )
        } else {
          this.$message.error(
            this.$t('notifications.error.deleteLastItem') as string,
          )
        }
      } else {
        arr.push(weekdays[name.toString()])
      }
      this.entityChanged('weekdays', 2, arr)
    },
    isButtonIncluded(name) {
      return this.inputModel.weekdays.includes(weekdays[name.toString()])
    },
    getWidthTitle() {
      return '20%'
    },
    getWidthSettings() {
      return '80%'
    },
    setTab() {},
  },
})
</script>

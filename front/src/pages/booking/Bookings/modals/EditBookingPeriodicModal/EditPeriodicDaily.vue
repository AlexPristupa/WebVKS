<template>
  <div class="mp-modal--edit-booking-periodic__frequency-settings--daily">
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
            v-model="inputModel.quantity1"
            :min="1"
            :max="31"
            @change="entityChanged('quantity1', item.id, $event)"
          />
          <el-input-number
            v-if="item.id === 2"
            v-model="inputModel.quantity2"
            :min="1"
            :max="inputModel.frequency2 === 1 ? 24 : 60"
            @change="entityChanged('quantity2', item.id, $event)"
          />
          <span class="el-input-number__text" v-if="item.id === 1">
            {{ $t(`forms.editBooking.texts.day`) }}
          </span>
          <mp-select
            v-if="item.id === 2"
            option-key="name"
            option-value="id"
            option-label="name"
            :option-list="frequencyList"
            :value="inputModel.frequency2"
            :popper-append-to-body="false"
            @change="entityChanged('frequency2', item.id, $event)"
          />
          <mp-date-picker
            v-if="item.id === 1"
            :append="true"
            :value="inputModel.duration1"
            :time-format="timeFormats.hhmmss"
            :date-picker-type="datePickerTypes.time"
            @change="entityChanged('duration1', item.id, $event)"
          />
          <mp-date-picker
            v-if="item.id === 3"
            :append="true"
            :time-format="timeFormats.hhmmss"
            :value="inputModel.duration3"
            :date-picker-type="datePickerTypes.time"
            @change="entityChanged('duration3', item.id, $event)"
          />
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
  EditPeriodicDailyEntity,
  IComputedBookingEditPeriodicDaily,
  IDataBookingEditPeriodicDaily,
  IMethodsBookingEditPeriodicDaily,
  IPropsBookingEditPeriodicDaily,
} from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicDaily.interface'
import {
  frequencyList,
  list,
  dayStringFormators,
} from './config/editPeriodicDaily.const'
import {
  datePickerTypes,
  timeFormats,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { DateTime } from '@/modules/DateTime/DateTime'

export default Vue.extend<
  IDataBookingEditPeriodicDaily,
  IMethodsBookingEditPeriodicDaily,
  IComputedBookingEditPeriodicDaily,
  IPropsBookingEditPeriodicDaily
>({
  name: 'EditPeriodicDaily',
  components: {
    MpDatePicker,
    MpSelect,
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
      default: '1,1',
    },
    initData: {
      type: Object as () => EditPeriodicDailyEntity,
      default: new EditPeriodicDailyEntity(),
    },
  },
  data() {
    return {
      list,
      timeFormats,
      frequencyList,
      datePickerTypes,
      inputModel: new EditPeriodicDailyEntity(),
    }
  },
  mounted() {
    this.inputModel = this.initData
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
  methods: {
    setTab(tab) {
      this.$emit('setTab', `1,${tab}`)
      this.setModel()
    },
    entityChanged(entity, tab, value) {
      if (
        entity === 'frequency2' &&
        value === 1 &&
        this.inputModel.quantity2 > 24
      ) {
        this.inputModel.quantity2 = 24
      }
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
          this.inputModel.quantity1 + dayStringFormators.day
        entityModel.dateStart = `${this.date} ${this.inputModel.duration1}`
      }
      if (this.tab === 2) {
        if (this.inputModel.frequency2 === 1) {
          entityModel.schedule =
            this.inputModel.quantity2 + dayStringFormators.hour
        } else {
          entityModel.schedule =
            this.inputModel.quantity2 + dayStringFormators.minute
        }
      }
      if (this.tab === 3) {
        entityModel.schedule = dayStringFormators.days
        entityModel.dateStart = `${this.date} ${this.inputModel.duration3}`
      }
      this.$emit('update', entityModel)
    },
    getWidthTitle(id) {
      if (id === 3) {
        return 'calc(100% - 150px)'
      }
      return '15%'
    },
    getWidthSettings(id) {
      if (id === 3) {
        return '150px'
      }
      return '85%'
    },
  },
})
</script>

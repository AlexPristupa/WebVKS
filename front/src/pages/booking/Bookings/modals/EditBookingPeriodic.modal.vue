<template>
  <el-dialog
    v-el-drag-dialog
    width="50%"
    class="mp-modal mp-modal--edit-booking-periodic"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
  >
    <template #default>
      <el-form
        ref="editBookingPeriodic"
        :label-position="'top'"
        class="mp-form"
      >
        <el-form-item
          prop="frequency"
          class="mp-modal--edit-booking-periodic__frequency"
        >
          <el-radio-group :value="tab">
            <el-radio
              v-for="(frequency, index) in frequencyList"
              :key="index"
              :label="frequency.id"
              class="mp-radio"
              @change="setTab(frequency.id)"
            >
              <span>
                {{ frequency.name }}
              </span>
            </el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item
          prop="weekdays"
          class="mp-modal--edit-booking-periodic__frequency-settings"
        >
          <edit-periodic-daily
            v-if="tab === 1"
            :date="date"
            :tabs="selectedEntity.tabs"
            :initData="initData"
            @setTab="$emit('updateTab', $event)"
            @update="update"
          />
          <edit-periodic-weekly
            v-else-if="tab === 2"
            :date="date"
            :tabs="selectedEntity.tabs"
            :initData="initData"
            @setTab="$emit('updateTab', $event)"
            @update="update"
          />
          <edit-periodic-monthly
            v-else
            :date="date"
            :tabs="selectedEntity.tabs"
            :initData="initData"
            @setTab="$emit('updateTab', $event)"
            @update="update"
          />
        </el-form-item>
      </el-form>
    </template>
    <template v-slot:footer>
      <mp-button
        type="primary"
        mp-status="normal"
        mp-type="save"
        :disabled="disabledSaveButton"
        @click="submit"
      >
        {{ $t('button.title.save') }}
      </mp-button>

      <mp-button type="" mp-status="normal" mp-type="cancel" @click="close">
        {{ $t('button.title.cancel') }}
      </mp-button>
    </template>
  </el-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import {
  BookingPeriodicEntity,
  IComputedBookingPeriodicModal,
  IDataBookingPeriodicModal,
  IMethodsBookingPeriodicModal,
  IPropsBookingPeriodicModal,
} from '@/pages/booking/Bookings/modals/config/editBookingPeriodicModal.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import EditPeriodicDaily from './EditBookingPeriodicModal/EditPeriodicDaily.vue'
import EditPeriodicWeekly from './EditBookingPeriodicModal/EditPeriodicWeekly.vue'
import EditPeriodicMonthly from './EditBookingPeriodicModal/EditPeriodicMonthly.vue'
import { DateTime } from '@/modules/DateTime/DateTime'
import { EditPeriodicDailyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicDaily.interface'
import { EditPeriodicWeeklyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.interface'
import { EditPeriodicMonthlyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.interface'
import { dayStringFormators } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicDaily.const'

export default Vue.extend<
  IDataBookingPeriodicModal,
  IMethodsBookingPeriodicModal,
  IComputedBookingPeriodicModal,
  IPropsBookingPeriodicModal
>({
  name: 'EditPeriodicModal',
  components: {
    MpButton,
    EditPeriodicDaily,
    EditPeriodicWeekly,
    EditPeriodicMonthly,
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Object as () => BookingPeriodicEntity,
      default: () => new BookingPeriodicEntity(),
    },
  },
  directives: {
    elDragDialog,
  },
  data() {
    return {
      initData: new EditPeriodicDailyEntity(),
      entityModel: new BookingPeriodicEntity(),
      savingLoading: false,
      nonCopiedValues,
    }
  },
  computed: {
    title() {
      return this.$t('dialogs.titles.bookingPeriodic')
    },
    disabledSaveButton() {
      return this.savingLoading
    },
    frequencyList() {
      return [
        { id: 1, name: this.placeholderToUpper('daily') },
        { id: 2, name: this.placeholderToUpper('weekly') },
        { id: 3, name: this.placeholderToUpper('monthly') },
      ]
    },
    date() {
      if (this.selectedEntity.dateStart) {
        return new DateTime({
          dateTime: this.selectedEntity.dateStart,
        }).getDateToString()
      }
      return new DateTime({
        date: new Date(),
      }).getDateToString()
    },
    time() {
      return new DateTime({
        dateTime: this.selectedEntity.dateStart,
      }).getTimeToString()
    },
    tab() {
      return isNaN(Number(this.selectedEntity.tabs[0]))
        ? 1
        : Number(this.selectedEntity.tabs[0])
    },
    dependentTab() {
      return isNaN(Number(this.selectedEntity.tabs.substring(2))) ||
        Number(this.selectedEntity.tabs.substring(2)) === 0
        ? 1
        : Number(this.selectedEntity.tabs.substring(2))
    },
  },
  watch: {
    tab() {
      this.setDefaults(this.selectedEntity.schedule)
    },
  },
  mounted() {
    this.entityModel = new BookingPeriodicEntity(this.selectedEntity)
    this.setDefaults(this.selectedEntity.schedule)
  },
  methods: {
    setTab(tabNumber) {
      if (tabNumber !== 2) {
        let dependent = 1
        const initTab = Number(this.entityModel?.tabs[0])
        const initSubTab = Number(this.entityModel?.tabs?.substring(2))
        if (this.entityModel.id && tabNumber === initTab) {
          dependent = initSubTab
        }
        this.$emit('updateTab', `${tabNumber},${dependent}`)
      } else {
        this.$emit('updateSchedule', this.entityModel.schedule)
        this.$emit('updateTab', `${tabNumber}`)
      }
    },
    setDefaults(schedule) {
      if (this.tab === 1) {
        this.initData = new EditPeriodicDailyEntity({
          duration1: this.time,
          duration3: this.time,
        })
        if (schedule) {
          const isQuantityTwoFigures = isNaN(Number(schedule.substring(0, 2)))
          const quantityOneFigure = isNaN(Number(schedule.substring(0, 1)))
            ? 1
            : Number(schedule.substring(0, 1))
          const quantityTwoFigures = Number(schedule.substring(0, 2))
          if (this.dependentTab === 1) {
            this.initData.quantity1 = isQuantityTwoFigures
              ? quantityOneFigure
              : quantityTwoFigures
          }
          if (this.dependentTab === 2) {
            this.initData.frequency2 = schedule.includes(
              dayStringFormators.hour,
            )
              ? 1
              : 2
            this.initData.quantity2 = isQuantityTwoFigures
              ? quantityOneFigure
              : quantityTwoFigures
            return
          }
        }
      }
      if (this.tab === 2) {
        this.initData = new EditPeriodicWeeklyEntity({
          duration: this.time,
        })
        if (schedule) {
          let weekdaysString = ''
          let weekdays
          const isUpperCase = string => /^[A-Z]*$/.test(string)
          const scheduleArray = Array.from(schedule)
          const indexOfFirstUpper = scheduleArray.findIndex(letter =>
            isUpperCase(letter),
          )
          if (
            indexOfFirstUpper !== -1 &&
            !schedule.includes('-1') &&
            isUpperCase(scheduleArray[5])
          ) {
            weekdaysString = schedule.substring(indexOfFirstUpper)
            weekdays = weekdaysString.split(',') as Array<string>
          }
          if (weekdays?.length) {
            this.initData.weekdays = weekdays
          }
        }
      }
      if (this.tab === 3) {
        this.initData = new EditPeriodicMonthlyEntity({
          duration1: this.time,
          duration2: this.time,
          duration3: this.time,
          duration4: this.time,
        })
        if (schedule) {
          const isQuantityTwoFigures = isNaN(Number(schedule.substring(5, 7)))
          const quantityOneFigure = isNaN(Number(schedule.substring(5, 6)))
            ? 1
            : Number(schedule.substring(5, 6))
          const quantityTwoFigures = Number(schedule.substring(5, 7))
          const monthDay = isQuantityTwoFigures
            ? quantityOneFigure
            : quantityTwoFigures
          if (this.dependentTab === 1) {
            this.initData.monthDay1 = monthDay
            return
          }
          if (this.dependentTab === 2) {
            return
          }
          if (this.dependentTab === 3) {
            const weekday = schedule.substring(7).toLocaleLowerCase()
            this.initData.weekday3 = this.$t(
              `button.weekdays.${weekday}`,
            ).toString()
            return
          }
          if (this.dependentTab === 4) {
            const weekday = schedule.substring(6).toLocaleLowerCase()
            this.initData.monthDay4 = monthDay
            this.initData.weekday4 = this.$t(
              `button.weekdays.${weekday}`,
            ).toString()
            return
          }
        }
      }
    },
    update(value) {
      this.$emit('updateSchedule', value.schedule)
      this.$emit(
        'updateDateStart',
        value.dateStart || this.entityModel.dateStart,
      )
    },
    submit() {
      this.$emit('updateText')
      this.$emit('close')
    },
    close() {
      this.resetSchedule()
      this.$emit('close')
    },
    resetSchedule() {
      this.$emit('updateTab', this.entityModel.tabs)
      this.$emit('updateSchedule', this.entityModel.schedule)
      this.$emit('updateDateStart', this.entityModel.dateStart)
      this.$emit('updateText', this.entityModel.text)
    },
    placeholderToUpper(key) {
      const string = this.$t(`forms.editBooking.texts.${key}`).toString()
      return FirstLetterToCase.firstToUpper(string)
    },
  },
})
</script>

<template>
  <div v-if="fieldsSetting.display && this.initialDate && value">
    {{ value }} {{ text }}
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpCellTimer,
  IDataMpCellTimer,
  IMethodsMpCellTimer,
  IPropsMpCellTimer,
} from '@/components/MpTable/cell/config/MpCellTimer.interface'
import { ICellRendererParams } from '@ag-grid-community/core'
import { DateTime } from '@/modules/DateTime/DateTime'
import { TimeConverting } from '@/modules/TimeConverting/TimeConverting'
import { InitialTime } from '@/modules/DateTime/InitialTime.const'
declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellTimer,
  IMethodsMpCellTimer,
  IComputedMpCellTimer,
  IPropsMpCellTimer
>({
  name: 'MpCellTimer',
  computed: {
    fieldData() {
      return this.params.data
    },
    fieldsSetting() {
      return this.fieldData.fieldsSetting.counter
    },
    initialDate() {
      const fieldData = this.fieldData[this.fieldsSetting.fieldTo]
      return fieldData
        ? new DateTime({ dateTime: fieldData }).toCurrentTimeZone()
        : ''
    },
    isPreviousDate() {
      const today = new DateTime({ date: new Date() })
      const initial = new DateTime({
        date: this.initialDate as Date,
      })
      return (
        initial.getJsDate().getTime() < today.getJsDate().getTime() &&
        today.getDateAndTimeToString() !== initial.getDateAndTimeToString()
      )
    },
  },
  data() {
    return {
      interval: undefined,
      value: '',
      text: '',
    }
  },
  mounted() {
    this.text = this.$t(
      // @ts-ignore
      `callTable.MpCellTimer.${this.params.tableName}.${this.fieldsSetting.fieldTo}`,
    )
    if (this.fieldsSetting.display && this.initialDate) {
      this.interval = setInterval(() => {
        this.updateTimer()
      }, 1000)
    }
  },
  beforeDestroy() {
    clearInterval(this.interval)
  },
  methods: {
    updateTimer() {
      const now = new Date().getTime()
      const counterTime = this.initialDate
        ? (this.initialDate as Date).getTime()
        : 0
      const timeLeft = counterTime - now
      if (Math.sign(timeLeft) === -1 || Math.sign(timeLeft) === 0) {
        this.value = InitialTime.first
        clearInterval(this.interval)
        if (!this.isPreviousDate) {
          // событие, которое уходит в таблицу
          this.$parent.$emit('on-timer-finished')
        }
      } else {
        this.value = new TimeConverting().fromSeconds(timeLeft / 1000)
        return
      }
    },
  },
})
</script>

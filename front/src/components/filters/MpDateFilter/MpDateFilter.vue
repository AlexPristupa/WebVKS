<template>
  <div class="column-filter__date">
    <date-filter-selects
      :initialDate="firstSelectsState"
      :dateFormat="dateFormat"
      :disabled="disabled"
      @operandChanged="operandChanged(firstSelectsState, ...arguments)"
      @dateChanged="dateChanged(firstSelectsState, ...arguments)"
      @timeChanged="timeChanged(firstSelectsState, ...arguments)"
      @clearDate="clearFirstDate"
      @clearTime="clearFirstTime"
      @clear="setFirstInitialState"
    />
    <date-filter-selects
      :initialDate="secondSelectsState"
      :dateFormat="dateFormat"
      :disabled="disabled"
      @operandChanged="operandChanged(secondSelectsState, ...arguments)"
      @dateChanged="dateChanged(secondSelectsState, ...arguments)"
      @timeChanged="timeChanged(secondSelectsState, ...arguments)"
      @clearDate="clearSecondDate"
      @clearTime="clearSecondTime"
      @clear="setSecondInitialState"
    />
  </div>
</template>

<script lang="ts">
/**
 * @description тело общего фильтра типа 'дата'
 */
import Vue from 'vue'
import DateFilterSelects from './FilterSelects/DateFilterSelects.vue'
import {
  IInitialDateFilterState,
  IDataDateFilter,
} from './DateFilter.interface'
import CONSTANTS from '@/constants'
import { dateMasks } from '@/constants/dateMasks'
import { FilterType } from '@/modules/Filters/Filters.const'
import { parseTime } from '@/utils'
import { mapGetters } from 'vuex'
import {
  valuesFieldDateFilterFirst,
  valuesFieldDateFilterSecond,
} from '@/modules/Filters/DateFilter/DateFilter.interface'
import { InitialTime } from '@/modules/DateTime/InitialTime.const'
import { DateOrder } from '@/modules/DateTime/DateOrder.const'
import { operandValue } from '@/modules/Filters/Operands/Operands.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { DateTime } from '@/modules/DateTime/DateTime'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export default Vue.extend({
  name: 'MpDateFilter',
  components: {
    DateFilterSelects,
  },
  props: {
    tableName: {
      type: String as () => string,
      default: '',
    },
    columnName: {
      type: String as () => string,
      default: '',
    },
    belongsFilteringPanel: {
      type: Boolean as () => boolean,
      default: false,
    },
    filterValueList: {
      type: Array as () => Array<
        valuesFieldDateFilterFirst | valuesFieldDateFilterSecond
      >,
      default: () => [],
    },
  },
  data(): IDataDateFilter {
    return {
      filterBody: {
        tableName: this.tableName,
        nameField: this.columnName,
        filterType: FilterType.date,
        valuesField: [],
      },
      firstSelectsState: {
        operand: operandValue.moreOrEqual,
        date: '',
        time: '',
      },
      secondSelectsState: {
        operand: operandValue.smallerOrEqual,
        date: '',
        time: '',
      },
      notSpecifiedValue: [
        {
          selectFirstContains: operandValue.equal,
          valueFirstDateFilter: null,
          timerFirstFilter: null,
        },
        {
          selectSecondContains: operandValue.equal,
          valueSecondDateFilter: null,
          timeSecondFilter: null,
        },
      ],
    }
  },
  computed: {
    ...mapGetters({
      findActiveFilters: 'tableHeaderEffects/getTableHeadersEffects',
    }),
    dateFormat(): string {
      const lang = this.$i18n.locale
      return dateMasks.withoutTime[lang] as string
    },
    dateOrder(): string {
      return this.dateFormat === CONSTANTS.dateMasks.withoutTime.ru
        ? DateOrder.ru
        : DateOrder.en
    },
    secondDate(): number {
      const date = new Date()
      return date.setDate(date.getDate() + 1)
    },
    disabled(): boolean {
      return !(
        this.firstSelectsState.operand && this.secondSelectsState.operand
      )
    },
  },
  watch: {
    filterBody: {
      handler: function(val) {
        this.$emit('state-date-filter', val)
      },
      deep: true,
    },
    firstSelectsState: {
      handler: function(val) {
        if (val.operand) {
          this.saveFirstFilterForApply()
        }
      },
      deep: true,
    },
    secondSelectsState: {
      handler: function(val) {
        if (val.operand) {
          this.saveSecondFilterForApply()
        }
      },
      deep: true,
    },
  },
  mounted() {
    let firstFilterValueFromStore, secondFilterValueFromStore
    const tableFiltersFromStore = this.findActiveFilters(this.tableName)
      ?.filters
    if (tableFiltersFromStore && tableFiltersFromStore.length) {
      const columnFilter = tableFiltersFromStore.find(
        (filter: IFilterState) => filter.nameField === this.columnName,
      )
      if (columnFilter) {
        firstFilterValueFromStore = columnFilter.valuesField.find(
          (value: valuesFieldDateFilterFirst) => value.selectFirstContains,
        )
        secondFilterValueFromStore = columnFilter.valuesField.find(
          (value: valuesFieldDateFilterSecond) => value.selectSecondContains,
        )
      }
    }
    this.setFirstSelectsData(firstFilterValueFromStore ?? null)
    this.setSecondSelectsData(secondFilterValueFromStore ?? null)

    if (this.filterValueList.length) {
      this.firstSelectsState = this.mapStateFirstSelects(
        this.filterValueList[0],
      )
      this.secondSelectsState = this.mapStateSecondSelects(
        this.filterValueList[1],
      )
    }
  },
  methods: {
    setFirstSelectsData(filterValue: valuesFieldDateFilterFirst | null): void {
      // проверка на то, что фильтр в состоянии 'не указано'
      const isFilterFromStoreNotSpecified =
        JSON.stringify(filterValue) ===
        JSON.stringify(this.notSpecifiedValue[0])
      if (!filterValue && !isFilterFromStoreNotSpecified) {
        // если фильтр не передан, и нет специального состояния, то возвразащем поля к начальному состоянию
        this.setFirstInitialState()
      } else if (filterValue && !isFilterFromStoreNotSpecified) {
        //  если фильтр передан, и нет специального состояния, то устанавливаем поля фильтра из стора
        this.setFirstStateFromStore(filterValue)
      } else {
        //  если есть специально состояние, то устанавливаем операнды в 'не указано', а дату и время обнуляем
        this.setNotSpecified()
        this.clearFirstDate()
        this.clearFirstTime()
      }
    },
    setFirstInitialState() {
      this.firstSelectsState.operand = operandValue.moreOrEqual
      this.clearFirstDate()
      this.clearFirstTime()
    },
    setFirstStateFromStore(filterValue: valuesFieldDateFilterFirst) {
      this.firstSelectsState = {
        operand: filterValue.selectFirstContains,
        date: filterValue.valueFirstDateFilter,
        time: filterValue.timerFirstFilter,
      }
    },
    mapStateFirstSelects(value): IInitialDateFilterState {
      const timeZoneValues = this.getCurrentTimeZoneValues(
        value.valueFirstDateFilter,
        value.timerFirstFilter,
      )
      return {
        operand: value.selectFirstContains || operandValue.moreOrEqual,
        date:
          timeZoneValues.date ||
          new DateTime({ date: new Date() }).getDateToString(),
        time: timeZoneValues.time || InitialTime.first,
      }
    },
    mapStateSecondSelects(value): IInitialDateFilterState {
      const timeZoneValues = this.getCurrentTimeZoneValues(
        value.valueSecondDateFilter,
        value.timeSecondFilter,
      )
      return {
        operand: value.selectSecondContains || operandValue.smallerOrEqual,
        date:
          timeZoneValues.date ||
          new DateTime({ date: new Date() }).getDateToString(),
        time: timeZoneValues.time || InitialTime.second,
      }
    },
    setSecondSelectsData(
      filterValue: valuesFieldDateFilterSecond | null,
    ): void {
      const isFilterFromStoreNotSpecified =
        JSON.stringify(filterValue) ===
        JSON.stringify(this.notSpecifiedValue[1])
      if (!filterValue && !isFilterFromStoreNotSpecified) {
        this.setSecondInitialState()
      } else if (filterValue && !isFilterFromStoreNotSpecified) {
        this.setSecondStateFromStore(filterValue)
      } else {
        this.setNotSpecified()
        this.clearSecondDate()
        this.clearSecondTime()
      }
    },
    setSecondInitialState() {
      this.secondSelectsState.operand = operandValue.smallerOrEqual
      this.clearSecondDate()
      this.clearSecondTime()
    },
    setSecondStateFromStore(filterValue: valuesFieldDateFilterSecond) {
      this.secondSelectsState = {
        operand: filterValue.selectSecondContains,
        date: filterValue.valueSecondDateFilter,
        time: filterValue.timeSecondFilter,
      }
    },
    operandChanged(
      option: IInitialDateFilterState,
      ...arg: Array<operandValue | null>
    ) {
      option.operand = arg[0]
      if (!option.operand) {
        this.setNotSpecified()
      }
    },
    setNotSpecified(): void {
      this.firstSelectsState.operand = null
      this.secondSelectsState.operand = null
      this.filterBody.valuesField = this.notSpecifiedValue
    },
    dateChanged(option: IInitialDateFilterState, ...arg: Array<string | null>) {
      option.date = arg[0]
    },
    timeChanged(option: IInitialDateFilterState, ...arg: Array<string>) {
      option.time = arg[0]
    },
    clearFirstTime() {
      this.firstSelectsState.time = InitialTime.first
    },
    clearSecondTime() {
      this.secondSelectsState.time = InitialTime.second
    },
    clearFirstDate() {
      this.firstSelectsState.date = parseTime(new Date(), this.dateOrder)
    },
    clearSecondDate() {
      this.secondSelectsState.date = parseTime(this.secondDate, this.dateOrder)
    },
    saveFirstFilterForApply(): void {
      const timeZoneValues = this.getGlobalTimeZoneValues(
        this.firstSelectsState.date,
        this.firstSelectsState.time,
      )
      this.filterBody.valuesField = this.filterBody.valuesField.filter(
        (value: valuesFieldDateFilterFirst | valuesFieldDateFilterSecond) =>
          !(value as valuesFieldDateFilterFirst).selectFirstContains,
      )
      this.filterBody.valuesField.unshift({
        selectFirstContains: this.firstSelectsState.operand,
        valueFirstDateFilter: timeZoneValues.date,
        timerFirstFilter: timeZoneValues.time,
      })
    },
    saveSecondFilterForApply(): void {
      const timeZoneValues = this.getGlobalTimeZoneValues(
        this.secondSelectsState.date,
        this.secondSelectsState.time,
      )
      this.filterBody.valuesField = this.filterBody.valuesField.filter(
        (value: valuesFieldDateFilterSecond | valuesFieldDateFilterFirst) =>
          !(value as valuesFieldDateFilterSecond).selectSecondContains,
      )
      this.filterBody.valuesField.push({
        selectSecondContains: this.secondSelectsState.operand,
        valueSecondDateFilter: timeZoneValues.date,
        timeSecondFilter: timeZoneValues.time,
      })
    },
    getGlobalTimeZoneValues(date, time): { date: string; time: string } {
      return {
        date: date
          ? this.getDateTime(date, time).toGlobalTime(datePickerType.date)
          : '',
        time: time
          ? this.getDateTime(date, time).toGlobalTime(datePickerType.time)
          : '',
      }
    },
    getCurrentTimeZoneValues(date, time): { date: string; time: string } {
      return {
        date: date
          ? (this.getDateTime(date, time).toCurrentTimeZone(
              datePickerType.date,
            ) as string)
          : '',
        time: time
          ? (this.getDateTime(date, time).toCurrentTimeZone(
              datePickerType.time,
            ) as string)
          : '',
      }
    },
    getDateTime(date, time): DateTime {
      return new DateTime({
        dateTime: `${date} ${time}`,
      })
    },
  },
})
</script>

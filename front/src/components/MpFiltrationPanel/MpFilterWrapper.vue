<template>
  <div class="filtration-panel-filter-wrapper">
    <div class="filtration-panel-filter-wrapper__controls">
      <span class="filtration-panel-filter-wrapper__title">
        {{ filter.filterTitle }}
      </span>
      <mp-button mp-type="clear" mp-status="normal" is-icon @click="clear" />
      <mp-filter-parameters-wrapper
        v-if="showParameters(filter)"
        :filter-type="filter.filterType"
        :filter-title="filter.filterTitle"
        :table-name="filter.tableName"
        :column-name="filter.columnName"
        @apply-filter="applyFilter"
      />
      <mp-button
        is-icon
        mp-type="remove"
        mp-status="normal"
        @click="cancel(filter)"
      />
    </div>
    <div class="filtration-panel-filter-wrapper__body">
      <mp-string-filter
        ref="stringFilter"
        :table-name="filter.tableName"
        :column-name="filter.columnName"
        :filter-value-list="filterValueList"
        v-if="showFilter(filter, enumFilterType.string)"
        @state-string-filter="changeFilter"
      />
      <mp-select-filter
        ref="selectFilter"
        v-if="showFilter(filter, enumFilterType.select)"
        :table-name="filter.tableName"
        :column-name="filter.columnName"
        :main-table-name="mainTableName"
        :belongs-filtering-panel="true"
        :filter-value-list="filterValueList"
        @state-select-filter="changeFilter"
        :filter="filter"
      />
      <mp-date-filter
        ref="dateFilter"
        v-if="showFilter(filter, enumFilterType.date)"
        :table-name="filter.tableName"
        :column-name="filter.columnName"
        :filter-value-list="filterValueList"
        @state-date-filter="changeFilter"
      />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpFilterParametersWrapper from '@/components/FilterParameters/MpFilterParametersWrapper.vue'
import {
  IComponentFilter,
  IFilterState,
  IValuesFieldFilter,
} from '@/modules/Filters/Filters.interface'
import MpSelectFilter from '@/components/filters/MpSelectFilter/MpSelectFilter.vue'
import MpStringFilter from '@/components/filters/MpStringFilter/MpStringFilter.vue'
import { FilterType } from '@/modules/Filters/Filters.const'
import { IFiltrationPanelItem } from '@/modules/FiltrationPanel/FiltrationPanel.interface'
import { ISelectFilterState } from '@/modules/Filters/SelectFilter/SelectFilter.interface'
import { IStringFilterState } from '@/modules/Filters/StringFilter/StringFilter.interface'
import MpDateFilter from '@/components/filters/MpDateFilter/MpDateFilter.vue'
import { mapActions } from 'vuex'

export default Vue.extend({
  name: 'MpFilterWrapper',
  components: {
    MpDateFilter,
    MpStringFilter,
    MpSelectFilter,
    MpFilterParametersWrapper,
    MpButton,
  },
  props: {
    filter: {
      type: Object as () => IFiltrationPanelItem,
      default: (): IFiltrationPanelItem => {
        return {
          id: 0,
          filterTitle: '',
          columnName: '',
          profileId: 0,
          profileName: '',
          isCommon: true,
          filterType: FilterType.select,
          privateName: '',
          tableName: '',
          isMainTable: false,
        }
      },
    },
    mainTableName: {
      type: String,
      default: '',
    },
    filterValueList: {
      type: Array as () => IValuesFieldFilter,
    },
  },
  data() {
    return {
      enumFilterType: FilterType,
    }
  },
  methods: {
    ...mapActions({
      clearActiveFiltersForTable: 'tableHeaderEffects/clearTableHeaderEffects',
    }),
    changeFilter(filterState: IFilterState) {
      this.$emit(
        'change-filter',
        this.filter.id,
        this.filter.isMainTable,
        filterState,
      )
    },
    showFilter(filter: IFiltrationPanelItem, filterType: FilterType): boolean {
      return filter.filterType === filterType
    },
    showParameters(filter: IFiltrationPanelItem): boolean {
      return (
        filter.filterType === FilterType.string ||
        filter.filterType === FilterType.select
      )
    },
    applyFilter(filterState: ISelectFilterState | IStringFilterState) {
      const newState = {
        ...filterState,
        nameField: this.filter.isMainTable
          ? this.filter.columnName
          : this.filter.privateName,
        tableName: this.mainTableName,
      }
      this.$emit(
        'change-filter',
        this.filter.id,
        this.filter.isMainTable,
        newState,
        true,
      )
    },
    clear(): void {
      if (this.filter.filterType === FilterType.string) {
        const stringFilter = this.$refs.stringFilter as IComponentFilter
        stringFilter.clearState()
      }
      if (this.filter.filterType === FilterType.select) {
        const selectFilter = this.$refs.selectFilter as IComponentFilter
        selectFilter.clearState()
      }
      this.$store.dispatch(
        'tableHeaderEffects/clearTableHeaderEffects',
        this.mainTableName,
      )
    },
    cancel(filter: IFiltrationPanelItem): void {
      this.$emit('remove-filter', filter.id)
    },
  },
})
</script>

<template>
  <div class="column-filter">
    <div class="column-filter__header">
      {{ $t('title') }}
    </div>
    <div class="column-filter__body">
      <mp-string-filter
        v-if="params.column.colDef.filterParams === filterParams.string"
        ref="stringFilter"
        :table-name="tableName"
        :column-name="columnName"
        :filter-value-list="filterValueList"
        @state-string-filter="stateFilter"
      />
      <mp-select-filter
        v-if="params.column.colDef.filterParams === filterParams.select"
        ref="selectFilter"
        :table-name="tableName"
        :column-name="columnName"
        :filter-value-list="filterValueList"
        @state-select-filter="stateFilter"
      />
      <mp-date-filter
        v-if="params.column.colDef.filterParams === filterParams.date"
        ref="dateFilter"
        :table-name="tableName"
        :column-name="columnName"
        :filter-value-list="filterValueList"
        @state-date-filter="stateFilter"
      />
    </div>
    <div class="column-filter__actions">
      <mp-button
        type="primary"
        :mp-type="MpTypeButton.apply"
        size="mini"
        @click="apply"
      >
        {{ $t('button.label.apply') }}
      </mp-button>
      <mp-button
        size="mini"
        :mp-type="MpTypeButton.cancel"
        @click="hidePopupFilter"
      >
        {{ $t('button.label.cancel') }}
      </mp-button>
      <mp-button
        type="danger"
        :mp-type="MpTypeButton.clear"
        size="mini"
        @click="clear"
      >
        {{ $t('button.label.clear') }}
      </mp-button>
    </div>
  </div>
</template>

<script>
import Vue from 'vue'
import MpStringFilter from '../../filters/MpStringFilter/MpStringFilter'
import MpSelectFilter from '@/components/filters/MpSelectFilter/MpSelectFilter'
import MpDateFilter from '../../filters/MpDateFilter/MpDateFilter'
import MpButton from '@/components/basic/MpButton/MpButton'
import { MpTableFiltersLang } from '../../filters/MpTableFilters.lang'
import { FilterType } from '@/modules/Filters/Filters.const'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { mapGetters } from 'vuex'

export default Vue.extend({
  name: 'MpWrapperTableFilter',
  components: {
    MpButton,
    MpSelectFilter,
    MpStringFilter,
    MpDateFilter,
  },
  props: {
    filterType: {
      type: String,
      validator: value => {
        return ['string', 'select'].includes(value)
      },
    },
    params: {
      type: Object,
      default: () => {
        return {}
      },
    },
  },
  data() {
    return {
      filter: {},
      filterParams: FilterType,
      MpTypeButton: MpTypeButton,
    }
  },
  computed: {
    ...mapGetters({
      findActiveColumnsEffects: 'tableHeaderEffects/getTableHeadersEffects',
    }),
    tableName() {
      return this.params.api.tableName
    },
    columnName() {
      return this.params.column.colDef.field
    },
    filterValueList() {
      const tableFilters = this.findActiveColumnsEffects(this.tableName)
        ?.filters
      if (tableFilters?.length) {
        const columnFilter = tableFilters.find(
          filter => filter.nameField === this.columnName,
        )
        return columnFilter?.valuesField || []
      }
      return []
    },
  },
  methods: {
    // сохранение в компоненте, для дальнейшей передачи через apply
    stateFilter(data) {
      this.filter = data
    },
    apply() {
      this.$emit('filter-changed', this.filter)
      this.hidePopupFilter()
    },
    clear() {
      this.$emit('filter-changed', {
        nameField: this.columnName,
        valuesField: [],
      })
      this.hidePopupFilter()
    },
    hidePopupFilter() {
      this.$emit('hide-popup-filter')
    },
  },
  i18n: {
    messages: MpTableFiltersLang,
  },
})
</script>

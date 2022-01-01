<template>
  <div>
    <div class="mp-table__blur" v-if="disabled">
      <div class="mp-table__blur-substrate"></div>
    </div>
    <ag-grid-vue
      :style="{ width: '100%', height: height }"
      class="ag-theme-balham mp-table"
      :column-defs="columnDefs"
      :row-data="rowData"
      :framework-components="mpTableComponents"
      :grid-options="tableOptions"
      rowSelection="single"
      :tooltip-show-delay="1000"
      :suppressRowClickSelection="disabled"
      @action-button-click="actionButtonClick"
      @item-open="itemOpen"
      @on-filter-changed="filterChanged"
      @one-of-checkboxes-changed="oneOfCheckboxesChanged"
      @on-select-changed="selectChanged"
      @grid-ready="onGridReady"
      @sort-by="sortBy"
      @structure="handlerStructure"
      @check-row="handlerCheckRow"
      @check-all-row="checkAllRow"
      @on-columns-settings-changed="columnsSettingsChanged"
      @edit-value="editValue"
      @follow-link="followLink"
      @on-timer-finished="timerFinished"
    />
  </div>
</template>

<script>
import Vue from 'vue'
import { AgGridVue } from 'ag-grid-vue'

import { mpTableOptions } from './mpTableOptions'
import { mpTableLang } from './mpTable.lang'
import MpCellActionButton from './cell/MpCellActionButton'
import MpCellMatching from './cell/MpCellMatching'
import MpCellStatus from './cell/MpCellStatus'
import MpCellHeader from './header/mpCellHeader'
import MpCellOpen from './cell/MpCellOpen'
import MpCellInOut from './cell/MpCellInOut'
import MpCellPhoneMask from './cell/MpCellPhoneMask'
import MpCellDateTime from './cell/MpCellDateTime'
import MpCellStructure from './cell/MpCellStructure'
import MpCellGroup from './cell/MpCellGroup'
import MpCellCheckbox from './cell/MpCellCheckbox'
import MpCellContextMenu from './cell/MpCellContextMenu'
import MpCellBoolean from './cell/MpCellBoolean'
import MpCellEditValue from './cell/MpCellEditValue'
import { mapActions } from 'vuex'
import { setFilter } from '@/modules/Filters/SetTableFilters'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import MpCellTranslate from '@/components/MpTable/cell/MpCellTranslate'
import MpCellCheckboxes from '@/components/MpTable/cell/MpCellCheckboxes'
import MpCellSelect from '@/components/MpTable/cell/MpCellSelect'
import MpCellTimer from '@/components/MpTable/cell/MpCellTimer'
import CustomTooltip from '@/components/MpTable/customTooltip/index.vue'

export default Vue.extend({
  name: 'MpTable',
  components: {
    AgGridVue,
  },
  props: {
    tableName: {
      type: String,
      default: '',
    },
    listQuery: {
      type: Object,
      default: () => DtoFactory.create(DtoName.table),
    },
    columnDefs: {
      type: Array,
      default: () => [],
    },
    rowData: {
      type: Array || null,
      default: () => null,
    },
    height: {
      type: String,
      default: '',
    },
    loading: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  data: () => {
    return {
      gridApi: null,
      headerHeight: 35,
      rowHeight: 28,
      selectedRowId: null,
      structure: {},
    }
  },
  computed: {
    mpTableComponents() {
      return {
        agColumnHeader: MpCellHeader,
        CustomTooltip: CustomTooltip,
        MpCellActionButton: MpCellActionButton,
        MpCellStatus: MpCellStatus,
        MpCellOpen: MpCellOpen,
        MpCellInOut: MpCellInOut,
        MpCellPhoneMask: MpCellPhoneMask,
        MpCellMatching: MpCellMatching,
        MpCellDateTime: MpCellDateTime,
        MpCellStructure: MpCellStructure,
        MpCellGroup: MpCellGroup,
        MpCellCheckbox: MpCellCheckbox,
        MpCellContextMenu: MpCellContextMenu,
        MpCellEditValue: MpCellEditValue,
        MpCellTranslate: MpCellTranslate,
        MpCellCheckboxes: MpCellCheckboxes,
        MpCellBoolean: MpCellBoolean,
        MpCellSelect: MpCellSelect,
        MpCellTimer: MpCellTimer,
      }
    },
    tableOptions() {
      return mpTableOptions({
        messages: {
          noData: this.$t('noData'),
        },
        context: this,
        tableName: this.tableName,
      })
    },
  },
  watch: {
    loading() {
      this.handlerLoading()
    },
  },
  beforeDestroy() {
    this.clearActiveHeadersEffects(this.tableName)
  },
  methods: {
    ...mapActions({
      clearActiveHeadersEffects: 'tableHeaderEffects/clearTableHeaderEffects',
      setActiveFiltersForTable: 'tableHeaderEffects/changeColumnActiveEffects',
    }),
    onGridReady({ api }) {
      this.gridApi = api
      this.gridApi.tableName = this.tableName
      this.handlerLoading()
    },
    actionButtonClick(row) {
      this.$emit(`action-button-click`, row)
    },
    timerFinished() {
      this.$emit(`on-timer-finished`)
    },
    sortBy(data) {
      this.$emit('sort-by', data)
    },
    itemOpen(row) {
      this.$emit(`item-open`, row)
    },
    handlerCheckRow(data) {
      this.$emit(`check-row`, data)
    },
    oneOfCheckboxesChanged(data) {
      this.$emit(`one-of-checkboxes-changed`, data)
    },
    selectChanged(data) {
      this.$emit(`select-changed`, data)
    },
    checkAllRow(value) {
      this.$emit('check-all-row', value)
    },
    followLink(value) {
      this.$emit('follow-link', value)
    },
    editValue(value) {
      this.$emit('edit-value', value)
    },
    handlerStructure(eventData) {
      this.$emit(`structure`, eventData)
    },
    filterChanged(filterState) {
      const dto = setFilter(this.listQuery, this.tableName, filterState)
      this.$emit('onFilterChanged', dto)
    },
    columnsSettingsChanged() {
      this.$emit('columns-settings-changed')
    },
    handlerLoading() {
      if (this.gridApi) {
        if (this.loading) {
          this.gridApi.showLoadingOverlay()
        }
      }
    },
  },
  i18n: {
    messages: mpTableLang,
  },
})
</script>

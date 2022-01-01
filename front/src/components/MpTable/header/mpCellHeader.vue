<template>
  <div
    class="mp-cell-header"
    @click.right="toggleColumnsSettings"
    @contextmenu.capture.prevent
  >
    <MpCellHeaderCheckbox
      v-if="params.column.colDef.field === reservedFieldName.checkbox"
      :params="params"
      @check-all-row="checkAllRow"
    />

    <div
      v-if="params.column.colDef.field !== reservedFieldName.checkbox"
      class="mp-cell-header__title"
      @click="handleSort"
    >
      <div class="mp-cell-header__name">
        {{ params.displayName }}
      </div>
      <mp-button-sort
        v-if="params.column.colDef.sortable"
        :order-by="orderBy"
        :params="params"
      />
    </div>

    <div
      v-if="params.column.colDef.filterParams"
      class="mp-cell-header__filter"
    >
      <el-popover
        v-model="showPopupFilter"
        :popper-class="themeName"
        placement="bottom"
        width="auto"
        trigger="click"
      >
        <mp-wrapper-table-filter
          v-if="showPopupFilter"
          :params="params"
          @hide-popup-filter="hidePopupFilter"
          @filter-changed="filterChanged"
        />

        <mp-button
          slot="reference"
          class="mp-button--cell"
          size="mini"
          circle
          is-icon
          :mp-type="
            activeFilter
              ? MpTypeButton.activeProperties
              : MpTypeButton.properties
          "
        />
      </el-popover>
    </div>
    <div class="mp-cell-header__settings">
      <el-popover
        :value="
          showColumnsSettingsMenu &&
            whereOpenedColumnsSettings === params.column.colDef.field
        "
        popper-class="columns-settings-menu__popover"
        placement="bottom"
        width="auto"
        trigger="click"
      >
        <mp-wrapper-columns-settings
          v-if="
            showColumnsSettingsMenu &&
              whereOpenedColumnsSettings === params.column.colDef.field
          "
          :listForDraggable="listForDraggable"
          :columnName="params.column.colDef.field"
          :tableName="params.api.tableName"
          @update="getColumns"
          @close-popup="showColumnsSettingsMenu = false"
        />
        <span slot="reference"></span>
      </el-popover>
    </div>
  </div>
</template>

<script>
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton'
import MpButtonSort from '@/components/basic/MpButtonSort/MpButtonSort'
import MpWrapperTableFilter from '../filters/MpWrapperTableFilter'
import MpWrapperColumnsSettings from '../columnSettings/MpWrapperColumnsSettings'
import { mapActions, mapGetters } from 'vuex'
import MpCellHeaderCheckbox from '@/components/MpTable/header/MpCellHeaderCheckbox'
import { ListReservedTableFieldName } from '@/modules/table_grid/Const/listReservedTableFieldName.const'
import { sortType } from '@/modules/Sort/Sort.const'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export default Vue.extend({
  name: 'MpCellHeader',
  components: {
    MpCellHeaderCheckbox,
    MpButton,
    MpButtonSort,
    MpWrapperTableFilter,
    MpWrapperColumnsSettings,
  },
  data() {
    return {
      orderBy: '',
      showPopupFilter: false,
      MpTypeButton: MpTypeButton,
      showColumnsSettingsMenu: false,
      reservedFieldName: ListReservedTableFieldName,
    }
  },
  computed: {
    // используется для взаимодействия с компонентом сохранения фильтров
    ...mapGetters({
      findActiveColumnsEffects: 'tableHeaderEffects/getTableHeadersEffects',
    }),
    activeFilter() {
      const tableFilters = this.findActiveColumnsEffects(
        this.params.api.tableName,
      )?.filters
      if (tableFilters) {
        const columnFilter = tableFilters.find(
          filter => filter.nameField === this.params.column.colDef.field,
        )
        return !!columnFilter
      }
      return false
    },
    whereOpenedColumnsSettings() {
      return this.findActiveColumnsEffects(this.params.api.tableName)
        ?.whereMenuIsOpened
    },
    listForDraggable() {
      return this.params.columnApi.columnController.columnDefs
    },
    themeName() {
      return `theme-${process.env.VUE_APP_THEME}`
    },
  },
  methods: {
    ...mapActions({
      changeActiveEffectColumnForTable:
        'tableHeaderEffects/changeColumnActiveEffects',
    }),
    toggleColumnsSettings(event) {
      event.preventDefault()
      this.showColumnsSettingsMenu = !this.showColumnsSettingsMenu
      this.changeActiveEffectColumnForTable({
        tableName: this.params.api.tableName,
        effects: { whereMenuIsOpened: this.params.column.colDef.field },
      })
    },
    // передача событий в таблицу
    getColumns() {
      this.$parent.$emit('on-columns-settings-changed')
    },
    filterChanged(data) {
      this.$parent.$emit('on-filter-changed', data)
    },
    sortChanged() {
      this.changeActiveEffectColumnForTable({
        tableName: this.params.api.tableName,
        effects: { sorted: this.params.column.colDef.field },
      })
      this.$parent.$emit('sort-by', {
        orderBy: this.orderBy,
        field: this.params.column.colDef.field,
      })
    },
    handleSort() {
      if (this.params.column.colDef.sortable) {
        switch (this.orderBy) {
          case '':
            this.orderBy = sortType.asc
            break
          case sortType.asc:
            this.orderBy = sortType.desc
            break
          case sortType.desc:
            this.orderBy = ''
        }
        this.sortChanged()
      }
    },
    hidePopupFilter() {
      this.showPopupFilter = false
    },
    checkAllRow(value) {
      this.$parent.$emit('check-all-row', value)
    },
  },
})
</script>

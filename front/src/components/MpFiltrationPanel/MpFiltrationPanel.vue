<template>
  <section class="filtration-panel__wrapper">
    <mp-button
      :mp-type="filterIcon"
      mp-status="normal"
      :disabled="disabled"
      is-icon
      @click="open"
    />

    <el-dialog
      v-if="visible"
      width="60vw"
      class="mp-modal"
      append-to-body
      :close-on-click-modal="false"
      :visible="visible"
      :title="$t('general.filterOptions')"
      :before-close="close"
      :draggable="false"
      custom-class="filtration-panel"
    >
      <template #default>
        <div class="filtration-panel__controls">
          <mp-search
            v-if="visible"
            :extension-search-string="listQuery.tableSearchBy"
            @search="setSearch"
          />
          <mp-select
            v-model="selectedFilter"
            :popper-append-to-body="false"
            filterable
            @change="selectFilter"
            :placeholder="$t('forms.placeholders.addingFilter')"
            :option-list="filterList"
            option-key="id"
            option-label="filterTitle"
            option-value="id"
          />
        </div>
        <div class="filtration-panel__body">
          <mp-filter-wrapper
            v-for="filter in selectedFilterList"
            :key="filter.id"
            :filter="filter"
            :main-table-name="listQuery.tableName"
            :filter-value-list="setFilterValueList(filter)"
            @remove-filter="removeFilter"
            @change-filter="changeFilter"
          />
        </div>
      </template>

      <template v-slot:footer>
        <mp-button
          type="primary"
          mp-status="normal"
          mp-type="apply"
          @click="apply"
        >
          {{ $t('general.apply') }}
        </mp-button>

        <mp-button type="" mp-status="normal" mp-type="cancel" @click="close">
          {{ $t('general.cancel') }}
        </mp-button>

        <mp-button type="" mp-status="normal" mp-type="clear" @click="clear">
          {{ $t('general.clear') }}
        </mp-button>
      </template>
    </el-dialog>
  </section>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import MpFilterWrapper from '@/components/MpFiltrationPanel/MpFilterWrapper.vue'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { MpFiltrationPanelLang } from './MpFiltrationPanel.lang'
import { IStateFilterList } from '@/components/MpFiltrationPanel/filterList.interface'
import { FiltrationPanel } from '@/modules/FiltrationPanel/FiltrationPanel'
import { IFiltrationPanelItem } from '@/modules/FiltrationPanel/FiltrationPanel.interface'
import { IEventSearch } from '@/components/basic/MpSearch/MpSearch.interface'
import { mapActions } from 'vuex'
import { paginationParams } from '@/components/MpPagination/type'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'
import MpSelect from '@/components/MpSelect/MpSelect.vue'

export default Vue.extend({
  name: 'MpFiltrationPanel',
  components: {
    MpSelect,
    MpFilterWrapper,
    MpSearch,
    MpButton,
  },
  props: {
    disabled: {
      type: Boolean,
      default: false,
    },
    listQuery: {
      type: Object as () => TableDto,
      default: (): TableDto => DtoFactory.create(DtoName.table),
    },
  },
  data() {
    return {
      filtrationPanel: {} as FiltrationPanel,
      visible: false,
      search: '',
      filterList: [] as IFiltrationPanelItem[],
      selectedFilter: null,
      selectedFilterList: [] as IFiltrationPanelItem[],
      stateFilterList: {
        main: {},
        extension: {},
      } as IStateFilterList,
      loading: false,
    }
  },
  created() {
    this.$i18n.mergeLocaleMessage('en', MpFiltrationPanelLang.en)
    this.$i18n.mergeLocaleMessage('ru', MpFiltrationPanelLang.ru)
  },
  mounted() {
    this.filtrationPanel = new FiltrationPanel(this.listQuery.tableName)
  },
  computed: {
    filterIcon() {
      if (
        this.listQuery.filters.length ||
        this.listQuery.extensionFilters.length
      ) {
        return MpTypeButton.filterContains
      }
      return MpTypeButton.filter
    },
  },
  methods: {
    ...mapActions({
      setActiveFiltersForTable: 'tableHeaderEffects/changeColumnActiveEffects',
    }),
    async getFilterList() {
      this.loading = true
      const result: Array<IFiltrationPanelItem> = await this.filtrationPanel.getList()
      if (result) {
        this.filterList = result.map(filterItem => {
          filterItem.filterTitle = this.filterTitle(filterItem)
          return filterItem
        })
        this.filterList = this.sortByFilterTitle(this.filterList)
      }
      this.loading = false
    },

    removeFilter(id: IFiltrationPanelItem['id']): void {
      const filter = this.selectedFilterList.find(filter => filter.id === id)
      if (filter) {
        this.filterList.push(filter)
        this.filterList = this.sortByFilterTitle(this.filterList)
        this.selectedFilterList = this.selectedFilterList.filter(
          filter => filter.id !== id,
        )
        delete this.stateFilterList.main[filter.id]
        delete this.stateFilterList.extension[filter.id]
        this.selectedFilter = null
      }
    },
    selectFilter(id: IFiltrationPanelItem['id']): void {
      const filter = this.filterList.find(filter => filter.id === id)
      if (filter) {
        this.selectedFilterList.push(filter)
        this.filterList = this.filterList.filter(filter => filter.id !== id)
        this.filterList = this.sortByFilterTitle(this.filterList)
        this.selectedFilter = null
      }
    },
    sortByFilterTitle(filterList: Array<IFiltrationPanelItem>) {
      return filterList.sort(
        (a: IFiltrationPanelItem, b: IFiltrationPanelItem) => {
          if (a.filterTitle > b.filterTitle) return 1
          if (a.filterTitle < b.filterTitle) return -1
          return 0
        },
      )
    },
    changeFilter(
      filterId: IFiltrationPanelItem['id'],
      isMainTable: IFiltrationPanelItem['isMainTable'],
      filterState: IFilterState,
      apply,
    ) {
      this.stateFilterList[isMainTable ? 'main' : 'extension'][
        filterId
      ] = filterState
      if (apply) {
        this.apply()
      }
    },
    async open() {
      this.visible = !this.visible
      this.selectedFilterList = []
      this.search = this.listQuery.tableSearchBy
      await this.getFilterList()
      this.setFiltersMainTable()
    },
    setFiltersMainTable() {
      this.filterList.forEach(filterItem => {
        if (filterItem.isMainTable) {
          const result = this.listQuery.filters.find(
            item => item.nameField === filterItem.columnName,
          )
          if (result) {
            this.selectFilter(filterItem.id)
          }
        } else {
          // @ts-ignore
          const result = this.listQuery.extensionFilters.find(
            item => item.tableName === filterItem.tableName,
          )
          if (result) {
            this.selectFilter(filterItem.id)
          }
        }
      })
      Object.keys(this.stateFilterList.extension).forEach(filterId => {
        if (this.selectedFilterList.find(filter => filter.id !== +filterId)) {
          delete this.stateFilterList.extension[filterId]
        }
      })
      Object.keys(this.stateFilterList.main).forEach(filterId => {
        if (this.selectedFilterList.find(filter => filter.id !== +filterId)) {
          delete this.stateFilterList.main[filterId]
        }
      })
    },
    setFilterValueList(filter) {
      if (filter.isMainTable) {
        const result = this.listQuery.filters.find(
          item => item.nameField === filter.columnName,
        )
        return result ? result.valuesField : []
      }
      const result = this.listQuery.extensionFilters.find(
        item => item.nameField === filter.columnName,
      )
      return result ? result.valuesField : []
    },
    apply() {
      this.$emit('filtration-panel-state', {
        ...this.listQuery,
        Filters: this.clearEmptyFilter(
          this.convertObjectToArray(this.stateFilterList.main),
        ),
        extensionFilters: this.clearEmptyFilter(
          this.convertObjectToArray(this.stateFilterList.extension),
        ),
        Page: paginationParams.defaultPage,
      })
      this.setActiveFiltersForTable({
        tableName: this.listQuery.tableName,
        effects: {
          filters: this.clearEmptyFilter(
            this.convertObjectToArray(this.stateFilterList.main),
          ),
        },
      })
      this.visible = false
    },
    convertObjectToArray(obj: {
      [key: number]: IFilterState
    }): Array<IFilterState> {
      return Object.values(obj).map(item => item)
    },
    clearEmptyFilter(filterList: Array<IFilterState>): Array<IFilterState> {
      return filterList.reduce((acc, item) => {
        if (item.valuesField.length) {
          acc.push(item)
        }
        return acc
      }, [] as Array<IFilterState>)
    },
    setSearch(eventSearch: IEventSearch) {
      this.listQuery.tableSearchBy = eventSearch.searchString
    },
    close() {
      this.visible = false
    },
    clear() {
      this.stateFilterList = {
        main: {},
        extension: {},
      }
      this.selectedFilterList = []
      this.$emit('filtration-panel-state', {
        ...this.listQuery,
        Filters: [],
        extensionFilters: [],
        TableSearchBy: '',
        Page: paginationParams.defaultPage,
      })
      this.setActiveFiltersForTable({
        tableName: this.listQuery.tableName,
        effects: { filters: [] },
      })
      this.visible = false
    },
    filterTitle(filter) {
      if (filter.filterTitle) {
        if (filter.propertyGroupName) {
          return `${filter.propertyName} (${filter.propertyGroupName})`
        }
        return `${filter.propertyName}`
      }
      return this.$t(`tables.${this.listQuery.tableName}.${filter.columnName}`)
    },
  },
})
</script>

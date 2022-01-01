<template>
  <div v-if="!belongsFilteringPanel" class="column-filter--select">
    <mp-search
      :placeholder="$t('forms.placeholders.search')"
      @search="searchOption"
    />
    <div class="column-filter--select__label">
      <span>{{ $t('general.firstRecords') }}</span>
    </div>
    <div class="column-filter--select__options-container" v-loading="loading">
      <div class="column-filter--select__option">
        <el-checkbox
          :value="checkAll"
          @change="checkAllOption"
          :label="$t('general.chooseEntity', [$t('general.entities.all')])"
        />
      </div>
      <el-checkbox-group
        v-for="option in options"
        :key="option.id"
        :value="checkCheckedCheckbox(option)"
        @change="checkOption(option)"
        class="column-filter--select__option"
      >
        <el-checkbox
          :checked="option.checked"
          :key="option.id"
          :label="option.translatedValue"
        />
      </el-checkbox-group>
    </div>
  </div>
  <div v-else class="column-filter--select">
    <el-select
      ref="fp-select"
      :value="checkedItems"
      value-key="id"
      name="value"
      multiple
      :popper-append-to-body="false"
      placeholder=""
      class="filter-select__drop-down"
      @visible-change="focusSearch"
      @remove-tag="removeCheckedItem"
    >
      <div class="column-filter--select__search-collapse">
        <mp-search ref="search" @search="searchOption" />
        <mp-button
          mp-type="collapse"
          mp-status="normal"
          is-icon
          :disabled="false"
          @click="collapse"
        />
      </div>
      <div class="column-filter--select__options-container" v-loading="loading">
        <el-option
          v-if="options.length"
          :key="-1"
          :label="'all'"
          :value="{ id: 0, value: 'all' }"
        >
          <div class="column-filter--select__option">
            <el-checkbox
              :value="checkAll"
              @change="checkAllOption"
              :label="$t('general.chooseEntity', [$t('general.entities.all')])"
            />
          </div>
        </el-option>
        <el-option
          v-if="!options.length"
          class="column-filter--select__option column-filter--select__option--not-match"
          :key="-2"
          :label="$t('general.noMatchesFound')"
          :value="{ id: 0, value: 'noMatchesFound' }"
        >
        </el-option>
        <el-option
          v-for="option in options"
          :key="option.id"
          :value="option"
          :label="option.translatedValue"
        >
          <el-checkbox
            :value="checkCheckedCheckbox(option)"
            :key="option.id"
            :label="option.translatedValue"
            @change="checkOption(option)"
            class="column-filter--select__option"
          />
        </el-option>
      </div>
    </el-select>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { selectFilterFactory } from '@/modules/Filters/SelectFilterFactory/SelectFilterFactory.ts'
import { methods } from '@/api_services/httpMethods.enum'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import { mapGetters } from 'vuex'
import {
  ISelectFilterQuery,
  ISelectFilterResponse,
} from '@/modules/Filters/SelectFilterFactory/SelectFilterFactory.interface'
import { IDataMpSelectTableFilterInterface } from './MpSelectFilter.interface'
import { IEventSearch } from '@/components/basic/MpSearch/MpSearch.interface'
import { FilterType } from '@/modules/Filters/Filters.const'
import {
  ISelectFilterOption,
  ISelectFilterState,
  valuesFieldSelectFilter,
} from '@/modules/Filters/SelectFilter/SelectFilter.interface'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { IFiltrationPanelItem } from '@/modules/FiltrationPanel/FiltrationPanel.interface'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { IExtendElSelectInterface } from '@/external_lib/element-ui/IExtendElSelect.interface'

export default Vue.extend({
  name: 'MpSelectFilter',
  components: {
    MpButton,
    MpSearch,
  },
  props: {
    tableName: {
      type: String,
      default: () => '',
    },
    columnName: {
      type: String,
      default: () => '',
    },
    belongsFilteringPanel: {
      type: Boolean,
      default: false,
    },
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
          isMainTable: true,
        }
      },
    },
    mainTableName: {
      type: String,
      default: '',
    },
    filterValueList: {
      type: Array as () => Array<valuesFieldSelectFilter>,
      default: () => [],
    },
  },
  data(): IDataMpSelectTableFilterInterface {
    return {
      forTranslate: ['currentStatus'],
      listQueryForOptions: {
        filterId: null,
        columnName: '',
        tableName: '',
        search: null,
        checkList: [],
        addParameters: [],
      },
      checkedItems: [],
      options: [],
      checkAll: false,
      loading: true,
    }
  },
  computed: {
    // используется для взаимодействия с компонентом сохранения фильтров
    ...mapGetters({
      findActiveFilter: 'tableHeaderEffects/getTableHeadersEffects',
    }),
  },
  async mounted() {
    this.setListQueryForOptions()
    await this.getOptionsForFilter(this.listQueryForOptions)
    await this.setFilterState()
  },
  methods: {
    collapse() {
      const fpSelect = this.$refs['fp-select'] as IExtendElSelectInterface
      fpSelect.visible = false
    },
    async setFilterState() {
      const tableFiltersFromStore = await this.findActiveFilter(this.tableName)
        ?.filters
      if (tableFiltersFromStore && tableFiltersFromStore.length) {
        const columnFilter = tableFiltersFromStore.find(
          (filter: IFilterState) => filter.nameField === this.columnName,
        )
        if (columnFilter) {
          this.setCheckedItem(columnFilter.valuesField)
          this.checkAll = this.checkedItems.length === this.options.length
        }
      }
      if (!this.filter.isMainTable) {
        this.setCheckedItem(this.filterValueList)
        this.checkAll = this.checkedItems.length === this.options.length
      }
    },
    setListQueryForOptions(): ISelectFilterQuery {
      this.listQueryForOptions = selectFilterFactory()
      this.listQueryForOptions.columnName = !this.belongsFilteringPanel
        ? this.columnName
        : this.filter.isMainTable
        ? this.filter.columnName
        : this.filter.privateName
      this.listQueryForOptions.tableName = this.belongsFilteringPanel
        ? this.mainTableName
        : this.tableName
      return this.listQueryForOptions
    },
    async getOptionsForFilter(listQuery: ISelectFilterQuery) {
      this.loading = true
      const res: ISelectFilterResponse = await this.$api.fetchSelectFilter({
        method: methods.post,
        data: listQuery,
      })
      if (res) {
        this.options = res.columnForStringFilter.map(item => {
          const newItem: ISelectFilterOption = {
            id: item.id,
            value: item.value,
            translatedValue: !this.forTranslate.includes(this.columnName)
              ? item.value
              : this.$t(`filter.${this.columnName}.${item.value}`).toString(),
            // todo Харкод. :( значение этого поля должно прилетать с сервера, как будет прилетать,
            // то убрать харкод.
            valueMember: this.setValueMember(),
          }
          return newItem
        })
      }
      this.loading = false
    },
    setValueMember() {
      if (!this.filter.isMainTable) {
        return 'id'
      }
      return 'value'
    },
    setCheckedItem(valuesField: Array<valuesFieldSelectFilter>): void {
      this.options.forEach(option => {
        if (valuesField.includes(option[option.valueMember])) {
          this.checkOption({
            id: option.id,
            value: option.value,
            translatedValue: option.translatedValue,
            valueMember: option.valueMember,
          })
        }
      })
    },
    searchOption(value: IEventSearch): void {
      this.listQueryForOptions.search = value.searchString
      this.getOptionsForFilter(this.listQueryForOptions)
    },
    checkOption(selectedOptions: ISelectFilterOption): void {
      if (this.checkedItems.find(item => item.id === selectedOptions.id)) {
        this.checkedItems = this.checkedItems.filter(
          item => item.id !== selectedOptions.id,
        )
      } else {
        this.checkedItems.push(selectedOptions)
      }
      this.checkAll = this.checkedItems.length === this.options.length
      this.emitState()
    },
    removeCheckedItem(selectedOptions: ISelectFilterOption): void {
      this.checkedItems = this.checkedItems.filter(
        item => item.id !== selectedOptions.id,
      )
      this.checkAll = this.checkedItems.length === this.options.length
      this.emitState()
    },
    checkAllOption() {
      if (this.checkAll) {
        this.checkedItems = []
        this.checkAll = false
      } else {
        this.checkedItems = this.options
        this.checkAll = true
      }
      this.emitState()
    },
    checkCheckedCheckbox(option: ISelectFilterOption): boolean {
      return !!this.checkedItems.find(item => item.id === option.id)
    },
    emitState(): void {
      const filterState: ISelectFilterState = {
        filterType: FilterType.select,
        nameField: this.columnName,
        tableName: this.tableName,
        valuesField: this.checkedItems.map(item => item[item.valueMember]),
      }
      this.$emit('state-select-filter', filterState)
    },
    clearState() {
      this.checkedItems = []
      this.checkAll = false
      this.emitState()
    },
    focusSearch(value) {
      if (value) {
        this.$nextTick(() => {
          // @ts-ignore
          this.$refs.search.$el?.children[0]?.focus()
        })
      }
    },
  },
})
</script>

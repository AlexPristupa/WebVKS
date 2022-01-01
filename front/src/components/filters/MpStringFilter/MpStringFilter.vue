<template>
  <div class="column-filter__string column-filter-string">
    <div
      v-for="(itemFilter, filterIndex) in valueList"
      :key="filterIndex"
      class="column-filter-string__item"
    >
      <el-select
        v-model="itemFilter.operand"
        class="column-filter-string__operand"
        @change="emitState"
      >
        <el-option
          v-for="operand in operands"
          :key="operand.id"
          :value="operand.value"
          :label="operand.name"
        >
        </el-option>
      </el-select>
      <el-input
        v-model="itemFilter.compareValue"
        class="column-filter-string__value"
        :placeholder="$t('placeholder.stringFilter')"
        @input="emitState"
      />
      <mp-button
        class="mp-button--cell column-filter-string__action"
        size="mini"
        circle
        :title="buttonTitle(buttonName(filterIndex))"
        :mp-type="buttonName(filterIndex)"
        @click="action(filterIndex)"
      />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { MpTableFiltersLang } from '../MpTableFilters.lang'
import { operandValue } from '@/modules/Filters/Operands/Operands.const'
import { FilterType } from '@/modules/Filters/Filters.const'
import {
  ButtonNameStringFilter,
  IDataMpStringFilter,
  IStringFilterState,
  IValuesFieldStringFilter,
} from '@/modules/Filters/StringFilter/StringFilter.interface'
import { TranslateResult } from 'vue-i18n'
import { stringOperandsConstants } from '@/modules/Filters/Operands/StringOperands.const'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export default Vue.extend({
  name: 'MpStringFilter',
  components: {
    MpButton,
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
    filterValueList: {
      type: Array as () => Array<IValuesFieldStringFilter>,
      default: () => [],
    },
  },
  data: (): IDataMpStringFilter => {
    return {
      valueList: [
        {
          operand: operandValue.includes,
          compareValue: '',
        },
      ],
    }
  },
  created() {
    this.$i18n.mergeLocaleMessage('en', MpTableFiltersLang.en)
    this.$i18n.mergeLocaleMessage('ru', MpTableFiltersLang.ru)
  },
  mounted() {
    if (this.filterValueList.length) {
      this.valueList = this.filterValueList
    }
  },
  computed: {
    operands() {
      return stringOperandsConstants.map(operand => {
        return {
          id: operand.id,
          value: operand.value,
          name: this.$t(`operand.${operand.name}`),
        }
      })
    },
  },
  methods: {
    buttonTitle(buttonName: ButtonNameStringFilter): TranslateResult {
      return buttonName === MpTypeButton.add
        ? this.$t('button.label.add')
        : this.$t('button.label.remove')
    },
    buttonName(filterIndex: number): ButtonNameStringFilter {
      return filterIndex === 0 ? MpTypeButton.add : MpTypeButton.remove
    },
    action(filterIndex: number): void {
      if (filterIndex) {
        this.valueList.splice(filterIndex, 1)
      } else {
        this.valueList.push({
          operand: operandValue.includes,
          compareValue: '',
        } as IValuesFieldStringFilter)
      }
    },
    emitState(): void {
      const filterState: IStringFilterState = {
        filterType: FilterType.string,
        nameField: this.columnName,
        tableName: this.tableName,
        valuesField: this.valueList,
      }
      this.$emit('state-string-filter', filterState)
    },
    clearState() {
      this.valueList = [
        {
          operand: operandValue.includes,
          compareValue: '',
        },
      ]
      const filterState: IStringFilterState = {
        filterType: FilterType.string,
        nameField: this.columnName,
        tableName: this.tableName,
        valuesField: this.valueList,
      }
      this.$emit('state-string-filter', filterState)
    },
  },
  // i18n: {
  //   messages: { ...MpTableFiltersLang },
  // },
})
</script>

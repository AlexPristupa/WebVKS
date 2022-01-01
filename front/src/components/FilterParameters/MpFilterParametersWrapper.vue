<template>
  <div class="filter-parameters">
    <mp-button
      mp-type="import"
      mp-status="normal"
      is-icon
      @click="visible = true"
    />
    <el-dialog
      v-if="visible"
      width="70vw"
      class="mp-modal"
      append-to-body
      :close-on-click-modal="false"
      :visible="visible"
      :title="$t('forms.filterParameters.title', [filterTitle])"
      :before-close="() => (visible = false)"
      :draggable="false"
    >
      <template #default>
        <mp-filter-parameters-select
          v-if="filterType === enumFilterType.select"
          :checked-data="checkedData"
          @update-filter-parameters-select="updateFilterParametersSelect"
        />
        <mp-filter-parameters-string
          v-if="filterType === enumFilterType.string"
          @update-filter-parameters-string="updateFilterParametersString"
        />
      </template>

      <template v-slot:footer>
        <mp-button
          v-if="
            checkedData.found.length || filterType === enumFilterType.string
          "
          type="primary"
          mp-status="normal"
          mp-type="apply"
          @click="apply"
        >
          {{ $t('general.apply') }}
        </mp-button>

        <mp-button
          v-if="
            filterType === enumFilterType.select && !checkedData.found.length
          "
          :disabled="!formData.select"
          type="primary"
          mp-status="normal"
          mp-type="apply"
          @click="check"
        >
          {{ $t('general.check') }}
        </mp-button>

        <mp-button
          type=""
          mp-status="normal"
          mp-type="cancel"
          @click="visible = false"
        >
          {{ $t('general.cancel') }}
        </mp-button>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { FilterType } from '@/modules/Filters/Filters.const'
import { MpFilterParametersLang } from '@/components/FilterParameters/MpFilterParameters.lang'
import {
  IDataMpFilterParametersWrapper,
  IFilterParametersUpdateEvent,
  IFilterParametersUpdateEventString,
} from './MpFilterParametersWrapper.interface'
import MpFilterParametersSelect from './MpFilterParametersSelect.vue'
import MpFilterParametersString from './MpFilterParametersString.vue'
import { IFiltrationPanelItem } from '@/modules/FiltrationPanel/FiltrationPanel.interface'
import { FilterParameters } from '@/modules/Filters/FilterParameters/FilterParameters'
import { IResponseCheckValue } from '@/modules/Filters/FilterParameters/FilterParameters.interface'
import { IValuesFieldStringFilter } from '@/modules/Filters/StringFilter/StringFilter.interface'
import { separatorList } from '@/modules/Separators/Separators.const'
import { stringOperandsConstants } from '@/modules/Filters/Operands/StringOperands.const'

const moduleFilterParameters = (tableName, columnName) => {
  return new FilterParameters(tableName, columnName)
}

export default Vue.extend({
  name: 'MpFilterParametersWrapper',
  components: {
    MpFilterParametersString,
    MpFilterParametersSelect,
    MpButton,
  },
  data: function(): IDataMpFilterParametersWrapper {
    return {
      visible: false,
      enumFilterType: {
        string: FilterType['string'],
        select: FilterType['select'],
      },
      formData: {
        string: {
          text: '',
          separator: separatorList[0],
          operand: stringOperandsConstants[0],
        },
        select: {
          text: '',
          separator: separatorList[0],
        },
      },
      checkedData: {
        notFound: [],
        found: [],
      } as IResponseCheckValue,
      filterParameters: moduleFilterParameters(this.tableName, this.columnName),
    }
  },
  props: {
    filterType: {
      type: String as () => FilterType.string | FilterType.select,
    },
    filterTitle: {
      type: String as () => IFiltrationPanelItem['filterTitle'],
      default: '',
    },
    tableName: {
      type: String,
      default: '',
    },
    columnName: {
      type: String,
      default: '',
    },
  },
  created() {
    this.$i18n.mergeLocaleMessage('en', MpFilterParametersLang.en)
    this.$i18n.mergeLocaleMessage('ru', MpFilterParametersLang.ru)
  },
  methods: {
    updateFilterParametersSelect(data: IFilterParametersUpdateEvent) {
      this.formData.select = data
    },
    updateFilterParametersString(data: IFilterParametersUpdateEventString) {
      this.formData.string = data
    },
    apply() {
      if (this.filterType === this.enumFilterType.select) {
        this.$emit('apply-filter', {
          filterType: FilterType.select,
          nameField: this.columnName,
          tableName: this.tableName,
          valuesField: this.checkedData?.found,
        })
        this.visible = false
      }
      if (this.filterType === this.enumFilterType.string) {
        this.$emit('apply-filter', {
          filterType: FilterType.string,
          nameField: this.columnName,
          tableName: this.tableName,
          valuesField: this.checkString(),
        })
        this.visible = false
      }
    },
    check() {
      if (this.filterType === this.enumFilterType.select) {
        this.checkSelect()
      }
    },
    async checkSelect() {
      if (this.formData.select?.text && this.formData.select?.separator) {
        const checkedRes: IResponseCheckValue = await this.filterParameters
          .checkData(this.formData.select.text, this.formData.select.separator)
          .checkOnServer()
        if (checkedRes.notFound && checkedRes.found) {
          this.checkedData = checkedRes
        }
      }
    },
    checkString(): Array<IValuesFieldStringFilter> {
      if (
        this.formData.string?.text &&
        this.formData.string?.separator &&
        this.formData.string?.operand
      ) {
        const checkedRes: Array<string> = this.filterParameters.checkData(
          this.formData.string.text,
          this.formData.string.separator,
        ).dataArray
        if (checkedRes) {
          return checkedRes.map(item => {
            return {
              operand: this.formData.string?.operand?.value,
              compareValue: item,
            }
          }) as Array<IValuesFieldStringFilter>
        }
      }
      return []
    },
  },
})
</script>

<template>
  <div class="mp-cell__edit-value mp-cell-edit-value">
    <div v-if="!editing" class="mp-cell-edit-value__value">
      {{ cellData }}
    </div>

    <el-input
      v-if="showInput"
      v-model="cellDataString"
      clearable
      :disabled="disabled"
      @input="inputValue"
    />

    <mp-select
      v-if="showCatalog"
      :value="cellDataCatalog"
      value-key="catalogId"
      name="catalogValue"
      filterable
      :allow-create="false"
      default-first-option
      remote
      clearable
      reserve-keyword
      :remote-method="searchInCatalog"
      :loading="loadingCatalog"
      :disabled="disabled"
      placeholder=""
      size="mini"
      @change="changeCatalog"
      @focus="focusCatalog"
      :option-list="filteredCatalogList"
      option-key="catalogId"
      option-label="catalogValue"
      option-value=""
    />

    <el-date-picker
      v-if="showDatePicker"
      v-model="cellDataDate"
      placeholder=""
      type="date"
      :format="format"
      :disabled="disabled"
      @change="changeDatePicker"
    />
  </div>
</template>

<script lang="ts">
/**
 * @description Компонет ячейки значение которой может быть отредактировано.
 *              Поддерживает три варианта значений: строка, селект (список),
 *              дата-пикер. При изменении значения в ячейка на компоненте
 *              таблицы событие @edit-value, в пайлоаде события передает
 *              выбранное/введенное значение.
 *              Из-за неудачного проектирования серверной части, сильно связан
 *              с методом получения данных для списочных свойств.
 *              Работает совместно с модулем GroupingRows.
 */
import Vue from 'vue'
import { DateTime } from '@/modules/DateTime/DateTime'
import { methods } from '@/api_services/httpMethods.enum'
import { dateFormat } from '@/constants/dateFormat'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { PropertyEditValue } from '@/modules/Property/PropertyEditValue'
import { SettingPropertyEditValueDictionary } from '@/modules/PropertyEditValue/SettingPropertyEditValue.Dictionary'
import {
  IComputedMpCellEditValue,
  IDataMpCellEditValue,
  IMethodsMpCellEditValue,
  IPropsMpCellEditValue,
} from '@/components/MpTable/cell/config/MpCellEditValue.interface'
import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellEditValue,
  IMethodsMpCellEditValue,
  IComputedMpCellEditValue,
  IPropsMpCellEditValue
>({
  name: 'MpCellEditValue',
  components: { MpSelect },
  data() {
    return {
      cellDataString: '',
      cellDataCatalog: {},
      cellDataDate: '',
      catalogList: [],
      loadingCatalog: false,
      format: dateFormat.date,
    }
  },
  mounted() {
    if (
      !this.params.data.isCatalogProperty &&
      !this.params.data.isDateProperty
    ) {
      this.cellDataString = this.params.data.value
    }
    if (this.params.data.isCatalogProperty) {
      this.cellDataCatalog = {
        catalogId: this.params.data.catalogId || this.idForEmpty,
        catalogValue: this.params.data.value,
      }
      this.catalogList.push(this.cellDataCatalog)
    }
    if (this.params.data.isDateProperty) {
      const date = new DateTime({
        dateTime: this.params.data.value,
      }).getJsDate()
      this.cellDataDate = date || ''
    }
  },
  computed: {
    editing() {
      return this.params.data.editValue || false
    },
    cellData() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    idForEmpty() {
      return Math.random() * 10000000000000
    },
    showInput() {
      return (
        this.editing &&
        !this.isTitleGroup &&
        !this.params.data.isDateProperty &&
        !this.params.data.isCatalogProperty
      )
    },
    showCatalog() {
      return (
        this.editing &&
        !this.isTitleGroup &&
        !this.params.data.isDateProperty &&
        this.params.data.isCatalogProperty
      )
    },
    showDatePicker() {
      return (
        this.editing &&
        !this.isTitleGroup &&
        this.params.data.isDateProperty &&
        !this.params.data.isCatalogProperty
      )
    },
    isTitleGroup() {
      return this.params.data.isTitleGroup
    },
    privateName() {
      return this.params.data.privateName
    },
    tableName() {
      // @ts-ignore
      return this.params.api.tableName
    },
    nodeList() {
      return this.params.node.parent
        ? this.params.node.parent.allLeafChildren
        : []
    },
    /**
     * @description Подключение фабрики особых бизнес-правил применяемых при
     *              редактировании значений.
     */
    setting() {
      const setting = SettingPropertyEditValueDictionary.getSettings(
        this.tableName,
      )
      return setting ? new setting(this.nodeList) : null
    },
    /**
     * @description Возвращает значение для атрибута disabled если правила существуют
     */
    disabled() {
      return this.setting
        ? this.setting.getDisabledStatus(this.privateName)
        : true
    },
    /**
     * @description Возвращает отфильтрованную коллекцию catalogList если правила
     *              существуют
     */
    filteredCatalogList() {
      return this.setting
        ? this.setting.getCatalogList(this.privateName, this.catalogList)
        : this.catalogList
    },
  },
  methods: {
    changeCatalog(item) {
      this.cellDataCatalog = item
      this.params.data.value = item.catalogValue || item
      this.$parent.$emit(
        'edit-value',
        new PropertyEditValue(
          this.privateName,
          item.catalogValue ? item.catalogValue : item,
          item.catalogId ? item.catalogId : null,
        ),
      )
    },
    inputValue(value) {
      this.params.data.value = value
      this.$parent.$emit(
        'edit-value',
        new PropertyEditValue(this.privateName, value),
      )
    },
    changeDatePicker(value) {
      this.params.data.value = value
      this.$parent.$emit(
        'edit-value',
        new PropertyEditValue(this.privateName, value),
      )
    },
    async searchInCatalog(input) {
      await this.getCatalog(input)
    },
    async focusCatalog() {
      await this.getCatalog()
    },
    async getCatalog(search = '') {
      this.loadingCatalog = true
      const result = await this.$api.invExtensionPropertyCatalog({
        method: methods.get,
        data: {
          privateName: this.params.data.privateName,
          value: search,
        },
      })
      if (result && result.values) {
        this.catalogList = result.values
      }
      this.loadingCatalog = false
    },
  },
})
</script>

<template>
  <div class="mp-cell__select">
    <mp-select
      :filterable="settings.search"
      :option-key="settings.display.name"
      :option-value="settings.display.id"
      :option-label="settings.display.name"
      :clearable="settings.clearable"
      :option-list="options"
      :value="value"
      :popper-append-to-body="true"
      :filter-method="getOptions"
      @change="change"
      @focus="focus"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpCellSelect,
  IDataMpCellSelect,
  IMethodsMpCellSelect,
  IPropsMpCellSelect,
} from '@/components/MpTable/cell/config/MpCellSelect/MpCellSelect.interface'
import { ICellRendererParams } from '@ag-grid-community/core'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { mpSelectOptionsGetters } from '@/components/MpTable/cell/config/MpCellSelect/MpCellSelect.const'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellSelect,
  IMethodsMpCellSelect,
  IComputedMpCellSelect,
  IPropsMpCellSelect
>({
  name: 'MpCellSelect',
  components: { MpSelect },
  computed: {
    tableName() {
      // @ts-ignore
      return this.params.tableName
    },
    field() {
      return this.params.colDef.field || ''
    },
    data() {
      return this.params.data
    },
    settings() {
      return mpSelectOptionsGetters[this.tableName][this.field]
    },
  },
  data() {
    return {
      options: [],
      value: null,
    }
  },
  async mounted() {
    await this.getOptions()
  },
  methods: {
    async getOptions(search = '') {
      const data: { [key: string]: string } = {
        ...this.settings.defaultParams,
        search: search,
      }
      if (this.settings.additionalParams.length) {
        this.settings.additionalParams.forEach((param: string) => {
          data[param] = this.data[param]
        })
      }
      const res = await this.settings.request({
        method: this.settings.method,
        data: data,
      })
      if (res && !res.validation) {
        this.options = res
        this.value = this.data[this.field] || null
      }
    },
    focus() {
      this.params.node.setSelected(true, true)
    },
    change(value) {
      this.$parent.$emit('on-select-changed', {
        id: this.data.id,
        field: this.field,
        value: value,
        option: this.options.find(
          option => option[this.settings.display.id] === value,
        ),
      })
    },
  },
})
</script>

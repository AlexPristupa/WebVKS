<template>
  <div class="mp-cell-header-checkbox">
    <el-popover
      trigger="click"
      placement="bottom-start"
      width="300"
      v-model="submenuVisible"
      popper-class="mp-cell-header-checkbox__modal"
      @hide="submenuVisible = false"
    >
      <el-button
        type="text"
        class="el-dialog__headerbtn"
        icon="el-icon-close"
        @click="submenuVisible = false"
      />
      <div class="mp-cell-header-checkbox__modal-title">
        {{ `${$t('callTable.mpCellHeaderCheckbox.selectedEntries')}: ` }}
        <strong class="mp-cell-header-checkbox__modal-title-count">{{
          selectedEntries
        }}</strong>
      </div>

      <el-radio
        v-model="fullItems"
        :label="checkboxState.page"
        class="mp-radio"
        @change="emitEventState"
      >
        {{ $t('callTable.mpCellHeaderCheckbox.entriesPage') }}
      </el-radio>
      <el-radio
        v-model="fullItems"
        :label="checkboxState.full"
        class="mp-radio"
        @change="emitEventState"
      >
        {{ $t('callTable.mpCellHeaderCheckbox.entriesAll') }}
      </el-radio>

      <el-checkbox
        slot="reference"
        :value="checkAll"
        :disabled="disabled()"
        @change="checkboxChange"
      />
    </el-popover>
  </div>
</template>

<script lang="ts">
/**
 * @description Компонент отображения чекбокса в заголовке таблицы. Работает
 *              совместно с:
 *              - MpCellCheckbox
 *              - class RowDataChecked
 */
import Vue from 'vue'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { mapGetters } from 'vuex'
import {
  IComputedMpCellHeaderCheckbox,
  IDataMpCellHeaderCheckbox,
  IMethodsMpCellHeaderCheckbox,
  IPropsMpCellHeaderCheckbox,
} from '@/components/MpTable/header/MpCellHeaderCheckbox.interface'

export default Vue.extend<
  IDataMpCellHeaderCheckbox,
  IMethodsMpCellHeaderCheckbox,
  IComputedMpCellHeaderCheckbox,
  IPropsMpCellHeaderCheckbox
>({
  name: 'MpCellHeaderCheckbox',
  props: {
    params: {
      type: Object,
      default: () => {},
    },
  },
  data(): IDataMpCellHeaderCheckbox {
    return {
      submenuVisible: false as boolean,
      fullItems: HeaderCheckboxState.page,
      checkboxState: {
        page: HeaderCheckboxState.page,
        full: HeaderCheckboxState.full,
      },
    }
  },
  computed: {
    ...mapGetters({
      findActiveColumnsEffects: 'tableHeaderEffects/getTableHeadersEffects',
    }),
    checkAll() {
      return this.findActiveColumnsEffects(this.params.api.tableName)?.checkAll
    },
    selectedEntries() {
      const node = this.params.api.getRowNode('0')
      if (this.fullItems === HeaderCheckboxState.page) {
        return node?.data?.checkboxChecked ? node.data.checkboxChecked : 0
      }
      return node?.data?.total ? node.data.total : 0
    },
    showCheckboxPopover() {
      return this.params.column.colDef.showCheckboxPopover
    },
  },
  methods: {
    disabled() {
      const node = this.params.api.getRowNode(0)
      return node?.data?.checkboxAllDisabled
        ? node.data.checkboxAllDisabled
        : false
    },
    checkboxChange() {
      this.submenuVisible = this.showCheckboxPopover && !this.checkAll
      this.$emit('check-all-row', {
        checkedAll: !this.checkAll,
        checkedType: this.fullItems,
      })
    },
    emitEventState() {
      this.$emit('check-all-row', {
        checkedAll: this.checkAll,
        checkedType: this.fullItems,
      })
    },
  },
})
</script>

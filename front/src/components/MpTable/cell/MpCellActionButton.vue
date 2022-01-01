<template>
  <div class="mp-cell__action-button">
    <mp-button
      v-for="buttonName in cellData"
      :key="buttonName"
      :icon="setIcon(buttonName)"
      class="mp-button--cell"
      size="mini"
      circle
      :disabled="setDisabled(buttonName)"
      :title="messageToolTip(buttonName)"
      :mp-type="buttonName"
      @click="emitButtonClick(buttonName)"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import {
  ElementUIIcons,
  IComputedMpCellActionButton,
  IDataMpCellActionButton,
  IMethodsMpCellActionButton,
  IPropsMpCellActionButton,
} from '@/components/MpTable/cell/config/MpCellActionButton.interface'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellActionButton,
  IMethodsMpCellActionButton,
  IComputedMpCellActionButton,
  IPropsMpCellActionButton
>({
  name: 'MpCellActionButton',
  components: {
    MpButton,
  },
  data() {
    return {
      default: new ActionButtons([MpTypeButton.edit, MpTypeButton.remove]),
    }
  },
  computed: {
    cellData() {
      if (this.params.data.action) {
        return this.params.data.action.show
      } else {
        return this.default.show
      }
    },
    disabledList() {
      return this.params.data.action.disabled || this.default.disabled
    },
  },
  methods: {
    setDisabled(buttonName) {
      return this.disabledList.includes(buttonName)
    },
    setIcon(name) {
      if (name === MpTypeButton.add) {
        return ElementUIIcons.plus
      } else if (name === MpTypeButton.remove) {
        return ElementUIIcons.delete
      } else if (name === MpTypeButton.download) {
        return ElementUIIcons.download
      } else if (name === MpTypeButton.link) {
        return ElementUIIcons.link
      } else if (name === MpTypeButton.edit) {
        return ElementUIIcons.edit
      } else {
        return ''
      }
    },
    messageToolTip(buttonName) {
      return this.$t(`button.title.${buttonName}`).toString()
    },
    emitButtonClick(buttonName) {
      this.$parent.$emit(`action-button-click`, {
        buttonType: buttonName,
        rowData: this.params.data,
      })
    },
  },
})
</script>

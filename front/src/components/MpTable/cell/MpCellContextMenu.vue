<template>
  <div class="mp-cell--link">
    <el-popover
      trigger="click"
      placement="bottom"
      v-model="visible"
      popper-class="mp-cell--link__popover"
    >
      <div class="content">
        <h5 class="content__title">
          {{ $t('callTable.MpCellContextMenu.follow') }}
        </h5>
        <ul class="content__list">
          <li
            v-for="link in params.data.links"
            :key="link.value"
            @click="followToNumber(link.value)"
          >
            {{ link.label }}
          </li>
        </ul>
      </div>
      <div slot="reference" class="reference">
        {{ params.value.trim() }}
      </div>
    </el-popover>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpCellContextMenu,
  IDataMpCellContextMenu,
  IMethodsMpCellContextMenu,
  IPropsMpCellContextMenu,
} from '@/components/MpTable/cell/config/MpCellContextMenu.interface'
import { ICellRendererParams } from '@ag-grid-community/core'
/**
 * @description компонент ячейки таблицы с выпадающим меню ссылок.
 * Работает совместно с:
 *              - ContextMenu (модуль);
 */

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellContextMenu,
  IMethodsMpCellContextMenu,
  IComputedMpCellContextMenu,
  IPropsMpCellContextMenu
>({
  name: 'MpCellContextMenu',
  data() {
    return {
      visible: false,
    }
  },
  methods: {
    followToNumber(ext) {
      this.visible = false
      this.$parent.$emit('follow-link', ext)
    },
  },
})
</script>

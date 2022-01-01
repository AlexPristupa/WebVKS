<template>
  <div class="mp-pagination">
    <span
      v-if="position === paginationPosition.top"
      class="mp-pagination__prefix"
      >{{ prefix }}</span
    >
    <el-select
      v-if="position === paginationPosition.top"
      :value="listQuery.limit"
      @change="newPageSize"
      class="mp-pagination__page-size--top"
      placeholder="Select"
      size="mini"
    >
      <el-option v-for="item in pageSizes" :key="item" :value="item">
        <span>{{ `${item} ${$t(`pagination.postfix`)}` }}</span>
      </el-option>
    </el-select>
    <el-pagination
      v-if="position === paginationPosition.bottom"
      small
      :total="total"
      :current-page="listQuery.page"
      :page-size="listQuery.limit"
      :layout="layout"
      @current-change="newCurrentPage"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { CONFIG } from '@/config'
import { paginationParams } from './type'
import paginationGapCalculating from '@/modules/pagination/gapCalculating'
import {
  PaginationLayout,
  PaginationPosition,
} from '@/modules/pagination/Pagination.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export default Vue.extend({
  name: 'MpPagination',
  data() {
    return {
      paginationPosition: PaginationPosition,
    }
  },
  props: {
    position: {
      type: String as () => PaginationPosition,
      default: PaginationPosition.top,
    },
    total: {
      type: Number,
      default: paginationParams.defaultTotal,
    },
    listQuery: {
      type: Object as () => TableDto,
      default: (): TableDto => DtoFactory.create(DtoName.table),
    },
    pageSizes: {
      type: Array,
      default: () => {
        return CONFIG.table.pageSizes
      },
    },
  },
  computed: {
    prefix() {
      return paginationGapCalculating({
        page: this.listQuery.page as number,
        pageSize: this.listQuery.limit as number,
        total: this.total,
      })
    },
    layout() {
      if (this.position === PaginationPosition.top) {
        return PaginationLayout.top
      }
      if (this.position === PaginationPosition.bottom) {
        return PaginationLayout.bottom
      }
      return PaginationLayout.unknown
    },
  },
  methods: {
    newCurrentPage(currentPage) {
      this.$emit('pagination-state', {
        // @ts-ignore
        ...this.listQuery,
        page: currentPage,
      })
    },
    newPageSize(pageSize) {
      this.$emit('pagination-state', {
        // @ts-ignore
        ...this.listQuery,
        limit: pageSize,
        page: paginationParams.defaultPage,
      })
    },
  },
})
</script>

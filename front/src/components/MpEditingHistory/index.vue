<template>
  <FormLayout>
    <el-dialog
      v-el-drag-dialog
      :visible="visible"
      width="70%"
      class="editing-history"
      :destroy-on-close="true"
      :title="
        defaultFilters.length > 1
          ? $t('dialogs.titles.propertyEditingHistory')
          : $t('dialogs.titles.propertiesEditingHistory')
      "
      @close="$emit('close')"
    >
      <mp-table-layout>
        <template #table="propsSlotTable">
          <mp-table
            :table-name="listQuery.tableName"
            :column-defs="columnDefs"
            :height="propsSlotTable.tableHeight"
            :row-data="rowData"
            :loading="loading"
            :list-query="listQuery"
            @sort-by="sort"
            @onFilterChanged="filterChanged"
          />
        </template>
      </mp-table-layout>
      <span slot="footer">
        <mp-pagination
          :position="paginationPositionBottom"
          :total="paginationTotal"
          :list-query="listQuery"
          @pagination-state="setPaginationState"
        />
        <mp-button @click="close">
          {{ $t('general.close') }}
        </mp-button>
      </span>
    </el-dialog>
  </FormLayout>
</template>
<script lang="ts">
/**
 * @description Модальное окно для просмотра истории редактирования свойств основной таблицы
 */
import Vue from 'vue'
// @ts-ignore
import elDragDialog from '@/directive/el-dragDialog'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import { paginationParams } from '@/components/MpPagination/type.ts'
import { IColumnDefs } from '@/components/MpTable/MpTable.interface.ts'
import { COLUMN_DEFS_EDITING_HISTORY_TABLE } from './EditingHistory.const'
import { IDataMpEditingHistory } from './EditingHistory.interface'
import { mapActions } from 'vuex'
import { sortType } from '@/modules/Sort/Sort.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'

export default Vue.extend({
  name: 'MpEditingHistory',
  directives: {
    elDragDialog,
  },
  components: {
    MpTable,
    MpButton,
    FormLayout,
    MpPagination,
    MpTableLayout,
  },
  props: {
    visible: {
      type: Boolean as () => boolean,
      default: false,
    },
    defaultFilters: {
      type: Array as () => Array<IFilterState>,
      default: () => [],
    },
  },
  data(): IDataMpEditingHistory {
    return {
      loading: false,
      rowData: [],
      paginationPositionBottom: PaginationPosition.bottom,
      paginationTotal: 0,
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.editingHistory,
        sortField: 'UserName',
      }),
    }
  },
  computed: {
    columnDefs(): Array<IColumnDefs> {
      return COLUMN_DEFS_EDITING_HISTORY_TABLE.map(
        (columnHeader: IColumnDefs) => {
          const item: IColumnDefs = Object.assign({}, columnHeader)
          item.headerName = this.$t(item.headerName) as string
          return item
        },
      )
    },
  },
  mounted() {
    this.listQuery.filters = this.defaultFilters
    this.getList()
  },
  methods: {
    ...mapActions({
      setActiveFiltersForTable: 'tableHeaderEffects/changeColumnActiveEffects',
    }),
    async getList() {
      this.loading = true
      const res = await this.$api.fetchData({ data: this.listQuery })
      if (res) {
        this.rowData = res.items
        this.paginationTotal = res.total
      }
      this.loading = false
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    setPaginationState(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    sort(data: { field: string; orderBy: sortType }) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    close() {
      this.$emit('close')
    },
  },
})
</script>

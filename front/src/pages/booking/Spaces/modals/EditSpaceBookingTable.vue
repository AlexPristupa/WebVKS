<template>
  <mp-table-layout>
    <template #table>
      <mp-table
        :table-name="listQuery.tableName"
        :column-defs="columnDefs"
        :row-data="rowData"
        :loading="loading"
        :height="height"
      />
    </template>
  </mp-table-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import { editSpaceBookingTableColumns } from '@/pages/booking/Spaces/config/EditSpace.table.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import {
  IDataEditSpaceBookingTable,
  IMethodsEditSpaceBookingTable,
  IComputedEditSpaceBookingTable,
  IPropsEditSpaceBookingTable,
} from '@/pages/booking/Spaces/config/EditSpaceBooking.table.interface'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import { FilterType } from '@/modules/Filters/Filters.const'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity'
import { sortType } from '@/modules/Sort/Sort.const'

export default Vue.extend<
  IDataEditSpaceBookingTable,
  IMethodsEditSpaceBookingTable,
  IComputedEditSpaceBookingTable,
  IPropsEditSpaceBookingTable
>({
  name: 'EditSpaceBookingTable',
  components: {
    MpTable,
    MpTableLayout,
  },
  props: {
    selected: {
      type: Object,
      default: () => new VksSpaceEntity(),
    },
  },
  data() {
    return {
      loading: false,
      height: '300px',
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.editSpaceBooking,
      }),
      rowData: [],
      columnDefs: [],
      selectedEntity: {},
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.editSpaceBooking,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.editSpaceBooking, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
    }
  },
  computed: {
    filterActive() {
      return !this.listQuery.filters?.length
    },
  },
  async created() {
    await this.applyColumnRender()
    await this.getList()
  },
  methods: {
    async getList() {
      this.loading = true
      this.listQuery.filters = [
        {
          filterType: FilterType.select,
          nameField: 'spaceId',
          tableName: this.listQuery.tableName,
          valuesField: [this.selected.id],
        },
      ]
      this.listQuery.sortField = 'currentStatus'
      this.listQuery.orderBy = sortType.asc
      const result = await this.$api.fetchData({ data: this.listQuery })
      if (result) {
        this.rowData = result.items
        this.paginationTotal = result.total
      }
      this.loading = false
    },
    async applyColumnRender() {
      this.columnDefs = []
      const columnRender = editSpaceBookingTableColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    search() {},
    sortChanged() {},
    paginationChanged() {},
    filterChanged() {},
    savedFilterChange() {},
    checkRow() {},
    checkAllRow() {},
    prepareCheckedToDelete() {},
    rowSelected() {},
    emitTableDto() {},
    handlerRemove() {},
    actionsHandler() {},
    addEntity() {},
    editEntity() {},
    deleteEntity() {},
  },
})
</script>

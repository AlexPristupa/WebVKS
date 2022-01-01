<template>
  <mp-table-layout>
    <template #search>
      <mp-search
        ref="search"
        :extension-search-string="listQuery.TableSearchBy"
        @search="search"
      />
    </template>
    <template #buttons>
      <mp-button
        is-icon
        :disabled="loading"
        :title="$t('button.title.add')"
        mp-type="add"
        mp-status="normal"
        @click="addEntity"
      />
    </template>
    <template #table>
      <mp-table
        :table-name="listQuery.tableName"
        :column-defs="columnDefs"
        :row-data="rowData"
        :loading="loading"
        :height="height"
        @action-button-click="actionsHandler"
        @one-of-checkboxes-changed="$emit('table-checkboxes-changed', $event)"
        @select-changed="$emit('table-select-changed', $event)"
      />
    </template>
  </mp-table-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import { editSpaceParticipantsTableColumns } from '@/pages/booking/Spaces/config/EditSpace.table.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import {
  IComputedEditSpaceParticipantsTable,
  IDataEditSpaceParticipantsTable,
  IMethodsEditSpaceParticipantsTable,
  IPropsEditSpaceParticipantsTable,
} from '@/pages/booking/Spaces/config/EditSpaceParticipants.table.interface'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity'

export default Vue.extend<
  IDataEditSpaceParticipantsTable,
  IMethodsEditSpaceParticipantsTable,
  IComputedEditSpaceParticipantsTable,
  IPropsEditSpaceParticipantsTable
>({
  name: 'EditSpaceParticipantsTable',
  components: {
    MpButton,
    MpTable,
    MpSearch,
    MpTableLayout,
  },
  props: {
    selected: {
      type: Object as () => VksSpaceEntity,
      default: () => new VksSpaceEntity(),
    },
    tableRows: {
      type: Array as () => Array<any>,
      default: () => [],
    },
  },
  data() {
    return {
      searchString: '',
      loading: false,
      height: '300px',
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.editSpaceParticipants,
      }),
      rowData: [],
      columnDefs: [],
      selectedEntity: {},
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.editSpaceParticipants,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked(
        [],
        TableName.editSpaceParticipants,
        0,
      ),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
    }
  },
  computed: {
    filterActive() {
      return !this.listQuery.filters?.length
    },
  },
  watch: {
    tableRows: {
      handler: function() {
        this.getList()
      },
      deep: true,
    },
  },
  async mounted() {
    await this.applyColumnRender()
  },
  methods: {
    async getList() {
      this.loading = true
      this.rowData = !this.searchString
        ? JSON.parse(JSON.stringify(this.tableRows))
        : JSON.parse(JSON.stringify(this.tableRows?.filter(row => row.found)))
      this.loading = false
    },
    async applyColumnRender() {
      this.columnDefs = []
      const columnRender = editSpaceParticipantsTableColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    search(value) {
      this.searchString = value.searchString.toLowerCase()
      this.$emit('search', this.searchString)
    },
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
    actionsHandler(data) {
      this.$emit('delete', data.rowData.id)
    },
    addEntity() {
      // @ts-ignore
      this.$refs['search'].search = ''
      this.searchString = ''
      this.$emit('add-row')
    },
    editEntity() {},
    deleteEntity() {},
  },
})
</script>

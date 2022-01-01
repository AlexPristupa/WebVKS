<template>
  <mp-table-layout>
    <form-layout>
      <add-user-to-share-records-modal
        v-if="visibleEditModal"
        :selected-entity="selected"
        :visible-edit-modal="visibleEditModal"
        @update="getList"
        @close="visibleEditModal = false"
      />
    </form-layout>

    <template #search>
      <mp-search
        ref="search"
        :extension-search-string="listQuery.TableSearchBy"
        @search="search"
      />
    </template>

    <template #pagination-top>
      <mp-pagination
        :position="paginationPosition.top"
        :total="paginationTotal"
        :list-query="listQuery"
        @pagination-state="paginationChanged"
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
        :list-query="listQuery"
        :column-defs="columnDefs"
        :row-data="rowData"
        :loading="loading"
        :height="height"
        @check-row="checkRow"
        @sort-by="sortChanged"
        @onRowSelected="rowSelected"
        @onFilterChanged="filterChanged"
        @action-button-click="actionsHandler"
        @columns-settings-changed="applyColumnRender"
      />
    </template>

    <template #pagination-bottom>
      <mp-pagination
        :position="paginationPosition.bottom"
        :total="paginationTotal"
        :list-query="listQuery"
        @pagination-state="paginationChanged"
      />
    </template>
  </mp-table-layout>
</template>
<script lang="ts">
import Vue from 'vue'
import {
  IComputedShareRecordModalTable,
  IDataShareRecordModalTable,
  IMethodsShareRecordModalTable,
  IPropsShareRecordModalTable,
} from '@/pages/booking/Recordings/modals/config/shareRecordModalTable.interface'
import AddUserToShareRecordsModal from './AddUserToShareRecords.modal.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { shareRecordModalTableColumns } from '@/pages/booking/Recordings/modals/config/shareRecordModalTable.const'
import { paginationParams } from '@/components/MpPagination/type'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import { FilterType } from '@/modules/Filters/Filters.const'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import { methods } from '@/api_services/httpMethods.enum'
import { DateTime } from '@/modules/DateTime/DateTime'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { setFilter } from '@/modules/Filters/SetTableFilters'
import { ISelectFilterState } from '@/modules/Filters/SelectFilter/SelectFilter.interface'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

export default Vue.extend<
  IDataShareRecordModalTable,
  IMethodsShareRecordModalTable,
  IComputedShareRecordModalTable,
  IPropsShareRecordModalTable
>({
  name: 'ShareRecordModalTable',
  components: {
    MpTable,
    MpSearch,
    MpButton,
    MpPagination,
    MpTableLayout,
    FormLayout,
    AddUserToShareRecordsModal,
  },
  props: {
    selected: {
      type: Object,
      default: () => new VksRecordingsEntity(),
    },
  },
  data() {
    return {
      loading: false,
      height: '300px',
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.recordingsShare,
      }),
      rowData: [],
      columnDefs: [],
      selectedEntity: {},
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.recordingsShare,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.recordingsShare, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
    }
  },
  async mounted() {
    const filter: ISelectFilterState = {
      filterType: FilterType.select,
      nameField: 'id',
      tableName: this.listQuery.tableName,
      valuesField: [this.selected.id],
    }
    this.listQuery = setFilter(this.listQuery, this.listQuery.tableName, filter)
    await this.applyColumnRender()
    await this.getList()
  },
  methods: {
    async getList() {
      this.loading = true
      const result = await this.$api.fetchData({ data: this.listQuery })
      if (result) {
        this.rowData = result.items
        this.paginationTotal = result.total
      }
      this.loading = false
    },
    async applyColumnRender() {
      this.columnDefs = []
      const columnRender = shareRecordModalTableColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    search(value) {
      this.listQuery.page = paginationParams.defaultPage
      this.listQuery.tableSearchBy = value.searchString
      this.getList()
    },
    paginationChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    savedFilterChange() {},
    async checkRow(data) {
      const checkedRow = this.rowData.find(
        row => row.recordingVksUsersId === data.row.recordingVksUsersId,
      )
      const reqData = {
        ...checkedRow,
        id: checkedRow.recordingVksUsersId,
        recordingId: checkedRow.id,
        dateRecord: new DateTime({ dateTime: checkedRow.dateRecord }).toISO(),
        userId: checkedRow.userId,
        [data.column]: !checkedRow[data.column],
      }
      delete reqData.action
      delete reqData.recordingVksUsersId
      delete reqData.checkbox
      delete reqData.fieldsSetting
      delete reqData.user
      const res = await this.$api.recordingsUsers({
        method: methods.put,
        data: reqData,
      })
      if (res && !res.validation) {
        this.$message.success(this.$t('notifications.data.updated') as string)
        await this.getList()
      } else {
        const validationWithoutField = FormValidation.backValidationWithoutField(
          res.validation,
        )
        if (validationWithoutField) {
          this.$message.error(validationWithoutField)
        }
      }
    },
    checkAllRow() {},
    prepareCheckedToDelete() {},
    rowSelected() {},
    emitTableDto() {},
    handlerRemove() {},
    actionsHandler(data) {
      if (data.buttonType === MpTypeButton.remove) {
        this.deleteEntity(data.rowData)
      }
    },
    addEntity() {
      this.visibleEditModal = true
    },
    editEntity() {},
    async deleteEntity(row) {
      this.loadingDelete = true
      const result = await this.$api.recordingsUsers({
        method: methods.delete,
        params: { id: row.recordingVksUsersId },
      })
      this.loadingDelete = false
      if (result && !result.validation) {
        await this.getList()
      }
    },
  },
})
</script>

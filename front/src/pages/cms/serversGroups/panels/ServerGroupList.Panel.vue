<template>
  <mp-table-layout :title="$t('panels.title.serversGroups')">
    <form-layout>
      <edit-server-groups-modal
        v-if="visibleEditModal"
        :visible-edit-modal="visibleEditModal"
        :selected-entity="selectedEntity"
        @close="visibleEditModal = false"
        @update="getList"
      />
    </form-layout>

    <template #search>
      <mp-search
        ref="search"
        :extension-search-string="listQuery.tableSearchBy"
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
        mp-type="add"
        mp-status="normal"
        @click="addEntity"
      />
    </template>

    <template #table="propsSlotTable">
      <mp-table
        :table-name="listQuery.tableName"
        :column-defs="columnDefs"
        :row-data="rowData"
        :height="propsSlotTable.tableHeight"
        :loading="loading"
        :list-query="listQuery"
        :disabled="false"
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

    <mp-confirm-modal
      :title="$t('confirm.serversGroups.delete.title')"
      :text="[$t('confirm.serversGroups.delete.text', [checked])]"
      :visible-confirm-modal.sync="visibleConfirmDeleteModal"
      :loading="loadingDelete"
      @confirm="deleteEntity"
      @close="visibleConfirmDeleteModal = false"
    />
  </mp-table-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
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
import { paginationParams } from '@/components/MpPagination/type'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import { FilterType } from '@/modules/Filters/Filters.const'
import {
  IComputedServerGroupListPanel,
  IDataServerGroupListPanel,
  IMethodsServerGroupListPanel,
  IPropsServerGroupListPanel,
} from '@/pages/cms/serversGroups/config/serverGroupListPanel.interface'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import EditServerGroupsModal from '@/pages/cms/serversGroups/modals/EditServerGroups.Modal.vue'
import { serverGroupListColumns } from '@/pages/cms/serversGroups/config/serverGroupListPanel.const'
import { VksServersGroupEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServersGroup.entity'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'

export default Vue.extend<
  IDataServerGroupListPanel,
  IMethodsServerGroupListPanel,
  IComputedServerGroupListPanel,
  IPropsServerGroupListPanel
>({
  name: 'ServerGroupListPanel',
  components: {
    FormLayout,
    EditServerGroupsModal,
    MpConfirmModal,
    MpPagination,
    MpTable,
    MpButton,
    MpSearch,
    MpTableLayout,
  },
  data() {
    return {
      loading: false,
      loadingDelete: false,
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.serversGroups,
      }),
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.serversGroups,
        limit: null,
        page: null,
      }),
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      rowData: [],
      columnDefs: [],
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.serversGroups, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
      selectedEntity: new VksServersGroupEntity(),
    }
  },
  async mounted() {
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
      } else {
        this.rowData = []
      }
      this.loading = false
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
    async applyColumnRender() {
      this.columnDefs = []
      const columnRender = serverGroupListColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    addEntity() {
      this.selectedEntity = new VksServersGroupEntity()
      this.visibleEditModal = true
    },
    editEntity(row) {
      this.selectedEntity = new VksServersGroupEntity(row)
      this.visibleEditModal = true
    },
    prepareCheckedToDelete(type) {
      this.checked = [this.selectedEntity.name]
      if (type === HeaderCheckboxState.page) {
        this.listQueryToDelete.filters = [
          {
            filterType: FilterType.select,
            nameField: 'id',
            tableName: this.listQuery.tableName,
            valuesField: this.checked,
          },
        ]
      }
    },
    rowSelected(params) {
      this.selectedEntity = new VksServersGroupEntity(params.data)
      this.prepareCheckedToDelete(HeaderCheckboxState.page)
      this.$emit('setServersFilter', this.selectedEntity.id)
    },
    emitTableDto() {
      this.$emit('change-server-list-table-dto', {
        dto: this.listQuery,
      })
    },
    handlerRemove() {
      this.visibleConfirmDeleteModal = true
    },
    async deleteEntity() {
      this.loadingDelete = true
      const result = true
      // const result = await this.$api.vksCmsServerGroup({
      //   method: methods.delete,
      //   data: this.listQueryToDelete,
      // })
      this.loadingDelete = false
      if (result) {
        this.visibleConfirmDeleteModal = false
        // await this.getList()
      }
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    actionsHandler(data) {
      if (data.buttonType === MpTypeButton.edit) {
        this.editEntity(data.rowData)
      } else {
        this.visibleConfirmDeleteModal = true
      }
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    savedFilterChange() {},
    checkRow() {},
    checkAllRow() {},
  },
})
</script>

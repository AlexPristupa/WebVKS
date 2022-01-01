<template>
  <mp-table-layout :title="$t('panels.title.cmsServers')">
    <form-layout>
      <edit-server-modal
        v-if="visibleEditModal"
        :visibleEditModal="visibleEditModal"
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
      :title="$t('confirm.server.delete.title')"
      :text="[$t('confirm.server.delete.text', [checked])]"
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
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { paginationParams } from '@/components/MpPagination/type'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import {
  IComputedServerListPanel,
  IDataServerListPanel,
  IMethodsServerListPanel,
  IPropsServerListPanel,
} from '@/pages/cms/cmsServers/config/serverListPanel.interface'
import EditServerModal from '@/pages/cms/cmsServers/modals/EditServer.Modal.vue'
import { serverListColumns } from '@/pages/cms/cmsServers/config/serverList.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { FilterType } from '@/modules/Filters/Filters.const'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import { setFilter } from '@/modules/Filters/SetTableFilters'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'

export default Vue.extend<
  IDataServerListPanel,
  IMethodsServerListPanel,
  IComputedServerListPanel,
  IPropsServerListPanel
>({
  name: 'ServerListPanel',
  components: {
    FormLayout,
    MpConfirmModal,
    EditServerModal,
    MpSearch,
    MpButton,
    MpTable,
    MpPagination,
    MpTableLayout,
  },
  data() {
    return {
      loading: false,
      loadingDelete: false,
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.servers,
      }),
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.servers,
        limit: null,
        page: null,
      }),
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      rowData: [],
      columnDefs: [],
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.servers, 0),
      externalFilter: {},
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
      selectedEntity: new VksServerEntity(),
    }
  },
  watch: {
    externalFilter(value) {
      if (value) {
        const filter = {
          filterType: FilterType.select,
          nameField: 'serversGroupsId',
          tableName: this.listQuery.tableName,
          valuesField: [value],
        }
        // @ts-ignore
        this.$refs['search'].search = ''
        const dto = setFilter(this.listQuery, this.listQuery.tableName, filter)
        this.filterChanged({ ...dto, tableSearchBy: '' })
      }
    },
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
        this.rowDataChecked.init(
          result.items,
          result.items.length,
          result.total,
        )
        this.rowData = this.rowDataChecked.rowData
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
      const columnRender = serverListColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    addEntity() {
      this.selectedEntity = new VksServerEntity()
      this.visibleEditModal = true
    },
    editEntity(row) {
      this.selectedEntity = new VksServerEntity(row)
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
      this.selectedEntity = new VksServerEntity(params.data)
      this.prepareCheckedToDelete(HeaderCheckboxState.page)
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
      // const result = await this.$api.vksCmsServer({
      //   method: methods.delete,
      //   data: this.listQueryToDelete,
      // })
      this.loadingDelete = false
      if (result) {
        this.visibleConfirmDeleteModal = false
        // await this.getList()
      }
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    actionsHandler(data) {
      if (data.buttonType === MpTypeButton.edit) {
        this.editEntity(data.rowData)
      } else {
        this.visibleConfirmDeleteModal = true
      }
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    savedFilterChange() {},
    checkRow() {},
    checkAllRow() {},
  },
})
</script>

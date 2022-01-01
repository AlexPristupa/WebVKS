<template>
  <mp-table-layout :title="$t('panels.title.userProfile')">
    <template #select>
      <MpSavingFilter
        :list-query="listQuery"
        :disable-button="filterActive"
        @onSelectFilterChange="savedFilterChange"
      />
    </template>

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
        :disabled="loading || true"
        :title="$t('button.title.add')"
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
        @action-button-click="actionsHandler"
        @columns-settings-changed="applyColumnRender"
        @onFilterChanged="filterChanged"
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
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import { paginationParams } from '@/components/MpPagination/type'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import { TableName } from '@/modules/table_grid/TableName.const'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { PaginationPosition } from '@/modules/pagination/Pagination.const.ts'
import MpSavingFilter from '@/components/MpSavingFilter/MpSavingFilter.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender.ts'
import { mapActions } from 'vuex'
import { userProfileListColumns } from '../config/userProfileListPanel.const'
import {
  IComputedUserProfileListPanel,
  IDataUserProfileListPanel,
  IMethodsUserProfileListPanel,
  IPropsUserProfileListPanel,
} from '../config/userProfileListPanel.interface'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export default Vue.extend<
  IDataUserProfileListPanel,
  IMethodsUserProfileListPanel,
  IComputedUserProfileListPanel,
  IPropsUserProfileListPanel
>({
  name: 'UserProfileListPanel',
  components: {
    MpTable,
    MpButton,
    MpSearch,
    MpPagination,
    MpTableLayout,
    MpSavingFilter,
  },
  computed: {
    filterActive() {
      return !this.listQuery.filters?.length
    },
  },
  data() {
    return {
      loading: false,
      listQuery: DtoFactory.create(DtoName.table, {
        tableName: TableName.userProfiles,
      }),
      rowData: [],
      columnDefs: [],
      // пока что свойства не нужны
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.userProfiles,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.userProfiles, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
      selectedEntity: null,
    }
  },
  async created() {
    await this.applyColumnRender()
    await this.getList()
  },
  methods: {
    ...mapActions({
      setActiveFiltersForTable: 'tableHeaderEffects/changeColumnActiveEffects',
    }),
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
      const columnRender = userProfileListColumns
      const cdColumnRender = new ColumnDefsBaseCellRender(
        this.columnDefs,
        this.listQuery.tableName,
      )
      cdColumnRender.Ready.then(() => {
        this.columnDefs = cdColumnRender.applyColumnRender(columnRender)
      })
    },
    filterChanged(listQuery) {
      this.listQuery = listQuery
      this.getList()
    },
    savedFilterChange(filters) {
      this.listQuery.filters = filters
      this.setActiveFiltersForTable({
        tableName: this.listQuery.tableName,
        effects: { filters: this.listQuery.filters },
      })
      this.getList()
    },
    addEntity() {},
    editEntity() {},
    actionsHandler(data) {
      if (data.buttonType === MpTypeButton.edit) {
        this.editEntity(data.rowData)
      } else if (data.buttonType === MpTypeButton.play) {
        return
      } else {
        return
      }
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    deleteEntity() {},
    checkRow() {},
    checkAllRow() {},
    prepareCheckedToDelete() {},
    rowSelected() {},
    emitTableDto() {},
    handlerRemove() {},
  },
})
</script>

<template>
  <mp-table-layout :title="$t('panels.title.spaces')">
    <form-layout>
      <edit-space-modal
        v-if="visibleEditModal"
        :visibleEditModal="visibleEditModal"
        :selected-entity="selectedEntity"
        @close="visibleEditModal = false"
        @update="getList"
      />
    </form-layout>
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
      v-if="visibleConfirmDeleteModal"
      :title="$t('confirm.spaces.delete.title')"
      :text="[
        $t('confirm.spaces.delete.text', [this.selectedEntity.name || '']),
      ]"
      :visible-confirm-modal="visibleConfirmDeleteModal"
      :loading="loadingDelete"
      @confirm="deleteEntity"
      @close="visibleConfirmDeleteModal = false"
    />
  </mp-table-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSearch from '@/components/basic/MpSearch/MpSearch.vue'
import MpTableLayout from '@/layouts/tableLayout/MpTableLayout.vue'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import MpSavingFilter from '@/components/MpSavingFilter/MpSavingFilter.vue'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import { PaginationPosition } from '@/modules/pagination/Pagination.const'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { mapActions } from 'vuex'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { paginationParams } from '@/components/MpPagination/type'
import { bookingListColumns } from '@/pages/booking/Bookings/config/bookingListPanel.const'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender'
import EditSpaceModal from '../modals/EditSpace.modal.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import {
  IComputedSpacesListPanel,
  IDataSpacesListPanel,
  IMethodsSpacesListPanel,
  IPropsSpacesListPanel,
} from '@/pages/booking/Spaces/config/SpacesListPanel.interface'
import { VksSpaceTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksSpaceTable.entity'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import { FilterType } from '@/modules/Filters/Filters.const'
import { VksBookingTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksBookingTable.entity.ts'
import { methods } from '@/api_services/httpMethods.enum'
import { TIMEOUT_SERVICE } from '@/constant'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

export default Vue.extend<
  IDataSpacesListPanel,
  IMethodsSpacesListPanel,
  IComputedSpacesListPanel,
  IPropsSpacesListPanel
>({
  name: 'SpacesListPanel',
  components: {
    MpTable,
    MpButton,
    MpSearch,
    FormLayout,
    MpPagination,
    MpTableLayout,
    MpSavingFilter,
    EditSpaceModal,
    MpConfirmModal,
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
        tableName: TableName.spaces,
      }),
      rowData: [],
      columnDefs: [],
      selectedEntity: new VksBookingTableEntity(),
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.spaces,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.spaces, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
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
      const columnRender = bookingListColumns
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
    addEntity() {
      this.selectedEntity = new VksSpaceTableEntity()
      this.visibleEditModal = true
    },
    editEntity(row) {
      this.selectedEntity = new VksSpaceTableEntity(row)
      this.visibleEditModal = true
    },
    async deleteEntity() {
      this.loadingDelete = true
      const res = await this.$api.spaces({
        method: methods.get,
        data: {
          id: this.listQueryToDelete.filters[0].valuesField[0],
        },
      })
      if (res) {
        const isServiceOn = await this.$api.proxyBooking({
          method: methods.get,
          timeout: TIMEOUT_SERVICE,
        })
        if (isServiceOn) {
          await this.$api.proxyBookingSpaceDelete({
            method: methods.delete,
            data: {
              id: res.id,
              serversGroupsId: res.serversGroupsId,
              guid: res.guid,
            },
          })
        }
        const result = await this.$api.spaces({
          method: methods.delete,
          params: { id: res.id },
        })
        if (result && !result.validation) {
          await this.getList()
        } else {
          const validationWithoutField = FormValidation.backValidationWithoutField(
            result.validation,
          )
          if (validationWithoutField) {
            this.$message.error(validationWithoutField)
          }
        }
        this.visibleConfirmDeleteModal = false
        this.loadingDelete = false
      }
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
    checkRow() {},
    checkAllRow() {},
    prepareCheckedToDelete(type) {
      this.checked = [this.selectedEntity.id]
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
    rowSelected(row) {
      this.selectedEntity = new VksSpaceTableEntity(row.data)
      this.prepareCheckedToDelete(HeaderCheckboxState.page)
    },
    emitTableDto() {},
    handlerRemove() {},
  },
})
</script>

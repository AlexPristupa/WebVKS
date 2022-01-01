<template>
  <mp-table-layout :title="$t('panels.title.bookings')">
    <form-layout>
      <edit-booking-modal
        v-if="visibleEditModal"
        :visibleEditModal="visibleEditModal"
        :selected-entity="selectedEntity"
        :permanent="bookPermanentConference"
        @refresh-service="refreshServiceData"
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
      <el-dropdown
        trigger="click"
        placement="bottom"
        v-model="editPopoverVisible"
        @command="addEntity"
      >
        <mp-button
          is-icon
          :disabled="loading"
          :title="$t('button.title.book')"
          mp-type="add"
          mp-status="normal"
        />
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item :command="{ permanent: false }">
            {{ $t('dialogs.texts.bookConference') }}
          </el-dropdown-item>
          <el-dropdown-item :command="{ permanent: true }">
            {{ $t('dialogs.texts.bookPermanentConference') }}
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
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
        @on-timer-finished="toggleTimer"
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
      :title="$t('confirm.booking.delete.title')"
      :text="[$t('confirm.booking.delete.text', [selectedEntity.name])]"
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
import { paginationParams } from '@/components/MpPagination/type'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableName } from '@/modules/table_grid/TableName.const'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpPagination from '@/components/MpPagination/MpPagination.vue'
import { PaginationPosition } from '@/modules/pagination/Pagination.const.ts'
import MpSavingFilter from '@/components/MpSavingFilter/MpSavingFilter.vue'
import MpTable from '@/components/MpTable/mpTable.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import ColumnDefsBaseCellRender from '@/modules/table_grid/TableGridColumnsDefs/Classes/ColumnDefsBaseCellRender.ts'
import { mapActions } from 'vuex'
import {
  bookingListColumns,
  currentStatusEnum,
} from '@/pages/booking/Bookings/config/bookingListPanel.const'
import {
  IComputedBookingListPanel,
  IDataBookingListPanel,
  IMethodsBookingListPanel,
  IPropsBookingListPanel,
} from '@/pages/booking/Bookings/config/bookingListPanel.interface'
import { HeaderCheckboxState } from '@/components/MpTable/header/MpCellHeaderCheckbox.const'
import { RowDataChecked } from '@/modules/table_grid/RowDataChecked/RowDataChecked'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import EditBookingModal from '../modals/EditBooking.modal.vue'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import { VksBookingTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksBookingTable.entity'
import { methods } from '@/api_services/httpMethods.enum'
import { periodicTypes } from '@/pages/booking/Bookings/modals/config/editBookingModal.const'
import { User } from '@/modules/User/User'
import { TIMEOUT_SERVICE, TIMEOUT_SERVICE_SHORT } from '@/constant'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

export default Vue.extend<
  IDataBookingListPanel,
  IMethodsBookingListPanel,
  IComputedBookingListPanel,
  IPropsBookingListPanel
>({
  name: 'BookingListPanel',
  components: {
    MpTable,
    MpButton,
    MpSearch,
    FormLayout,
    MpPagination,
    MpTableLayout,
    MpConfirmModal,
    MpSavingFilter,
    EditBookingModal,
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
        tableName: TableName.booking,
      }),
      rowData: [],
      columnDefs: [],
      // пока что свойства не нужны
      loadingDelete: false,
      paginationPosition: PaginationPosition,
      paginationTotal: 0,
      listQueryToDelete: DtoFactory.create(DtoName.table, {
        tableName: TableName.booking,
        limit: null,
        page: null,
      }),
      checked: [],
      checkedType: HeaderCheckboxState.page,
      rowDataChecked: new RowDataChecked([], TableName.booking, 0),
      visibleEditModal: false,
      visibleConfirmDeleteModal: false,
      editPopoverVisible: false,
      selectedEntity: null,

      bookPermanentConference: false,
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
        this.rowData = result.items.map(row => {
          if (row.currentStatus) {
            const isActive = row.currentStatus === currentStatusEnum.start
            row.fieldsSetting.counter.display = true
            row.fieldsSetting.counter.fieldTo = isActive ? 'dateEnd' : 'nextRun'
            row.action = new ActionButtons(
              [
                MpTypeButton.play,
                MpTypeButton.stop,
                MpTypeButton.edit,
                MpTypeButton.remove,
              ],
              isActive ? [MpTypeButton.play] : [MpTypeButton.stop],
            )
          } else {
            row.action = new ActionButtons(
              [
                MpTypeButton.play,
                MpTypeButton.stop,
                MpTypeButton.edit,
                MpTypeButton.remove,
              ],
              [MpTypeButton.play, MpTypeButton.stop],
            )
          }
          return row
        })
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
    addEntity(setting) {
      if (setting) {
        this.bookPermanentConference = setting.permanent
      }
      this.selectedEntity = new VksBookingTableEntity()
      this.visibleEditModal = true
    },
    editEntity(row) {
      this.bookPermanentConference = row.type === periodicTypes.constantRU
      this.selectedEntity = new VksBookingTableEntity(row)
      this.visibleEditModal = true
    },
    actionsHandler(data) {
      if (data.buttonType === MpTypeButton.edit) {
        this.editEntity(data.rowData)
      } else if (data.buttonType === MpTypeButton.play) {
        this.switchStatusTo(currentStatusEnum.start, data.rowData.id)
      } else if (data.buttonType === MpTypeButton.stop) {
        this.switchStatusTo(currentStatusEnum.stop, data.rowData.id)
      } else {
        this.selectedEntity = new VksBookingTableEntity(data.rowData)
        this.visibleConfirmDeleteModal = true
      }
    },
    async switchStatusTo(status, id) {
      let res
      const user = User.getUserName()
      const data = {
        bookingId: id,
        userLogin: user,
      }
      if (status === currentStatusEnum.stop) {
        res = await this.$api.proxyBookingStop({
          timeout: TIMEOUT_SERVICE,
          method: methods.post,
          data: data,
        })
      } else {
        res = await this.$api.proxyBookingStart({
          timeout: TIMEOUT_SERVICE,
          method: methods.post,
          data: data,
        })
      }
      if (res) {
        // Бизнес логика: сервис не успевает обновить состояние до загрузки данных, поэтому ждем его 3 секунды
        this.loading = true
        setTimeout(async () => {
          await this.getList()
        }, TIMEOUT_SERVICE_SHORT)
      }
    },
    sortChanged(data) {
      this.listQuery.sortField = data.field
      this.listQuery.orderBy = data.orderBy
      this.listQuery.page = paginationParams.defaultPage
      this.getList()
    },
    rowSelected(row) {
      this.selectedEntity = new VksBookingTableEntity(row.data)
    },
    async deleteEntity() {
      this.loadingDelete = true
      const result = await this.$api.booking({
        method: methods.delete,
        params: { id: this.selectedEntity.id },
      })
      this.loadingDelete = false
      if (result && !result.validation) {
        await this.refreshServiceData(this.selectedEntity.id)
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
    },
    prepareCheckedToDelete() {},
    checkRow() {},
    checkAllRow() {},
    emitTableDto() {},
    handlerRemove() {},
    async refreshServiceData(id) {
      if (id) {
        await this.$api.proxyBookingRefresh({
          timeout: TIMEOUT_SERVICE,
          method: methods.post,
          data: { bookingId: id },
        })
      }
    },
    async toggleTimer() {
      await this.getList()
    },
  },
})
</script>

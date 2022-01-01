<template>
  <div class="header-select">
    <el-popover
      :append-to-body="false"
      placement="bottom"
      width="245"
      v-model="filterPopoverVisible"
      class="header-select__popover"
      trigger="click"
    >
      <MpSavingFilterList
        :filters="filters"
        :filter-id="currentFilter.id"
        :loading="loading"
        @edit-filter="editFilter"
        @delete-filters="deleteFilter"
        @set-current-filter="setCurrentFilter"
      />
      <el-button slot="reference" class="header-select__open-button">
        <span>{{ currentFilter.title }}</span>
        <i
          :class="{
            'el-icon-arrow-down': !filterPopoverVisible,
            'el-icon-arrow-up': filterPopoverVisible,
          }"
        />
      </el-button>
    </el-popover>
    <MpButton
      :mpType="MpTypeButton.save"
      :disabled="disableButton"
      is-icon
      @click="toggleConfirm(true)"
    />
    <MpConfirmModal
      :visible-confirm-modal="visibleConfirmModal"
      :text="[$t('filter.selectSaveFilters.questionSaveFilters')]"
      :title="$t('filter.selectSaveFilters.titleSaveFilters')"
      :submit-text="$t('filter.mentolTable.createFilterButton')"
      :close-text="$t('filter.selectSaveFilters.btnRewrite')"
      :close-disabled="!currentFilter.value || currentFilter.value === '-1'"
      submit-mp-type="create"
      close-mp-type="rewrite"
      @confirm="toggleDialogSaveFilter(true)"
      @optionalEvent="rewriteFilter"
      @close="toggleConfirm"
    />
    <MpSavingFilterDialog
      :visible="savingFilterDialogVisible"
      :listQuery="listQuery"
      @create-filter="createFilter"
      @close="toggleDialogSaveFilter"
    />
  </div>
</template>

<script lang="ts">
import MpSavingFilterDialog from './MpSavingFilterDialog.vue'
import MpConfirmModal from '@/components/basic/MpConfirmModal/MpConfirmModal.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSavingFilterList from './MpSavingFilterList.vue'
import Vue from 'vue'
import { methods } from '@/api_services/httpMethods.enum'
import { IDtoSavingFilter } from '@/components/MpSavingFilter/MpSavingFilter.interface'
import { ISavedFilterHeader } from '@/modules/Filters/SavingFilter/SavingFilter.interface.ts'
import {
  SavingFilterFunctionType,
  SavingFilterIsCommonStatus,
} from '@/components/MpSavingFilter/MpSavingFilter.const'
import SavingFilter from '@/modules/Filters/SavingFilter/SavingFilter'
import { TranslateResult } from 'vue-i18n'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export default Vue.extend({
  name: 'MpSavingFilter',
  components: {
    MpButton,
    MpConfirmModal,
    MpSavingFilterDialog,
    MpSavingFilterList,
  },
  data() {
    return {
      filters: [] as Array<ISavedFilterHeader>,
      savingFilterDialogVisible: false,
      visibleConfirmModal: false,
      currentFilter: {
        title: this.$t('filter.selectFilter.noFilter') as TranslateResult,
        disabled: true,
        group: null,
        selected: true,
        value: [],
        id: -1,
      } as ISavedFilterHeader,
      filterPopoverVisible: false,
      MpTypeButton: MpTypeButton,
      loading: false,
      savingFilter: {} as SavingFilter,
    }
  },
  props: {
    disableButton: {
      type: Boolean,
      default: false,
    },
    listQuery: {
      type: Object as () => TableDto,
      default: (): TableDto => {
        return DtoFactory.create(DtoName.table)
      },
    },
  },
  watch: {
    filterPopoverVisible(isVisible) {
      this.filters = []
      if (isVisible) {
        this.getFiltersList()
      }
    },
  },
  methods: {
    async getFiltersList() {
      this.loading = true
      this.savingFilter = await SavingFilter.initAsync(this.listQuery.tableName)
      if (this.savingFilter.getList) {
        this.filters = this.savingFilter.getList
      }
      this.loading = false
    },

    toggleDialogSaveFilter(value: boolean): void {
      this.savingFilterDialogVisible = value
      if (this.visibleConfirmModal) {
        this.toggleConfirm(false)
      }
    },

    toggleConfirm(value: boolean): void {
      this.visibleConfirmModal = value
    },

    async rewriteFilter() {
      if (this.currentFilter.value) {
        const dtoRewriteFilter: IDtoSavingFilter = {
          listQuery: this.listQuery,
          func: SavingFilterFunctionType.rewrite,
          newName: '',
          iscommon: SavingFilterIsCommonStatus.yes,
          filterId: this.currentFilter.id,
        }
        const res = await this.$api.saveFilter({
          method: methods.post,
          data: dtoRewriteFilter,
        })
        if (res) {
          this.$message.success(this.$t('notifications.data.saved') as string)
        }
      }
    },

    setCurrentFilter(filterHeader: ISavedFilterHeader) {
      this.currentFilter = filterHeader
      this.filterPopoverVisible = false
      this.$emit('onSelectFilterChange', filterHeader.value)
    },

    async editFilter(filter: ISavedFilterHeader) {
      await this.savingFilter.updateDataFilterById(+filter.id)
      this.currentFilter = filter
    },

    async createFilter(filterHeaderId: ISavedFilterHeader['id']) {
      await this.getFiltersList()
      const createdFilter: ISavedFilterHeader | undefined = this.filters.find(
        filter => +filter.id === filterHeaderId,
      )
      if (createdFilter) {
        this.currentFilter = createdFilter
      }
    },

    deleteFilter() {
      this.filterPopoverVisible = false
      this.currentFilter = {
        title: this.$t('filter.selectFilter.noFilter') as TranslateResult,
        disabled: true,
        group: null,
        selected: true,
        value: [],
        id: -1,
      } as ISavedFilterHeader
      this.getFiltersList()
      this.$emit('onSelectFilterChange', [])
    },
  },
})
</script>

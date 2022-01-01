<template>
  <div class="filter-list" v-loading="loading">
    <el-radio-group
      v-for="(filter, index) in filters"
      :key="index"
      :value="filterId"
    >
      <el-radio
        :value="filter.selected"
        :label="filter.id"
        @change="setFilter(filter)"
      >
        <span class="title" v-if="!filter.group">
          {{ filter.title ? filter.title : '' }}
        </span>
        <el-input v-else v-model="filter.title" size="mini" />
      </el-radio>
      <div class="filter-list__buttons">
        <mp-button
          v-if="!filter.group && filter.id !== -1"
          @click="filter.group = true"
          :mp-type="MpTypeButton.edit"
          icon="el-icon-edit"
        />
        <mp-button
          v-if="filter.group && filter.id !== -1"
          icon="el-icon-check"
          :mp-type="MpTypeButton.check"
          :disabled="filter.id === -1"
          @click="editNameFilter(filter)"
        />
        <mp-button
          v-if="filter.id !== -1"
          @click="deleteFilter(filter.id)"
          :mp-type="MpTypeButton.remove"
          :disabled="filter.id === -1"
          icon="el-icon-delete"
        />
      </div>
    </el-radio-group>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { methods } from '@/api_services/httpMethods.enum'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { ISavedFilterHeader } from '@/modules/Filters/SavingFilter/SavingFilter.interface.ts'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export default Vue.extend({
  name: 'MpSavingFilterList',
  components: { MpButton },
  props: {
    filters: {
      type: Array,
      default: () => {
        return [] as Array<ISavedFilterHeader>
      },
    },
    filterId: {
      default: -1,
    },
    loading: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      MpTypeButton: MpTypeButton,
    }
  },
  methods: {
    async setFilter(filter: ISavedFilterHeader) {
      this.$emit('set-current-filter', filter)
    },

    async editNameFilter(itemSavedFilter: ISavedFilterHeader) {
      itemSavedFilter.group = false
      const result = await this.$api.renameFilter({
        method: methods.post,
        data: {
          FilterId: itemSavedFilter.id,
          NewNameFilter: itemSavedFilter.title,
        },
      })
      if (result) {
        this.$emit('edit-filter', itemSavedFilter)
        this.$message.success(this.$t('notifications.data.updated') as string)
      }
    },

    async deleteFilter(filterId: string) {
      this.$confirm(
        this.$t('filter.mentolTable.removeFilterMessage') as string,
        this.$t('filter.mentolTable.warning') as string,
        {
          confirmButtonText: this.$t('general.delete') as string,
          cancelButtonText: this.$t('general.close') as string,
          type: 'warning',
          closeOnClickModal: false,
        },
      )
        .then(async () => {
          const result = await this.$api.deleteFilter({
            method: methods.delete,
            params: { id: +filterId },
          })
          if (result) {
            this.$emit('delete-filters')
            this.$message.success(
              this.$t('filter.selectSaveFilters.applyDelete') as string,
            )
          }
        })
        .catch(err => {
          console.log(err)
          this.$message.info(
            this.$t('filter.selectSaveFilters.cancelDelete') as string,
          )
        })
    },
  },
})
</script>

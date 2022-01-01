<template>
  <FormLayout>
    <div class="save-filter">
      <el-dialog
        @close="$emit('close', false)"
        width="535px"
        :visible.sync="visible"
        :title="this.$t('filter.selectSaveFilters.titleCreateFilters')"
      >
        <el-form ref="form" :model="form" label-width="230px">
          <el-form-item :label="$t('filter.createDialogSaveFilter.labelName')">
            <el-input v-model="form.name" />
          </el-form-item>

          <el-form-item
            :label="$t('filter.createDialogSaveFilter.labelTypeFilter')"
          >
            <el-radio-group v-model="form.isCommon" @change="changeName">
              <el-radio border label="general">
                {{ $t('filter.createDialogSaveFilter.filterTypeGeneral') }}
              </el-radio>
              <el-radio border label="personal">
                {{ $t('filter.createDialogSaveFilter.filterTypePersonal') }}
              </el-radio>
            </el-radio-group>
          </el-form-item>
        </el-form>
        <span slot="footer" class="dialog-footer">
          <el-button
            type="primary"
            class="mp-button mp-button__primary"
            @click="create"
          >
            {{ $t('filter.selectSaveFilters.btnCreate') }}
          </el-button>
          <el-button class="mp-button" @click="close">
            {{ $t('general.cancel') }}
          </el-button>
        </span>
      </el-dialog>
    </div>
  </FormLayout>
</template>

<script lang="ts">
import Vue from 'vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import { methods } from '@/api_services/httpMethods.enum'
import {
  IDtoSavingFilter,
  IFormSavingFilterDialog,
} from '@/components/MpSavingFilter/MpSavingFilter.interface'
import { SavingFilterFunctionType } from '@/components/MpSavingFilter/MpSavingFilter.const'
import { ISavedFilterHeader } from '@/modules/Filters/SavingFilter/SavingFilter.interface.ts'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'
import { DtoFactory } from '@/modules/dto/DtoFactory'
import { DtoName } from '@/modules/dto/DtoName.const'

export default Vue.extend({
  name: 'MpSavingFilterDialog',
  components: {
    FormLayout,
  },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    listQuery: {
      type: Object as () => TableDto,
      default: (): TableDto => DtoFactory.create(DtoName.table),
    },
  },
  data() {
    return {
      form: {
        name: this.$t('button.title.filter'),
        isCommon: 'personal',
      } as IFormSavingFilterDialog,
    }
  },
  methods: {
    async create() {
      const dtoSavingFilter: IDtoSavingFilter = {
        listQuery: this.listQuery,
        func: SavingFilterFunctionType.save,
        newName: this.form.name || this.$t('button.title.filter'),
        iscommon: this.form.isCommon === 'personal' ? '0' : '1',
        filterId: -1,
      }
      const filterHeaderId: ISavedFilterHeader['id'] = await this.$api.saveFilter(
        {
          method: methods.post,
          data: dtoSavingFilter,
        },
      )
      if (filterHeaderId) {
        await this.close()
        this.$emit('create-filter', filterHeaderId)
      }
    },
    changeName(value: 'general' | 'personal') {
      if (value === 'general') {
        this.form.name = `${this.form.name} (${this.$t(
          'filter.createDialogSaveFilter.filterTypeGeneral',
        )})`
      } else {
        const filterName: string = String(this.form.name).replace(
          `(${this.$t('filter.createDialogSaveFilter.filterTypeGeneral')})`,
          '',
        )
        this.form.name = filterName.trim()
      }
    },
    async close() {
      await this.$emit('close', false)
      this.form = {
        name: this.$t('button.title.filter'),
        isCommon: 'personal',
      }
    },
  },
})
</script>

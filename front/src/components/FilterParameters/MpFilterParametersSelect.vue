<template>
  <div class="filter-parameters-select">
    <el-form label-position="right">
      <el-form-item
        v-if="!checkedData.found.length"
        class="filter-parameters-select__item-textarea"
      >
        <el-input
          class="filter-parameters-select__input-data"
          type="textarea"
          :rows="8"
          :placeholder="$t('forms.filterParameters.placeholder.insertValue')"
          v-model="textarea"
          @input="raiseData"
        >
        </el-input>
      </el-form-item>

      <el-form-item v-else class="filter-parameters-select__item-textarea">
        <div class="filter-parameters-select__found-data">
          <span>{{
            `${$t('forms.filterParameters.label.foundMatches')}: ${
              checkedData.found.length
            }`
          }}</span>
          <el-input
            readonly
            disabled
            type="textarea"
            :rows="7"
            :placeholder="$t('forms.filterParameters.placeholder.found')"
            :value="checkedData.found.join('\n')"
            @input="raiseData"
          />
        </div>
        <div class="filter-parameters-select__not-found-data">
          <span>{{
            `${$t('forms.filterParameters.label.notFoundMatches')}: ${
              checkedData.notFound.length
            }`
          }}</span>
          <el-input
            readonly
            disabled
            type="textarea"
            :rows="7"
            :placeholder="$t('forms.filterParameters.placeholder.notFound')"
            :value="checkedData.notFound.join('\n')"
            @input="raiseData"
          />
        </div>
      </el-form-item>

      <div class="filter-parameters-select__controls">
        <el-form-item
          :label="`${$t('forms.filterParameters.label.separator')}:`"
          label-width="150px"
        >
          <el-select v-model="separator" @change="raiseData">
            <el-option
              v-for="separator in separatorList"
              :key="separator.id"
              :label="separator.label"
              :value="separator"
            />
          </el-select>
        </el-form-item>
      </div>
    </el-form>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { stringOperandsConstants } from '@/modules/Filters/Operands/StringOperands.const'
import { MpFilterParametersLang } from '@/components/FilterParameters/MpFilterParameters.lang'
import { separatorList } from '@/modules/Separators/Separators.const'
import { IResponseCheckValue } from '@/modules/Filters/FilterParameters/FilterParameters.interface'

export default Vue.extend({
  name: 'MpFilterParametersSelect',
  props: {
    checkedData: {
      type: Object as () => IResponseCheckValue,
      default: () => {
        return {
          found: [],
          notFound: [],
        }
      },
    },
  },
  data: () => {
    return {
      textarea: '',
      notMatchTextarea: '',
      separator: separatorList[0],
      separatorList: separatorList,
      selectedOperand: stringOperandsConstants[0],
      operands: stringOperandsConstants,
    }
  },
  created() {
    this.$i18n.mergeLocaleMessage('en', MpFilterParametersLang.en)
    this.$i18n.mergeLocaleMessage('ru', MpFilterParametersLang.ru)
  },
  computed: {},
  methods: {
    raiseData() {
      this.$emit('update-filter-parameters-select', {
        text: this.textarea,
        separator: this.separator,
      })
    },
  },
})
</script>

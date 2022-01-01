<template>
  <div class="filter-parameters-string">
    <el-form label-position="right">
      <el-form-item>
        <el-input
          type="textarea"
          :rows="7"
          :placeholder="$t('forms.filterParameters.placeholder.insertValue')"
          v-model="textarea"
          @input="raiseData"
        >
        </el-input>
      </el-form-item>
      <div class="filter-parameters-select__controls">
        <el-form-item
          :label="`${$t('forms.filterParameters.label.comparisonConditions')}:`"
          label-width="150px"
        >
          <el-select v-model="selectedOperand" @change="raiseData">
            <el-option
              v-for="operand in operands"
              :key="operand.id"
              :label="$t(`operand.${operand.name}`)"
              :value="operand"
            />
          </el-select>
        </el-form-item>
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
import { MpFilterParametersLang } from '@/components/FilterParameters/MpFilterParameters.lang'
import { separatorList } from '@/modules/Separators/Separators.const'
import { IDataMpFilterParametersSelect } from '@/components/FilterParameters/MpFilterParametersSelect.interface'
import { stringOperandsConstants } from '@/modules/Filters/Operands/StringOperands.const'

export default Vue.extend({
  name: 'MpFilterParametersString',
  data: (): IDataMpFilterParametersSelect => {
    return {
      textarea: '',
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
  methods: {
    raiseData() {
      this.$emit('update-filter-parameters-string', {
        text: this.textarea,
        separator: this.separator,
        operand: this.selectedOperand,
      })
    },
  },
})
</script>

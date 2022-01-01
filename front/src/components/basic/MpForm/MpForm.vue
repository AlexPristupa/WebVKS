<template>
  <el-card shadow="never" class="mp-form">
    <h3 v-if="formTitle.length" slot="header" class="mp-form__title">
      {{ formTitle }}
    </h3>
    <div slot="default" class="mp-form__body">
      <slot></slot>
    </div>
    <div class="mp-form__actions">
      <slot name="actions"></slot>
    </div>
  </el-card>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'MpForm',
  props: {
    formTitle: {
      type: String,
      default: '',
    },
  },
})
</script>

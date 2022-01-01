import Vue from 'vue'

export type VForm = Vue & {
  validate: (valid: any) => void
  resetFields: () => void
}

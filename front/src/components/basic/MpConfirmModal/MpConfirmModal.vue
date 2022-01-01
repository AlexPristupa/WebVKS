<template>
  <el-dialog
    width="30%"
    class="mp-modal mp-confirm-modal"
    :close-on-click-modal="false"
    :visible="visibleConfirmModal"
    :title="title"
    @close="$emit('close', false)"
    :draggable="false"
  >
    <template #default>
      <div class="mp-confirm-modal__body">
        <div class="mp-confirm-modal__icon"></div>
        <div class="mp-confirm-modal__text">
          <slot name="extensionBody"></slot>
          <p
            v-for="(item, index) of text"
            :key="index"
            class="mp-confirm-modal__message"
          >
            {{ item }}
          </p>
        </div>
      </div>
    </template>

    <template slot="footer">
      <mp-button
        :type="submitType"
        :loading="loading"
        mp-status="normal"
        :mp-type="submitMpType"
        @click="submit"
      >
        {{ submitText || $t('label.confirm') }}
      </mp-button>

      <mp-button
        v-if="showClose"
        mp-status="normal"
        :mp-type="closeMpType"
        :type="closeType"
        :disabled="closeDisabled || loading"
        @click="close"
      >
        {{ closeText || $t('label.cancel') }}
      </mp-button>
    </template>
  </el-dialog>
</template>

<script>
import Vue from 'vue'
import MpButton from '@/components/basic/MpButton/MpButton'
import { MpConfirmModalLang } from '@/components/basic/MpConfirmModal/MpConfirmModal.lang'

export default Vue.extend({
  name: 'MpConfirmModal',
  components: {
    MpButton,
  },
  props: {
    title: {
      type: String,
      default: 'Confirm window',
    },
    text: {
      type: Array,
      default() {
        return ['']
      },
    },
    visibleConfirmModal: {
      type: Boolean,
      default: false,
    },
    submitText: {
      type: String,
      default: '',
    },
    closeText: {
      type: String,
      default: '',
    },
    closeMpType: {
      type: String,
      default: 'cancel',
    },
    submitMpType: {
      type: String,
      default: 'confirm',
    },
    submitType: {
      type: String,
      default: 'primary',
    },
    closeType: {
      type: String,
      default: '',
    },
    closeDisabled: {
      type: Boolean,
      default: false,
    },
    loading: {
      type: Boolean,
      default: false,
    },
    showClose: {
      type: Boolean,
      default: true,
    },
  },
  methods: {
    submit() {
      this.$emit('confirm')
    },
    close() {
      this.$emit('optionalEvent')
      this.$message.info(this.$t('notifications.cancel.action').toString())
      this.$emit('close', false)
    },
  },
  i18n: {
    messages: MpConfirmModalLang,
  },
})
</script>

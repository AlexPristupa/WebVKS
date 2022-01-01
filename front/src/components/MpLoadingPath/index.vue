<template>
  <FormLayout>
    <el-dialog
      :visible="visible"
      :title="$t('general.pleaseWait')"
      width="500px"
      @close="interrupt"
    >
      <loading-progress
        :indeterminate="true"
        size="450"
        width="450"
        height="26"
        shape="line"
      />
      <template slot="footer">
        <mp-button
          type="primary"
          mp-status="normal"
          :title="$t('button.title.interrupt')"
          @click="interrupt"
        >
          {{ $t('button.title.interrupt') }}
        </mp-button>

        <mp-button
          v-if="hiddenButton"
          mp-status="normal"
          mp-type="close"
          @click="hideLoading"
        >
          {{ $t('button.title.hide') }}
        </mp-button>
      </template>
    </el-dialog>
  </FormLayout>
</template>
<script lang="ts">
/**
 * @description общий компонент загрузки
 */
import Vue from 'vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'

export default Vue.extend({
  name: 'MpLoadingPath',
  components: {
    FormLayout,
    MpButton,
  },
  props: {
    visible: {
      type: Boolean as () => boolean,
      default: false,
    },
    hiddenButton: {
      type: Boolean as () => boolean,
      default: false,
    },
  },
  methods: {
    interrupt() {
      this.$emit('interrupt')
    },
    hideLoading() {
      this.$emit('hide')
    },
  },
})
</script>

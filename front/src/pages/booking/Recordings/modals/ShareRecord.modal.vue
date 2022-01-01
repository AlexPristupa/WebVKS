<template>
  <el-dialog
    v-el-drag-dialog
    width="60%"
    class="mp-modal mp-modal--share-record"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
  >
    <template #default>
      <share-record-modal-table :selected="selectedEntity" />
    </template>
  </el-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedShareRecordModal,
  IDataShareRecordModal,
  IMethodsShareRecordModal,
  IPropsShareRecordModal,
} from '@/pages/booking/Recordings/modals/config/shareRecord.modal.interface'
import ShareRecordModalTable from '@/pages/booking/Recordings/modals/ShareRecord.modal.table.vue'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import elDragDialog from '@/directive/el-dragDialog'

export default Vue.extend<
  IDataShareRecordModal,
  IMethodsShareRecordModal,
  IComputedShareRecordModal,
  IPropsShareRecordModal
>({
  name: 'ShareRecordModal',
  components: {
    ShareRecordModalTable,
  },
  directives: {
    elDragDialog,
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Object,
      default: () => ({}),
    },
  },
  data() {
    return {
      nonCopiedValues: nonCopiedValues,
      entityModel: {},
      validationFromBackData: [],
      savingLoading: false,
    }
  },
  computed: {
    disabledSaveButton() {
      return true
    },
    title() {
      return this.$t('dialogs.titles.share')
    },
  },
  methods: {
    submit() {},

    close() {
      this.$emit('close')
    },

    validation() {},
  },
})
</script>

<style scoped></style>

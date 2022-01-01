<template>
  <el-dialog
    v-el-drag-dialog
    width="20%"
    class="mp-modal mp-modal--editBookingPinSetting"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
  >
    <template #default>
      <el-form
        ref="editBookingPinSetting"
        :label-position="'top'"
        :model="entityModel"
        :rules="rules"
        class="mp-form"
      >
        <el-form-item prop="shift" :label="$t('forms.editBooking.policy')">
          <mp-select
            :value="entityModel.pinPoliticsId"
            option-key="name"
            option-value="id"
            option-label="name"
            :option-list="shiftSettingList"
            :popper-append-to-body="false"
            :placeholder="
              $t('forms.placeholders.chooseEntity', [
                $t('forms.editBooking.placeholders.shift'),
              ])
            "
            @change="changeShift"
          />
        </el-form-item>
      </el-form>
    </template>
    <template v-slot:footer>
      <mp-button
        type="primary"
        mp-status="normal"
        mp-type="save"
        :disabled="disabledSaveButton"
        @click="submit"
      >
        {{ $t('button.title.save') }}
      </mp-button>

      <mp-button type="" mp-status="normal" mp-type="cancel" @click="close">
        {{ $t('button.title.cancel') }}
      </mp-button>
    </template>
  </el-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import {
  IComputedBookingPinSettingsModal,
  IDataBookingPinSettingsModal,
  IMethodsBookingPinSettingsModal,
  IPropsBookingPinSettingsModal,
} from '@/pages/booking/BookingsRestricted/modals/config/editBookingPinSettingsModal.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { methods } from '@/api_services/httpMethods.enum'

export default Vue.extend<
  IDataBookingPinSettingsModal,
  IMethodsBookingPinSettingsModal,
  IComputedBookingPinSettingsModal,
  IPropsBookingPinSettingsModal
>({
  name: 'EditPinSettingModal',
  components: {
    MpSelect,
    MpButton,
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Number,
      default: 1,
    },
  },
  directives: {
    elDragDialog,
  },
  data() {
    return {
      shiftSettingList: [],
      entityModel: {
        pinPoliticsId: 1,
      },
      savingLoading: false,
      nonCopiedValues,
      rules: {
        shift: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
      },
    }
  },
  computed: {
    title() {
      return this.$t('dialogs.titles.bookingPinSettings')
    },
    disabledSaveButton() {
      return this.savingLoading
    },
  },
  async mounted() {
    await this.getOptions()
    this.entityModel.pinPoliticsId = this.selectedEntity
  },
  methods: {
    async getOptions(search) {
      const res = await this.$api.dictionaryPinPolitics({
        method: methods.get,
        data: {
          search: search || '',
          limit: 300,
        },
      })
      if (res) {
        this.shiftSettingList = res
      }
    },
    changeShift(value) {
      this.entityModel.pinPoliticsId = value
    },
    submit() {
      this.$emit('update', this.entityModel.pinPoliticsId)
      this.close()
    },
    close() {
      this.$emit('close')
    },
  },
})
</script>

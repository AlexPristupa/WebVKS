<template>
  <div>
    <el-dialog
      v-el-drag-dialog
      width="50%"
      class="mp-modal mp-modal--edit-booking-pin-setting"
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
          class="mp-form"
        >
          <el-form-item prop="shift" :label="$t('forms.editBooking.policy')">
            <mp-select
              :value="selectedEntity.pinPoliticsId"
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
              @change="setPoliticsId"
            />
          </el-form-item>
          <div
            v-if="selectedEntity.pinPoliticsId === 3"
            class="button--schedule"
          >
            <mp-button
              type="primary"
              mp-status="normal"
              :title="$t('button.title.setSchedule')"
              @click="editPeriodicModalVisible = true"
            >
              <i class="el-icon-time"></i>
              {{ $t('button.title.setSchedule') }}
            </mp-button>
          </div>
          <p
            v-if="selectedEntity.pinPoliticsId === 3"
            class="el-form-item--with-text__text"
          >
            {{ text }}
          </p>
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
    <edit-booking-periodic-modal
      v-if="editPeriodicModalVisible"
      :selected-entity="{
        id: selectedEntity.id,
        text: text,
        tabs: selectedEntity.pinScheduleTab,
        schedule: selectedEntity.pinSchedule,
        dateStart: selectedEntity.pinDateStart,
      }"
      :visible-edit-modal="editPeriodicModalVisible"
      @updateText="updateText"
      @updateTab="$emit('updateTab', $event)"
      @updateSchedule="$emit('updateSchedule', $event)"
      @updateDateStart="$emit('updateDateStart', $event)"
      @close="editPeriodicModalVisible = false"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import EditBookingPeriodicModal from '@/pages/booking/Bookings/modals/EditBookingPeriodic.modal.vue'
import {
  IComputedBookingPinSettingsModal,
  IDataBookingPinSettingsModal,
  IMethodsBookingPinSettingsModal,
  IPropsBookingPinSettingsModal,
  PinSettingsEntity,
} from '@/pages/booking/Bookings/modals/config/editBookingPinSettingsModal.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { methods } from '@/api_services/httpMethods.enum'
import { DateTime } from '@/modules/DateTime/DateTime'
import { getPeriodicText } from '@/pages/booking/Bookings/modals/modules/getOptionsBookingModule'

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
    EditBookingPeriodicModal,
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Object as () => PinSettingsEntity,
      default: () => new PinSettingsEntity(),
    },
  },
  directives: {
    elDragDialog,
  },
  data() {
    return {
      editPeriodicModalVisible: false,
      shiftSettingList: [],
      text: '',
      entityModel: new PinSettingsEntity(),
      savingLoading: false,
      nonCopiedValues,
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
  watch: {
    'selectedEntity.pinPoliticsId'(value) {
      if (value === 3 && this.selectedEntity.pinSchedule) {
        this.updateText()
      }
    },
  },
  async mounted() {
    await this.getOptions()
    this.entityModel = new PinSettingsEntity(this.selectedEntity)
    if (
      this.selectedEntity.pinPoliticsId === 3 &&
      this.selectedEntity.pinSchedule
    ) {
      await this.updateText()
    }
  },
  methods: {
    setPoliticsId(id) {
      if (id === 3) {
        this.$emit(
          'updateTab',
          id !== this.entityModel.pinPoliticsId
            ? '2'
            : this.entityModel.pinScheduleTab,
        )
        this.$emit(
          'updateSchedule',
          id !== this.entityModel.pinPoliticsId
            ? 'days:Friday'
            : this.entityModel.pinSchedule,
        )
        this.$emit(
          'updateDateStart',
          id !== this.entityModel.pinPoliticsId
            ? new DateTime({
                date: new Date(),
              }).getDateToString() + ' 03:00'
            : this.entityModel.pinDateStart,
        )
      }
      this.$emit('updatePinPoliticsId', id)
    },
    async getOptions(search) {
      this.savingLoading = true
      const res = await this.$api.dictionaryPinPolitics({
        method: methods.get,
        data: {
          search: search || '',
          limit: 300,
        },
      })
      if (res) {
        this.shiftSettingList = res
        this.savingLoading = false
      }
    },
    submit() {
      this.$emit('close')
    },
    async updateText(text) {
      this.savingLoading = true
      if (arguments.length) {
        this.text = text || ''
      } else {
        this.text = await getPeriodicText(
          this.selectedEntity.pinSchedule || '',
          this.selectedEntity.pinDateStart || '',
        )
      }
      this.savingLoading = false
    },
    close() {
      this.$emit('updateTab', this.entityModel.pinScheduleTab)
      this.$emit('updatePinPoliticsId', this.entityModel.pinPoliticsId)
      this.$emit('updateSchedule', this.entityModel.pinSchedule)
      this.$emit('updateDateStart', this.entityModel.pinDateStart)
      this.$emit('close')
    },
  },
})
</script>

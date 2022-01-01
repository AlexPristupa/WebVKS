<template>
  <div>
    <el-dialog
      v-el-drag-dialog
      width="60%"
      class="mp-modal mp-modal--booking"
      :close-on-click-modal="false"
      :visible="visibleEditModal"
      :title="title"
      @close="$emit('close')"
    >
      <template #default>
        <el-form
          ref="editBooking"
          :label-position="'top'"
          :model="entityModel"
          :rules="rules"
          class="mp-form"
        >
          <form-column-layout
            :columns="columns"
            @add="addLinkBookingToParticipants"
          >
            <template #column_0>
              <el-form-item :label="$t('forms.editBooking.name')" prop="name">
                <el-input
                  v-model="entityModel.name"
                  :placeholder="placeholders.name"
                  clearable
                />
              </el-form-item>
              <el-form-item
                :label="$t('forms.editBooking.description')"
                prop="description"
              >
                <el-input
                  v-model="entityModel.description"
                  :placeholder="placeholders.description"
                  clearable
                />
              </el-form-item>
              <el-form-item
                :label="$t('forms.editBooking.location')"
                prop="location"
              >
                <el-input
                  v-model="entityModel.location"
                  :placeholder="placeholders.location"
                  clearable
                />
              </el-form-item>
              <el-form-item
                :label="$t('forms.editBooking.owner')"
                prop="ownerId"
              >
                <mp-select
                  filterable
                  :value="entityModel.ownerId"
                  option-key="name"
                  option-value="id"
                  option-label="name"
                  :option-list="ownerOptions"
                  :filter-method="getOptionsOwner"
                  :popper-append-to-body="false"
                  :placeholder="placeholders.ownerId"
                  @change="entityChanged('ownerId', $event)"
                />
              </el-form-item>
              <el-form-item
                :label="
                  permanent
                    ? $t('forms.editBooking.restart')
                    : $t('forms.editBooking.dateStart')
                "
                prop="dateStart"
              >
                <mp-date-picker-with-time-select
                  :initial="entityModel.dateStart"
                  :date-picker-settings="{
                    placeholder: placeholders.dateStart,
                    disablePreviousDates: true,
                    editable: true,
                  }"
                  @change="entityChanged('dateStart', $event)"
                />
              </el-form-item>
              <el-form-item
                :label="$t('forms.editBooking.timeZone')"
                prop="timeZone"
              >
                <mp-select
                  filterable
                  :value="entityModel.timeZone"
                  option-key="name"
                  option-value="id"
                  option-label="name"
                  :option-list="timeZoneOptions"
                  :filter-method="getTimeZoneOptions"
                  :popper-append-to-body="false"
                  :placeholder="placeholders.timeZone"
                  @change="entityChanged('timeZone', $event)"
                />
              </el-form-item>
              <el-form-item
                v-if="!permanent"
                :label="$t('forms.editBooking.duration')"
                prop="duration"
              >
                <duration-double-select
                  :hour="entityModel.hour"
                  :minute="entityModel.minute"
                  @changed="entityChanged"
                />
              </el-form-item>
              <el-form-item
                prop="isUsePin"
                :label="$t('forms.editBooking.isUsePin')"
              >
                <div class="el-form-item__element-with-button">
                  <el-switch
                    v-model="entityModel.isUsePin"
                    @change="entityChanged('pinCode', '')"
                  />
                  <mp-button
                    v-if="entityModel.isUsePin"
                    type="primary"
                    mp-status="normal"
                    :title="$t('button.title.shiftSettingUp')"
                    @click="editPinSettingModalVisible = true"
                  >
                    <i class="el-icon-time"></i>
                    {{ $t('button.title.shiftSettingUp') }}
                  </mp-button>
                </div>
              </el-form-item>
              <el-form-item class="el-form-item--with-text" prop="pinCode">
                <el-input
                  v-model="entityModel.pinCode"
                  :disabled="!entityModel.isUsePin"
                  :placeholder="placeholders.pinCode"
                  clearable
                />
              </el-form-item>
              <p class="el-form-item--with-text__text">
                {{ $t('forms.editBooking.texts.leaveEmptyPinCode') }}
              </p>
              <el-form-item
                v-if="!permanent"
                :label="$t('forms.editBooking.openConferenceBefore')"
                class="el-form-item--with-text"
                prop="openConferenceBefore"
              >
                <el-input-number
                  :value="entityModel.openConferenceBefore"
                  :min="0"
                  :max="60"
                  @change="entityChanged('openConferenceBefore', $event)"
                />
                <p class="el-input-number__text">
                  {{ $t('forms.editBooking.minuteToStart') }}
                </p>
              </el-form-item>
              <p class="el-form-item--with-text__text" v-if="!permanent">
                {{
                  $t('forms.editBooking.texts.minimalValueForThisSpace', ['60'])
                }}
              </p>
              <el-form-item
                v-if="!permanent"
                prop="periodic"
                class="el-form-item--with-text"
                :label="$t('forms.editBooking.periodic')"
              >
                <div class="el-form-item__element-with-button">
                  <el-switch v-model="entityModel.periodic" />
                  <mp-button
                    v-if="entityModel.periodic"
                    type="primary"
                    mp-status="normal"
                    :title="$t('button.title.frequencySettingUp')"
                    @click="editPeriodicModalVisible = true"
                  >
                    <i class="el-icon-time"></i>
                    {{ $t('button.title.frequencySettingUp') }}
                  </mp-button>
                </div>
              </el-form-item>
              <p
                v-if="entityModel.periodic && !permanent"
                class="el-form-item--with-text__text"
              >
                {{ text }}
              </p>
              <el-form-item
                v-if="permanent || entityModel.periodic"
                :label="$t('forms.editBooking.dateEnd')"
                prop="dateEnd"
              >
                <mp-date-picker-with-time-select
                  :initial="entityModel.dateEnd"
                  :date-picker-settings="{
                    placeholder: placeholders.dateEnd,
                    disableFromDate: entityModel.dateStart,
                    disablePreviousDates: true,
                    editable: true,
                  }"
                  @change="entityChanged('dateEnd', $event)"
                />
              </el-form-item>
              <el-form-item
                v-if="entityModel.periodic && !permanent"
                :label="$t('forms.editBooking.repeatCount')"
                prop="repeatCount"
              >
                <el-input-number v-model="entityModel.repeatCount" :min="0" />
              </el-form-item>
            </template>
            <template #column_1>
              <edit-booking-list
                ref="editBookingListSpaces"
                :list-class="listClass.spaces"
                :class="{ permanent: permanent }"
                :select-type="selectTypes.single"
                :options="spacesOptions"
                :checked="spacesChecked"
                :loading="optionsSpacesLoading"
                @search="getOptionsSpaces"
                @update="updateSpace"
              />
            </template>
            <template #column_2>
              <edit-booking-list
                ref="editBookingListParticipants"
                :list-class="listClass.participants"
                :select-type="selectTypes.multiple"
                :options="participantsOptions"
                :space-id="entityModel.spaceId"
                :selected-owner="entityModel.ownerId"
                :checked="participantsChecked"
                :checkedCopy="participantsCheckedCopy"
                :loading="optionsParticipantsLoading"
                @search="getOptionsParticipants"
                @update="updateParticipants"
                @update-one="updateParticipant"
                @create="createParticipant"
              />
              <el-form-item
                :label="$t('forms.editBooking.connectionType')"
                prop="connectionType"
              >
                <mp-select
                  filterable
                  :value="entityModel.connectionTypeId"
                  option-key="name"
                  option-value="id"
                  option-label="name"
                  :option-list="connectionTypeOptions"
                  :filter-method="getConnectionTypeOptions"
                  :popper-append-to-body="false"
                  :placeholder="placeholders.connectionType"
                  @change="entityChanged('connectionTypeId', $event)"
                />
              </el-form-item>
              <el-form-item
                prop="attemptsCount"
                :label="$t('forms.editBooking.attemptsCount')"
              >
                <el-input-number v-model="entityModel.attemptsCount" :min="0" />
              </el-form-item>
              <el-form-item prop="delay" :label="$t('forms.editBooking.delay')">
                <el-input-number v-model="entityModel.delay" :min="0" />
              </el-form-item>
              <el-form-item
                prop="isSendNotification"
                :label="$t('forms.editBooking.isSendNotification')"
              >
                <el-switch v-model="entityModel.isSendNotification" />
              </el-form-item>
              <el-form-item
                :label="$t('forms.editBooking.isSyncToExchange')"
                prop="isSyncToExchange"
              >
                <el-switch v-model="entityModel.isSyncToExchange" />
              </el-form-item>
            </template>
          </form-column-layout>
        </el-form>
      </template>
      <template v-slot:footer>
        <mp-button
          type="primary"
          mp-status="normal"
          mp-type="save"
          :loading="disabledSaveButton"
          :disabled="disabledSaveButton || hasIntersection"
          @click="submit"
        >
          {{ $t('button.title.save') }}
        </mp-button>

        <mp-button type="" mp-status="normal" mp-type="cancel" @click="close">
          {{ $t('button.title.cancel') }}
        </mp-button>
      </template>
    </el-dialog>
    <edit-booking-pin-setting-modal
      v-if="editPinSettingModalVisible"
      :selected-entity="{
        id: entityModel.id,
        pinScheduleTab: entityModel.pinScheduleTab,
        pinPoliticsId: entityModel.pinPoliticsId,
        pinSchedule: entityModel.pinSchedule,
        pinDateStart: entityModel.pinDateStart,
      }"
      :visible-edit-modal="editPinSettingModalVisible"
      @updateTab="entityChanged('pinScheduleTab', $event)"
      @updatePinPoliticsId="entityChanged('pinPoliticsId', $event)"
      @updateSchedule="entityChanged('pinSchedule', $event)"
      @updateDateStart="entityChanged('pinDateStart', $event)"
      @close="editPinSettingModalVisible = false"
    />
    <edit-booking-periodic-modal
      v-if="editPeriodicModalVisible"
      :selected-entity="{
        id: entityModel.id,
        text: text,
        tabs: entityModel.scheduleTab,
        schedule: entityModel.schedule,
        dateStart: entityModel.dateStart,
      }"
      :visible-edit-modal="editPeriodicModalVisible"
      @updateText="updateText"
      @updateTab="entityChanged('scheduleTab', $event)"
      @updateSchedule="entityChanged('schedule', $event)"
      @updateDateStart="entityChanged('dateStart', $event)"
      @close="editPeriodicModalVisible = false"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import FormColumnLayout from '@/layouts/formLayout/FormColumnLayout.vue'
import { VksBookingTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksBookingTable.entity'
import { VForm } from '@/modules/Form/Form.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import {
  IComputedEditBookingModal,
  IDataEditBookingModal,
  IMethodsEditBookingModal,
  IPropsEditBookingModal,
} from '@/pages/booking/Bookings/modals/config/editBookingModal.interface'
import EditBookingPeriodicModal from '@/pages/booking/Bookings/modals/EditBookingPeriodic.modal.vue'
import EditBookingPinSettingModal from '@/pages/booking/Bookings/modals/EditBookingPinSetting.modal.vue'
import EditBookingList from '@/pages/booking/Bookings/modals/EditBookingList.vue'
import {
  durationSelectHours,
  durationSelectMinutes,
  intersectionFields,
  modalColumns,
  modalPermanentsColumns,
  placeholders,
} from '@/pages/booking/Bookings/modals/config/editBookingModal.const'
import { methods } from '@/api_services/httpMethods.enum'
import {
  datePickerTypes,
  timeFormats,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import { EditBookingModalRequestEntity } from '@/pages/booking/Bookings/modals/config/editBookingModal.request.entity'
import {
  listClass,
  selectType,
} from '@/pages/booking/Bookings/modals/config/editBookingList.interface'
import { debounce } from '@/utils'
import CONSTANTS from '@/constants'
import MpDatePickerWithTimeSelect from '@/components/basic/MpDatePickerWithTimeSelect/MpDatePickerWithTimeSelect.vue'
import { DateTime } from '@/modules/DateTime/DateTime'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'
import * as module from './modules/getOptionsBookingModule'
import DurationDoubleSelect from '@/pages/booking/Bookings/DurationDoubleSelect.vue'
import { TIMEOUT_SERVICE_SHORT } from '@/constant'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

/**
 * TODO рефакторить все...
 */

export default Vue.extend<
  IDataEditBookingModal,
  IMethodsEditBookingModal,
  IComputedEditBookingModal,
  IPropsEditBookingModal
>({
  name: 'EditBookingModal',
  components: {
    DurationDoubleSelect,
    MpSelect,
    MpButton,
    EditBookingList,
    FormColumnLayout,
    EditBookingPeriodicModal,
    EditBookingPinSettingModal,
    MpDatePickerWithTimeSelect,
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
      default: () => new VksBookingTableEntity(),
    },
    permanent: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      placeholders,
      durationSelectHours,
      durationSelectMinutes,
      datePickerTypes,
      timeFormats,
      nonCopiedValues,
      listClass: {
        spaces: listClass.spaces,
        participants: listClass.participants,
      },
      selectTypes: {
        single: selectType.single,
        multiple: selectType.multiple,
      },
      text: '',
      entityModel: new VksBookingEntity(),
      savingLoading: false,
      hasIntersection: false,
      editPeriodicModalVisible: false,
      editPinSettingModalVisible: false,
      optionsParticipantsLoading: false,
      optionsSpacesLoading: false,
      ownerOptions: [],
      spacesOptions: [],
      participantsOptions: [],
      participantsCheckedCopy: [],
      connectionTypeOptions: [],
      timeZoneOptions: [],
      rules: {
        name: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
        ownerId: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
        duration: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            // @ts-ignore
            validator: this.durationValidation,
            trigger: TriggerType.blur,
          },
        ],
        dateStart: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
        dateEnd: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            // @ts-ignore
            validator: this.dateEndValidation,
            trigger: TriggerType.blur,
          },
        ],
        timeZone: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
        repeatCount: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
      },
      participantsChecked: [],
    }
  },
  watch: {
    async 'entityModel.periodic'(newPeriodic, oldPeriodic) {
      if (newPeriodic !== oldPeriodic) {
        this.entityChanged('periodic', newPeriodic)
        await this.getBookingTypeOptions()
      }
      if (newPeriodic) {
        await this.updateText()
      }
    },
    'entityModel.repeatCount'(newCount, oldCount) {
      if (newCount !== oldCount) {
        this.entityChanged('repeatCount', newCount)
      }
    },
  },
  computed: {
    spacesChecked() {
      const checked = this.spacesOptions.find(
        item => item.id === this.entityModel.spaceId,
      )
      return checked ? [checked] : []
    },
    columns() {
      return this.permanent ? modalPermanentsColumns : modalColumns
    },
    title() {
      return this.$t(
        this.selectedEntity.id
          ? 'dialogs.titles.editingEntity'
          : 'dialogs.titles.addingEntity',
        [this.$t('dialogs.entities.booking')],
      )
    },
    disabledSaveButton() {
      return this.savingLoading
    },
  },
  async mounted() {
    await this.getTimeZoneOptions()
    if (this.selectedEntity.id) {
      await this.getSelectedBooking(this.selectedEntity.id)
    }
    await this.getOptions()
    if (this.entityModel.id) {
      const option = this.spacesOptions.find(
        option => option.id === this.entityModel.spaceId,
      )
      if (this.entityModel.spaceId && option) {
        this.spacesOptions = module.setCheckedFirst(this.spacesOptions, [
          option,
        ])
      }
    }
    if (this.entityModel.schedule && this.entityModel.periodic) {
      await this.updateText()
    }
  },
  methods: {
    async getBookingTypeOptions() {
      this.entityModel.typeId = await module.getBookingTypeOptions(
        this.entityModel.periodic,
        this.permanent,
      )
    },
    async getOptions() {
      if (!this.selectedEntity.id) {
        await this.getBookingTypeOptions()
      }
      await this.getOptionsOwner()
      await this.getOptionsSpaces()
      await this.getOptionsParticipants()
      await this.getConnectionTypeOptions()
    },
    async getSelectedBooking(id) {
      const res = await this.$api.booking({
        method: methods.get,
        data: {
          id: id,
        },
      })
      if (res) {
        this.entityModel = new VksBookingEntity(
          res,
          module.getTimezoneOffset(this.timeZoneOptions, res.timeZone),
        )
        this.participantsCheckedCopy = res.linkBookingToVksUsersOthers
          .concat(res.linkBookingToParticipants)
          .map(participant => {
            return JSON.parse(JSON.stringify(participant))
          })
      }
    },
    async getOptionsSpaces(search) {
      this.optionsSpacesLoading = true
      this.spacesOptions = await module.getOptionsSpaces(search)
      if (!this.selectedEntity.id && this.spacesOptions.length) {
        this.entityModel.spaceId = this.spacesOptions[0].id
      }
      this.optionsSpacesLoading = false
    },
    async getOptionsParticipants(search) {
      this.optionsParticipantsLoading = true
      this.participantsOptions = await module.getOptionsParticipantsAndOwner(
        search,
      )
      const participantsOthersOptions = await module.getOptionsParticipantsOthers(
        search,
      )
      this.participantsOptions = this.participantsOptions.concat(
        participantsOthersOptions,
      )
      const checked = this.entityModel.linkBookingToVksUsersOthers.concat(
        this.entityModel.linkBookingToParticipants,
      )
      const foundInsideChecked = search
        ? checked.filter(
            item =>
              item.uri.toLowerCase().includes(search) ||
              item.name.toLowerCase().includes(search) ||
              item.email.toLowerCase().includes(search),
          )
        : checked
      this.participantsOptions = module.setCheckedFirst(
        this.participantsOptions,
        foundInsideChecked,
      )
      this.setParticipantsChecked()
      this.optionsParticipantsLoading = false
    },
    async getOptionsOwner(search) {
      this.ownerOptions = await module.getOptionsParticipantsAndOwner(search)
    },
    dateEndValidation(rule, value, callback): void {
      const start = new DateTime({
        dateTime: this.entityModel.dateStart,
      }).getJsDate()
      const end = new DateTime({
        dateTime: this.entityModel.dateEnd,
      }).getJsDate()
      if (start.getTime() >= end.getTime()) {
        callback(new Error(this.$t('forms.validationError.dateEnd').toString()))
      } else {
        callback()
      }
    },
    durationValidation(rule, value, callback): void {
      if (!value) {
        callback(
          new Error(this.$t('forms.validationError.duration').toString()),
        )
      } else {
        callback()
      }
    },
    async submit() {
      const form: VForm = this.$refs['editBooking'] as VForm
      form.validate(async valid => {
        if (valid) {
          this.savingLoading = true
          const method = this.selectedEntity.id ? methods.put : methods.post
          const res = await this.$api.booking({
            method: method,
            data: new EditBookingModalRequestEntity(
              this.permanent,
              this.entityModel,
              module.getTimezoneOffset(
                this.timeZoneOptions,
                this.entityModel.timeZone,
              ),
            ).model,
          })
          if (res && !res.validation) {
            this.$emit('refresh-service', res.id)
            // Бизнес логика: сервис не успевает обновить состояние до загрузки данных, поэтому ждем его 3 секунды
            setTimeout(async () => {
              await this.$emit('update')
              this.$emit('close')
              this.savingLoading = false
            }, TIMEOUT_SERVICE_SHORT)
          } else {
            const validationWithoutField = FormValidation.backValidationWithoutField(
              res.validation,
            )
            if (validationWithoutField) {
              this.$message.error(validationWithoutField)
            }
            this.savingLoading = false
          }
        } else {
          this.$message.error(this.$t('forms.validationError.error') as string)
          this.savingLoading = false
        }
      })
    },
    close() {
      this.$message.info(this.$t('notifications.cancel.action') as string)
      this.$emit('close')
    },
    entityChanged(entity, value) {
      this.entityModel[entity] = value
      if (entity === 'minute' || entity === 'hour') {
        this.entityModel.duration =
          this.entityModel.hour + this.entityModel.minute
      }
      if (intersectionFields.includes(entity)) {
        this.findIntersection()
      }
    },
    async updateText(text) {
      if (arguments.length) {
        this.text = text || ''
      } else {
        this.text = await module.getPeriodicText(
          this.entityModel.schedule || '',
          this.entityModel.dateStart || '',
        )
      }
    },
    async getConnectionTypeOptions(search) {
      this.connectionTypeOptions = await module.getConnectionTypeOptions(search)
    },
    async getTimeZoneOptions(search) {
      const timeZoneOptions = await module.getTimeZoneOptions(search)
      this.timeZoneOptions = timeZoneOptions.list
      if (!this.selectedEntity.id && timeZoneOptions.initial) {
        this.entityModel.timeZone = timeZoneOptions.initial
      }
    },
    updateSpace(spaces) {
      if (spaces[0].id) {
        this.entityChanged('spaceId', spaces[0].id)
      }
    },
    updateParticipants(participants) {
      this.entityModel.linkBookingToParticipants = []
      this.entityModel.linkBookingToVksUsersOthers = []
      participants.forEach(participant => {
        if (!participant.isFromOtherList) {
          this.entityModel.linkBookingToParticipants.push(
            participant as VksBookingLinkToParticipant,
          )
        } else {
          this.entityModel.linkBookingToVksUsersOthers.push(
            participant as VksBookingLinkBookingToVksUsersOthers,
          )
        }
      })
      this.setParticipantsChecked()
    },
    setParticipantsCheckedCopy(participant) {
      const existedIndex = this.participantsCheckedCopy.findIndex(
        copiedParticipant => copiedParticipant.id === participant.id,
      )
      if (existedIndex !== -1) {
        this.participantsCheckedCopy[existedIndex] = participant
      } else {
        this.participantsCheckedCopy.push(participant)
      }
    },
    setParticipantsChecked() {
      this.participantsChecked = this.entityModel.linkBookingToVksUsersOthers.concat(
        this.entityModel.linkBookingToParticipants,
      )
    },
    updateParticipant(updatedParticipant) {
      this.setParticipantsCheckedCopy(updatedParticipant)
      this.participantsOptions = this.participantsOptions.map(participant => {
        return participant.id !== updatedParticipant.id
          ? participant
          : updatedParticipant
      })
    },
    createParticipant(createdParticipant) {
      this.setParticipantsCheckedCopy(createdParticipant)
      this.participantsOptions.unshift(createdParticipant)
    },
    findIntersection: debounce(async function(this) {
      const model = new EditBookingModalRequestEntity(
        this.permanent,
        this.entityModel,
        module.getTimezoneOffset(
          this.timeZoneOptions,
          this.entityModel.timeZone,
        ),
      ).model
      this.hasIntersection = await module.findIntersection(
        model,
        this.selectedEntity.id,
      )
    }, CONSTANTS.debounce.timeOut.slow),

    addLinkBookingToParticipants() {
      const participantsEl: any = this.$refs.editBookingListParticipants
      participantsEl.selectedItem = {}
      participantsEl.isVisibleParticipantDialog = true
    },
  },
})
</script>

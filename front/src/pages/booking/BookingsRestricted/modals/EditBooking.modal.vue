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
                v-if="entityModel.periodic"
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
                <el-input-number
                  v-model="entityModel.repeatCount"
                  :min="0"
                  @change="entityChanged('repeatCount', $event)"
                />
              </el-form-item>
            </template>
            <template #column_1>
              <edit-booking-list
                ref="editBookingListParticipants"
                :list-class="'participants'"
                :select-type="selectTypes.multiple"
                :options="participantsOptions"
                :space-id="entityModel.spaceId"
                :selected-owner="entityModel.ownerId"
                :loading="optionsParticipantsLoading"
                :checkedCopy="participantsCheckedCopy"
                :checked="participantsChecked"
                @search="getOptionsParticipants"
                @update="updateParticipants"
                @update-one="updateParticipant"
                @create="createParticipant"
              />
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
      @updateSchedule="entityChanged('schedule', $event)"
      @updateTab="entityChanged('scheduleTab', $event)"
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
} from '@/pages/booking/BookingsRestricted/config/editBookingModal.interface'
import EditBookingPeriodicModal from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodic.modal.vue'
import EditBookingList from '@/pages/booking/BookingsRestricted/modals/EditBookingList.vue'
import {
  modalColumns,
  selectTypes,
} from '@/pages/booking/BookingsRestricted/config/editBookingModal.const'
import { methods } from '@/api_services/httpMethods.enum'
import {
  datePickerType,
  datePickerTypes,
  timeFormats,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { DateTime } from '@/modules/DateTime/DateTime'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import {
  intersectionFields,
  placeholders,
} from '@/pages/booking/Bookings/modals/config/editBookingModal.const'
import { User } from '@/modules/User/User'
import { EditBookingModalRequestEntity } from '@/pages/booking/Bookings/modals/config/editBookingModal.request.entity'
import { debounce } from '@/utils'
import CONSTANTS from '@/constants'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import MpDatePickerWithTimeSelect from '@/components/basic/MpDatePickerWithTimeSelect/MpDatePickerWithTimeSelect.vue'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'
import DurationDoubleSelect from '@/pages/booking/Bookings/DurationDoubleSelect.vue'
import * as module from '@/pages/booking/Bookings/modals/modules/getOptionsBookingModule'
import { FormValidation } from '@/modules/FormValidation/FormValidation'
import { TIMEOUT_SERVICE_SHORT } from '@/constant'

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
    MpSelect,
    MpButton,
    EditBookingList,
    FormColumnLayout,
    DurationDoubleSelect,
    EditBookingPeriodicModal,
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
      datePickerTypes,
      timeFormats,
      nonCopiedValues,
      selectTypes,
      text: '',
      entityModel: new VksBookingEntity(),
      participantsCheckedCopy: [],
      savingLoading: false,
      hasIntersection: false,
      editPeriodicModalVisible: false,
      optionsParticipantsLoading: false,
      ownerOptions: [],
      participantsOptions: [],
      connectionTypeOptions: [],
      timeZoneOptions: [],
      participantsChecked: [],
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
    }
  },
  computed: {
    columns() {
      return modalColumns
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
  watch: {
    'entityModel.periodic'(newPeriodic, oldPeriodic) {
      if (newPeriodic !== oldPeriodic) {
        this.entityChanged('periodic', newPeriodic)
        this.getBookingTypeOptions()
      }
      if (newPeriodic) {
        this.updateText()
      }
    },
  },
  async mounted() {
    await this.getTimeZoneOptions()
    if (this.selectedEntity.id) {
      await this.getSelectedBooking(this.selectedEntity.id)
      this.participantsOptions = module.setCheckedFirst(
        this.participantsOptions,
        this.entityModel.linkBookingToVksUsersOthers.concat(
          this.entityModel.linkBookingToParticipants,
        ),
      )
      this.setParticipantsChecked()
      if (this.entityModel.schedule) {
        await this.updateText()
      }
    } else {
      await this.getBookingTypeOptions()
    }
    const user = User.getUserName()
    if (user && !this.selectedEntity.id) {
      await this.getOptionsOwnerByLogin(user)
    }
    await this.getOptionsParticipants()
    await this.getOptionsSpaces()
  },
  methods: {
    async getOptionsOwnerByLogin(search) {
      if (search) {
        this.ownerOptions = await module.getOptionsOwnerByLogin(search)
        if (this.ownerOptions.length) {
          this.entityModel.ownerId = this.ownerOptions[0].id
        }
      }
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
        if (this.entityModel.schedule) {
          await this.updateText()
        }
        this.participantsCheckedCopy = res.linkBookingToVksUsersOthers
          .concat(res.linkBookingToParticipants)
          .map(participant => {
            return JSON.parse(JSON.stringify(participant))
          })
      }
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
    async getOptionsSpaces(search) {
      const res = await module.getOptionsSpaces(search)
      this.entityModel.spaceId = res[0].id
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
    toTimeZone(date) {
      return new DateTime({ dateTime: date }).toGlobalTime(
        datePickerType.iso,
        module.getTimezoneOffset(
          this.timeZoneOptions,
          this.entityModel.timeZone,
        ),
      )
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
    async getBookingTypeOptions() {
      this.entityModel.typeId = await module.getBookingTypeOptions(
        this.entityModel.periodic,
        this.permanent,
      )
    },
    async getTimeZoneOptions(search) {
      const timeZoneOptions = await module.getTimeZoneOptions(search)
      this.timeZoneOptions = timeZoneOptions.list
      if (!this.selectedEntity.id && timeZoneOptions.initial) {
        this.entityModel.timeZone = timeZoneOptions.initial
      }
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
    async submit() {
      const form: VForm = this.$refs['editBooking'] as VForm
      form.validate(async valid => {
        if (valid) {
          this.savingLoading = true
          const method = this.selectedEntity.id ? methods.put : methods.post
          const data = {
            ...this.entityModel,
            id: this.entityModel.id || undefined,
            description: this.entityModel.description,
            dateStart: this.toTimeZone(this.entityModel.dateStart),
            pinDateStart: this.toTimeZone(this.entityModel.pinDateStart),
            dateEnd: this.entityModel.periodic
              ? this.toTimeZone(this.entityModel.dateEnd)
              : null,
            schedule: this.entityModel.periodic
              ? this.entityModel.schedule
              : '',
            repeatCount: this.entityModel.periodic
              ? this.entityModel.repeatCount
              : 0,
            linkBookingToParticipants: this.entityModel.linkBookingToParticipants.map(
              (participant: VksBookingLinkToParticipant) => ({
                vksParticipantId: participant.id,
                vksUserName: participant.name,
                uri: participant.uri,
                email: participant.email,
                callLegProfileGuid: participant.callLegProfileGuid,
              }),
            ),
            linkBookingToVksUsersOthers: this.entityModel.linkBookingToVksUsersOthers.map(
              (participant: VksBookingLinkBookingToVksUsersOthers) => ({
                vksUsersOtherId: participant.id.toString().includes('other')
                  ? participant.id.toString().replace(/other/i, '')
                  : 0,
                vksUserOtherName: participant.name,
                uri: participant.uri,
                email: participant.email,
              }),
            ),
            scheduleTab: this.entityModel.scheduleTab,
            typeId: this.entityModel.typeId,
            isSendNotification: true,
            isSyncToExchange: true,
          }
          const res = await this.$api.booking({
            method: method,
            data: data,
          })
          if (res && !res.validation) {
            this.$emit('refresh-service', res.id)
            // Бизнес логика: сервис не успевает обновить состояние до загрузки данных, поэтому ждем его 3 секунды
            setTimeout(async () => {
              this.$emit('update')
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
    addLinkBookingToParticipants() {
      const participantsEl: any = this.$refs.editBookingListParticipants
      participantsEl.selectedItem = {}
      participantsEl.isVisibleParticipantDialog = true
    },
    close() {
      this.$message.info(this.$t('notifications.cancel.action') as string)
      this.$emit('close')
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
  },
})
</script>

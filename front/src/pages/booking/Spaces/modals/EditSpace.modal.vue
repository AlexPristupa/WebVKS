<template>
  <el-dialog
    v-el-drag-dialog
    width="65%"
    class="mp-modal mp-modal--spaces"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
  >
    <template #default>
      <el-form
        ref="editSpace"
        :model="entityModel"
        :rules="rules"
        label-width="40%"
        class="mp-form"
      >
        <el-form-item
          :label="$t('forms.editSpace.serversGroupsId')"
          prop="serversGroupsId"
        >
          <mp-select
            filterable
            v-model="entityModel.serversGroupsId"
            option-key="name"
            option-value="id"
            option-label="name"
            :option-list="serversList"
            :filter-method="getOptionsServersGroups"
            :popper-append-to-body="false"
            :placeholder="placeholders.serversGroupsId"
            @change="entityChanged('serversGroupsId', $event)"
          />
        </el-form-item>

        <el-form-item prop="name" :label="$t('forms.editSpace.name')">
          <el-input
            clearable
            v-model="entityModel.name"
            :placeholder="placeholders.name"
          />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.uri')" prop="uri">
          <el-input
            clearable
            v-model="entityModel.uri"
            :placeholder="placeholders.uri"
            @input="entityChanged('uri', $event)"
          />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.ownerId')" prop="ownerId">
          <mp-select
            filterable
            clearable
            v-model="entityModel.ownerId"
            option-key="name"
            option-value="id"
            option-label="name"
            :option-list="ownerOptions"
            :filter-method="setOwnerOptions"
            :popper-append-to-body="false"
            :placeholder="placeholders.ownerId"
            @change="entityChanged('ownerId', $event)"
          />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editSpace.callLegProfileGuid')"
          prop="callLegProfileGuid"
        >
          <mp-select
            filterable
            clearable
            v-model="entityModel.callLegProfileGuid"
            option-key="callLegProfilesName"
            option-value="callLegProfilesId"
            option-label="callLegProfilesName"
            :option-list="callLegProfileGuidOptions"
            :filter-method="getCallLegProfileGuidOptions"
            :popper-append-to-body="false"
            :placeholder="placeholders.callLegProfileGuid"
            @change="entityChanged('callLegProfileGuid', $event)"
          />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editSpace.callBrandingProfileGuid')"
          prop="callBrandingProfileGuid"
        >
          <mp-select
            filterable
            clearable
            v-model="entityModel.callBrandingProfileGuid"
            option-key="callBrandingProfileId"
            option-value="callBrandingProfileId"
            option-label="callBrandingProfileId"
            :option-list="callBrandingProfileGuidOptions"
            :filter-method="getCallBrandingProfileGuidOptions"
            :popper-append-to-body="false"
            :placeholder="placeholders.callBrandingProfileGuid"
            @change="entityChanged('callBrandingProfileGuid', $event)"
          />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.tagCdr')" prop="tagCdr">
          <el-input
            clearable
            v-model="entityModel.tagCdr"
            :placeholder="placeholders.tagCdr"
          />
        </el-form-item>

        <el-form-item
          v-if="!selectedEntity.id"
          :label="$t('forms.editSpace.callIdGeneration')"
          prop="callIdGeneration"
        >
          <el-switch v-model="entityModel.callIdGeneration" />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.callId')" prop="callId">
          <el-input
            clearable
            v-model="entityModel.callId"
            :disabled="entityModel.callIdGeneration"
            :placeholder="placeholders.callIdGeneration"
          />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.password')" prop="password">
          <el-input
            clearable
            v-model="entityModel.password"
            :placeholder="placeholders.password"
          />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.uriAlt')" prop="uriAlt">
          <el-input
            clearable
            v-model="entityModel.uriAlt"
            :placeholder="placeholders.uriAlt"
          />
        </el-form-item>

        <el-form-item
          v-if="!selectedEntity.id"
          :label="$t('forms.editSpace.guestPasswordGeneration')"
          prop="guestPasswordGeneration"
        >
          <el-switch v-model="entityModel.guestPasswordGeneration" />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editSpace.passwordGuest')"
          prop="passwordGuest"
        >
          <el-input
            clearable
            v-model="entityModel.passwordGuest"
            :disabled="entityModel.guestPasswordGeneration"
            :placeholder="placeholders.passwordGuest"
          />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editSpace.isGuestAccessible')"
          prop="isGuestAccessible"
        >
          <el-switch v-model="entityModel.isGuestAccessible" />
        </el-form-item>

        <el-form-item :label="$t('forms.editSpace.uriVideo')" prop="uriVideo">
          <el-input
            clearable
            v-model="entityModel.uriVideo"
            :placeholder="placeholders.uriVideo"
          />
        </el-form-item>
        <div class="mp-modal__text-insertion">
          <i class="el-icon-user"></i>
          <p>{{ $t('forms.editSpace.spaceParticipantsSettings') }}</p>
        </div>
        <edit-space-participants-table
          ref="editSpaceParticipantsTable"
          :selected="entityModel"
          :table-rows="linkSpaceToParticipantsRows"
          @table-checkboxes-changed="tableParticipantsCheckboxesChanged"
          @table-select-changed="tableParticipantsSelectChanged"
          @delete="deleteLinkSpaceToParticipantsRow"
          @search="tableParticipantsSearchChanged"
          @add-row="addParticipantsRow"
        />
        <div class="mp-modal__text-insertion">
          <i class="el-icon-time"></i>
          <p>{{ $t('forms.editSpace.bookingSettings') }}</p>
        </div>
        <el-form-item
          :label="$t('forms.editSpace.isAvailableForBooking')"
          prop="isAvailableForBooking"
        >
          <el-switch v-model="entityModel.isAvailableForBooking" />
        </el-form-item>
        <edit-space-booking-table
          ref="editSpaceBookingTable"
          v-if="entityModel.id"
          :selected="entityModel"
        />
      </el-form>
    </template>
    <template v-slot:footer>
      <mp-button
        type="primary"
        mp-status="normal"
        mp-type="save"
        :loading="disabledSaveButton"
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
import {
  IComputedEditSpaceModal,
  IDataEditSpaceModal,
  IMethodsEditSpaceModal,
  IPropsEditSpaceModal,
} from '../config/EditSpace.modal.interface'
import EditSpaceBookingTable from '@/pages/booking/Spaces/modals/EditSpaceBookingTable.vue'
import EditSpaceParticipantsTable from '@/pages/booking/Spaces/modals/EditSpaceParticipantsTable.vue'
import { VksSpaceTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksSpaceTable.entity'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { methods } from '@/api_services/httpMethods.enum'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import { FormValidation } from '@/modules/FormValidation/FormValidation'
import { TIMEOUT_SERVICE } from '@/constant'
import * as module from '@/pages/booking/Spaces/modules/EditSpace.modal.module'
import { placeholders } from '@/pages/booking/Spaces/config/EditSpace.modal.const'
import { VForm } from '@/modules/Form/Form.interface'

export default Vue.extend<
  IDataEditSpaceModal,
  IMethodsEditSpaceModal,
  IComputedEditSpaceModal,
  IPropsEditSpaceModal
>({
  name: 'EditSpaceModal',
  components: {
    MpButton,
    MpSelect,
    EditSpaceBookingTable,
    EditSpaceParticipantsTable,
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
      default: () => new VksSpaceTableEntity(),
    },
  },
  data() {
    return {
      placeholders,
      ownerOptions: [],
      callLegProfileGuidOptions: [],
      callBrandingProfileGuidOptions: [],
      serversList: [],
      nonCopiedValues: nonCopiedValues,
      entityModel: new VksSpaceEntity(),
      validationFromBackData: [],
      linkSpaceToParticipantsRows: [],
      savingLoading: false,
    }
  },
  computed: {
    rules() {
      return {
        serversGroupsId: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            validator: this.validation,
            trigger: TriggerType.blur,
          },
        ],
        name: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            validator: this.validation,
            trigger: TriggerType.blur,
          },
        ],
        uri: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            validator: this.validation,
            trigger: TriggerType.blur,
          },
        ],
      }
    },
    title() {
      return this.$t(
        this.selectedEntity.id
          ? 'dialogs.titles.editingEntity'
          : 'dialogs.titles.addingEntity',
        [this.$t('dialogs.entities.cmsSpace')],
      ).toString()
    },
    disabledSaveButton() {
      return this.savingLoading
    },
  },
  async mounted() {
    await this.getOptionsServersGroups()
    await this.setOwnerOptions()
    if (this.selectedEntity.id) {
      await this.getSelected(this.selectedEntity.id)
    }
    await this.getCallLegProfileGuidOptions()
    await this.getCallBrandingProfileGuidOptions()
    await this.setLinkSpaceToParticipantsRows()
    this.setLinkSpaceToParticipantsModel()
  },
  methods: {
    async addParticipantsRow() {
      const ownerOptions = await this.getOptionsOwner()
      this.entityModel.linkSpaceToParticipants = module.addParticipantsRow(
        this.entityModel.linkSpaceToParticipants,
        ownerOptions,
      )
      await this.setLinkSpaceToParticipantsRows()
    },
    tableParticipantsSelectChanged(data) {
      const row = this.linkSpaceToParticipantsRows.find(
        item => item.id === data.id,
      )
      if (row) {
        row[data.field] = data.value
        if (data.field === 'vksUser') {
          row.vksUserName = data.option.name.toString()
        }
        this.setLinkSpaceToParticipantsModel()
        this.setLinkSpaceToParticipantsRows()
      }
    },
    tableParticipantsSearchChanged(searchString) {
      this.entityModel.linkSpaceToParticipants = this.entityModel.linkSpaceToParticipants.map(
        row => {
          row.found = searchString
            ? row.vksUserName.toLowerCase().includes(searchString.toLowerCase())
            : false
          return row
        },
      )
      this.setLinkSpaceToParticipantsRows()
    },
    tableParticipantsCheckboxesChanged(data) {
      const rowRights =
        this.linkSpaceToParticipantsRows.find(item => item.id === data.id)
          ?.rights || []
      const right = rowRights.find(right => right.field === data.field)
      if (right) {
        right.checked = !right.checked
        this.setLinkSpaceToParticipantsModel()
        this.setLinkSpaceToParticipantsRows()
      }
    },
    async getSelected(id) {
      const res = await this.$api.spaces({
        method: methods.get,
        data: {
          id: id,
        },
      })
      if (res) {
        this.entityModel = new VksSpaceEntity(res)
      }
    },
    async getOptionsServersGroups(searchString = '') {
      const res = await this.$api.dictionaryServersGroups({
        method: methods.get,
        data: {
          search: searchString,
          limit: 300,
        },
      })
      this.serversList = res || []
      if (!this.selectedEntity.id && !searchString) {
        this.entityModel.serversGroupsId = this.serversList[0].id
      }
    },
    async entityChanged(entity, value) {
      if (entity === 'uri') {
        this.entityModel.tagCdr = 'cdr' + value
        this.entityModel.callId = value
        return
      }
      this.entityModel[entity] = value
      if (entity === 'serversGroupsId') {
        this.entityModel.callBrandingProfileGuid = ''
        this.entityModel.callLegProfileGuid = ''
        await this.setLinkSpaceToParticipantsRows()
        await this.getCallLegProfileGuidOptions()
        await this.getCallBrandingProfileGuidOptions()
        this.setLinkSpaceToParticipantsModel()
      }
    },
    submit() {
      const isUniqueUserValid = this.checkUniqueUsersData()
      const form: VForm = this.$refs['editSpace'] as VForm
      form.validate(async valid => {
        if (valid && isUniqueUserValid) {
          this.savingLoading = true
          await this.addEquipment()
          const method = this.selectedEntity.id ? methods.put : methods.post
          const res = await this.$api.spaces({
            method: method,
            data: {
              ...this.entityModel,
              id: this.selectedEntity.id || undefined,
            },
          })
          if (!res.validation) {
            this.$message.success(
              this.$t(
                `notifications.data.${
                  this.selectedEntity.id ? 'updated' : 'added'
                }`,
              ) as string,
            )
            this.$emit('update')
            this.$emit('close')
          } else {
            this.validationFromBackData = res.validation
            form.validate(() => {
              this.validationFromBackData = []
            })
          }
          this.savingLoading = false
        }
      })
    },
    checkUniqueUsersData() {
      const valueArr: Array<number> = this.entityModel.linkSpaceToParticipants
        .map(participant => participant.vksUserId)
        .filter((e, i, a) => a.indexOf(e) !== i)
      if (valueArr.length) {
        const usersNames: Array<string> = []
        valueArr.forEach((id: number) => {
          const user: string = this.entityModel.linkSpaceToParticipants.find(
            participant => participant.vksUserId === id,
          ).vksUserName
          if (user) {
            usersNames.push(user)
          }
        })
        this.$message.error(
          this.$t('forms.validationError.deleteRepeatedUsers', [
            usersNames.join(),
          ]).toString(),
        )
        return false
      }
      return true
    },
    async addEquipment() {
      const method = this.selectedEntity.id ? methods.put : methods.post
      const data = {
        ...this.entityModel,
        id: this.selectedEntity.id || undefined,
        guid: this.selectedEntity.id ? this.entityModel.guid : '',
      }
      const endpoint = this.selectedEntity.id
        ? 'proxyBookingSpaceEdit'
        : 'proxyBookingSpaceAdd'
      const res = await this.$api.proxyBooking({
        method: methods.get,
        timeout: TIMEOUT_SERVICE,
      })
      if (res) {
        const eventResponse = await this.$api[endpoint]({
          method: method,
          data: data,
        })
        if (eventResponse && eventResponse.guid) {
          this.entityModel.guid = eventResponse.guid
          this.entityModel.callId = eventResponse.callid
          this.entityModel.passwordGuest = eventResponse.passwordguest
        }
      }
    },
    close() {
      this.$message.info(this.$t('notifications.cancel.action').toString())
      this.$emit('close')
    },
    async setLinkSpaceToParticipantsRows() {
      //@ts-ignore
      this.$refs.editSpaceParticipantsTable.loading = true
      if (this.entityModel.linkSpaceToParticipants.length) {
        const ownerOptions = await this.getOptionsOwner()
        this.linkSpaceToParticipantsRows = module.setLinkSpaceToParticipantsRows(
          ownerOptions,
          this.entityModel.linkSpaceToParticipants,
          this.entityModel.serversGroupsId,
        )
      }
      //@ts-ignore
      this.$refs.editSpaceParticipantsTable.loading = false
    },
    setLinkSpaceToParticipantsModel() {
      this.entityModel.linkSpaceToParticipants = module.setLinkSpaceToParticipantsModel(
        this.linkSpaceToParticipantsRows,
      )
    },

    deleteLinkSpaceToParticipantsRow(id) {
      this.linkSpaceToParticipantsRows = this.linkSpaceToParticipantsRows.filter(
        row => row.id !== id,
      )
      this.setLinkSpaceToParticipantsModel()
      this.setLinkSpaceToParticipantsRows()
    },
    async setOwnerOptions(search) {
      this.ownerOptions = await this.getOptionsOwner(search)
    },
    async getOptionsOwner(search) {
      const res = await this.$api.dictionaryVksUser({
        method: methods.get,
        data: {
          limit: 300,
          search: search,
        },
      })
      if (res) {
        return res
      }
    },
    async getCallLegProfileGuidOptions() {
      const res = await this.$api.proxySpacesCallLegProfiles({
        method: methods.post,
        data: {
          serversGroupsId: this.entityModel.serversGroupsId,
        },
      })
      this.callLegProfileGuidOptions = res || []
    },
    async getCallBrandingProfileGuidOptions() {
      const res = await this.$api.proxySpacesCallBrandingProfiles({
        method: methods.post,
        data: {
          serversGroupsId: this.entityModel.serversGroupsId,
        },
      })
      this.callBrandingProfileGuidOptions = res || []
    },
    validation(rule, value, callback) {
      if (this.validationFromBackData) {
        FormValidation.backValidationField(
          this.validationFromBackData,
          rule,
          value,
          callback,
        )
        const validationWithoutField = FormValidation.backValidationWithoutField(
          this.validationFromBackData,
        )
        if (validationWithoutField) {
          this.$message.error(validationWithoutField)
        }
      }
    },
  },
})
</script>

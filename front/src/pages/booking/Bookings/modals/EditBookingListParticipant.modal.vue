<template>
  <form-layout>
    <el-dialog
      v-el-drag-dialog
      class="mp-modal mp-modal--edit-booking-list-participant"
      :visible="visibleEditModal"
      :title="title"
      :append-to-body="true"
      width="50%"
      @close="close"
    >
      <template #default>
        <el-form
          ref="editBookingListParticipant"
          :model="entityModel"
          :rules="rules"
          label-width="200px"
          class="mp-form"
        >
          <el-form-item
            prop="name"
            :label="$t('forms.editBookingListParticipant.vksUserName')"
          >
            <el-input
              clearable
              :disabled="!!entityModel.id && !selectedEntity.isFromOtherList"
              v-model="entityModel.name"
              :placeholder="
                $t('forms.placeholders.enterEntity', [
                  FirstLetterToCase.placeholderToLower(
                    'vksUserName',
                    'editBookingListParticipant',
                  ),
                ])
              "
            />
          </el-form-item>
          <el-form-item
            prop="uri"
            :label="$t('forms.editBookingListParticipant.uri')"
          >
            <el-input
              clearable
              v-model="entityModel.uri"
              :placeholder="
                $t('forms.placeholders.enterEntity', [
                  $t('forms.editBookingListParticipant.uri'),
                ])
              "
            />
          </el-form-item>
          <el-form-item
            prop="email"
            :label="$t('forms.editBookingListParticipant.email')"
          >
            <el-input
              :disabled="!!selectedEntity.id && !selectedEntity.isFromOtherList"
              clearable
              v-model="entityModel.email"
              :placeholder="
                $t('forms.placeholders.enterEntity', [
                  FirstLetterToCase.placeholderToLower(
                    'email',
                    'editBookingListParticipant',
                  ),
                ])
              "
            />
          </el-form-item>
          <el-form-item
            v-if="selectedEntity.id && !selectedEntity.isFromOtherList"
            prop="callLegProfileGuid"
            :label="$t('forms.editBookingListParticipant.callLegProfileGuid')"
          >
            <mp-select
              filterable
              :value="entityModel.callLegProfileGuid"
              option-key="callLegProfilesName"
              option-value="callLegProfilesId"
              option-label="callLegProfilesName"
              :option-list="callLegProfileGuidOptions"
              :popper-append-to-body="false"
              :placeholder="
                $t('forms.placeholders.chooseEntity', [
                  FirstLetterToCase.placeholderToLower(
                    'callLegProfileGuid',
                    'editBookingListParticipant',
                  ),
                ])
              "
              @change="entityChanged('callLegProfileGuid', $event)"
            />
          </el-form-item>
        </el-form>
      </template>
      <template slot="footer">
        <mp-button
          type="primary"
          :disabled="savingLoading"
          mp-status="normal"
          :mp-type="entityModel.id ? 'save' : 'create'"
          @click="submit"
        >
          {{
            entityModel.id ? $t('button.title.save') : $t('button.title.create')
          }}
        </mp-button>

        <mp-button
          type=""
          mp-status="normal"
          mp-type="close"
          @click="$emit('close')"
        >
          {{ $t('button.title.close') }}
        </mp-button>
      </template>
    </el-dialog>
  </form-layout>
</template>
<script lang="ts">
import Vue from 'vue'
// @ts-ignore
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import { methods } from '@/api_services/httpMethods.enum'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { VForm } from '@/modules/Form/Form.interface'
import {
  IComputedEditBookingListParticipantModal,
  IDataEditBookingListParticipantModal,
  IMethodsEditBookingListParticipantModal,
  IPropsEditBookingListParticipantModal,
} from '@/pages/booking/Bookings/modals/config/editBookingListParticipantModal.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'

export default Vue.extend<
  IDataEditBookingListParticipantModal,
  IMethodsEditBookingListParticipantModal,
  IComputedEditBookingListParticipantModal,
  IPropsEditBookingListParticipantModal
>({
  name: 'EditBookingListParticipantModal',
  components: { MpButton, FormLayout, MpSelect },
  directives: {
    elDragDialog,
  },
  props: {
    selectedEntity: {
      type: Object as () =>
        | VksBookingLinkToParticipant
        | VksSelectOptionEntity
        | VksBookingLinkBookingToVksUsersOthers,
      default: () => new VksSelectOptionEntity(),
    },
    spaceId: {
      type: Number,
      default: 0,
    },
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    disabledSaveButton() {
      return false
    },
    title() {
      return this.entityModel.id
        ? this.$t('dialogs.titles.editBookingListParticipant')
        : this.$t('dialogs.titles.addBookingListParticipant')
    },
    rules() {
      return {
        uri: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
          {
            validator: this.uriValidator,
            trigger: TriggerType.blur,
          },
        ],
        email: [
          {
            type: 'email',
            message: this.$t('forms.validationError.pleaseEnterEntity', [
              (this.$t(
                'forms.validationError.pleaseOptions.correctEmail',
              ) as string).toLowerCase(),
            ]),
            trigger: TriggerType.blur,
          },
        ],
      }
    },
  },
  data() {
    return {
      nonCopiedValues,
      savingLoading: false,
      entityModel: new VksBookingLinkToParticipant(),
      callLegProfileGuidOptions: [],
      FirstLetterToCase,
    }
  },
  mounted() {
    this.getCallLegProfileGuidOptions()
    if (this.selectedEntity.id) {
      this.entityModel = this.selectedEntity
    }
  },
  methods: {
    submit() {
      this.savingLoading = true
      const form: VForm = this.$refs['editBookingListParticipant'] as VForm
      form.validate(async valid => {
        if (valid) {
          if (!this.entityModel.id) {
            const newEntity = new VksBookingLinkBookingToVksUsersOthers(
              this.entityModel,
            )
            this.$emit('create', newEntity)
          } else {
            const updatedEntity = (this.selectedEntity as
              | VksBookingLinkToParticipant
              | VksBookingLinkBookingToVksUsersOthers).isFromOtherList
              ? new VksBookingLinkBookingToVksUsersOthers(this.entityModel)
              : new VksBookingLinkToParticipant(this.entityModel)
            this.$emit('update', updatedEntity)
          }
          this.close()
        }
      })
      this.savingLoading = false
    },
    close() {
      this.$emit('close')
    },
    entityChanged(entity, value) {
      this.entityModel[entity] = value
    },
    async getCallLegProfileGuidOptions() {
      const spacesCallLegId = await this.getSpaceId(this.spaceId)
      const res = await this.$api.proxySpacesCallLegProfiles({
        method: methods.post,
        data: {
          serversGroupsId: spacesCallLegId,
        },
      })
      this.callLegProfileGuidOptions = res || []
    },
    async getSpaceId(id) {
      const res = await this.$api.spaces({
        method: methods.get,
        data: {
          id: id,
        },
      })
      if (res) {
        return res.serversGroupsId
      }
    },
    uriValidator(rule, value, callback) {
      const validRegexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
      const validRegexNumber = /^[0-9 ()+-]+$/
      const validRegexWhitespace = /\s/
      if (
        (value.match(validRegexEmail) || value.match(validRegexNumber)) &&
        !value.match(validRegexWhitespace)
      ) {
        callback()
      } else {
        callback(
          new Error(this.$t('forms.validationError.uriValidation').toString()),
        )
      }
    },
  },
})
</script>

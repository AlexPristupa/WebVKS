<template>
  <el-dialog
    v-el-drag-dialog
    width="40%"
    class="mp-modal mp-modal--share-record__add-user"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :append-to-body="true"
    :title="title"
    @close="$emit('close')"
  >
    <template #default>
      <el-form
        ref="shareRecordAddUser"
        :label-position="'right'"
        label-width="8rem"
        class="mp-form"
        :model="entityModel"
        :rules="rules"
      >
        <el-form-item
          :label="$t('forms.shareRecordAddUser.user')"
          prop="userId"
        >
          <mp-select
            filterable
            option-key="name"
            option-value="id"
            option-label="name"
            :option-list="userOptions"
            :value="entityModel.userId"
            :popper-append-to-body="false"
            :filter-method="getUserOptions"
            @change="userChanged"
            :placeholder="
              $t('forms.placeholders.chooseEntity', [
                $t('dialogs.entities.user'),
              ])
            "
          />
        </el-form-item>
        <el-form-item
          :label="$t('forms.shareRecordAddUser.download')"
          prop="isDownload"
        >
          <el-switch v-model="entityModel.isDownload" />
        </el-form-item>
        <el-form-item
          :label="$t('forms.shareRecordAddUser.watch')"
          prop="isPlay"
        >
          <el-switch v-model="entityModel.isPlay" />
        </el-form-item>
        <el-form-item
          :label="$t('forms.shareRecordAddUser.description')"
          prop="description"
        >
          <el-input
            v-model="entityModel.description"
            type="textarea"
            :rows="2"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                FirstLetterToCase.placeholderToLower(
                  'description',
                  'shareRecordAddUser',
                ),
              ])
            "
            clearable
          />
        </el-form-item>
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
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import elDragDialog from '@/directive/el-dragDialog'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import {
  IComputedShareRecordAddUserModal,
  IDataShareRecordAddUserModal,
  IMethodsShareRecordAddUserModal,
  IPropsShareRecordAddUserModal,
} from '@/pages/booking/Recordings/modals/config/addUserToShareRecord.modal.interface'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import { methods } from '@/api_services/httpMethods.enum'
import { DateTime } from '@/modules/DateTime/DateTime'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'
import { VForm } from '@/modules/Form/Form.interface'
import { FormValidation } from '@/modules/FormValidation/FormValidation'

export default Vue.extend<
  IDataShareRecordAddUserModal,
  IMethodsShareRecordAddUserModal,
  IComputedShareRecordAddUserModal,
  IPropsShareRecordAddUserModal
>({
  name: 'AddUserToShareRecordsModel',
  components: {
    MpSelect,
    MpButton,
  },
  directives: {
    elDragDialog,
  },
  props: {
    visibleEditModal: {
      type: Boolean as () => boolean,
      default: false,
    },
    selectedEntity: {
      type: Object as () => VksRecordingsEntity,
      default: () => new VksRecordingsEntity(),
    },
  },
  data() {
    return {
      FirstLetterToCase,
      nonCopiedValues: nonCopiedValues,
      entityModel: {
        userId: null,
        isDownload: false,
        isPlay: false,
        description: '',
      },
      userOptions: [],
      validationFromBackData: [],
      savingLoading: false,
    }
  },
  computed: {
    rules() {
      return {
        userId: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField').toString(),
            trigger: TriggerType.blur,
          },
        ],
      }
    },
    disabledSaveButton() {
      return this.savingLoading
    },
    title() {
      return this.$t('dialogs.titles.addingEntity', [
        this.$t('dialogs.entities.user'),
      ])
    },
  },
  async mounted() {
    await this.getUserOptions('')
  },
  methods: {
    async getUserOptions(search) {
      const res = await this.$api.dictionaryAspNetUsers({
        method: methods.get,
        data: {
          limit: 300,
          search: search,
        },
      })
      if (res && !res.validation) {
        this.userOptions = res
      }
    },
    userChanged(value) {
      this.entityModel.userId = value
    },

    async submit() {
      const form: VForm = this.$refs['shareRecordAddUser'] as VForm
      form.validate(async valid => {
        if (valid) {
          this.savingLoading = true
          const res = await this.$api.recordingsUsers({
            method: methods.post,
            data: {
              ...this.entityModel,
              recordingId: this.selectedEntity.id,
              dateRecord: new DateTime({ date: new Date() }).toISO(),
            },
          })
          if (res && !res.validation) {
            this.$message.success(this.$t('notifications.data.saved') as string)
            this.$emit('update')
            this.$emit('close')
          } else {
            const validationWithoutField = FormValidation.backValidationWithoutField(
              res.validation,
            )
            if (validationWithoutField) {
              this.$message.error(validationWithoutField)
            }
          }
          this.savingLoading = false
        }
      })
    },

    close() {
      this.$emit('close')
    },

    validation() {},
  },
})
</script>

<style scoped></style>

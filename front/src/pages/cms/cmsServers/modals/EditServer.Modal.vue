<template>
  <el-dialog
    width="30%"
    v-el-drag-dialog
    class="mp-modal mp-modal--servers"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
    :draggable="false"
  >
    <template #default>
      <el-form
        ref="editServer"
        :model="entityModel"
        :rules="rules"
        label-width="170px"
        class="mp-form"
      >
        <el-form-item prop="name" :label="$t('forms.editServer.label.name')">
          <el-input
            clearable
            v-model="entityModel.name"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                FirstLetterToCase.placeholderToLower(
                  'name',
                  'placeholders.enterEntity',
                ),
              ])
            "
          />
        </el-form-item>

        <el-form-item
          prop="remoteIpAddress"
          :label="$t('forms.editServer.label.remoteIpAddress')"
        >
          <el-input
            clearable
            v-model="entityModel.remoteIpAddress"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                $t('forms.editServer.label.remoteIpAddress'),
              ])
            "
          />
        </el-form-item>

        <el-form-item prop="login" :label="$t('forms.editServer.label.login')">
          <el-input
            clearable
            v-model="entityModel.login"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                FirstLetterToCase.placeholderToLower(
                  'login',
                  'placeholders.enterEntity',
                ),
              ])
            "
          />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editServer.label.password')"
          prop="password"
        >
          <el-input
            clearable
            v-model="entityModel.password"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                FirstLetterToCase.placeholderToLower(
                  'password',
                  'placeholders.enterEntity',
                ),
              ])
            "
            type="password"
          />
        </el-form-item>
      </el-form>
    </template>

    <template #footer>
      <mp-button
        type="primary"
        mp-status="normal"
        :disabled="disabledSaveButton"
        @click="submit"
        :loading="savingLoading"
      >
        {{ $t('button.title.save') }}
      </mp-button>

      <mp-button
        type=""
        mp-status="normal"
        :disabled="savingLoading"
        @click="close"
      >
        {{ $t('button.title.cancel') }}
      </mp-button>
    </template>
  </el-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import elDragDialog from '@/directive/el-dragDialog'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface.ts'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase.ts'
import {
  IComputedEditServerModal,
  IDataEditServerModal,
  IMethodsEditServerModal,
  IPropsEditServerModal,
} from '@/pages/cms/cmsServers/config/editServerModal.interface.ts'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'

export default Vue.extend<
  IDataEditServerModal,
  IMethodsEditServerModal,
  IComputedEditServerModal,
  IPropsEditServerModal
>({
  name: 'EditServerModal',
  components: {
    MpButton,
  },
  directives: {
    elDragDialog,
  },
  data() {
    return {
      FirstLetterToCase,
      entityModel: new VksServerEntity(),
      savingLoading: false,
      rules: {
        name: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
      },
      nonCopiedValues,
    }
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Object,
      default: () => new VksServerEntity(),
    },
  },
  computed: {
    title() {
      return this.selectedEntity.id !== -1
        ? this.$t('forms.editServer.title.edit', [this.selectedEntity.name])
        : this.$t('forms.editServer.title.add')
    },
    disabledSaveButton() {
      return !this.entityModel.name
    },
  },
  mounted() {
    if (this.selectedEntity.id) {
      Object.keys(this.selectedEntity).forEach(key => {
        if (!this.nonCopiedValues.includes(key)) {
          this.entityModel[key] = this.selectedEntity[key]
        }
      })
    }
  },
  methods: {
    submit() {
      this.$emit('close')
      this.$emit('update')
    },
    close() {
      this.$emit('close')
    },
  },
})
</script>

<template>
  <el-dialog
    width="40%"
    v-el-drag-dialog
    class="mp-modal mp-modal--servers-groups"
    :close-on-click-modal="false"
    :visible="visibleEditModal"
    :title="title"
    @close="$emit('close')"
    :draggable="false"
  >
    <template #default>
      <el-form
        ref="editServerGroups"
        :model="entityModel"
        :rules="rules"
        label-width="170px"
        class="mp-form"
      >
        <el-form-item
          prop="name"
          :label="$t('forms.editServerGroup.label.name')"
        >
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
          prop="description"
          :label="$t('forms.editServerGroup.label.description')"
        >
          <el-input
            clearable
            v-model="entityModel.description"
            :placeholder="
              $t('forms.placeholders.enterEntity', [
                FirstLetterToCase.placeholderToLower(
                  'description',
                  'placeholders.enterEntity',
                ),
              ])
            "
          />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editServerGroup.label.isUseBalancer')"
          label-width="250px"
          prop="isUseBalancer"
        >
          <el-switch v-model="entityModel.isUseBalancer" />
        </el-form-item>

        <el-form-item
          :label="$t('forms.editServerGroup.label.balancerAlgId')"
          prop="balancerAlgId"
        >
          <mp-select
            :value="entityModel.balancerAlgId"
            :disabled="!entityModel.isUseBalancer"
            :option-list="algorithmOptions"
            option-label="privateName"
            option-key="id"
            option-value="id"
            :placeholder="
              $t('forms.placeholders.chooseEntity', [
                FirstLetterToCase.placeholderToLower(
                  'balancerAlgId',
                  'placeholders.enterEntity',
                ),
              ])
            "
            @change="entityChanged('balancerAlgId', $event)"
          />
        </el-form-item>
        <div class="mp-modal__text-insertion">
          <div class="icon--eye" />
          <p>{{ $t('forms.editServerGroup.insertion.text') }}</p>
        </div>
        <mp-draggable
          :settings="{
            withSearch: true,
            disabled: false,
            titles: [
              $t('forms.editServerGroup.label.availableServers'),
              $t('forms.editServerGroup.label.selectedServers'),
            ],
            listForDraggable: entityModel.servers,
            buttons: {
              position: 'middle',
              isHorizontal: true,
            },
          }"
          @update="entityChanged('servers', $event)"
        />
      </el-form>
    </template>

    <template #footer>
      <mp-button
        type="primary"
        mp-status="normal"
        @click="submit"
        :disabled="disabledSaveButton"
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
import { BalancingAlgorithmFactory } from '@/domain'
import { BALANCING_ALGORITHM_CONFIG } from '@/domain/BalancingAlgorithm/BalancingAlgorithm.config.const'
import {
  IComputedEditServerModal,
  IDataEditServerModal,
  IMethodsEditServerModal,
  IPropsEditServerModal,
} from '@/pages/cms/serversGroups/config/editServerGroupsModal.interface'
import { nonCopiedValues } from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { VksServersGroupEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServersGroup.entity'
import MpDraggable from '@/components/MpDraggable/MpDraggable.vue'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'

export default Vue.extend<
  IDataEditServerModal,
  IMethodsEditServerModal,
  IComputedEditServerModal,
  IPropsEditServerModal
>({
  name: 'EditServerGroupsModal',
  components: {
    MpSelect,
    MpDraggable,
    MpButton,
  },
  directives: {
    elDragDialog,
  },
  data() {
    return {
      FirstLetterToCase,
      nonCopiedValues: nonCopiedValues,
      entityModel: new VksServersGroupEntity(),
      rules: {
        name: [
          {
            required: true,
            message: this.$t('forms.validationError.requiredField'),
            trigger: TriggerType.blur,
          },
        ],
      },
      savingLoading: false,
      algorithmOptions: BalancingAlgorithmFactory.create(
        BALANCING_ALGORITHM_CONFIG,
      ),
    }
  },
  props: {
    visibleEditModal: {
      type: Boolean,
      default: false,
    },
    selectedEntity: {
      type: Object,
      default: () => new VksServersGroupEntity(),
    },
  },
  computed: {
    title() {
      return this.selectedEntity.id !== -1
        ? this.$t('forms.editServerGroup.title.edit', [
            this.selectedEntity.name,
          ])
        : this.$t('forms.editServerGroup.title.add')
    },
    disabledSaveButton() {
      return true
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
      console.log('save', this.entityModel)
    },
    close() {
      if (!this.savingLoading) {
        this.$emit('close')
      }
    },
    entityChanged(entity, value) {
      this.entityModel[entity] = value
    },
  },
})
</script>

<template>
  <form-layout>
    <el-dialog
      class="mp-modal dialog--columns-settings"
      :visible="visible"
      :title="$t('general.columnsSetup')"
      @close="$emit('close')"
      close-on-press-escape
      :append-to-body="true"
      width="550px"
    >
      <div class="draggable_section">
        <div class="column">
          <h3 class="column__title">
            {{ $t('general.available') }}
          </h3>
          <div class="column__list">
            <draggable
              :list="availableList"
              group="article"
              class="dragArea"
              @change="columnMoved"
            >
              <div
                v-for="column in availableList"
                :key="column.field"
                class="item"
              >
                <span class="text"> {{ column.headerName }} </span>
                <el-tooltip
                  class="item"
                  effect="dark"
                  :content="$t('notifications.addToCurrent')"
                  placement="top-start"
                >
                  <div class="icon--eye" @click="addColumn(column)" />
                </el-tooltip>
              </div>
            </draggable>
          </div>
        </div>
        <div class="column">
          <h3 class="column__title">
            {{ $t('general.current') }}
          </h3>
          <div class="column__list">
            <draggable
              :list="currentList"
              group="article"
              class="dragArea"
              :disabled="disabledCurrent"
              @change="columnMoved"
              ghost-class="ghost"
            >
              <div
                v-for="column in currentList"
                :key="column.field"
                @click="setActiveColumn(column)"
                :class="{ active: activeColumn === column }"
                class="item"
              >
                <el-tooltip
                  class="item"
                  effect="dark"
                  :content="$t('notifications.addToAvailable')"
                  placement="top-end"
                >
                  <div
                    class="icon--eye slashed"
                    @click="removeColumn(column)"
                  />
                </el-tooltip>
                <span class="text"> {{ column.headerName }} </span>
              </div>
            </draggable>
          </div>
        </div>
      </div>
      <div class="buttons-section">
        <mp-button
          v-for="(button, index) in buttons"
          :key="index"
          :mp-type="button.type"
          @click="changePositionInList(button.arg)"
        >
          {{ button.content }}
        </mp-button>
      </div>
      <div slot="footer" class="dialog-footer">
        <mp-button :mp-type="typeButton.apply" type="primary" @click="save">
          {{ $t('general.apply') }}
        </mp-button>
        <mp-button :mp-type="typeButton.cancel" @click="cancel">
          {{ $t('general.close') }}
        </mp-button>
      </div>
    </el-dialog>
  </form-layout>
</template>

<script lang="ts">
/**
 * @description модальное окно непосредственно настроек колонок определенной таблицы.
 * Включает в себя два draggable листа и кнопки редактирования позиции.
 * Работает совместно с: MpWrapperColumnsSettings
 */
import Vue from 'vue'
import draggable from 'vuedraggable'
import FormLayout from '@/layouts/formLayout/FormLayout.vue'
import { IColumnDefs } from '@/components/MpTable/MpTable.interface'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { movingButtonsVertical } from '@/components/MpDraggable/config/mpDraggable.const'
import { MovingButtonButtonsVerticalArgs } from '@/components/MpDraggable/config/mpDraggable.enum'
import { IColumnsSettingsData } from '@/components/MpTable/columnSettings/ColumnsSettingsDialog.interface'
import { IDragEvent } from '@/components/MpDraggable/config/mpDraggable.interface'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export default Vue.extend({
  name: 'ColumnsSettingDialog',
  components: {
    FormLayout,
    MpButton,
    draggable,
  },
  props: {
    visible: {
      type: Boolean as () => boolean,
      required: true,
      default: false,
    },
    listForDraggable: {
      type: Array as () => Array<IColumnDefs>,
      default: (): Array<IColumnDefs> => [],
    },
  },
  data(): IColumnsSettingsData {
    return {
      typeButton: {
        apply: MpTypeButton.apply,
        cancel: MpTypeButton.cancel,
      },
      buttons: movingButtonsVertical,
      activeColumn: null,
      availableList: [],
      currentList: [],
    }
  },
  computed: {
    disabledCurrent(): boolean {
      return this.currentList.length < 2
    },
  },
  mounted() {
    this.setLists()
  },
  methods: {
    setLists() {
      this.availableList = this.listForDraggable.filter(
        (column: IColumnDefs) => column.hide,
      )
      this.currentList = this.listForDraggable.filter(
        (column: IColumnDefs) => !column.hide,
      )
    },
    changePositionInList(position: string) {
      if (this.activeColumn) {
        const index = this.currentList.indexOf(this.activeColumn)
        this.currentList.splice(index, 1)
        switch (position) {
          case MovingButtonButtonsVerticalArgs.first:
            this.currentList.unshift(this.activeColumn)
            break
          case MovingButtonButtonsVerticalArgs.up:
            this.currentList.splice(index - 1, 0, this.activeColumn)
            break
          case MovingButtonButtonsVerticalArgs.down:
            this.currentList.splice(index + 1, 0, this.activeColumn)
            break
          case MovingButtonButtonsVerticalArgs.last:
            this.currentList.push(this.activeColumn)
            break
        }
      }
    },
    columnMoved(event: IDragEvent) {
      if (event.added) {
        const column: IColumnDefs | undefined = this.listForDraggable.find(
          column => column.field === event.added.element.field,
        )
        if (column) {
          column.hide = !column.hide
          this.setLists()
        }
      }
    },
    addColumn(column: IColumnDefs) {
      column.hide = false
      this.setLists()
    },
    removeColumn(column: IColumnDefs) {
      if (!this.disabledCurrent) {
        column.hide = true
        this.setLists()
      } else {
        this.$message.error(
          this.$t('notifications.error.deleteLastColumn') as string,
        )
      }
    },
    setActiveColumn(column: IColumnDefs) {
      this.activeColumn = column
    },
    save(): void {
      this.$emit(
        'save',
        this.currentList.map(item => item.field),
      )
    },
    cancel() {
      this.$emit('close')
    },
  },
})
</script>

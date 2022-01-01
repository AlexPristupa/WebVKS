<template>
  <div class="mp-draggable">
    <div class="mp-draggable__column">
      <p class="mp-draggable__column__title">
        {{ settings.titles[0] }}
      </p>
      <mp-draggable-list
        :with-search="settings.withSearch"
        :list="availableList"
        :tooltip="$t('notifications.addToCurrent')"
        @set-active="setActive"
        @update="setAvailableList"
      />
    </div>
    <mp-draggable-buttons
      v-if="settings.buttons.position === 'middle'"
      :settings="settings.buttons"
      @click="changePositionInList"
    />
    <div class="mp-draggable__column">
      <p class="mp-draggable__column__title">
        {{ settings.titles[1] }}
      </p>
      <mp-draggable-list
        :with-search="settings.withSearch"
        :list="currentList"
        :disabled="disabledCurrent"
        :tooltip="$t('notifications.addToAvailable')"
        @set-active="setActive"
        @update="setCurrentList"
      />
    </div>
    <mp-draggable-buttons
      v-if="settings.buttons.position === 'right'"
      :settings="settings.buttons"
      :tooltip="$t('notifications.addToAvailable')"
      @click="changePositionInList"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpDraggable,
  IDataMpDraggable,
  IMethodsMpDraggable,
  IPropsMpDraggable,
} from '@/components/MpDraggable/config/mpDraggable.interface'
import {
  MovingButtonButtonsHorizontalArgs,
  MovingButtonButtonsVerticalArgs,
} from '@/components/MpDraggable/config/mpDraggable.enum'
import { buttonPositions } from '@/components/MpDraggable/config/mpDraggableButtons.interface'
import MpDraggableButtons from '@/components/MpDraggable/MpDraggableButtons.vue'
import MpDraggableList from '@/components/MpDraggable/MpDraggableList.vue'

export default Vue.extend<
  IDataMpDraggable,
  IMethodsMpDraggable,
  IComputedMpDraggable,
  IPropsMpDraggable
>({
  name: 'MpDraggable',
  components: {
    MpDraggableButtons,
    MpDraggableList,
  },
  props: {
    settings: {
      type: Object,
      default: () => ({
        withSearch: false,
        disabled: false,
        titles: ['', ''],
        listForDraggable: [],
        buttons: {
          position: buttonPositions.middle,
          isHorizontal: false,
        },
      }),
    },
  },
  data() {
    return {
      active: null,
      selectedIn: false,
      availableList: [],
      currentList: [],
    }
  },
  computed: {
    disabledCurrent() {
      return this.settings.disabled ? this.currentList.length < 2 : false
    },
  },
  mounted() {
    this.setLists()
  },
  watch: {
    'settings.listForDraggable'() {
      this.setLists()
    },
  },
  methods: {
    setLists() {
      this.availableList = this.settings.listForDraggable.filter(
        column => column.hide,
      )
      this.currentList = this.settings.listForDraggable.filter(
        column => !column.hide,
      )
    },
    changePositionInList(position) {
      if (this.active) {
        const index = this.currentList.indexOf(this.active)
        if (this.settings.buttons.isHorizontal) {
          this.moveHorizontal(position)
        } else {
          this.moveVertical(index, position)
        }
      }
    },
    moveHorizontal(position) {
      /**
       * TODO доделать логику при наличии рабочих данных с бэка
       */
      const fromOneLeftSide =
        this.selectedIn && position === MovingButtonButtonsHorizontalArgs.left
      const fromOneRightSide =
        !this.selectedIn && position === MovingButtonButtonsHorizontalArgs.right
      if (!fromOneLeftSide && !fromOneRightSide) {
        switch (position) {
          case MovingButtonButtonsHorizontalArgs.left:
            if (!this.availableList.find(item => item.id === this.active.id)) {
              const index = this.availableList.indexOf(this.active)
              this.currentList.splice(index, 1)
              this.availableList.push(this.active)
            }
            break
          case MovingButtonButtonsHorizontalArgs.right:
            if (!this.currentList.find(item => item.id === this.active.id)) {
              const index = this.availableList.indexOf(this.active)
              this.availableList.splice(index, 1)
              this.currentList.push(this.active)
            }
            break
        }
      }
    },
    moveVertical(index, position) {
      this.currentList.splice(index, 1)
      switch (position) {
        case MovingButtonButtonsVerticalArgs.first:
          this.currentList.unshift(this.active)
          break
        case MovingButtonButtonsVerticalArgs.up:
          this.currentList.splice(index - 1, 0, this.active)
          break
        case MovingButtonButtonsVerticalArgs.down:
          this.currentList.splice(index + 1, 0, this.active)
          break
        case MovingButtonButtonsVerticalArgs.last:
          this.currentList.push(this.active)
          break
      }
    },
    setActive(value) {
      this.active = value.active
      this.selectedIn = value.from
    },
    setCurrentList(list) {
      this.currentList = list
      this.update()
    },
    setAvailableList(list) {
      this.availableList = list
      this.update()
    },
    update() {
      this.$emit('update', this.currentList.concat(this.availableList))
    },
  },
})
</script>

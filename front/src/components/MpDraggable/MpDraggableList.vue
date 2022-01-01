<template>
  <div class="mp-draggable__column__list">
    <el-input
      clearable
      v-model="search"
      v-if="withSearch"
      :disabled="!list.length"
      class="mp-draggable__column__search"
      @input="activeSearch"
    />
    <draggable
      group="article"
      class="dragArea"
      :list="copiedList"
      :disabled="disabled"
      @change="moved"
    >
      <div
        v-for="(item, index) in copiedList"
        class="item"
        :key="index"
        :class="{ active: selected === item }"
        @click="setActive(item)"
      >
        <span class="text"> {{ item.name }} </span>
        <el-tooltip
          class="item"
          effect="dark"
          placement="top-start"
          :content="tooltip"
        >
          <div
            :class="{
              'icon--eye': item.hide,
              'icon--eye slashed': !item.hide,
            }"
            @click="item.hide ? add(item) : remove(item)"
          />
        </el-tooltip>
      </div>
    </draggable>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import draggable from 'vuedraggable'
import {
  IComputedMpDraggableList,
  IDataMpDraggableList,
  IMethodsMpDraggableList,
  IPropsMpDraggableList,
} from '@/components/MpDraggable/config/mpDraggableList.interface'
import { debounce } from '@/utils'
import CONSTANTS from '@/constants'

export default Vue.extend<
  IDataMpDraggableList,
  IMethodsMpDraggableList,
  IComputedMpDraggableList,
  IPropsMpDraggableList
>({
  name: 'MpDraggableList',
  components: {
    draggable,
  },
  props: {
    withSearch: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    list: {
      type: Array,
      default: () => [],
    },
    selected: {
      type: Object,
      default: () => ({}),
    },
    tooltip: {
      type: String,
      default: '',
    },
  },
  data() {
    return {
      search: '',
      copiedList: [],
    }
  },
  watch: {
    list() {
      if (!this.search) {
        this.copiedList = this.list.map(item => {
          return JSON.parse(JSON.stringify(item))
        })
      } else {
        this.activeSearch()
      }
    },
  },
  methods: {
    moved(event) {
      if (event.added) {
        const item = this.copiedList.find(
          item => item.id === event.added.element.id,
        )
        if (item) {
          item.hide = !item.hide
        }
      }
      this.update()
    },
    add(item) {
      item.hide = false
      this.update()
    },
    remove(item) {
      if (!this.disabled) {
        item.hide = true
      } else {
        this.$message.error(
          this.$t('notifications.error.deleteLastItem') as string,
        )
      }
      this.update()
    },
    setActive(item) {
      const from = item.hide
      this.$emit('set-active', { active: item, from: from })
    },
    update() {
      this.$emit('update', this.copiedList)
    },
    activeSearch: debounce(
      function(this) {
        this.copiedList = this.list.filter(item => {
          return item.name.includes(this.search)
        })
      },
      CONSTANTS.debounce.timeOut.slow,
      false,
    ),
  },
})
</script>

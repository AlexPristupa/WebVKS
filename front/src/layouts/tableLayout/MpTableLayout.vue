<template>
  <section class="mp-table-layout">
    <div v-if="title" class="mp-table-layout__title">
      {{ title }}
    </div>
    <div class="mp-table-layout__select">
      <slot name="select" />
    </div>
    <div class="mp-table-layout__top-panel">
      <div class="mp-table-layout__top-panel-left">
        <slot name="top-panel-left" />
        <slot name="search" />
        <div
          class="mp-table-layout__pagination mp-table-layout__pagination--top"
        >
          <slot name="pagination-top" />
        </div>
      </div>
      <div class="mp-table-layout__top-panel-right">
        <div class="mp-table-layout__buttons">
          <slot name="buttons"></slot>
        </div>
      </div>
    </div>
    <div class="mp-table-layout__table">
      <slot name="table" :tableHeight="tableHeight"></slot>
    </div>
    <div class="mp-table-layout__bottom-panel">
      <div
        v-if="!!$slots['pagination-bottom']"
        class="mp-table-layout__pagination mp-table-layout__pagination--bottom"
      >
        <slot name="pagination-bottom" />
      </div>
    </div>
    <slot />
  </section>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'MpTableLayout',
  data() {
    return {
      tableHeight: '300px',
    }
  },
  props: {
    title: {
      type: String,
      default: '',
    },
    isNeedToRecalculate: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    isNeedToRecalculate(v) {
      if (v) {
        this.calculate()
      }
    },
  },
  mounted() {
    this.calculate()
    this.setListenerResize()
    this.$emit('smallestTableHeight', this.getSmallestTableHeight())
  },
  methods: {
    setListenerResize() {
      window.addEventListener('resize', this.calculate)
    },
    getSmallestTableHeight() {
      return (
        this.heightTitle() +
        this.heightSelect() +
        this.heightTopPanel() +
        this.heightBottomPanel() +
        this.heightTableHeader() +
        //  отступы
        32 +
        //  28 - высота одной строки
        28 * 3
      )
    },
    calculate() {
      const height =
        this.heightParent() -
        this.heightTitle() -
        this.heightSelect() -
        this.heightTopPanel() -
        this.heightBottomPanel() -
        32
      this.tableHeight = `${height}px`
    },
    heightParent() {
      return this.$el.parentNode ? this.$el.parentNode.offsetHeight : 0
    },
    heightTitle() {
      const title = this.$el.getElementsByClassName('mp-table-layout__title')[0]
      return title ? title.offsetHeight : 0
    },
    heightSelect() {
      return this.$el.getElementsByClassName('mp-table-layout__select')[0]
        .offsetHeight
    },
    heightTopPanel() {
      return this.$el.getElementsByClassName('mp-table-layout__top-panel')[0]
        .offsetHeight
    },
    heightTableHeader() {
      const header = this.$el.getElementsByClassName('ag-header')[0]
      return header ? header.offsetHeight : 0
    },
    heightBottomPanel() {
      const height = this.$el.getElementsByClassName(
        'mp-table-layout__bottom-panel',
      )[0].offsetHeight

      return height ? height + 15 : height
    },
  },
})
</script>

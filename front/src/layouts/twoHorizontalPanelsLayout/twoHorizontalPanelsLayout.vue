<template>
  <section
    ref="two-horizontal-panels-layout"
    class="two-horizontal-panels-layout"
  >
    <div
      ref="two-horizontal-panels-layout__top"
      class="panels"
      :class="{
        'two-horizontal-panels-layout__top': !report,
        'two-horizontal-panels-layout__top-report': report,
        'two-horizontal-panels-layout__top--closed': !isOpen.top,
      }"
      :style="
        initialLayoutDataStyle(TwoHorizontalPanelsLayoutSides.top) ||
          initialStyleTop
      "
    >
      <div
        v-if="isCollapsibleTop"
        class="two-horizontal-panels-layout__expand-collapse"
        :class="{
          'two-horizontal-panels-layout__expand-collapse--closed': !isOpen.top,
        }"
        :title="$t('general.collapse') + '/' + $t('general.expand')"
        @click="expandCollapseHandler(TwoHorizontalPanelsLayoutSides.top)"
      ></div>
      <slot name="topPanel" />
    </div>

    <MpResizeLine ref="resize-line" type="horizontal" :size="resizeLineSize" />

    <div
      ref="two-horizontal-panels-layout__bottom"
      class="panels"
      :class="{
        'two-horizontal-panels-layout__bottom': !report,
        'two-horizontal-panels-layout__bottom-report': report,
        'two-horizontal-panels-layout__bottom--closed': !isOpen.bottom,
      }"
      :style="
        initialLayoutDataStyle(TwoHorizontalPanelsLayoutSides.bottom) ||
          initialStyleBottom
      "
    >
      <div
        v-if="isCollapsibleBottom"
        class="two-horizontal-panels-layout__expand-collapse"
        :title="$t('general.collapse') + '/' + $t('general.expand')"
        @click="expandCollapseHandler(TwoHorizontalPanelsLayoutSides.bottom)"
      >
        <div
          class="icon"
          :class="{
            'icon--closed': !isOpen.bottom,
          }"
        />
      </div>
      <slot name="bottomPanel" />
    </div>
  </section>
</template>

<script lang="ts">
/**
 * @description компонент лэйаута для размещения двух горизонтальных панелей
 * с разделителем. в лэйаут вшит функционал ресайза разделителем и сворачивания
 * в слоты вставлять компоненты с MpTableLayout
 */
import Vue from 'vue'
import MpResizeLine from '@/components/basic/MpResizeLine/MpResizeLine.vue'
import { Validation } from '@/modules/Validation/Validation'
import { mapActions, mapGetters } from 'vuex'
import {
  IPropsTwoHorizontalPanelsLayout,
  IDataTwoHorizontalPanelsLayout,
  IComputedTwoHorizontalPanelsLayout,
  IMethodsTwoHorizontalPanelsLayout,
} from '@/layouts/twoHorizontalPanelsLayout/twoHorizontalPanelsLayout.interface.ts'
import { TwoHorizontalPanelsLayoutSides } from '@/layouts/twoHorizontalPanelsLayout/twoHorizontalPanelsLayout.const'

export default Vue.extend<
  IDataTwoHorizontalPanelsLayout,
  IMethodsTwoHorizontalPanelsLayout,
  IComputedTwoHorizontalPanelsLayout,
  IPropsTwoHorizontalPanelsLayout
>({
  name: 'TwoHorizontalPanelsLayout',
  components: {
    MpResizeLine,
  },
  props: {
    layoutName: {
      type: String,
      required: true,
    },
    report: {
      type: Boolean,
      default: false,
    },
    initialHeightTop: {
      type: String,
      default: '60%',
      validator: value => Validation.percentage(value),
    },
    resizeLineSize: {
      type: Number,
      default: 2,
    },
    initialHeightBottom: {
      type: String,
      default: '40%',
      validator: value => Validation.percentage(value),
    },
    minHeight: {
      type: Number,
      default: 100,
    },
    smallestSizes: {
      type: Object,
      default: () => ({
        top: 0,
        bottom: 0,
      }),
    },
    isCollapsibleTop: {
      type: Boolean,
      default: false,
    },
    isCollapsibleBottom: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      parent: null,
      top: null,
      line: null,
      bottom: null,
      currHeight: 0,
      unlock: false,
      TwoHorizontalPanelsLayoutSides,
    }
  },
  computed: {
    ...mapGetters({
      layoutItemData: 'userSettings/layoutItemData',
    }),

    layoutData: {
      get() {
        return this.layoutItemData(this.layoutName)
      },
      set(data) {
        this.setLayoutItem({
          name: this.layoutName,
          data,
        })
      },
    },
    sizes() {
      return this.layoutData?.sizes || null
    },
    isOpen() {
      return this.layoutData?.isOpen || null
    },
    initialStyleTop() {
      return this.getHeight(this.initialHeightTop)
    },
    initialStyleBottom() {
      return this.getHeight(this.initialHeightBottom)
    },
  },
  created() {
    if (!this.isOpen) {
      this.updateLayoutDataIsOpen({
        top: true,
        bottom: true,
      })
    }
  },
  mounted() {
    this.parent = this.$refs['two-horizontal-panels-layout']
    this.top = this.$refs['two-horizontal-panels-layout__top']
    this.line = (this.$refs['resize-line'] as Vue).$el
    this.bottom = this.$refs['two-horizontal-panels-layout__bottom']
    this.currHeight = this.getOffsetHeight(this.top)
    this.unlock = false
    this.addListeners()
    if (this.isOpen && (!this.isOpen.top || !this.isOpen.bottom)) {
      const maxHeight =
        this.getOffsetHeight(this.parent) -
        this.minHeight -
        this.getOffsetHeight(this.line)
      this.setNewHeights({
        top: this.isOpen.top ? maxHeight : this.minHeight,
        bottom: this.isOpen.bottom ? maxHeight : this.minHeight,
      })
    }
    this.updateLayoutDataSizesAndInitiateRecalculation()
  },
  beforeDestroy() {
    this.removeListeners()
  },
  methods: {
    ...mapActions({
      setLayoutItem: 'userSettings/setLayoutItem',
    }),

    getOffsetHeight(element) {
      return element?.offsetHeight || 0
    },
    getHeight(percentString) {
      return {
        height: this.parent
          ? this.getOffsetHeight(this.parent) *
              (parseFloat(percentString) / 100) -
            this.resizeLineSize / 2 +
            'px'
          : percentString,
      }
    },
    getBoundingClientRectFromTop(element) {
      return element?.getBoundingClientRect().top
    },
    initialLayoutDataStyle(side) {
      return this.sizes && this.sizes[side]
        ? `height: ${this.sizes[side]}px;`
        : ''
    },
    updateLayoutDataSizes(sizes) {
      this.layoutData = {
        ...this.layoutData,
        sizes,
      }
    },
    updateLayoutDataIsOpen(isOpen) {
      this.layoutData = {
        ...this.layoutData,
        isOpen,
      }
    },
    initiateRecalculation() {
      this.$emit('recalculate')
    },
    updateLayoutDataSizesAndInitiateRecalculation() {
      this.updateLayoutDataSizes({
        top: this.getOffsetHeight(this.top),
        bottom: this.getOffsetHeight(this.bottom),
      })
      this.initiateRecalculation()
    },
    setNewHeightToElement(element, value, isAddPixels = true) {
      const pixelStr = isAddPixels ? 'px' : ''
      element?.setAttribute('style', `height: ${value + pixelStr};`)
    },
    setNewHeights({ top = 0, bottom = 0, isAddPixels = true }) {
      this.setNewHeightToElement(this.top, top, isAddPixels)
      this.setNewHeightToElement(this.bottom, bottom, isAddPixels)
    },
    mousedownLineHandler(e) {
      this.currHeight = this.getOffsetHeight(this.top)
      this.unlock = true
    },
    mousedownWindowHandler(e) {
      if (this.unlock) {
        e.preventDefault()
      }
    },
    mousemoveHandler(e) {
      if (this.unlock) {
        const lineHeight = this.getOffsetHeight(this.line)
        const newHeight =
          this.currHeight +
          (e.clientY -
            this.currHeight -
            this.getBoundingClientRectFromTop(this.top) -
            lineHeight / 2)
        const oldParentHeight = this.getOffsetHeight(this.parent)
        const maxHeight = oldParentHeight - this.minHeight - lineHeight

        this.updateLayoutDataIsOpen({
          top: true,
          bottom: true,
        })

        if (newHeight < this.minHeight) {
          this.setNewHeights({ top: this.minHeight, bottom: maxHeight })
        } else if (this.getOffsetHeight(this.bottom) < this.minHeight) {
          this.setNewHeights({ top: maxHeight, bottom: this.minHeight })
        } else {
          if (newHeight < maxHeight) {
            this.setNewHeights({
              top: newHeight,
              bottom: oldParentHeight - newHeight - lineHeight,
            })
          }
        }

        this.updateLayoutDataSizesAndInitiateRecalculation()
      }
    },
    mouseupHandler(e) {
      this.checkMaxMinValues(this.sizes.top, this.sizes.bottom)
      this.unlock = false
    },
    checkMaxMinValues(top, bottom) {
      if (bottom < this.smallestSizes.bottom || top < this.smallestSizes.top) {
        if (bottom < this.smallestSizes.bottom) {
          this.setNewHeights({
            top: this.getOffsetHeight(this.parent) - this.smallestSizes.bottom,
            bottom: this.smallestSizes.bottom,
          })
        }
        if (top < this.smallestSizes.top) {
          this.setNewHeights({
            top: this.smallestSizes.top,
            bottom: this.getOffsetHeight(this.parent) - this.smallestSizes.top,
          })
        }
        this.updateLayoutDataSizesAndInitiateRecalculation()
      }
    },
    dblclickHandler(e) {
      this.updateLayoutDataIsOpen({
        top: true,
        bottom: true,
      })

      this.setNewHeights({
        top: this.getHeight(this.initialHeightTop)?.height,
        bottom: this.getHeight(this.initialHeightBottom)?.height,
        isAddPixels: false,
      })

      this.updateLayoutDataSizesAndInitiateRecalculation()
    },
    getInitialHeightNumber(elementStr) {
      return (
        (parseFloat(
          this[
            'initialHeight' +
              elementStr.charAt(0).toUpperCase() +
              elementStr.slice(1)
          ],
        ) /
          100) *
          this.getOffsetHeight(this.parent) -
        this.resizeLineSize / 2
      )
    },
    expandCollapseHandler(elementStr) {
      const secondaryElementStr =
        elementStr === TwoHorizontalPanelsLayoutSides.top
          ? TwoHorizontalPanelsLayoutSides.bottom
          : TwoHorizontalPanelsLayoutSides.top

      this.updateLayoutDataIsOpen({
        [elementStr]: !this.isOpen[elementStr],
        [secondaryElementStr]: true,
      })

      if (this.isOpen[elementStr]) {
        const wasOpenedEarlier = this.sizes[elementStr] !== this.minHeight

        this.setNewHeights({
          [elementStr]: wasOpenedEarlier
            ? this.sizes[elementStr]
            : this.getInitialHeightNumber(elementStr),
          [secondaryElementStr]: wasOpenedEarlier
            ? this.sizes[secondaryElementStr]
            : this.getInitialHeightNumber(secondaryElementStr),
        })
      } else {
        const maxHeight =
          this.getOffsetHeight(this.parent) -
          this.minHeight -
          this.getOffsetHeight(this.line)
        this.setNewHeights({
          [elementStr]: this.minHeight,
          [secondaryElementStr]: maxHeight,
        })
      }

      this.updateLayoutDataSizesAndInitiateRecalculation()
    },
    addListener(element, eventString, func) {
      element?.addEventListener(eventString, func)
    },
    removeListener(element, eventString, func) {
      element?.removeEventListener(eventString, func)
    },
    addListeners() {
      this.addListener(this.line, 'mousedown', this.mousedownLineHandler)
      // специально повешаны на window, а не на this.$el, чтобы избавиться от
      // эффекта залипания вне компонента при завершении ресайза за пределами компонента
      this.addListener(window, 'mousedown', this.mousedownWindowHandler)
      this.addListener(window, 'mousemove', this.mousemoveHandler)
      this.addListener(this.line, 'mouseup', this.mouseupHandler)
      this.addListener(this.line, 'dblclick', this.dblclickHandler)
    },
    removeListeners() {
      this.removeListener(this.line, 'mousedown', this.mousedownLineHandler)
      this.removeListener(window, 'mousedown', this.mousedownWindowHandler)
      this.removeListener(window, 'mousemove', this.mousemoveHandler)
      this.removeListener(this.line, 'mouseup', this.mouseupHandler)
      this.removeListener(this.line, 'dblclick', this.dblclickHandler)
    },
  },
})
</script>

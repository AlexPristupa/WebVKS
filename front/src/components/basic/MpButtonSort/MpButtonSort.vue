<template>
  <div class="mp-button mp-button__sort mp-button--cell" @click="toggle">
    <div
      class="sort__asc"
      :class="{ active: orderBy === sortType.asc && activeSortedColumn }"
    />
    <div
      class="sort__desc"
      :class="{ active: orderBy === sortType.desc && activeSortedColumn }"
    />
  </div>
</template>

<script>
import Vue from 'vue'
import { mapGetters } from 'vuex'
import { sortType } from '@/modules/Sort/Sort.const'

export default Vue.extend({
  name: 'MpButtonSort',
  data() {
    return {
      sortType: sortType,
    }
  },
  props: {
    orderBy: {
      type: String,
      default: '',
      validator: value => {
        return ['', 'asc', 'desc'].includes(value)
      },
    },
    params: {
      type: Object,
      default: () => {
        return {}
      },
    },
  },
  computed: {
    ...mapGetters({
      getSortParams: 'tableHeaderEffects/getTableHeadersEffects',
    }),
    activeSortedColumn() {
      return (
        this.params.column.colDef.field ===
        this.getSortParams(this.params.api.tableName)?.sorted
      )
    },
  },
  methods: {
    toggle() {
      this.$emit('toggle-order-by')
    },
  },
})
</script>

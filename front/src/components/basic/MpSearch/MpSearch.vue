<template>
  <el-input
    v-model="search"
    class="mp-search"
    :placeholder="$t('placeholder')"
    size="mini"
    clearable
    @input="changeSearchString"
  >
  </el-input>
</template>

<script lang="ts">
import Vue from 'vue'
import { MpSearchLang } from './MpSearch.lang'
import { debounce } from '@/utils'
import { IDataMpSearch, IEventSearch } from './MpSearch.interface'
import CONSTANTS from '@/constants'

const MpSearch = Vue.extend<
  IDataMpSearch,
  {
    changeSearchString: typeof debounce
    setExtensionSearchString: () => void
  },
  {},
  { extensionSearchString: string }
>({
  name: 'MpSearch',
  props: {
    extensionSearchString: {
      type: String as () => string,
      default: '',
    },
  },
  data(): IDataMpSearch {
    return {
      search: '',
    }
  },
  mounted() {
    if (this.extensionSearchString) {
      this.search = this.extensionSearchString
    }
  },
  methods: {
    changeSearchString: debounce(
      function(this) {
        const eventSearchPayload: IEventSearch = { searchString: this.search }
        this.$emit('search', eventSearchPayload)
      },
      CONSTANTS.debounce.timeOut.slow,
      false,
    ),
    setExtensionSearchString() {
      this.search = this.extensionSearchString
    },
  },
  i18n: {
    messages: MpSearchLang,
  },
})

export default MpSearch
</script>

<template>
  <one-panel-layout>
    <template #panel>
      <div>{{ $t('routing.notFound') }}</div>
    </template>
  </one-panel-layout>
</template>

<script>
import Vue from 'vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout'

export default Vue.extend({
  name: 'PageNotFound',
  components: { OnePanelLayout },
})
</script>

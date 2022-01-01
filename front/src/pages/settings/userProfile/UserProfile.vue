<template>
  <one-panel-layout>
    <template #panel>
      <user-profile-list-panel />
    </template>
  </one-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout.vue'
import UserProfileListPanel from '@/pages/settings/userProfile/panels/UserProfileListPanel.vue'

export default Vue.extend({
  name: 'UserProfile',
  components: {
    OnePanelLayout,
    UserProfileListPanel,
  },
})
</script>

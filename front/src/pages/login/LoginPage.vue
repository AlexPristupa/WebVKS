<template>
  <login-layout>
    <div class="login-page">
      <mp-login-form />
    </div>
  </login-layout>
</template>

<script>
import Vue from 'vue'
import MpLoginForm from '@/components/login/MpLoginForm'

import LoginLayout from '@/layouts/loginLayout/LoginLayout'

export default Vue.extend({
  name: 'LoginPage',
  components: {
    LoginLayout,
    MpLoginForm,
  },
})
</script>

<template>
  <el-form
    ref="loginForm"
    label-width="4rem"
    class="login-form"
    label-position="left"
    :model="formModel"
    :rules="rules"
  >
    <el-select
      v-model="formModel.provider"
      class="login-form__select"
      :popper-append-to-body="false"
    >
      <el-option
        :label="$t('authenticationType.local')"
        value="Integrated"
      ></el-option>
      <el-option
        :label="$t('authenticationType.domain')"
        value="Ldap"
      ></el-option>
    </el-select>
    <el-form-item prop="login" :label="$t('forms.login.login')">
      <el-input
        autofocus
        v-model="formModel.login"
        :placeholder="$t('forms.login.login')"
      />
    </el-form-item>
    <el-form-item prop="password" :label="$t('forms.login.password')">
      <el-input
        v-model="formModel.password"
        :placeholder="$t('forms.login.password')"
        show-password
        @keyup.enter.native="onSubmit"
      />
    </el-form-item>
    <mp-button
      type="primary"
      @click="onSubmit"
      mp-status="normal"
      :mp-type="mpTypeLogin"
      :disabled="loading || !formModel.login || !formModel.password"
      :loading="loading"
    >
      {{ $t('label.buttonAction') }}
    </mp-button>
  </el-form>
</template>

<script lang="ts">
import Vue from 'vue'
import { loginLang } from './login.lang'
import { User } from '@/modules/User/User'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { BASE_SELECTION_SEQUENCE_ROUTE } from '@/router/BASE_SELECTION_SEQUENCE_ROUTE.const'
import { VForm } from '@/modules/Form/Form.interface'
import { TriggerType } from '@/modules/FormValidation/FormValidation.const'
import { FormValidation } from '@/modules/FormValidation/FormValidation'
import {
  IComputedLoginPage,
  IDataLoginPage,
  IMethodsLoginPage,
  IPropsLoginPage,
} from '@/pages/login/config/LoginPage.interface'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { methods } from '@/api_services/httpMethods.enum'
import api from '@/api_services'

export default Vue.extend<
  IDataLoginPage,
  IMethodsLoginPage,
  IComputedLoginPage,
  IPropsLoginPage
>({
  name: 'MpLoginForm',
  components: { MpButton },
  data() {
    return {
      validationFromBackData: [],
      mpTypeLogin: MpTypeButton.login,
      formModel: {
        provider: 'Integrated',
        login: '',
        password: '',
      },
      loading: false,
    }
  },
  computed: {
    rules() {
      return {
        login: [
          {
            required: true,
            message: this.$t('forms.validationError.pleaseEnterEntity', [
              (this.$t('forms.login.login') as string).toLowerCase(),
            ]),
            trigger: TriggerType.blur,
          },
          {
            validator: this.validation,
            trigger: TriggerType.blur,
          },
        ],
        password: [
          {
            required: true,
            message: this.$t('forms.validationError.pleaseEnterEntity', [
              (this.$t('forms.login.password') as string).toLowerCase(),
            ]),
            trigger: TriggerType.blur,
          },
          {
            validator: this.validation,
            trigger: TriggerType.blur,
          },
        ],
      }
    },
  },
  mounted() {
    User.logout()
  },
  methods: {
    async onSubmit() {
      const form: VForm = this.$refs['loginForm'] as VForm
      form.validate(async valid => {
        if (valid) {
          this.loading = true
          const data = await api.login({
            method: methods.post,
            data: this.formModel,
          })
          if (data && data.accessToken) {
            await User.login(data)
            await this.$router.push(
              getRedirectRouteByPermission(BASE_SELECTION_SEQUENCE_ROUTE),
            )
          } else {
            this.validationFromBackData = data.validation
            form.validate(() => {
              this.validationFromBackData = []
            })
          }
        }
      })
      this.loading = false
    },
    validation(rule, value, callback) {
      FormValidation.backValidationField(
        this.validationFromBackData,
        rule,
        value,
        callback,
      )
      const validationWithoutField = FormValidation.backValidationWithoutField(
        this.validationFromBackData,
      )
      if (validationWithoutField) {
        this.$message.error(validationWithoutField)
      }
    },
  },
  i18n: {
    messages: loginLang,
  },
})
</script>

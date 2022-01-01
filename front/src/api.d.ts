import Vue from 'vue'
import { apiService } from '@/api_services/IApiService.interface'

declare module 'vue/types/vue' {
  interface Vue {
    $api: apiService
  }
}

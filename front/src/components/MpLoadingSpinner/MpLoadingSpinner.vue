<template>
  <div class="mp-loading-spinner">
    <svg viewBox="25 25 50 50" class="mp-loading-spinner__circular">
      <circle
        cx="50"
        cy="50"
        r="20"
        fill="none"
        class="mp-loading-spinner__path"
      ></circle>
    </svg>
  </div>
</template>

<script>
import Vue from 'vue'

/**
 * @description Компонент кастомного корпоративного спинера загрузки данных.
 *              Применяется в компонентах выпадающих списков (select)
 */
export default Vue.extend({
  name: 'MpLoadingSpinner',
})
</script>

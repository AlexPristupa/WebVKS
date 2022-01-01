import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import i18n from '@/i18n'
import { bookingRoute } from '@/router/routes/bookingRoute'
import { cmsRoute } from '@/router/routes/cmsRoute'
import { conferencesRoute } from '@/router/routes/conferencesRoute'
import { reportsRoute } from '@/router/routes/reportsRoute'
import { settingsRoute } from '@/router/routes/settingsRoute'
import { Auth } from '@/modules/Auth/Auth'
import { RouteName } from '@/router/RouteName.enum'
import { RouteSection } from '@/router/RouteSection.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { BASE_SELECTION_SEQUENCE_ROUTE } from '@/router/BASE_SELECTION_SEQUENCE_ROUTE.const'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { checkPermissionToAccess } from '@/router/checkPermissionToAccess/checkPermissionToAccess.ts'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: RouteName.main,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(BASE_SELECTION_SEQUENCE_ROUTE)
    },
    meta: { title: i18n.t('routing.booking.bookings') },
  },
  {
    path: '/login',
    name: RouteName.login,
    component: () => import('@/pages/login/LoginPage.vue'),
    meta: {
      section: RouteSection.login,
    },
  },
  ...bookingRoute,
  ...cmsRoute,
  ...conferencesRoute,
  ...reportsRoute,
  ...settingsRoute,
  {
    path: '/noAccess',
    name: RouteName.noAccess,
    component: () => {
      return import('@/pages/noAccess/PageNoAccess.vue')
    },
    meta: {
      requiresAuth: true,
      section: RouteSection.app,
      page: RoutePage.noAccess,
      title: i18n.t('routing.noAccess.accessError'),
    },
  },
  {
    path: '*',
    name: RouteName.notFound,
    component: () => {
      return import('@/pages/notFound/PageNotFound.vue')
    },
    meta: {
      requiresAuth: true,
      section: RouteSection.app,
      page: RoutePage.notFound,
      title: i18n.t('routing.notFound'),
    },
  },
]

const router = new VueRouter({
  mode: 'history',
  base: '/',
  routes,
})

router.beforeEach((to, from, next) => {
  if (to.name !== RouteName.login && !Auth.getAccessToken()) {
    next({ name: RouteName.login })
  }
  if (checkPermissionToAccess(to, from)) {
    next({ name: RouteName.noAccess })
  }
  next()
})

export default router

import i18n from '@/i18n'
import { RouteConfig } from 'vue-router'
import { RouteName } from '@/router/RouteName.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { RouteSection } from '@/router/RouteSection.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { IRoutePermission } from '@/router/IRoutePermission.interface'

const SELECTION_SEQUENCE_ROUTE: Array<IRoutePermission> = [
  {
    route: RouteName.bookingList,
    permission: PermissionValidValue.MMS_BOOKING_BOOKING,
  },
  {
    route: RouteName.bookingListRestricted,
    permission: PermissionValidValue.MMS_BOOKING_BOOKING_RESTRICTED,
  },
  {
    route: RouteName.spacesList,
    permission: PermissionValidValue.MMS_BOOKING_SPACES,
  },
  {
    route: RouteName.recordingsList,
    permission: PermissionValidValue.MMS_BOOKING_RECORDS,
  },
]

export const bookingRoute: Array<RouteConfig> = [
  {
    path: '/bookings',
    name: RouteName.bookings,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(SELECTION_SEQUENCE_ROUTE)
    },
    meta: {
      permission: PermissionValidValue.MMS_BOOKING,
      section: RouteSection.app,
      page: RoutePage.bookings,
      title: i18n.t('routing.booking.bookings'),
    },
  },
  {
    path: '/bookings/bookings',
    name: RouteName.bookingList,
    component: () => {
      return import('@/pages/booking/Bookings/Bookings.vue')
    },
    meta: {
      requiresAuth: true,
      permission: PermissionValidValue.MMS_BOOKING_BOOKING,
      section: RouteSection.app,
      page: RoutePage.bookings,
      title: i18n.t('routing.booking.bookings'),
    },
  },
  {
    path: '/bookings/bookings-restricted',
    name: RouteName.bookingListRestricted,
    component: () => {
      return import('@/pages/booking/BookingsRestricted/BookingsRestricted.vue')
    },
    meta: {
      requiresAuth: true,
      permission: PermissionValidValue.MMS_BOOKING_BOOKING_RESTRICTED,
      section: RouteSection.app,
      page: RoutePage.bookings,
      title: i18n.t('routing.booking.bookings'),
    },
  },
  {
    path: '/bookings/spaces',
    name: RouteName.spacesList,
    component: () => {
      return import('@/pages/booking/Spaces/Spaces.vue')
    },
    meta: {
      requiresAuth: true,
      permission: PermissionValidValue.MMS_BOOKING_SPACES,
      section: RouteSection.app,
      page: RoutePage.bookings,
      title: i18n.t('routing.booking.spaces'),
    },
  },
  {
    path: '/bookings/recordings',
    name: RouteName.recordingsList,
    component: () => {
      return import('@/pages/booking/Recordings/Recordings.vue')
    },
    meta: {
      requiresAuth: true,
      permission: PermissionValidValue.MMS_BOOKING_RECORDS,
      section: RouteSection.app,
      page: RoutePage.bookings,
      title: i18n.t('routing.booking.recordings'),
    },
  },
]

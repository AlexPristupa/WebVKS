import { IMenuItem } from '@/components/asideMenu/interface.menu'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { RouteName } from '@/router/RouteName.enum'

export const ASIDE_MENU_APP: Array<IMenuItem> = [
  {
    id: 'Bookings',
    routeName: RouteName.bookings,
    page: RoutePage.bookings,
    srcIcon: '@/assets/icons/asideMenu/icon_org_select_hover',
    altIcon: 'icon',
    title: 'menuItemTitle.bookings',
    iconCssClass: 'aside-menu__item-icon--booking',
    roles: [PermissionValidValue.MMS_BOOKING],
  },
  // {
  //   id: 'Conferences',
  //   routeName: RouteName.conferences,
  //   page: RoutePage.conferences,
  //   srcIcon: '@/assets/icons/asideMenu/icon_org_select_hover',
  //   altIcon: 'icon',
  //   title: 'menuItemTitle.conferences',
  //   iconCssClass: 'aside-menu__item-icon--conference',
  //   roles: [PermissionValidValue.MMS_CONFERENCES],
  // },
  {
    id: 'settings',
    routeName: RouteName.settings,
    page: RoutePage.settings,
    srcIcon: '@/assets/icons/asideMenu/icon_manual_select',
    altIcon: 'icon',
    title: 'menuItemTitle.settings',
    iconCssClass: 'aside-menu__item-icon--setting',
    roles: [PermissionValidValue.MMS_SETTINGS],
  },
  {
    id: 'cms',
    routeName: RouteName.cms,
    page: RoutePage.cms,
    srcIcon: '@/assets/icons/asideMenu/icon_org_select_hover',
    altIcon: 'icon',
    title: 'menuItemTitle.cms',
    iconCssClass: 'aside-menu__item-icon--cms',
    roles: [PermissionValidValue.MMS_CMS],
  },
  {
    id: 'reports',
    routeName: RouteName.reports,
    page: RoutePage.reports,
    srcIcon: '@/assets/icons/asideMenu/icon_org_select_hover',
    altIcon: 'icon',
    title: 'menuItemTitle.reports',
    iconCssClass: 'aside-menu__item-icon--reports',
    roles: [PermissionValidValue.MMS_REPORTS],
  },
]

export const ASIDE_MENU_SETTING: Array<IMenuItem> = [
  {
    id: 'userProfile',
    routeName: 'userProfile',
    page: 'setting',
    srcIcon: '@/assets/icons/asideMenu/icon_org_select_hover',
    altIcon: 'icon',
    title: 'menuItemTitle.conferences',
    iconCssClass: 'aside-menu__item-icon--setting',
    roles: ['admin'],
  },
]

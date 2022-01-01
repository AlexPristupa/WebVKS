import { TranslateResult } from 'vue-i18n'
import i18n from '@/i18n'
import { FirstLetterToCase } from '@/modules/EditEntityModal/FirstLetterToCase'

const editSpacePlaceholderToLower = function(field: string): string {
  return FirstLetterToCase.placeholderToLower(field, 'editSpace')
}

export const placeholders: { [key: string]: string | TranslateResult } = {
  serversGroupsId: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('serversGroupsId'),
  ]),
  name: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('name'),
  ]),
  uri: i18n.t('forms.placeholders.enterEntity', [
    i18n.t('forms.editSpace.uri'),
  ]),
  ownerId: i18n.t('forms.placeholders.chooseEntity', [
    i18n.t('forms.editSpace.placeholders.ownerId'),
  ]),
  callLegProfileGuid: i18n.t('forms.placeholders.chooseEntity', [
    editSpacePlaceholderToLower('callLegProfileGuid'),
  ]),
  callBrandingProfileGuid: i18n.t('forms.placeholders.chooseEntity', [
    editSpacePlaceholderToLower('callBrandingProfileGuid'),
  ]),
  tagCdr: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('tagCdr'),
  ]),
  callIdGeneration: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('callId'),
  ]),
  password: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('password'),
  ]),
  uriAlt: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('uriAlt'),
  ]),
  passwordGuest: i18n.t('forms.placeholders.enterEntity', [
    editSpacePlaceholderToLower('passwordGuest'),
  ]),
  uriVideo: i18n.t('forms.placeholders.enterEntity', [
    i18n.t('forms.editSpace.uriVideo'),
  ]),
}

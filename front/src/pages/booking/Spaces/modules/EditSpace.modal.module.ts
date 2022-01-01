import i18n from '@/i18n'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { ILinkSpaceToParticipantsRow } from '@/pages/booking/Spaces/config/EditSpace.modal.interface'
import { VksLinkSpaceToParticipantsEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksLinkSpaceToParticipants.entity'

function findRight(row: ILinkSpaceToParticipantsRow, field: string): boolean {
  const right = row.rights.find(right => right.field === field)
  return right ? right.checked : false
}

export function setLinkSpaceToParticipantsModel(
  from: Array<ILinkSpaceToParticipantsRow>,
) {
  return from.map(row => {
    return {
      vksUserId: row.vksUser,
      vksUserName: row.vksUserName,
      found: row.found,
      callLegProfileGuid: row.callLegProfileGuid,
      canDestroy: findRight(row, 'canDestroy'),
      canAddRemoveMember: findRight(row, 'canAddRemoveMember'),
      canChangeName: findRight(row, 'canChangeName'),
      canChangeNonMemberAccessAllowed: findRight(
        row,
        'canChangeNonMemberAccessAllowed',
      ),
      canChangeUri: findRight(row, 'canChangeUri'),
      canChangeCallId: findRight(row, 'canChangeCallId'),
      canChangePassCode: findRight(row, 'canChangePassCode'),
      canRemoveSelf: findRight(row, 'canRemoveSelf'),
    }
  })
}

export function setLinkSpaceToParticipantsRows(
  options: Array<VksSelectOptionEntity>,
  from: Array<VksLinkSpaceToParticipantsEntity>,
  serversGroupsId: number,
): Array<ILinkSpaceToParticipantsRow> {
  return from.map((row, index) => {
    const userName =
      row.vksUserName ||
      options.find(option => option.id === row.vksUserId)?.name
    return {
      id: index,
      vksUser: row.vksUserId,
      vksUserName: userName || '',
      callLegProfileGuid: row.callLegProfileGuid,
      serversGroupsId: serversGroupsId,
      found: row.found,
      rights: [
        {
          label: i18n.t('callTable.MpCellCheckboxes.canDestroy'),
          field: 'canDestroy',
          checked: row.canDestroy || false,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canAddRemoveMember'),
          field: 'canAddRemoveMember',
          checked: row.canAddRemoveMember || false,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canChangeName'),
          field: 'canChangeName',
          checked: row.canChangeName || false,
        },
        {
          label: i18n.t(
            'callTable.MpCellCheckboxes.canChangeNonMemberAccessAllowed',
          ),
          field: 'canChangeNonMemberAccessAllowed',
          checked: row.canChangeNonMemberAccessAllowed || false,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canChangeUri'),
          field: 'canChangeUri',
          checked: row.canChangeUri || false,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canChangeCallId'),
          field: 'canChangeCallId',
          checked: row.canChangeCallId,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canChangePassCode'),
          field: 'canChangePassCode',
          checked: row.canChangePassCode || false,
        },
        {
          label: i18n.t('callTable.MpCellCheckboxes.canRemoveSelf'),
          field: 'canRemoveSelf',
          checked: row.canRemoveSelf || false,
        },
      ],
      action: new ActionButtons([MpTypeButton.remove]),
    }
  })
}

export function addParticipantsRow(
  participantsArray: Array<VksLinkSpaceToParticipantsEntity>,
  options: Array<VksSelectOptionEntity>,
): Array<VksLinkSpaceToParticipantsEntity> {
  const array = participantsArray
  const row = new VksLinkSpaceToParticipantsEntity()
  row.vksUserId = options[0].id || null
  row.vksUserName = options[0].name || ''
  array.unshift(row)
  return array
}

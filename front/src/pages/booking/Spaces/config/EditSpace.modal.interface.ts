import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { IFormRules } from '@/modules/FormValidation/FormValidation.interface'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { ActionButtons } from '@/modules/table_grid/cells/ActionButtons'
import { ICheckboxesItem } from '@/components/MpTable/cell/config/MpCellCheckboxes.interface'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { SpacesCaLegProfilesProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/SpacesCaLegProfilesProxy.entity'
import { TranslateResult } from 'vue-i18n'

export interface ILinkSpaceToParticipantsRow {
  id: number
  vksUser: number | null
  vksUserName: string | TranslateResult
  callLegProfileGuid: string
  found: boolean
  serversGroupsId: number
  rights: Array<ICheckboxesItem>
  action: ActionButtons
}

export interface IComputedEditSpaceModal extends IComputedEditModal {
  rules: IFormRules
}

export interface IDataEditSpaceModal extends IDataEditModal {
  ownerOptions: Array<VksSelectOptionEntity>
  callLegProfileGuidOptions: Array<SpacesCaLegProfilesProxyEntity>
  callBrandingProfileGuidOptions: Array<any>
  serversList: Array<VksServerEntity>
  linkSpaceToParticipantsRows: Array<ILinkSpaceToParticipantsRow>
}

export interface IPropsEditSpaceModal extends IPropsEditModal {}

export interface IMethodsEditSpaceModal extends IMethodsEditModal {
  addEquipment(): void

  getOptionsServersGroups(searchString?: string): void

  setOwnerOptions(search?: string): void

  getOptionsOwner(search?: string): Promise<Array<VksSelectOptionEntity>>

  getCallLegProfileGuidOptions(): void

  getCallBrandingProfileGuidOptions(): void

  getSelected(id: number): void

  entityChanged(entity: string, value: string | number): void

  setLinkSpaceToParticipantsModel(): void

  setLinkSpaceToParticipantsRows(): void

  deleteLinkSpaceToParticipantsRow(id: number): void

  tableParticipantsSearchChanged(searchString: string): void

  tableParticipantsCheckboxesChanged(data: { id: number; field: string }): void

  tableParticipantsSelectChanged(data: {
    id: number
    field: string
    value: string | number
    option: VksSelectOptionEntity
  }): void

  addParticipantsRow(): void

  checkUniqueUsersData(): boolean
}

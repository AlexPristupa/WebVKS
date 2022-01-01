import { TableName } from '@/modules/table_grid/TableName.const'
import api from '@/api_services'
import { methods } from '@/api_services/httpMethods.enum'
import { IMpCellSelectSettings } from '@/components/MpTable/cell/config/MpCellSelect/MpCellSelect.interface'

export const mpSelectOptionsGetters: {
  [key: string]: { [key: string]: IMpCellSelectSettings }
} = {
  [TableName.editSpaceParticipants]: {
    vksUser: {
      display: {
        id: 'id',
        name: 'name',
      },
      search: true,
      request: api.dictionaryVksUser,
      method: methods.get,
      defaultParams: { limit: 300, search: '' },
      additionalParams: [],
    },
    callLegProfileGuid: {
      display: {
        id: 'callLegProfilesId',
        name: 'callLegProfilesName',
      },
      clearable: true,
      search: false,
      request: api.proxySpacesCallLegProfiles,
      method: methods.post,
      defaultParams: {},
      additionalParams: ['serversGroupsId'],
    },
  },
}

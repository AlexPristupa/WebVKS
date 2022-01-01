import { URLs } from '@/api_services/Urls.const'
import { tableApiDataValidation } from '@/modules/ApiDataValidation/table.ApiDataValidation.ts'
import { singleNestingApiDataValidation } from '@/modules/ApiDataValidation/singleNesting.ApiDataValidation'
import { doubleNestingApiDataValidation } from '@/modules/ApiDataValidation/doubleNesting.ApiDataValidation'
import { treeApiDataValidation } from '@/modules/ApiDataValidation/tree.ApiDataValidation.ts'
import { StructureTree } from '@/modules/ApiDataValidation/ResponseDto/StructureTree/StructureTree'
import { ExtensionPropertyCatalogResponseData } from '@/modules/ApiDataValidation/ResponseDto/PropertyCatalog/ExtensionPropertyCatalog.ResponseData'
import { PropertyCatalogEntity } from '@/modules/ApiDataValidation/ResponseDto/PropertyCatalog/PropertyCatalog.Entity'
import { ExtensionPropertyFieldListResponseData } from '@/modules/ApiDataValidation/ResponseDto/PropertyFieldList/ExtensionPropertyFieldList.ResponseData'
import { ColumnTableServer } from '@/modules/ApiDataValidation/ResponseDto/ColumnTableServer/ColumnTableServer'
import { methods } from '@/api_services/httpMethods.enum'
import { LoginEntity } from '@/modules/ApiDataValidation/ResponseDto/Login/Login.Entity'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity.ts'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import { VksSelectOptionWithPrivateEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOptionWithPrivate.entity'
import { ScheduleTextEntity } from '@/modules/ApiDataValidation/ResponseDto/Schedule/ScheduleText.entity.ts'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity.ts'
import { proxyApiDataValidation } from '@/modules/ApiDataValidation/proxy.apiDataValidation'
import { VksTimeZoneEntity } from '@/modules/ApiDataValidation/ResponseDto/TimeZone/VksTimeZone.entity'
import { VksLinkSpaceToParticipantsEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksLinkSpaceToParticipants.entity'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity.ts'
import { VksUserEntity } from '@/modules/ApiDataValidation/ResponseDto/User/User.entity.ts'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'
import { VksUserOtherEntity } from '@/modules/ApiDataValidation/ResponseDto/UserOthers/UserOther.entity'

export const apiDataValidation = (
  data,
  url?: string,
  method: methods = methods.get,
) => {
  if (url && Object.values(URLs).includes(url as URLs)) {
    // Коллекция url ответы из которых будут валидированы
    const validationUrl: Array<URLs> = [
      URLs.fetchData,
      URLs.invExtensionPropertyCatalog,
      URLs.filtrationPanelList,
      URLs.fetchNameFieldAgGridVue,
      URLs.login,
      URLs.vksUser,
      URLs.dictionaryServersGroups,
      URLs.dictionaryConnectionType,
      URLs.dictionarySpaces,
      URLs.dictionaryVksUser,
      URLs.vksUserOther,
      URLs.dictionaryVksUserOthers,
      URLs.dictionaryAspNetUsers,
      URLs.bookingGetUserByLogin,
      URLs.dictionaryVksServer,
      URLs.booking,
      URLs.spaces,
      URLs.bookingScheduleText,
    ]

    if (url.includes('proxy')) {
      return proxyApiDataValidation(data, url, method)
    }

    if (!Object.values(validationUrl).includes(url as URLs)) {
      return data.data ? data.data : true
    }
    switch (url) {
      case URLs.fetchData:
        if (method === methods.post) {
          return tableApiDataValidation(data.data)
        } else {
          return data.data ? data.data : true
        }

      case URLs.invTreeStructuresTree:
        if (method === methods.post) {
          return treeApiDataValidation(data.data, StructureTree)
        } else {
          return data.data ? data.data : true
        }

      case URLs.invExtensionPropertyCatalog:
        if (method === methods.get) {
          return doubleNestingApiDataValidation(
            data.data,
            ExtensionPropertyCatalogResponseData,
            [
              {
                fieldName: 'values',
                ClassName: PropertyCatalogEntity,
              },
            ],
          )
        } else {
          return data.data ? data.data : true
        }

      case URLs.filtrationPanelList:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            ExtensionPropertyFieldListResponseData,
          )
        }
        break

      case URLs.fetchNameFieldAgGridVue:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, ColumnTableServer)
        }
        break

      case URLs.login:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, LoginEntity)
        }
        break

      case URLs.dictionaryServersGroups:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, VksServerEntity)
        }
        break

      case URLs.dictionarySpaces:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionEntity,
          )
        }
        break

      case URLs.bookingGetUserByLogin:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionEntity,
          )
        }
        break

      case URLs.dictionaryVksUser:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionEntity,
          )
        }
        break

      case URLs.dictionaryVksUserOthers:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, VksUserOtherEntity)
        }
        break

      case URLs.vksUserOther:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, VksUserOtherEntity)
        }
        break

      case URLs.vksUser:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, VksUserEntity)
        }
        break

      case URLs.dictionaryAspNetUsers:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionWithPrivateEntity,
          )
        }
        break

      case URLs.dictionaryConnectionType:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionWithPrivateEntity,
          )
        }
        break

      case URLs.dictionaryTimeZone:
        if (method === methods.get) {
          return singleNestingApiDataValidation(data.data, VksTimeZoneEntity)
        }
        break

      case URLs.dictionaryBookingType:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionWithPrivateEntity,
          )
        }
        break

      case URLs.dictionaryPinPolitics:
        if (method === methods.get) {
          return singleNestingApiDataValidation(
            data.data,
            VksSelectOptionWithPrivateEntity,
          )
        }
        break

      case URLs.booking:
        if (
          method === methods.get ||
          method === methods.put ||
          method === methods.post
        ) {
          return doubleNestingApiDataValidation(data.data, VksBookingEntity, [
            {
              fieldName: 'linkBookingToVksUsersOthers',
              ClassName: VksBookingLinkBookingToVksUsersOthers,
            },
            {
              fieldName: 'linkBookingToParticipants',
              ClassName: VksBookingLinkToParticipant,
            },
          ])
        }
        break

      case URLs.spaces:
        if (method === methods.get) {
          return doubleNestingApiDataValidation(data.data, VksSpaceEntity, [
            {
              fieldName: 'linkSpaceToParticipants',
              ClassName: VksLinkSpaceToParticipantsEntity,
            },
          ])
        }
        break

      case URLs.bookingScheduleText:
        if (method === methods.post) {
          return singleNestingApiDataValidation(data.data, ScheduleTextEntity)
        }
        break

      default:
        return data.data ? data.data : true
    }
  }
  return data.data ? data.data : true
}

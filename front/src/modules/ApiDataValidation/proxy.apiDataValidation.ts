import { methods } from '@/api_services/httpMethods.enum'
import { URLs } from '@/api_services/Urls.const'
import { AddBookingProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/AddBookingProxy.entity.ts'
import { singleNestingApiDataValidation } from '@/modules/ApiDataValidation/singleNesting.ApiDataValidation'
import { SpacesCaLegProfilesProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/SpacesCaLegProfilesProxy.entity'
import { SpacesCallBrandingProfilesProxyEntity } from '@/modules/ApiDataValidation/ResponseDto/serviceProxyEntities/SpacesCallBrandingProfilesProxy.entity'

export const proxyApiDataValidation = (
  data,
  url?: string,
  method: methods = methods.get,
) => {
  const proxyValidationUrl = [URLs.proxyBookingSpaceAdd]
  if (!Object.values(proxyValidationUrl).includes(url as URLs)) {
    return data.data ? data.data : true
  }
  switch (url) {
    case URLs.proxyBookingSpaceAdd:
      if (method === methods.post) {
        return singleNestingApiDataValidation(data.data, AddBookingProxyEntity)
      }
      break
    case URLs.proxySpacesCallLegProfiles:
      if (method === methods.post) {
        return singleNestingApiDataValidation(
          data.data,
          SpacesCaLegProfilesProxyEntity,
        )
      }
      break
    case URLs.proxySpacesCallBrandingProfiles:
      if (method === methods.post) {
        return singleNestingApiDataValidation(
          data.data,
          SpacesCallBrandingProfilesProxyEntity,
        )
      }
      break
    default:
      data.data ? data.data : true
  }
}

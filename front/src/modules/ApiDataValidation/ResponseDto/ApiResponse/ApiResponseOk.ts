import { Expose, Type } from 'class-transformer'
import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'
import { IsDefined, IsEnum, IsOptional } from 'class-validator'
import { TypeApiResult } from '@/modules/CheckApiResponse/CheckApiResponse.const'
import { IApiResponse } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/IApiResponse'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface.ts'

export class ApiResponseOk implements IApiResponse, IEntity {
  toValidationName(): string {
    return 'ApiResponseOk'
  }

  @Type(() => ApiResponseData)
  public data: Array<ApiResponseData> = []

  @Expose()
  @IsOptional()
  public message = null

  @Expose()
  @IsDefined()
  @IsEnum(TypeApiResult)
  public result: TypeApiResult = TypeApiResult.ok
}

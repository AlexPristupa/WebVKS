import { IApiResponse } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/IApiResponse'
import { Expose } from 'class-transformer'
import { IsDefined, IsOptional, IsString } from 'class-validator'
import { TypeApiResult } from '@/modules/CheckApiResponse/CheckApiResponse.const'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface.ts'

export class ApiResponseError implements IApiResponse, IEntity {
  toValidationName(): string {
    return 'ApiResponseError'
  }

  @Expose()
  @IsString()
  public data: Array<string> | string | null = []

  @Expose()
  @IsOptional()
  @IsString()
  public message: string = ''

  @Expose()
  @IsDefined()
  public result: typeof TypeApiResult.error = TypeApiResult.error
}

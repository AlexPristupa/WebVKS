import { Expose } from 'class-transformer'
import { IsDefined, IsOptional, IsString } from 'class-validator'
import { TypeApiResult } from '@/modules/CheckApiResponse/CheckApiResponse.const'
import { IApiResponse } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/IApiResponse'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface.ts'

export class ApiResponseWarning implements IApiResponse, IEntity {
  toValidationName(): string {
    return 'ApiResponseWarning'
  }

  @Expose()
  @IsString()
  public data: Array<string> | string | null = null

  @Expose()
  @IsOptional()
  @IsString()
  public message: string = ''

  @Expose()
  @IsDefined()
  public result: typeof TypeApiResult.warning = TypeApiResult.warning
}

import { TableName } from '@/modules/table_grid/TableName.const'
import { plainToClass } from 'class-transformer'
import { ColumnFilterResponseData } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/ColumnFilter.ResponseData'
import { validate } from 'class-validator'
import { apiValidationMessage } from '@/modules/ApiDataValidation/apiValidationMessage'
import { VksBookingTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksBookingTable.entity'
import { VksSpaceTableEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksSpaceTable.entity.ts'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'
import { VksUserProfileEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksUserProfile.entity'
import { VksServersGroupEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServersGroup.entity'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'
import { VksSpaceEditTableBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksSpaceEditBookingTable.entity.ts'
import { VksRecordingsShareEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordingsShare.entity'

// Коллекция имен таблиц и соответстыующим им классам. Будет запущена валидация
export const validationTableClassList = {
  [TableName.booking]: VksBookingTableEntity,
  [TableName.spaces]: VksSpaceTableEntity,
  [TableName.recordings]: VksRecordingsEntity,
  [TableName.recordingsShare]: VksRecordingsShareEntity,
  [TableName.userProfiles]: VksUserProfileEntity,
  [TableName.serversGroups]: VksServersGroupEntity,
  [TableName.servers]: VksServerEntity,
  [TableName.editSpaceBooking]: VksSpaceEditTableBookingEntity,
}

export const tableApiDataValidation = (responseData: any) => {
  // Приведение к классу сущности первого уровня вложенности
  const result = plainToClass<ColumnFilterResponseData, any>(
    ColumnFilterResponseData,
    responseData,
    {
      excludeExtraneousValues: true,
    },
  )

  if (Object.keys(validationTableClassList).includes(result.tableName)) {
    result.items = responseData.items?.map((item: object) => {
      const classType = validationTableClassList[result.tableName]
      // Приведение к классу данных для строк таблицы
      const entity = plainToClass<typeof classType, object>(classType, item, {
        excludeExtraneousValues: true,
      })

      // Валидация данных для строк таблицы
      validate(entity).then(errors => {
        if (errors.length) {
          apiValidationMessage(errors, entity)
        }
      })

      return entity
    })
  }

  // Валидация данных класса ColumnFilterResponseData
  validate(result).then(errors => {
    if (errors.length) {
      apiValidationMessage(errors, result)
    }
  })
  return result
}

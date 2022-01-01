import { IStructureItem } from '@/modules/TreeStructure/TreeStructure.types'
import { Expose, Type } from 'class-transformer'
import {
  IsBoolean,
  IsDefined,
  IsInt,
  IsOptional,
  IsString,
} from 'class-validator'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export class StructureTree implements IStructureItem, IEntity {
  toValidationName(): string {
    return 'StructureTree'
  }

  @Type(() => StructureTree)
  public children: Array<StructureTree> = []

  @Expose()
  @IsDefined()
  @IsBoolean()
  public collapsed: boolean = false

  @Expose()
  @IsOptional()
  @IsString()
  public description: string | null = ''

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isFound: boolean = false

  @Expose()
  @IsDefined()
  @IsInt()
  public level: number = 0

  @Expose()
  @IsOptional()
  @IsInt()
  public parentId: number | null = null

  @Expose()
  @IsDefined()
  @IsString()
  public structureName: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public structureRangeString: string = ''
}

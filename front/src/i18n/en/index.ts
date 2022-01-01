import dialogs from './dialogs'
import forms from './forms'
import filter from './filter'
import general from './general'
import tableGrid from './table_grid'
import tables from './tables'
import entities from '@/i18n/en/entities'
import routing from '@/i18n/en/routing'
import elementLocale from 'element-ui/lib/locale/lang/en' // element-ui lang
import button from './button'
import matching from '@/i18n/en/matching'
import operand from '@/i18n/en/operand'
import callTable from '@/i18n/en/cell_table'
import confirm from '@/i18n/en/confirm'
import panels from '@/i18n/en/panels'
import pagination from '@/i18n/en/pagination'
import videoPlayer from '@/i18n/en/video-player'

export default {
  ...elementLocale,
  dialogs,
  forms,
  filter,
  general,
  tableGrid,
  tables,
  button,
  entities,
  pagination,
  routing,
  matching,
  operand,
  callTable,
  confirm,
  panels,
  videoPlayer,
}

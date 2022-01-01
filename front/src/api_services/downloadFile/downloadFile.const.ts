import { URLs } from '@/api_services/Urls.const'

export enum exportTypes {
  excel = 'excel',
  video = 'video',
}

export enum downloadTypes {
  download = 'download',
  show = 'show',
}

export const hrefs = {
  excel: URLs.exportDownload + '?fileName=',
  video: URLs.recordingFile + '?id=',
}

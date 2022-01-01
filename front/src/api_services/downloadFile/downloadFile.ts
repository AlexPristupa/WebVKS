/**
 * @description мудуль формирования ссылки и выгрузки файлов.
 */
import { CONFIG } from '@/config'
import { downloadTypes, exportTypes, hrefs } from './downloadFile.const'
import { Auth } from '@/modules/Auth/Auth'
import axios, { CancelTokenSource } from 'axios'

function getHref(fileName: string | number, format: string): string {
  const href = format ? hrefs[format] + fileName : hrefs[exportTypes.excel]
  return process.env.NODE_ENV === 'development'
    ? CONFIG.domain.dev + href
    : href
}

export function downloadFile(
  fileName: string | number,
  format: string,
  href?: string,
  fileNameFromHeader?: string,
) {
  const link = document.createElement('a')

  link.href = !href ? encodeURI(getHref(fileName, format)) : href
  link.download = fileNameFromHeader || 'download'
  document.body.appendChild(link)
  link.click()

  document.body.removeChild(link)
}

export async function downloadBlob(
  fileName: string | number,
  format: string,
  type: 'download' | 'show',
  cancelToken: CancelTokenSource['token'] | null,
) {
  try {
    let src = ''
    if (Auth.getAccessToken()) {
      const href = encodeURI(getHref(fileName, format))
      await axios({
        url: href,
        method: 'GET',
        cancelToken: cancelToken ? cancelToken : undefined,
        responseType: 'blob',
      }).then(response => {
        if (response) {
          const name = response.headers?.['content-disposition']
            ?.split('filename=')[1]
            .split(';')[0]
          const blob = new Blob([response.data as any], { type: 'video/mp4' })
          const linkSource = URL.createObjectURL(blob)
          if (type === downloadTypes.show) {
            src = linkSource
          } else {
            downloadFile('', '', linkSource, name)
            return
          }
        }
      })
    }
    return src
  } catch (e) {
    console.warn(e)
  }
}

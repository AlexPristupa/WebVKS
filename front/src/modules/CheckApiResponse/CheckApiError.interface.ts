export type CheckApiError = (error: Error) => void
export type CheckError = (error: Error) => Promise<boolean> | boolean

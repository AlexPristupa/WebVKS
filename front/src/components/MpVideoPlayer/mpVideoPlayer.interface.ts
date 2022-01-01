interface IPlayerOptions {
  autoplay: boolean
  muted: boolean
  language: string
  playbackRates: Array<number>
  sources: Array<{ src: string; type: string }>
}

export interface IDataMpVideoPlayer {
  playerOptions: IPlayerOptions
}

export interface IMethodsMpVideoPlayer {
  onPlayerPlay(player: any): void
  onPlayerPause(player: any): void
  playerStateChanged(playerCurrentState: any): void
  playerReadied(player: any): void
}

export interface IComputedMpVideoPlayer {}

export interface IPropsMpVideoPlayer {
  visible: boolean
  src: string
}

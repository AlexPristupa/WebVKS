<template>
  <el-dialog
    top="10vh"
    width="fit-content"
    :close-on-click-modal="true"
    :visible="visible"
    :draggable="false"
    @close="$emit('close')"
    class="mp-dialog__video-player"
  >
    <div class="mp-video-player">
      <i class="el-icon-close" @click="$emit('close')" />
      <video-player
        class="video-player-box"
        ref="videoPlayer"
        :options="playerOptions"
        :playsinline="true"
        @play="onPlayerPlay($event)"
        @pause="onPlayerPause($event)"
        @statechanged="playerStateChanged($event)"
        @ready="playerReadied"
      />
    </div>
  </el-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
// @ts-ignore
import videojs from 'video.js'
// @ts-ignore
import { videoPlayer } from 'vue-video-player'
import videoPlayerRu from '@/i18n/ru/video-player'
import videoPlayerEng from '@/i18n/en/video-player'
import { getLanguage } from '@/i18n'
import {
  IComputedMpVideoPlayer,
  IDataMpVideoPlayer,
  IMethodsMpVideoPlayer,
  IPropsMpVideoPlayer,
} from '@/components/MpVideoPlayer/mpVideoPlayer.interface'

export default Vue.extend<
  IDataMpVideoPlayer,
  IMethodsMpVideoPlayer,
  IComputedMpVideoPlayer,
  IPropsMpVideoPlayer
>({
  name: 'MpVideoPlayer',
  components: {
    videoPlayer,
  },
  props: {
    visible: {
      type: Boolean as () => boolean,
      default: false,
    },
    src: {
      type: String as () => string,
      default: '',
    },
  },
  data() {
    return {
      playerOptions: {
        autoplay: true,
        muted: false,
        language: getLanguage(),
        playbackRates: [0.7, 1.0, 1.5, 2.0],
        sources: [
          {
            type: 'video/mp4',
            src: '',
          },
        ],
      },
    }
  },
  mounted() {
    videojs.addLanguage('en', videoPlayerEng)
    videojs.addLanguage('ru', videoPlayerRu)
    this.playerOptions = {
      autoplay: true,
      muted: false,
      language: getLanguage(),
      playbackRates: [0.7, 1.0, 1.5, 2.0],
      sources: [
        {
          type: 'video/mp4',
          src: this.src,
        },
      ],
    }
  },
  methods: {
    onPlayerPlay(player) {
      return
    },
    onPlayerPause(player) {
      return
    },
    // or listen state event
    playerStateChanged(playerCurrentState) {
      return
    },
    // player is ready
    playerReadied(player) {
      return
    },
  },
})
</script>

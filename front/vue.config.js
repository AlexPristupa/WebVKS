'use strict'
const path = require('path')

function resolve(dir) {
  return path.join(__dirname, dir)
}

module.exports = {
  outputDir: '../back/MentolVKS/wwwroot',
  configureWebpack: {
    performance: {
      maxEntrypointSize: 10240000,
      maxAssetSize: 5120000,
    },
    resolve: {
      alias: {
        '@': resolve('src'),
        THEME_NAME: resolve(
          `./src/styles/theme/${process.env.VUE_APP_THEME}/index.scss`,
        ),
      },
    },
  },
}

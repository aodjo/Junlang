import { defineConfig } from 'vitepress'
import { ko } from '../ko/config.mts'

export default defineConfig({
  title: '준랭 (Junlang)',

  themeConfig: {
    socialLinks: [
      { icon: 'github', link: 'https://github.com/aodjo/junlang' }
    ],
    search: {
      provider: 'local'
    }
  },

  locales: {
    ko: { label: '한국어', ...ko }
  }
})
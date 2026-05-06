import { defineConfig } from 'vitepress'
import { ko } from '../ko/locale.mts'
import { en } from '../en/locale.mts'
import type { LanguageRegistration } from 'shiki'
import junlangGrammar from './syntaxes/junlang.tmLanguage.json'

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
    ko: { label: '한국어', ...ko },
    en: { label: 'English', ...en }
  },
  markdown: {
    languages: [
      {
        ...junlangGrammar,
        aliases: ['jun', '준랭']
      } as unknown as LanguageRegistration
    ]
  }
})
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
    },
    notFound: {
      title: '페이지를 찾을 수 없어요. :(',
      quote: "여러분이 페이지를 잘못 찾아오셨거나, 준서가 이 페이지를 먹었을 지도 몰라요.",
      linkText: '홈으로 데려가줘요'
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
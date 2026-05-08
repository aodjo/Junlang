import { defineConfig, type DefaultTheme } from 'vitepress'

export const ko = defineConfig({
  lang: 'ko-KR',
  description: '준랭(Junlang)에 대해 설명합니다.',

  themeConfig: {
    search: {
      provider: 'local',
      options: {
        locales: {
          ko: {
            translations: {
              button: {
                buttonText: '검색',
                buttonAriaLabel: '검색'
              },
              modal: {
                displayDetails: '상세 정보 표시',
                resetButtonTitle: '검색 초기화',
                backButtonTitle: '검색 닫기',
                noResultsText: '결과를 찾을 수 없습니다',
                footer: {
                  selectText: '선택',
                  selectKeyAriaLabel: '선택',
                  navigateText: '이동',
                  navigateUpKeyAriaLabel: '위로',
                  navigateDownKeyAriaLabel: '아래로',
                  closeText: '닫기',
                  closeKeyAriaLabel: 'esc'
                }
              }
            }
          }
        }
      }
    },
    nav: nav(),
    sidebar: sidebar(),

    outline: {
      label: '이 페이지의 목차'
    },
    docFooter: {
      prev: '이전 페이지',
      next: '다음 페이지'
    },
    lastUpdatedText: '마지막 업데이트',
    darkModeSwitchLabel: '다크 모드',
    sidebarMenuLabel: '메뉴',
    returnToTopLabel: '맨 위로',
    langMenuLabel: '언어 변경'
  }
})

function nav(): DefaultTheme.NavItem[] {
  return [
    { text: '메인', link: '/ko/' },
    { text: '문서', link: '/ko/before-starting/what-is-junlang' }
  ]
}

function sidebar(): DefaultTheme.Sidebar {
  return [
    {
      text: '시작하기 앞서',
      items: [
        { text: '준랭이란 무엇일까?', link: '/ko/before-starting/what-is-junlang' },
        { text: '오준서가 누구인가요?', link: '/ko/before-starting/who-is-o-jun-seo' },
      ]
    },
    {
      text: '준랭',
      items: [
        { text: '시작하기', link: '/ko/junlang/getting-started' },
        { text: '수 체계', link: '/ko/junlang/number-system' },
        { text: '입출력', link: '/ko/junlang/io' },
        { text: '참과 거짓', link: '/ko/junlang/boolen' },
        { text: '변수', link: '/ko/junlang/variables' },
        {
          text: '연산자',
          collapsed: false,
          items: [
            { text: '연산자', link: '/ko/junlang/operators' },
            { text: '붙여 쓰기 규칙', link: '/ko/junlang/parsing-rules' },
            { text: '준, 서', link: '/ko/junlang/jun-seo' },
          ]
        },
        { text: '조건문', link: '/ko/junlang/conditionals' },
        { text: '반복문', link: '/ko/junlang/loops' },
        { text: '흐름 제어', link: '/ko/junlang/flow-control' },
        { text: '문장과 블록', link: '/ko/junlang/statements' },
        { text: '주석', link: '/ko/junlang/comments' },
        { text: '에러 메세지', link: '/ko/junlang/error' },
        {
          text: '예제',
          collapsed: false,
          items: [
            { text: '유클리드 호제법', link: '/ko/junlang/gcd' },
            { text: '피보나치 수열', link: '/ko/junlang/fibonacci' }
          ]
        }
      ]
    },
    {
      text: '확장 프로그램',
      items: [
        { text: 'VSCode', link: '/ko/extentions/vscode'}
      ]
    },
    {
      text: '정보',
      items: [
        { text: 'LICENSE', link: '/ko/about/license'}
      ]
    },
  ]
}
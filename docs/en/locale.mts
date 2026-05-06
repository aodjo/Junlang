import { defineConfig, type DefaultTheme } from 'vitepress'

export const en = defineConfig({
  lang: 'en-US',
  description: 'Documentation for Junlang.',

  themeConfig: {
    nav: nav(),
    sidebar: sidebar(),

    outline: {
      label: 'On this page'
    },
    docFooter: {
      prev: 'Previous',
      next: 'Next'
    },
    lastUpdatedText: 'Last updated',
    darkModeSwitchLabel: 'Dark mode',
    sidebarMenuLabel: 'Menu',
    returnToTopLabel: 'Back to top',
    langMenuLabel: 'Change language'
  }
})

function nav(): DefaultTheme.NavItem[] {
  return [
    { text: 'Home', link: '/en/' },
    { text: 'Docs', link: '/en/before-starting/what-is-junlang' }
  ]
}

function sidebar(): DefaultTheme.Sidebar {
  return [
    {
      text: 'Before Starting',
      items: [
        { text: 'What is Junlang?', link: '/en/before-starting/what-is-junlang' },
        { text: 'Who is O Junseo?', link: '/en/before-starting/who-is-o-jun-seo' },
      ]
    },
    {
      text: 'Junlang',
      items: [
        { text: 'Getting Started', link: '/en/junlang/getting-started' },
        { text: 'Number System', link: '/en/junlang/number-system' },
        { text: 'Input / Output', link: '/en/junlang/io' },
        { text: 'True and False', link: '/en/junlang/boolen' },
        { text: 'Variables', link: '/en/junlang/variables' },
        {
          text: 'Operators',
          collapsed: false,
          items: [
            { text: 'Operators', link: '/en/junlang/operators' },
            { text: 'Inline Writing Rules', link: '/en/junlang/parsing-rules' },
            { text: '준, 서', link: '/en/junlang/jun-seo' },
          ]
        },
        { text: 'Conditionals', link: '/en/junlang/conditionals' },
        { text: 'Loops', link: '/en/junlang/loops' },
        { text: 'Flow Control', link: '/en/junlang/flow-control' },
        { text: 'Statements and Blocks', link: '/en/junlang/statements' },
        { text: 'Comments', link: '/en/junlang/comments' },
        { text: 'Error Messages', link: '/en/junlang/error' },
        {
          text: 'Examples',
          collapsed: false,
          items: [
            { text: 'Euclidean Algorithm', link: '/en/junlang/gcd' },
            { text: 'Fibonacci Sequence', link: '/en/junlang/fibonacci' }
          ]
        }
      ]
    },
    {
      text: 'Extensions',
      items: [
        { text: 'VSCode', link: '/en/extentions/vscode' }
      ]
    },
    {
      text: 'About',
      items: [
        { text: 'LICENSE', link: '/en/about/license' }
      ]
    },
  ]
}

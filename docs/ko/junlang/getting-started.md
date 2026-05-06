---
description: Junlang을 시작하는 방법을 안내합니다.
---

# 시작하기

이 문서에서는 Junlang을 설치하고 첫 코드를 실행하는 방법을 안내합니다.

## 다운로드

[GitHub Releases](https://github.com/aodjo/junlang/releases) 페이지에서 사용 중인 운영체제에 맞는 실행 파일을 다운로드하세요.

| 운영체제 | 파일 |
| --- | --- |
| Windows | `junlang.exe` |
| Linux | `junlang` |

## 실행하기

다운로드한 실행 파일을 통해 `.junl` 파일을 실행할 수 있습니다.

::: code-group

```powershell [Windows]
.\junlang.exe hello.junl
```

```bash [Linux / macOS]
chmod +x junlang
./junlang hello.junl
```

:::

::: tip 💡 매번 경로를 입력하기 번거롭다면 실행 파일을 환경 변수에 등록해두는 것을 권장합니다.
:::

## 파일 확장자

Junlang 소스 코드 파일의 확장자는 **`.junl`** 입니다.

```bash
./junlang hello.junl   # O
./junlang hello.txt    # X
```
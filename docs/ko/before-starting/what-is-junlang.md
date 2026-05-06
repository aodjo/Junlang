---
description: 준랭(junlang)은 Python으로 제작된 난해한 프로그래밍 언어입니다.
---

# 준랭이란 무엇일까?

준랭(Junlang)은 [선린인터넷고등학교](https://sunrint.sen.hs.kr/)를 다니고 있는 121기 오준서를 위한 [난해한 프로그래밍 언어](https://ko.wikipedia.org/wiki/%EB%82%9C%ED%95%B4%ED%95%9C_%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%98%EB%B0%8D_%EC%96%B8%EC%96%B4) (esolang)입니다.

<div class="tip custom-block" style="padding-top: 8px">

아래 기술된 내용은 몰라도 상관 없는 [TMI](https://ko.wikipedia.org/wiki/%EC%A0%95%EB%B3%B4_%EA%B3%BC%EB%8B%A4)입니다. 빨리 시작하고 싶으시면 [건너뛰세요](/ko/junlang/getting-started).

</div>

## 특징

준랭은 esolang을 작성할 때 **불필요한 가독성**을 최대한 배제하고자 합니다.

극도로 제한된 문자 집합과 문맥 의존적 파싱 규칙을 통해, 같은 기호가 위치에 따라 전혀 다른 의미를 가지도록 설계되었습니다. 동시에 키워드와 에러 메시지는 일상적인 한국어 회화체로 작성되어, 코드를 읽는 경험과 실제 동작 사이에 의도적인 괴리를 만듭니다.

조건문, 반복문, 변수, 입출력, 산술/비교 연산을 모두 지원하므로 이론상 [유클리드 호제법](../junlang/gcd)부터 [피보나치 수열](../junlang/fibonacci)까지 무엇이든 작성할 수 있습니다.

## 다른 esolang과 무엇이 다른가요? 

대부분의 esolang은 **언어 자체의 제약**에 초점을 맞춥니다. [Brainfuck](https://ko.wikipedia.org/wiki/%EB%B8%8C%EB%A0%88%EC%9D%B8%ED%8D%BD)은 8개의 명령어, [Whitespace](https://ko.wikipedia.org/wiki/%ED%99%94%EC%9D%B4%ED%8A%B8%EC%8A%A4%ED%8E%98%EC%9D%B4%EC%8A%A4_(%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%98%EB%B0%8D_%EC%96%B8%EC%96%B4))는 공백 문자만, [Malbolge](https://ko.wikipedia.org/wiki/%EB%A7%90%EB%A0%88%EB%B3%BC%EC%A0%9C)는 사실상 작성 불가능한 문법으로 유명합니다.

준랭은 여기서 한 발 더 나아가, **특정 인물**을 언어의 일부로 편입시켰습니다. 변수 대입 키워드가 `준서`이고, 에러는 모두 오준서에 관한 것입니다. 이는 esolang의 전통적인 "기계적 난해함"과는 다른, **사회적 난해함**이라는 새로운 축을 제시합니다.

## 도대체 오준서가 누구인가요??
<div class="tip custom-block" style="padding-top: 8px">

해당 내용은 [이 문서](/ko/before-starting/who-is-o-jun-seo)에서 기술하겠습니다.

</div>
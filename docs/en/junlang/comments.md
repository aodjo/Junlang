---
description: Comments in Junlang are used to leave notes without affecting the code.
---

# Comments

Comments are used to leave explanations or notes without affecting how the code runs. Junlang provides two comment syntaxes.

## Single-Line Comments

Everything from `걍` to the end of the line is treated as a comment.

```junlang
걍 이 줄은 무시됩니다
오~준서!ㅋ 걍 변수1에 1을 대입
```

Code can appear before `걍` on the same line. Only the part after `걍` becomes a comment.

## Block Comments

Use block comments when you want to leave comments spanning multiple lines. They start with `참고로` and end with `알았냐?`.

```junlang
참고로
이 코드는 10부터 1까지 출력합니다.
변수1을 카운터로 사용합니다.
알았냐?

오 오?~준서!ㅋ
준서야 ! 또처먹냐?
  오준서!ㅋ
  !~?오~준서!ㅋ
ㅋ
```

Everything between `참고로` and `알았냐?` is ignored.

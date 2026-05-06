---
description: Covers the flow control keywords 더처먹어 and 그만처먹어 used inside loops in Junlang.
---

# Flow Control

Inside a [loop](./loops), there are two keywords for changing the flow of iteration.

| Junlang | Common Equivalent | Behavior |
| --- | --- | --- |
| `더처먹어ㅋ` | `continue` | Skip the current iteration and restart from the condition check |
| `그만처먹어ㅋ` | `break` | Exit the loop immediately |

## `더처먹어` — Skip to the Next Iteration

When `더처먹어` is encountered, the remaining statements in the block are skipped and execution jumps directly to the next iteration (condition check).

```junlang
오 오?~준서!ㅋ
준서야 ! 또처먹냐?
  !~?오~준서!ㅋ
  준서야 !@오오오 맞냐?
    더처먹어ㅋ
  ㅋ
  오준서!ㅋ
ㅋ
```

This code prints from 10 down to 1, but when variable 1 equals `3`, `더처먹어` causes the print statement to be skipped. Therefore, `3` is not printed.

## `그만처먹어` — Exit the Loop

When `그만처먹어` is encountered, the loop terminates immediately, even if the condition is still true.

```junlang
오 오?~준서!ㅋ
준서야 ! 또처먹냐?
  준서야 !@오오오오오 맞냐?
    그만처먹어ㅋ
  ㅋ
  오준서!ㅋ
  !~?오~준서!ㅋ
ㅋ
```

This code prints from 10 downward, but exits the loop the moment variable 1 reaches `5`. Therefore, only `10, 9, 8, 7, 6` are printed.

::: warning Can only be used inside loops
`더처먹어` and `그만처먹어` can only be used inside a [loop](./loops). Using them outside a loop produces the following error:

```text
먹지도 않았는데 뭘 그만 처 먹어?
```
:::

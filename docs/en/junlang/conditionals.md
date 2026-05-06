---
description: Conditionals in Junlang branch the flow of code based on whether an expression is true.
---

# Conditionals

Conditionals execute different code depending on whether a condition is true or false. Junlang conditionals are written in a conversational style, as if talking to a person.

::: tip
For how conditions are evaluated as true/false, see [True and False](./boolen).
:::

## Simple Condition (`if`)

If the condition is true, the statements inside the block are executed.

```junlang
준서야 [condition] 맞냐?
  [statements]
ㅋ
```

### Example

```junlang
준서야 오 맞냐?
  오준서오ㅋ
ㅋ
```

The condition `오`(1) is true, so it outputs `오`(1).

## Either-Or (`if-else`)

If the condition is true, the first block is executed; if false, the second block.

```junlang
준서야 [condition] 맞냐?
  [statements when true]
ㅋ 아니냐?
  [statements when false]
ㅋ
```

### Example

```junlang
준서야 오? 맞냐?
  오준서오ㅋ
ㅋ 아니냐?
  오준서오오ㅋ
ㅋ
```

The condition `오?`(0) is false, so it outputs `오오`(2).

## Additional Conditions (`if-elif`)

When the first condition is false, another condition is checked.

```junlang
준서야 [condition1] 맞냐?
  [when condition1 is true]
ㅋ 아니면 [condition2] 이건?
  [when condition2 is true]
ㅋ
```

`아니면 ... 이건?` can be chained multiple times.

```junlang
준서야 [condition1] 맞냐?
  [when condition1 is true]
ㅋ 아니면 [condition2] 이건?
  [when condition2 is true]
ㅋ 아니면 [condition3] 이건?
  [when condition3 is true]
ㅋ
```

## All Conditions + Fallback (`if-elif-else`)

By combining `아니면 ... 이건?` with `아니냐?`, you can add a block that executes when all conditions are false.

```junlang
준서야 [condition1] 맞냐?
  [when condition1 is true]
ㅋ 아니면 [condition2] 이건?
  [when condition2 is true]
ㅋ 아니냐?
  [when all are false]
ㅋ
```

::: warning Keyword order matters
Within a conditional, `아니면 ... 이건?` can appear multiple times, but **`아니냐?` (else) must always come last and only once**.
:::

## Writing on a Single Line

Conditionals can also be compressed into a single line following the [inline writing rules](./statements#inline-writing).

```junlang
준서야 오 맞냐?
  오준서오ㅋ
ㅋ
```

```junlang
준서야 오 맞냐? 오준서오ㅋㅋ
```

The two code snippets above have exactly the same meaning.

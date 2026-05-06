---
description: Junlang has no separate boolean type — it represents true and false using numbers.
---

# True and False

## What are true and false?

In programming, **true** and **false** are the most fundamental values that indicate whether a condition is met or not.

- "Is 1 equal to 1?" → **True** (yes)
- "Is 1 equal to 2?" → **False** (no)
- "Is it raining today?" → True or false depending on the situation

Most programming languages have a dedicated type called boolean (`true` and `false`) to represent these two values. Conditionals and loops look at these true/false values to decide what to do next.

:::: details How booleans are represented in other programming languages

::: code-group

```javascript [JavaScript]
true
false
```

```python [Python]
True
False
```

```java [Java]
true
false
```

```cpp [C++]
true
false
```

```rust [Rust]
true
false
```

```go [Go]
true
false
```

```ruby [Ruby]
true
false
```

```haskell [Haskell]
True
False
```

```r [R]
TRUE
FALSE
```

:::

::::

## True and False in Junlang

Junlang has no separate boolean type like `true` or `false`. Instead, all values are treated as **numbers**, and truth or falsehood is determined by whether the value is zero or not.

## Evaluation Rules

| Value | Meaning |
| --- | --- |
| Any number other than `0` | True (truthy) |
| `0` | False (falsy) |

In other words, `오`(1), `오오`(2), `?오`(-1), and `오ㅋ오오오오오`(1.5) are all evaluated as **true**, while only `오?`(0) is **false**.

## Comparison Results

The results of comparison operators (`@`, `ㅁ`, `ㅊ`, etc.) are also expressed as numbers.

| Result | Junlang Notation | Value |
| --- | --- | --- |
| True | `오` | 1 |
| False | `오?` | 0 |

::: info
Junlang uses only two values to represent true and false: `오`(1) and `오?`(0). Therefore, if you output the result of a comparison directly, it will always be either `오` or `오?`.
:::

### Examples

```junlang
오@오
```

The above code evaluates `1 == 1`, so the result is `오`(1, true).

```junlang
오@오오
```

The above code evaluates `1 == 2`, so the result is `오?`(0, false).

## Usage in Conditionals

Conditionals treat the result of an expression as true if it is not 0. This means you can branch using just numbers or variables, without an explicit comparison.

```junlang
준서야 오 맞냐?
  오준서오ㅋ
ㅋ
```

The condition in this code is `오`(1), which is not 0, so it always evaluates to true and the statements inside the block are executed.

```junlang
준서야 오? 맞냐?
  오준서오ㅋ
ㅋ
```

In contrast, the condition here is `오?`(0), which is false, so the statements inside the block are not executed.

::: details &nbsp;Not sure what comparison operators and conditionals are?
&nbsp;— Don't worry!<br />
&nbsp;The `comparison operators` and `conditionals` mentioned in this document will be explained in detail later.
:::

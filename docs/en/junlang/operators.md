---
description: Learn about Junlang's arithmetic, comparison, and unary operators, as well as operator precedence.
---

# Expressions and Operators

This document covers how to compute and compare values in Junlang.

::: details What is an expression?
An **expression** is *a piece of code that produces a value*. For example, `1 + 2` is an expression, and its result is the value `3`.

The simplest expression in Junlang is a number by itself.

```junlang
오
```

This code alone is an expression, and its value is `1`. By adding operators, you can build more complex expressions.
:::

## Arithmetic Operators

| Junlang | Meaning | Common Equivalent |
| --- | --- | --- |
| `~` | Addition | `+` |
| `.` | Multiplication | `*` |
| `..` | Exponentiation | `**` |
| `#` | Division | `/` |

```junlang
오~오오
```

`1 + 2`, so the result is `오오오`(3).

```junlang
오오.오오오
```

`2 × 3`, so the result is `오오오오오오`(6).

```junlang
오오..오오오
```

`2³`, so the result is `오오오오오오오오`(8).

::: warning There is no subtraction operator
Junlang has no dedicated subtraction operator. Instead, combine the [negative sign `?`](./number-system#negative-numbers) with addition `~`.

```junlang
오~?오
```

`1 + (-1)` → `오?`(0)
:::

::: details What happens when you divide by 0 or raise to a decimal power?
**An error occurs. (See: [Errors](./error))**

**Division by zero:**
```text
이건 기하반도 안 하는 실수인데?
```

**Decimal exponent:**
```text
준서가 어렵대
```
:::


## Comparison Operators

| Junlang | Meaning | Common Equivalent |
| --- | --- | --- |
| `@` | Equal to | `==` |
| `!@` | Not equal to | `!=` |
| `ㅁ` | Less than | `<` |
| `ㅊ` | Greater than | `>` |
| `ㅁ@` | Less than or equal to | `<=` |
| `ㅊ@` | Greater than or equal to | `>=` |

The result of a comparison operator is always `오`(1, true) or `오?`(0, false).

```junlang
오@오
```

`1 == 1` → `오`(true)

```junlang
오오ㅁ오오오
```

`2 < 3` → `오`(true)

```junlang
오오오ㅁ@오오
```

`3 <= 2` → `오?`(false)

::: warning Don't confuse `!@` with variables
Since `!` is also used as a variable name, the following two pieces of code have completely different meanings.

| Code | Interpretation |
| --- | --- |
| `!@오오` | `variable 1 == 2` |
| `!!@오오` | `variable 1 != 2` |

For detailed rules, see [Inline Writing Rules](./parsing-rules).
:::


## Unary Operators

Operators that require only one operand.

| Junlang | Meaning | Common Equivalent |
| --- | --- | --- |
| `?` | Negative sign | `-` |
| `!` | Logical NOT | `!` |

```junlang
?오오
```

`-2`

```junlang
!오
```

`not 1` → `오?`(0)

```junlang
!오?
```

`not 0` → `오`(1)

::: warning The dual meaning of `!`
`!` is both a unary operator and a **variable name**. The interpreter distinguishes between them based on context.

| Code | Interpretation |
| --- | --- |
| `!오` | `not 1` (negation operator) |
| `!~오` | `variable 1 + 1` (variable name) |

If `!` is followed by a *value*, it's the negation operator; if followed by an *operator*, it's a variable name.
:::

## Operator Precedence

When an expression contains multiple operators, those with higher precedence are computed first.

| Rank | Operator | Description |
| --- | --- | --- |
| 1 (highest) | `?`, `!` | Unary operators |
| 2 | `..` | Exponentiation |
| 3 | `.`, `#` | Multiplication, Division |
| 4 | `~` | Addition |
| 5 (lowest) | `@`, `!@`, `ㅁ`, `ㅊ`, `ㅁ@`, `ㅊ@` | Comparison operators |

Operators with the same precedence are evaluated **left to right** (left-associative).

```junlang
오~오오.오오오
```

`1 + 2 × 3` → `1 + 6` → **`오오오오오오오`(7)**

```junlang
오오오#오오#오오
```

`(3 ÷ 2) ÷ 2` → **`0.75`**

::: details Step-by-step walkthrough
**`오~오오.오오오`** (`1 + 2 × 3`)

1. Multiplication has higher precedence than addition, so it's computed first.
   - `오오.오오오` → `2 × 3` → `오오오오오오`(6)
2. Then addition.
   - `오~오오오오오오` → `1 + 6` → `오오오오오오오`(7)

**`오오..오오오.오오`** (`2³ × 2`)

1. Exponentiation has higher precedence than multiplication, so it's computed first.
   - `오오..오오오` → `2³` → `오오오오오오오오`(8)
2. Then multiplication.
   - `오오오오오오오오.오오` → `8 × 2` → `오 오오오오오`(16)
:::

::: tip
To explicitly change precedence, use [준, 서](./jun-seo).
:::

## Assignment Operator

Used to store the result of an expression in a variable.

```junlang
[expression]~준서[variable name]ㅋ
```

This operator assigns `[expression]` to `[variable name]`.

::: warning `ㅋ` is not part of the assignment operator
`ㅋ` acts as a terminator at the end of a statement. (Or as a [decimal point](./number-system#decimals))
See [Statements and Blocks](./statements) for details.
:::

```junlang
오~준서!ㅋ
```

Assigns `오`(1) to variable 1 (`!`).

```junlang
오오오~준서!!ㅋ
```

Assigns `오오오`(3) to variable 2 (`!!`).

```junlang
오~오오~준서!!!ㅋ
```

Assigns `오~오오`(1 + 2 = 3) to variable 3 (`!!!`). The expression is evaluated first, then the result is stored in the variable.

::: tip
For more on variables, see [Variables](./variables).
:::

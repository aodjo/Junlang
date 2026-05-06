---
description: Junlang uses '오' to represent numbers.
---

# Number System

Junlang does not use Arabic numerals. Instead, it represents all numbers using the count of the character `오` along with a few auxiliary symbols.

## Single-Digit Numbers

The number of `오` characters represents the digit.

| Junlang | Value&nbsp;&nbsp;&nbsp;&nbsp; |
| --- | --- |
| `오` | 1 |
| `오오` | 2 |
| `오오오` | 3 |
| `오오오오` | 4 |
| `오오오오오` | 5 |
| `오오오오오오` | 6 |
| `오오오오오오오` | 7 |
| `오오오오오오오오` | 8 |
| `오오오오오오오오오` | 9 |

::: warning Digit Limit
If 10 or more `오` characters appear in a row, the following error occurs. See [Error Messages](./error) for details.

```text
오 10개? 좀 진정해
```

Numbers 10 and above must use [multi-digit notation](#multi-digit-numbers).
:::

## Zero (0)

`0` is represented as `오?`.

```junlang
오?
```

| Junlang | Value&nbsp;&nbsp;&nbsp;&nbsp; |
| --- | --- |
| `오?` | 0 |

## Negative Numbers

The negative sign is expressed by prefixing the number with `?`.


```junlang
?[number]
```

| Junlang | Value&nbsp;&nbsp;&nbsp;&nbsp; |
| --- | --- |
| `?오` | -1 |
| `?오오` | -2 |
| `?오오오` | -3 |
| `?오오오오오오오오` | -8 |

:::info
For more details, see [Operators - Unary Operators](./operators#unary-operators).
:::

## Multi-Digit Numbers

Numbers 10 and above are written by separating digit groups with **spaces**. Each group represents a single digit (0–9), listed in decimal place order from left to right.

For example, breaking down `오오 오?` gives `2 0`. Since Junlang separates digits with spaces, this becomes `20`.


| Junlang | Value | Breakdown |
| --- | --- | --- |
| `오 오?` | 10 | 1, 0 |
| `오오 오?` | 20 | 2, 0 |
| `오오오 오?` | 30 | 3, 0 |
| `오 오? 오?` | 100 | 1, 0, 0 |
| `오오 오오오` | 23 | 2, 3 |
| `오오오오오 오오오오오오오` | 57 | 5, 7 |

Negative numbers work the same way — just prefix with `?`.

| Junlang | Value |
| --- | --- |
| `?오 오?` | -10 |
| `?오오오 오?` | -30 |

## Decimals

The integer part and the decimal part are separated by `ㅋ`.

```junlang
[integer part]ㅋ[decimal part]
```

| Junlang | Value |
| --- | --- |
| `오ㅋ오오오오오` | 1.5 |
| `오?ㅋ오오` | 0.2 |
| `오오오 오?ㅋ오오오` | 30.3 |
| `?오오오 오?ㅋ오오오` | -30.3 |
| `오오오 오ㅋ오오 오오오오오 오오오오오오` | 31.256 |

The decimal part uses the same space-separated digit groups as the integer part. In the last example above, `31.256` breaks down as follows:

- Integer part: `오오오 오` → `3`, `1` → 31
- Decimal point: `ㅋ` → .
- Decimal part: `오오 오오오오오 오오오오오오` → `2`, `5`, `6` → 256

:::warning &nbsp;Note
&nbsp;`ㅋ` is also used in Junlang to terminate statements. See [Statements and Blocks](./statements) for details.
:::

## Summary

| Symbol | Role |
| --- | --- |
| `오` | Represents 1; the count determines the single digit value |
| `오?` | 0 |
| `?` | Negative sign (at the very front of the number) |
| `ㅋ` | Separates integer and decimal parts |
| (space) | Separates digit groups |

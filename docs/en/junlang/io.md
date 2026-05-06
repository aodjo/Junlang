---
description: Covers how to read values from standard input and write values to standard output in Junlang.
---

# Input / Output
This document covers input and output.

## Input

Reads one line from standard input to get a number.

```junlang
오ㅋ준ㅋ서ㅋ
```

### Input Notation

Input follows the [number system](./number-system).

| Input | Value |
| --- | --- |
| `오오 오?` | 20 |
| `?오오오` | -3 |
| `오ㅋ오오오오오` | 1.5 |

### Examples

```junlang
오ㅋ준ㅋ서ㅋ~준서!ㅋ
```
Assigns the input value to `!`.

```junlang
오준서오ㅋ준ㅋ서ㅋㅋ
```
Outputs the input value as-is.

```junlang
오준서오ㅋ준ㅋ서ㅋ~오ㅋ
```
Adds `오`(1) to the input value and outputs it.

## Output

```junlang
오준서[expression]ㅋ
```

### Examples

```junlang
오준서오~오ㅋ
```
Outputs the value of `오`(1) + `오`(1). (Output: `오오`)

```junlang
오준서!ㅋ
```
Outputs the value of `!`.

```junlang
오준서오오오#오오ㅋ
```
Outputs the value of `오오오`(3) divided by `오오`(2).

### Output Format

Output values are converted to Junlang number notation.

- **Integers** are output in integer notation.
- **Finite decimals**: trailing `오?`(0) in the last digit is omitted. (e.g., `오ㅋ오오 오?` → `오ㅋ오오`)
- **Infinite decimals** or **values exceeding 8 decimal places** are rounded to 8 decimal places.

| Code | Output |
| --- | --- |
| `오준서오 오?#오오ㅋ` | `오오오오오` |
| `오준서오오오#오오ㅋ` | `오ㅋ오오오오오` |
| `오준서오오 오?#오 오? 오?ㅋ` | `오?ㅋ오오` |

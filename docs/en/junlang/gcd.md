---
description: A Junlang example that computes the GCD of two positive integers using the subtraction-based Euclidean algorithm.
---

# Euclidean Algorithm

The [Euclidean algorithm](https://en.wikipedia.org/wiki/Euclidean_algorithm) is a classic algorithm for finding the greatest common divisor (GCD) of two positive integers. This example uses a variant that repeatedly subtracts the smaller number from the larger until the two are equal.

This variant relies on the following properties:

- `gcd(a, b) = gcd(a - b, b)` (when `a > b`)
- `gcd(a, a) = a`

## Code

```junlang
오ㅋ준ㅋ서ㅋ~준서!ㅋ
오ㅋ준ㅋ서ㅋ~준서!!ㅋ

준서야 !!@!! 또처먹냐?
  준서야 !ㅊ!! 맞냐?
    !~?!!~준서!ㅋ
  ㅋ 아니냐?
    !!~?!~준서!!ㅋ
  ㅋ
ㅋ

오준서!ㅋ
```

## Variables

| Variable | Meaning |
| --- | --- |
| `!` | First number (a) |
| `!!` | Second number (b) |

## How It Works

1. Read two positive integers `a` and `b` from input and store them in `!` and `!!`.
2. While `a` and `b` are not equal, repeat:
   - If `a > b`, assign `a - b` to `a`.
   - Otherwise, assign `b - a` to `b`.
3. When the loop ends, both variables hold the same value, which is the GCD. Output `!`.

## Execution Example

| Input (a, b) | Output | Meaning |
| --- | --- | --- |
| `오 오오`, `오 오오오오오오오오` | `오오오오오오` | gcd(12, 18) = 6 |
| `오 오오오오오`, `오 오?` | `오오오오오` | gcd(15, 10) = 5 |
| `오오오오오오오`, `오 오오오` | `오` | gcd(7, 13) = 1 |

::: warning
- This algorithm only works with **positive integers**. Inputting 0 or a negative number may cause the loop to never terminate.<br />
- The subtraction-based variant used here has a number of iterations proportional to the difference between the two numbers. Cases like `gcd(1, 1000000)` where one number is very large can be extremely slow.
:::

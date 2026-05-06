---
description: A Junlang example that takes N as input and prints the first N terms of the Fibonacci sequence.
---

# Fibonacci Sequence

The [Fibonacci sequence](https://en.wikipedia.org/wiki/Fibonacci_sequence) is a sequence where the first two terms are 1, and each subsequent term is the sum of the two preceding terms.

```text
1, 1, 2, 3, 5, 8, 13, 21, 34, 55, ...
```

This example reads N from standard input and prints the first N terms, one per line.

## Code

```junlang
오ㅋ준ㅋ서ㅋ~준서!!!!!ㅋ
오?~준서!ㅋ
오~준서!!ㅋ
오?~준서!!!!ㅋ

준서야 !!!!ㅁ!!!!! 또처먹냐?
  오준서!!ㅋ
  !~!!~준서!!!ㅋ
  !!~준서!ㅋ
  !!!~준서!!ㅋ
  !!!!~오~준서!!!!ㅋ
ㅋ
```

## Variables

| Variable | Meaning |
| --- | --- |
| `!` | Previous term (a) |
| `!!` | Current term (b) |
| `!!!` | Temporary value for computing the next term (c) |
| `!!!!` | Loop counter (i) |
| `!!!!!` | Number of terms to print (N) |

## How It Works

1. Read N from input and store it in `!!!!!`.
2. Initialize `a` to 0 and `b` to 1.
3. While `i` is less than N, repeat:
   - Print the current value `b`.
   - Compute `c = a + b`.
   - Shift one step: assign `b` to `a` and `c` to `b`.
   - Increment `i` by 1.

## Execution Example

If the input is `오오오오오오오` (7), the output is:

```text
오
오
오오
오오오
오오오오오
오오오오오오오오
오 오오오
```

Each line represents 1, 1, 2, 3, 5, 8, and 13 respectively.

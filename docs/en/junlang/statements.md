---
description: In Junlang, ㅋ marks both the end of a statement and the end of a block.
---

# Statements and Blocks

In Junlang, `ㅋ` marks both **the end of a statement** and **the end of a block**.

- Every regular statement ends with `ㅋ`.
- Blocks are also closed with `ㅋ`.

## Regular Statements

```junlang
오ㅋ
```

A statement consisting of the single expression `오`(1).

## Blocks

Conditionals, loops, and similar constructs use blocks. A block groups multiple statements together and is closed with `ㅋ`.

```junlang
준서야 오 맞냐?
  오준서오ㅋ
ㅋ
```

This code consists of two parts:

1. Statement inside the block: `오준서오ㅋ` — a print statement ending with `ㅋ`
2. Block close: the `ㅋ` on the following line

## Inline Writing

When a statement-ending `ㅋ` and a block-closing `ㅋ` appear consecutively, they merge into `ㅋㅋ`.

```junlang
준서야오맞냐?오준서오ㅋㅋ
```

The code above has **exactly the same meaning** as the previous example. Line breaks and indentation are purely for readability and have no effect on behavior.

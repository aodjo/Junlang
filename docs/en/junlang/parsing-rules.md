---
description: Covers the rules for how ! and @ are interpreted together in Junlang.
---

# Inline Writing Rules

In Junlang, `!` is both **part of a variable name** and a **logical NOT operator**, `@` is the **equality comparison operator**, and `!@` is the **inequality comparison operator**. These symbols frequently end up adjacent to each other, which can look ambiguous.

This document explains how Junlang resolves such ambiguities.

::: details Why are these rules needed?
The same symbol `!` serves two roles:

- **Variable name**: Variables are identified by the number of `!` characters (`!` is variable 1, `!!` is variable 2, ...)
- **Logical NOT operator**: `!오` means `not 1`

Because these two meanings can clash in the same code, the interpreter needs consistent rules to distinguish them.
:::

## Core Rules

Junlang interprets tokens with the following priority:

1. **If `!@` appears together, it is first treated as the "not equal" operator.**
2. **If there are `!` characters before it, they are part of the variable name.**
3. **When you want to compare a variable itself with `@` (equality), wrap it with `준` and `서`.**

Let's look at each rule with examples.

## `!@[expression]`

```junlang
!@오오
```

This code is interpreted as:

```text
! @ 오오
variable 1 == 2
```

Although `!@` appear together, they are **not** treated as the "not equal" operator here. Instead, it decomposes into `!`(variable 1) + `@`(equality) + `오오`(2).

::: warning
Counter-intuitively, `!@오오` is **not** `variable 1 != 2`. To get "not equal", see the next rule.
:::

---

## `!!@[expression]`

```junlang
!!@오오
```

This code is interpreted as:

```text
! !@ 오오
variable 1 != 2
```

With two `!` characters, the first `!` becomes the variable name and the trailing `!@` becomes the "not equal" operator.


## How to Resolve Ambiguity

```junlang
!!@오오
```

We've seen that the code above is interpreted as `variable 1 != 2`. But what if you want to check whether **variable 2 equals 2**?

Writing `!!@오오` as-is gives `variable 1 != 2`, so you can't express that intent. This is where `준` and `서` come in to clarify your intention.

```junlang
준!!서@오오
```

This code is interpreted as:

```text
variable 2 == 2
```

::: tip
`준` and `서` are also used to change expression precedence in general. See [준, 서](./jun-seo) for details.
:::

## Summary

| Code | Interpretation |
| --- | --- |
| `!@오오` | `variable 1 == 2` |
| `!!@오오` | `variable 1 != 2` |
| `!!!@오오` | `variable 2 != 2` |
| `준!서@오오` | `variable 1 == 2` *(explicit with 준, 서)* |
| `준!!서@오오` | `variable 2 == 2` *(explicit with 준, 서)* |
| `준!!서!@오오` | `variable 2 != 2` *(explicit with 준, 서)* |

::: warning When in doubt, use `준` and `서`
If the meaning isn't clear at a glance, wrap the variable scope with `준` and `서`. The code may get longer, but the intent becomes crystal clear.
:::

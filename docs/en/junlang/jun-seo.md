---
description: 준 and 서 in Junlang are used to specify the scope and precedence of expressions.
---

# 준, 서

`준` and `서` are used to group the scope of an expression. They function the same as an opening/closing pair of parentheses in conventional programming languages.

| Junlang | Common Equivalent |
| --- | --- |
| `준` | `(` |
| `서` | `)` |

## Changing Precedence

Use them when you want to override operator precedence and compute a specific part first.

```junlang
준오~오서.오오
```

`(1 + 1) × 2` → **`오오오오`(4)**

Without `준` and `서`, multiplication would have been computed first, resulting in `1 + (1 × 2) = 3`. By wrapping with `준` and `서`, the addition is computed first.

```junlang
준준오~오서.오오오서.오오
```

`((1 + 1) × 3) × 2` → **`오 오오`(12)**

`준` and `서` can be nested.

## Clarifying Variable Scope

When `!` (variable name) is followed by `@` (equality check), the interpreter parses according to the [inline writing rules](./parsing-rules). When the intent becomes ambiguous, you can use `준` and `서` to clearly delimit the variable's scope.

```junlang
준!!서@오오
```

This is interpreted as `variable 2 == 2`. See [Inline Writing Rules](./parsing-rules) for more details.

::: tip
`준` and `서` must always be used in pairs. Using only one will cause a syntax error.
:::

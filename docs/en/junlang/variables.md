---
description: Variables in Junlang are distinguished by the number of ! characters.
---

# Variables

A variable is a label for storing a value so you can retrieve it later. In Junlang, you don't give variables names — instead, variables are distinguished by the **number of `!` characters**.

::: details What is a variable?
A **variable** is a name attached to a value. For example, if you compute `1 + 2` and store the result somewhere, you don't have to recompute it every time you need it later. A variable is the box that holds that value.

Most languages give variables names like `x`, `name`, or `total`, but Junlang doesn't.
:::

## Variable Names

In Junlang, a variable's name is determined by the **count** of `!` characters.

| Junlang | Meaning |
| --- | --- |
| `!` | Variable 1 |
| `!!` | Variable 2 |
| `!!!` | Variable 3 |
| `!!!!` | Variable 4 |

The more `!` characters, the different the variable. In other words, `!` and `!!` are completely separate variables.

## Variable Reference

To retrieve a stored value, simply use the variable name in an expression.

```junlang
오준서!ㅋ
```

Outputs the value of variable 1 (`!`).

```junlang
오~준서!ㅋ
```

An expression that adds `1` to the value of variable 1. If variable 1 holds `2`, the result of this expression is `3`.

::: tip
How to output a variable's value is covered in [Input / Output](./io).<br />
How to store a value in a variable (assignment) is covered in [Expressions and Operators - Assignment Operator](./operators#assignment-operator).
:::

## Undefined Variables

Referencing a variable that has never been assigned a value produces the following error:

```text
그 준서는 누구야?
```

Therefore, you must assign a value to a variable before using it.

```junlang
오~준서!ㅋ
오준서!ㅋ
```

This code first assigns `오`(1) to variable 1, then outputs its value. The result is `오`(1).

::: warning The dual role of `!`
`!` serves as both a variable name and a [logical NOT operator](./operators#unary-operators). The interpreter distinguishes between the two based on context.

| Code | Interpretation |
| --- | --- |
| `!오` | `not 1` (negation operator) |
| `!~오` | `variable 1 + 1` (variable name) |

For detailed rules, see [Inline Writing Rules](./parsing-rules).
:::

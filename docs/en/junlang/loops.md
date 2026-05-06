---
description: Loops in Junlang repeatedly execute a block as long as the condition is true.
---

# Loops

A loop repeatedly executes the statements inside its block as long as the condition is true.

```junlang
준서야 [condition] 또처먹냐?
  [statements]
ㅋ
```

When the condition becomes false, the loop stops and execution continues with the statement after the block.

::: tip
For how conditions are evaluated as true/false, see [True and False](./boolen).
:::

## Example

Code that prints from 1 to 10:

```junlang
오 오?~준서!ㅋ
준서야 ! 또처먹냐?
  오준서!ㅋ
  !~?오~준서!ㅋ
ㅋ
```

This code works in the following steps:

1. Assign `오 오?`(10) to variable 1.
2. If variable 1 is not 0 (= truthy), execute the block.
   1. Print the value of variable 1.
   2. Add `?오`(-1) to variable 1 and store the result back. (= decrement by 1)
3. When variable 1 reaches 0, the loop stops.

As a result, `10, 9, 8, ..., 1` is printed.

::: warning Watch out for infinite loops
If the condition stays true forever, the loop never ends. You must change the value (usually a variable) that affects the condition inside the block.

```junlang
준서야 오 또처먹냐?
  오준서오ㅋ
ㅋ
```

The above code never stops because the condition is always `오`(1).
:::

## Breaking Out Mid-Loop

Inside a loop, you can use the [flow control](./flow-control) keywords `더처먹어`(continue) and `그만처먹어`(break) to change the flow of iteration.

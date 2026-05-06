---
description: A list of error messages that Junlang outputs and their meanings.
---

# Error Messages

When something goes wrong during execution, Junlang outputs a predefined error message.

## Error Message Format

Errors are output in the following format:

```text
[line]:[column]: [error message]
```

- **line**: The line number where the error occurred
- **column**: The character position on that line where the error occurred
- **error message**: One of the predefined messages

### Example

```text
3:5: 이건 기하반도 안 하는 실수인데?
```

This means a division by zero occurred at line 3, column 5.

## Error List

| Situation | Error Message |
| --- | --- |
| Cannot find or read the file to execute | `아 먹을거 어디있어` |
| Invalid input notation or incorrect syntax | `어! 이건 오~준서! 도 안 할 실수인데?` |
| No input received from the input function | `준서가 먹을거 없어서 슬프대` |
| Division by zero | `이건 기하반도 안 하는 실수인데?` |
| Exponent is a decimal and cannot be processed | `준서가 어렵대` |
| Digit overflow (10 or more consecutive `오`) | `오 10개? 좀 진정해` |
| Missing `ㅋ` terminator | `준서야 그만 처 먹어` |
| Reference to an undefined variable | `그 준서는 누구야?` |
| `더처먹어` / `그만처먹어` used outside a loop | `먹지도 않았는데 뭘 그만 처 먹어?` |
| `ㅋ` with no block to close | `넌 이 상황이 처 웃겨?` |

::: tip
The "cannot find or read file" error is output without line/column information.
:::

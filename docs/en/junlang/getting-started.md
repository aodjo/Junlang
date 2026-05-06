---
description: A guide to getting started with Junlang.
---

# Getting Started

This document walks you through installing Junlang and running your first code.

## Download

Download the executable for your operating system from the [GitHub Releases](https://github.com/aodjo/junlang/releases) page.

| OS | File |
| --- | --- |
| Windows | `junlang.exe` |
| Linux | `junlang` |

## Running

You can run `.junl` files using the downloaded executable.

::: code-group

```powershell [Windows]
.\junlang.exe hello.junl
```

```bash [Linux / macOS]
chmod +x junlang
./junlang hello.junl
```

:::

::: tip If typing the path every time is a hassle, consider adding the executable to your system's PATH.
:::

## File Extension

The file extension for Junlang source code files is **`.junl`**.

```bash
./junlang hello.junl   # O
./junlang hello.txt    # X
```

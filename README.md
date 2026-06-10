# Pure.Primitives.Switches

Switch-based conditional primitives for the **Pure** ecosystem — evaluate one of N branches based on a discriminant value.

[![.NET build & test](https://github.com/kudima03/Pure.Primitives.Switches/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Primitives.Switches/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Primitives.Switches/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Primitives.Switches/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Primitives.Switches)](https://www.nuget.org/packages/Pure.Primitives.Switches)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Primitives.Switches` provides a `*Switch` type for every primitive in the Pure ecosystem. Each switch type holds a discriminant value and a collection of `(predicate, result)` branches, evaluating the first branch whose predicate matches and returning the associated result.

## Types

| Type | Implements | Discriminant |
|------|-----------|--------------|
| `BoolSwitch` | `IBool` | `IBool` |
| `CharSwitch` | `IChar` | `IChar` |
| `StringSwitch` | `IString` | `IString` |
| `NumberSwitch<T>` | `INumber<T>` | `INumber<T>` |
| `GuidSwitch` | `IGuid` | `IGuid` |
| `DateSwitch` | `IDate` | `IDate` |
| `TimeSwitch` | `ITime` | `ITime` |
| `DateTimeSwitch` | `IDateTime` | `IDateTime` |
| `DayOfWeekSwitch` | `IDayOfWeek` | `IDayOfWeek` |

## Design Principles

- **Lazy evaluation** — branches are evaluated only when the value property is accessed.
- **Composable** — switch results are Pure primitives and can be nested inside other operations.
- **AOT-compatible** — no reflection; fully compatible with Native AOT.

## Dependencies

- [`Pure.Primitives.Abstractions`](https://github.com/kudima03/Pure.Primitives.Abstractions) — Pure primitive interfaces

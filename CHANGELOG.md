# Changelog

All notable changes to Pure.Primitives.Switches are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [1.1.3] — 2026-06-25

- Maintenance release: dependency and build updates.

## [1.1.2] — 2026-05-20

- Maintenance release: dependency and build updates.

## [1.1.1] — 2026-05-07

- Maintenance release: dependency and build updates.

## [1.1.0] — 2025-12-10

### Changed

- Target frameworks expanded from `net9.0` only to `net7.0`, `net8.0`,
  `net9.0`, and `net10.0`.

## [1.0.0] — 2025-11-01

### Changed

- **Breaking:** the hash-based switch types (`BoolSwitch`, `CharSwitch`,
  `DateSwitch`, `DateTimeSwitch`, `DayOfWeekSwitch`, `GuidSwitch`,
  `NumberSwitch`, `StringSwitch`, `TimeSwitch`) now take their
  `Func<TSelector, IDeterminedHash>` hash-factory delegate using
  `IDeterminedHash` from `Pure.HashCodes.Abstractions` instead of
  `Pure.HashCodes`. Callers must update the hash-factory implementations
  they pass in to use the new package/namespace.

### Added

- Trimming and NativeAOT compatibility analyzers enabled for the package.

## [0.1.0] — 2025-09-02

Initial release.

### Added

- Hash-based switch/case primitives that select a value by matching a
  selector against branch keys via a pluggable hash function, with an
  optional default branch: `BoolSwitch<TSelector>`,
  `CharSwitch<TSelector>`, `DateSwitch<TSelector>`,
  `DateTimeSwitch<TSelector>`, `DayOfWeekSwitch<TSelector>`,
  `GuidSwitch<TSelector>`, `NumberSwitch<TSelector, TNumber>`,
  `StringSwitch<TSelector>`, `TimeSwitch<TSelector>`.

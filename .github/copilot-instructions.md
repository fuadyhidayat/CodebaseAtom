# Repository agent rules (template codebase)

This repository is a **layered .NET boilerplate**. Domain content in the checkout is sample data for patterns, not the long-term product model.

This file is the **single always-on hub** for agent instructions (GitHub Copilot and other agents working in this repo). Task skills live under `.github/skills/`.

## Architecture (short)

```
05.WebUI → 04.Logics → 02.Services → 01.Domain
                ↘           ↑
           03.Infrastructure implements Services
```

Use cases are **`ILogic<TInput, TOutput>`** under `src/04.Logics` (not MediatR).

## Namespace convention

Layer roots follow:

```text
{Company}.{Application}.{Layer}
```

(`Domain` | `Services` | `Infrastructure` | `Logics` | `WebUI`)

Resolve `{Company}.{Application}` from each layer project’s `RootNamespace` / `AssemblyName` in `*.csproj`. Operation namespaces look like:

```text
{Company}.{Application}.Logics.{Feature}.{Operation}
```

Project skills document these patterns with placeholders only—do not hardcode product names into skill usage.

## Implementation habit

1. Load the relevant `codebase-*` skill from `.github/skills/<name>/SKILL.md` (pattern + placeholders).
2. Resolve namespaces from `*.csproj`.
3. Mirror an existing sibling of the same kind in the checkout for local style.
4. Prefer `dotnet build` on the solution file at the repo root (`*.slnx` / `*.sln`) after structural changes.
5. Expect strict builds (`TreatWarningsAsErrors` / analyzers via `Directory.Build.props` when enabled)—leave no warnings.
6. Style authority is **`.editorconfig`**. If build/IDE fails on style (`IDE*`, `CA*`, formatting), load `codebase-codestyle` and fix until `dotnet build` is green.

## Hard rules

1. Do not introduce MediatR, AutoMapper, or generic repositories unless the user explicitly redesigns the template.
2. WebUI must call `ILogic<,>`, not `DbContext` / `DatabaseService` directly.
3. Interfaces live in `02.Services`; implementations in `03.Infrastructure` (WebUI may host a few host-specific implementations such as current user).
4. Mirror an existing sibling feature before inventing new structure.
5. Skills under `.github/skills/codebase-*` are pattern docs—not a catalog of this product’s domain names.
6. Soft-delete is **mixed**: entities expose `IsDeleted`; queries often filter it; deletes may hard-remove or soft-flag—**mirror the sibling operation of the same kind**.
7. UI labels: prefer `DomainDisplayTextFor` / `UIDisplayTextFor` unless localization is explicitly re-enabled.
8. Logic projections: prefer `*Dto` records unless a sibling uses another name.
9. Do not invent infrastructure capabilities (e.g. background jobs, realtime) that are not in the checkout unless the user asks.
10. C# formatting/naming must match `.editorconfig` (always braces, Allman, prefer `var`, file-scoped namespaces, discards)—see `codebase-codestyle`.

## Project skills

Tutorial pemakaian (decision tree, case A–E, prompt ID/EN, keywords): [docs/codebase-skills-guide.md](../docs/codebase-skills-guide.md).

Load the matching skill from `.github/skills/<name>/SKILL.md` before implementing changes. Copilot discovers project skills under **`.github/skills/`** automatically. Skills are project-local and self-contained.

| Skill | When to load |
|-------|----------------|
| `.github/skills/codebase-overview/SKILL.md` | Architecture, onboarding, “how is this structured”, `/codebase-overview` |
| `.github/skills/codebase-domain/SKILL.md` | Entity, EF configuration, DbSet, migration, seed, `/codebase-domain` |
| `.github/skills/codebase-infra/SKILL.md` | New service interface + infrastructure provider/DI, `/codebase-infra` |
| `.github/skills/codebase-logic/SKILL.md` | Input/Output/Logic use cases, `/codebase-logic` |
| `.github/skills/codebase-webui/SKILL.md` | Blazor pages, routes, ILogic wiring, `/codebase-webui` |
| `.github/skills/codebase-mudblazor/SKILL.md` | MudBlazor + Common UI wrappers (table, form, dialog, theme), `/codebase-mudblazor` |
| `.github/skills/codebase-feature/SKILL.md` | Full vertical slice end-to-end, `/codebase-feature` |
| `.github/skills/codebase-codestyle/SKILL.md` | Code style, naming, braces/Allman, IDE/CA style fix-loop (follows `.editorconfig`), `/codebase-codestyle` |

# Commit message custom instructions for GitHub Copilot

The first line of the commit message should be a short summary of the changes made, ideally under 50 characters.
The second line should be left blank
The third line should provide a more detailed description of the changes, including any relevant context or background information. Use the bullet points to highlight key changes or features.
The line before the footer should be left blank.
The last line is the footer that should look like this: "AI-Assisted Generated Commit Message"

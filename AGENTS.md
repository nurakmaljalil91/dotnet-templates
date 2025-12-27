# AGENTS.md

## Purpose
This repository contains a .NET template that helps create Clean Architecture
ASP.NET Core Web API projects. Use the README as the source of truth for setup
and install instructions.

## Setup prerequisites
- .NET 10.0 SDK (latest)
- Node.js (latest LTS) only if using Angular or React

## Template install and verify
```bash
dotnet new install .
dotnet new -l
dotnet new --list
```

## Uninstall
```bash
dotnet new uninstall .
```

## Notes for agents
- Prefer updating `README.md` when changing user-facing install instructions.
- Keep commands in sync with the .NET SDK version referenced in `README.md`.

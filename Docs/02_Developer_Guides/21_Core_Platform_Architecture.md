# Core Platform Architecture

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Process Topology

```text
Client Mod/LCD: input, page runtime, widgets, cache, network client
                         │
                         ▼
Torch/Server: sessions, command bus, authority, domains, projections,
alarms, procedures, rules, persistence, audit, game/integration adapters
```

## Threading

All game-entity access is scheduled onto the correct game/server thread. Pure validation may run off-thread on immutable snapshots. Never hold a domain lock while waiting for the game thread.

## Identity

Use strongly typed VesselId, EquipmentId, SystemId, CompartmentId, StationId, OperatorId, TrackId, CommandId, and CorrelationId. Space Engineers entity IDs are bindings, not the sole domain identity.

## Command Bus

Authentication, authorisation, schema validation, idempotency, revision checks, logical locking, journaling, bounded execution, verification, reason codes, events, and audit are mandatory.

## Projection Model

Clients consume vessel/department summaries, page projections, entity detail, maps, trends, alarms, procedures, and command result streams. Projections enforce role and sensor/security disclosure.

## Discovery

Equipment discovery is commissioning/reconciliation work, not render-loop scanning. Names and Custom Data may bootstrap mapping but stable validated metadata becomes authoritative.

## Degraded Mode

Mark failed capability degraded/unavailable, retain last values only as stale, disable unsafe commands, create one root-cause alarm, continue independent services, and expose recovery actions.

# Testing and Acceptance Strategy

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Test Layers

Contract compatibility, pure domain, persistence/migration, adapter, server integration, client view model, protocol/replay, representative-vessel acceptance, and live-server soak/performance.

## Representative Vessels

Small ship, subgrid industrial ship, hydrogen combat ship, atmospheric carrier, production base with docked craft, deliberately damaged vessel, and multi-vessel fleet.

## Failure Injection

Power loss, entity removal, split/merge/dock, stale telemetry, disconnect, duplicate request, restart mid-command, storage failure, permission change, missing integration, and conflicting stations.

## Status

Experimental means incomplete or unresolved correctness. Build-candidate means coherent and ready for target build/test. Product-ready requires target compilation, acceptance, and no unresolved known gaps.

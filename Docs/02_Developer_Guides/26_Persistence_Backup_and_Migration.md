# Persistence, Backup, and Migration

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Durable State

Configuration/topology, permissions/stations, procedures/rules/profiles, tasks/orders/requests, maintenance/lockouts, gateway cursors, audit/incidents, optional simulation state, and in-flight journals.

## Save Discipline

Write temporary, flush, validate checksum/schema, atomically replace, and retain previous-good generation. Do not perform large synchronous writes in hot callbacks.

## Startup

Load config, validate schema, load durable state, rebind entities, reconcile in-flight work, report drift, start read-only projections, and enable actuation only after validation.

## Migration/Restore

Migrations are ordered, versioned, idempotent, logged, backed up, and tested. Restore requires maintenance/safe mode and quiesced writers.

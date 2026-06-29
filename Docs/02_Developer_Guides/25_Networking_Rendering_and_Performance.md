# Networking, Rendering, and Performance

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Protocol

Versioned messages include session sequence, vessel/station, type/schema, correlation/subscription ID, payload limits, and replay/rate controls.

## Snapshot/Delta

A page receives snapshot revision N, then ordered deltas. A gap causes resnapshot rather than guessing.

## Rendering

Reusable widgets support clipping, focus, pointer hit testing, disabled reason, pending/result state, density scaling, and colour-independent critical indications. Rendering never queries blocks.

## Performance

Use event-driven topology, staggered inventories, bounded fast state, spatial sensor indexes, server trend aggregation, paginated logs, merged subscriptions, and per-service observability.

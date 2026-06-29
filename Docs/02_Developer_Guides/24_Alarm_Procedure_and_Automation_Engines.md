# Alarm, Procedure, and Automation Engines

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Alarm Engine

Durable root-cause records with severity, debounce, hysteresis, repeat policy, acknowledgement, assignment, suppression, clearance evidence, and correlation.

## Procedure Engine

Immutable published versions contain ordered/parallel steps, owner, automatic command/manual instruction/confirmation/telemetry gate, preconditions, timeout, retry, abort, safe state, evidence, and restart reconciliation.

## Rules Engine

```text
Trigger + Conditions + Cooldown + Scope + Actions + Safe-State Policy
```

Actions are ordinary commands. Rules have owner, quotas, rate limits, simulation, revision, and history.

## Product Rule

Do not hide important automation in timer blocks, arbitrary Custom Data, or page handlers. Import it into explicit rules/procedures.

# Shared Interface, Alarms, Procedures, and Authority

**Product:** CommandShellOS for Space Engineers  
**Document type:** Cross-department user guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Screen Regions

Header, navigation rail, breadcrumbs, primary workspace, context inspector, alarm strip, and command bar.

## Guarded Actions

A guarded action displays target, expected consequence, affected systems, interlocks, required authority, and expiry. A second authorisation must come from a distinct identity and binds to the exact payload.

## Alarm Lifecycle

```text
Active/Unacknowledged → Acknowledged → Assigned → Action in Progress → Cleared → Closed
```

Shelving and inhibition require reason, duration, authority, and visible indication.

## Procedure Lifecycle

```text
Draft → Validated → Published → Starting → Running → Paused/Aborting → Completed/Failed/Aborted
```

Published versions are immutable.

## Operational Objects

An order expresses command intent; a task is tracked work; a request asks another authority; a procedure is ordered execution; an automation rule is bounded event-condition-action behaviour.

## Emergency Override

Emergency override is a separate audited command. It may override ordinary policy but cannot bypass impossible physical conditions or server-integrity safeguards.

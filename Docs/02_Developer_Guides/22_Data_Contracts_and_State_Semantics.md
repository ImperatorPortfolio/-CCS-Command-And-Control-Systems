# Data Contracts and State Semantics

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Command Envelope

CommandId, CorrelationId, VesselId, StationId, OperatorIdentity, command type/version, payload, request time/expiry, expected target revision, and authorisation references.

## Command Result

Accepted/Rejected/Queued/Executing/Verifying/Completed/Partial/Failed/Cancelled/TimedOut/Superseded, stable reason code, operator explanation, affected IDs, final revisions, verification evidence, and generated alarms.

## Telemetry Envelope

Vessel/object ID, schema version, authoritative revision, server timestamp, quality, source, confidence/uncertainty where relevant, and snapshot/delta payload.

## Reason Codes

Examples: Auth.ClaimMissing, Station.NotOwner, Target.RevisionChanged, Interlock.MaintenanceLockout, Interlock.LifeSafety, Resource.InsufficientPower, Telemetry.Stale, Integration.Unsupported, Execution.VerificationFailed.

## Capability Advertisement

The server advertises currently possible commands and disabled reasons. The server still revalidates on submission.

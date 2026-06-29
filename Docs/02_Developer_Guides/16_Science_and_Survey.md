# Science and Survey — Developer Implementation Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department implementation companion  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Ownership Boundary

Science owns survey tasks, observations, analysis, products, and optional experiment state. Sensors owns hardware and Scenarium owns campaign objective completion.

This boundary is mandatory. Presentation does not own state, and adapters do not make policy decisions.

## 2. Required Layers

1. **Contracts:** stable IDs, DTOs, enums, reason codes, events, and versioned payloads.
2. **Domain:** policy, calculations, state machines, invariants, and pure tests.
3. **Server application:** command orchestration, logical locks, audit, projections, and subscriptions.
4. **Adapters:** Space Engineers, Torch, Scenarium, MES, or optional mod/plugin interfaces.
5. **Presentation:** page definitions, view models, pointer hit testing, formatting, and command submission.

The LCD/client side is untrusted. It submits intent and renders authoritative snapshots/deltas.

## 3. Domain Entities

- SurveyTask
- SurveyObservation
- SurveyCoverage
- ResourceEstimate
- AnomalyCase
- Experiment
- ResearchDataset
- ScienceObjectiveLink

Every durable entity needs a stable ID independent of display name, owning VesselId, revision, lifecycle state, source/quality, timestamps, validation, and audit correlation.

## 4. Services

- ISurveyService
- IObservationService
- IResourceAnalysisService
- IAnomalyService
- IExperimentService
- IResearchArchive
- IScienceObjectiveGateway

Services expose interfaces in Contracts or Domain-facing abstractions. They must not expose Torch or game concrete types to UI or pure domain logic.

## 5. Command Surface

- CreateSurveyTask
- ScheduleSurveyScan
- RecordObservation
- PublishSurveyLayer
- CreateAnomalyCase
- StartExperiment
- ArchiveResearchDataset
- SubmitScienceEvidence

Every physical command follows:

```text
Receive → authenticate → authorise → validate target/revision
→ evaluate interlocks → acquire logical lock → journal
→ execute on game thread through adapter → verify actual state
→ publish result/events/alarms → release lock
```

Use a globally unique CommandId for idempotency and a CorrelationId for procedures, events, and audit.

## 6. Page Implementation Matrix

| Page | Server projection | Likely command contracts | Update policy |
| --- | --- | --- | --- |
| Science Dashboard | Science_DashboardProjection | SubmitScienceEvidence | Event-driven alarms; configured normal/fast telemetry |
| Planetary Survey | Planetary_SurveyProjection | CreateSurveyTask, ScheduleSurveyScan, PublishSurveyLayer | Event-driven alarms; configured normal/fast telemetry |
| Asteroid Survey | Asteroid_SurveyProjection | CreateSurveyTask, ScheduleSurveyScan, PublishSurveyLayer | Event-driven alarms; configured normal/fast telemetry |
| Ore and Mineral Analysis | Ore_and_Mineral_AnalysisProjection | Read-only projection or domain-service operation; no direct page-to-adapter command is permitted. | Event-driven alarms; configured normal/fast telemetry |
| Environmental Measurements | Environmental_MeasurementsProjection | Read-only projection or domain-service operation; no direct page-to-adapter command is permitted. | Event-driven alarms; configured normal/fast telemetry |
| Celestial Database | Celestial_DatabaseProjection | Read-only projection or domain-service operation; no direct page-to-adapter command is permitted. | Event-driven alarms; configured normal/fast telemetry |
| Anomaly Investigation | Anomaly_InvestigationProjection | CreateAnomalyCase | Event-driven alarms; configured normal/fast telemetry |
| Scan Planning | Scan_PlanningProjection | ScheduleSurveyScan | Event-driven alarms; configured normal/fast telemetry |
| Sample Inventory | Sample_InventoryProjection | Read-only projection or domain-service operation; no direct page-to-adapter command is permitted. | Event-driven alarms; configured normal/fast telemetry |
| Experiment Queue | Experiment_QueueProjection | StartExperiment | Event-driven alarms; configured normal/fast telemetry |
| Research Archive | Research_ArchiveProjection | ArchiveResearchDataset | Event-driven alarms; configured normal/fast telemetry |
| Mission Science Objectives | Mission_Science_ObjectivesProjection | SubmitScienceEvidence | Event-driven alarms; configured normal/fast telemetry |
| Survey Map Export | Survey_Map_ExportProjection | CreateSurveyTask, ScheduleSurveyScan, PublishSurveyLayer | Event-driven alarms; configured normal/fast telemetry |

A page subscribes to projections; it never scans blocks. Opening a page acquires a scoped subscription, and leaving the page releases it.

## 7. Telemetry Contract

Every snapshot or delta includes VesselId, object/system ID, schema version, authoritative revision, server timestamp, quality, source, and optional confidence/uncertainty. Commanded state and verified actual state are separate fields.

Recommended ceilings: flight critical 10 Hz, combat operational 5 Hz, normal technical 1–2 Hz, background forecast 0.1–0.5 Hz, alarms and transitions event-driven.

## 8. Alarm Model

Implement root-cause alarms for:

- Survey invalid from calibration
- Objective deadline at risk
- Anomaly hazardous
- Experiment unsafe/unpowered
- Research storage unavailable
- Classified data published incorrectly

Each alarm stores severity, source, affected IDs, first/last occurrence, acknowledgement, assignment, suppression reason, clearance evidence, and correlation ID. Avoid flooding one alarm per downstream block.

## 9. Security and Interlocks

- Page visibility is not command permission.
- Authorise server-side using operator, vessel, station, watch role, claims, target, alert condition, remote-session scope, and lockouts.
- Life-safety and maintenance lockouts override routine commands.
- Emergency override is a separate audited command.
- Multi-party approvals bind to the exact payload hash and target revision.
- Adapters execute only a validated domain decision.

## 10. Persistence and Recovery

Persist versioned configuration, active durable domain state, in-flight command/procedure journal, alarms requiring history, audit, and this department's assignments or plans. On startup: validate schema, load state, rebind game entities, mark missing bindings unavailable, reconcile in-flight work, publish a recovery report, then enable actuation.

## 11. Integration Boundaries

- Sensors
- Navigation maps
- Logistics resources
- Scenarium objectives/world state
- Security custody
- Computer storage

Optional integrations advertise capability, version, health, and failure reason. Missing support returns Unsupported or Unavailable rather than a false healthy value. Scenarium owns campaign state; CommandShellOS consumes projections and submits evidence through a gateway. MES remains the encounter/NPC layer.

## 12. Failure Semantics

For every command define preconditions, invariant, timeout, cancellation, partial-success meaning, safe terminal state, compensation where physically valid, verification source, alarm, operator explanation, and restart strategy. Do not claim rollback for irreversible world changes; contain the outcome and provide a forward recovery procedure.

## 13. Test Matrix

| Class | Method | Required result |
| --- | --- | --- |
| Authorisation | Run each command from every authority level and station type | Only explicit claims and valid station context succeed |
| Idempotency | Repeat the same CommandId before, during, and after completion | One physical effect; original result returned |
| Stale data | Stop or delay the owning adapter | UI marks stale/unavailable and unsafe actions block |
| Restart | Restart while queued, executing, and verifying | No duplicate actuation; journal reconciles safely |
| Concurrency | Submit conflicting commands from two stations | Deterministic winner and exact conflict reason |
| Failure injection | Remove power, block, link, inventory, or permission mid-command | Safe terminal state, alarm, audit, and recovery path |
| Audit | Execute and reject representative commands | Actor, station, payload, decision, result, and evidence correlate |

## 14. Implementation Order

1. IDs, enums, DTOs, events, and reason codes.
2. Service interfaces and ownership boundaries.
3. Pure domain state machines and tests.
4. Persistence and restart reconciliation.
5. Read-only adapters and projections.
6. Command adapters and post-action verification.
7. Page definitions with controls disabled until capability is advertised.
8. Alarms, procedures, audit, and failure injection.
9. Representative-vessel acceptance and performance testing.

## 15. Acceptance Criteria

- Published results retain source/time/resolution/confidence
- Science cannot obtain hidden truth
- Scenarium evidence uses gateway
- Revisions are traceable
- Survey yields to operational priority

This section remains Experimental until the criteria are demonstrated. It becomes Build-candidate only after target-environment compilation and restart/concurrency/failure tests.

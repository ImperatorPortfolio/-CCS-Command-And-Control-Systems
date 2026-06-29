# Science and Survey — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Plan and execute planetary, asteroid, environmental, resource, anomaly, and mission-science surveys while preserving provenance, coverage, resolution, and age.

## 2. Typical Roles

- Science Officer
- Survey Officer
- Resource Analyst
- Mission Specialist

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Active surveys and coverage
- Detected resources and confidence
- Environmental data
- Anomalies
- Sensor availability
- Mission science objectives

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Science Dashboard | Summarise survey, anomaly, resource, environment, and objective state. |
| Planetary Survey | Map terrain, sites, structures, and resource indicators with resolution and age. |
| Asteroid Survey | Characterise shape, size, rotation, resources, and hazards. |
| Ore and Mineral Analysis | Estimate composition, quantity band, confidence, and accessibility. |
| Environmental Measurements | Record atmosphere, gravity, temperature model, and hazards with calibration. |
| Celestial Database | Store versioned body and reference data. |
| Anomaly Investigation | Track evidence, hypotheses, assigned scans, and classification. |
| Scan Planning | Allocate sensor geometry, time, power, and emission budget. |
| Sample Inventory | Optionally track physical samples and chain of custody. |
| Experiment Queue | Manage configured experiments without arbitrary hidden commands. |
| Research Archive | Separate raw observations, analysis, revisions, and published results. |
| Mission Science Objectives | Link evidence to mission goals while Scenarium owns completion. |
| Survey Map Export | Publish versioned map layers to Navigation, Logistics, or Fleet. |

## 5. Standard Operating Workflow

1. Define question, area, resolution, and evidence
2. Plan sensor time and constraints
3. Collect calibrated observations
4. Analyse uncertainty and contradiction
5. Publish versioned products

## 6. Alarm Responsibilities

- Survey invalid from calibration
- Objective deadline at risk
- Anomaly hazardous
- Experiment unsafe/unpowered
- Research storage unavailable
- Classified data published incorrectly

Acknowledging an alarm confirms it has been seen; it does not clear the condition. Assign it, investigate the root cause, execute the correct procedure, verify recovery, and close it with evidence.

## 7. Watch Handover

1. Confirm vessel, station, operator, and role.
2. Review critical and unacknowledged alarms.
3. Review active commands, procedures, tasks, lockouts, and automation.
4. State temporary limitations and expected events.
5. Transfer station control positively; do not assume the relieving operator has control.

## 8. Degraded Operation

When telemetry is stale, conflicted, or unavailable, suspend dependent automation, use a verified alternate source where available, notify Operations, and record manual decisions made under uncertainty. Never treat the last displayed value as current merely because it remains on screen.

## 9. Department Interfaces

- Sensors
- Navigation maps
- Logistics resources
- Scenarium objectives/world state
- Security custody
- Computer storage

---

## Science Dashboard

**Purpose**

Summarise survey, anomaly, resource, environment, and objective state.

**What this page contains**

- Summarise survey, anomaly, resource, environment, and objective state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Science Dashboard** from the Science navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Planetary Survey

**Purpose**

Map terrain, sites, structures, and resource indicators with resolution and age.

**What this page contains**

- Map terrain, sites, structures, and resource indicators with resolution and age.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Planetary Survey** from the Planetary navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Asteroid Survey

**Purpose**

Characterise shape, size, rotation, resources, and hazards.

**What this page contains**

- Characterise shape, size, rotation, resources, and hazards.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Asteroid Survey** from the Asteroid navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ore and Mineral Analysis

**Purpose**

Estimate composition, quantity band, confidence, and accessibility.

**What this page contains**

- Estimate composition, quantity band, confidence, and accessibility.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Ore and Mineral Analysis** from the Ore navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Environmental Measurements

**Purpose**

Record atmosphere, gravity, temperature model, and hazards with calibration.

**What this page contains**

- Record atmosphere, gravity, temperature model, and hazards with calibration.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Environmental Measurements** from the Environmental navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Celestial Database

**Purpose**

Store versioned body and reference data.

**What this page contains**

- Store versioned body and reference data.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Celestial Database** from the Celestial navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Anomaly Investigation

**Purpose**

Track evidence, hypotheses, assigned scans, and classification.

**What this page contains**

- Track evidence, hypotheses, assigned scans, and classification.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Anomaly Investigation** from the Anomaly navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Scan Planning

**Purpose**

Allocate sensor geometry, time, power, and emission budget.

**What this page contains**

- Allocate sensor geometry, time, power, and emission budget.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Scan Planning** from the Scan navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sample Inventory

**Purpose**

Optionally track physical samples and chain of custody.

**What this page contains**

- Optionally track physical samples and chain of custody.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Sample Inventory** from the Sample navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Experiment Queue

**Purpose**

Manage configured experiments without arbitrary hidden commands.

**What this page contains**

- Manage configured experiments without arbitrary hidden commands.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Experiment Queue** from the Experiment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Research Archive

**Purpose**

Separate raw observations, analysis, revisions, and published results.

**What this page contains**

- Separate raw observations, analysis, revisions, and published results.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Research Archive** from the Research navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Mission Science Objectives

**Purpose**

Link evidence to mission goals while Scenarium owns completion.

**What this page contains**

- Link evidence to mission goals while Scenarium owns completion.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Mission Science Objectives** from the Mission navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Survey Map Export

**Purpose**

Publish versioned map layers to Navigation, Logistics, or Fleet.

**What this page contains**

- Publish versioned map layers to Navigation, Logistics, or Fleet.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active surveys and coverage
- Detected resources and confidence
- Environmental data

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Survey Map Export** from the Survey navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

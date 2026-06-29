# Sensors — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Create a server-authoritative, uncertain, multi-source contact picture using detection, correlation, classification, confidence, and emission trade-offs.

## 2. Typical Roles

- Sensor Officer
- Radar Operator
- Electronic Support Operator
- Classification Analyst
- Survey Sensor Operator

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Sensor coverage and health
- Contact count and confidence
- Selected track and detections
- Own emissions and signature
- Active search tasks
- Unresolved and stale contacts

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Sensors Dashboard | Summarise detection capability, contacts, emissions, tasks, and faults. |
| Contact List | List correlated tracks with bearing, range estimate, velocity, class, confidence, and age. |
| Sensor Fusion | Explain contributing detections, residuals, duplicates, and conflicts. |
| Passive EM | Detect antennas, radar, communications, and active emitters without range certainty. |
| Active Radar | Search defined volumes while accepting emission and power cost. |
| Optical Tracking | Use camera line of sight for angular tracks and visual evidence. |
| Thermal Sensors | Detect server-modelled engine, reactor, weapon, and environmental heat. |
| Lidar and Rangefinding | Provide precise short-range geometry for docking and inspection. |
| Mass and Gravity Detection | Optionally provide coarse large-object anomaly detection. |
| Long-Range Search | Perform slow, sensitive sector sweeps. |
| Planetary Survey | Map terrain, sites, and surface activity with age and resolution. |
| Track Correlation | Merge, split, or retain contradictory candidate tracks. |
| Classification | Assess object type, allegiance, and capability from evidence. |
| Signature Management | Show own EM, thermal, optical, communications, weapons, and thrust signature. |
| Emission Control | Enforce passive, restricted, tactical, or unrestricted emission posture. |
| Search Patterns | Create reusable search volumes and schedules. |
| Calibration | Track sensor bias, drift, alignment, and test results. |
| Sensor History | Replay detection and track evolution. |

## 5. Standard Operating Workflow

1. Set emission posture and verify health
2. Create search tasks
3. Correlate detections while preserving uncertainty
4. Classify and publish tracks
5. Downgrade or drop stale tracks

## 6. Alarm Responsibilities

- Critical sensor offline
- Unexpected active emission
- Target confidence collapse
- Source conflict
- Calibration drift
- Contact enters protected volume
- Data stale for autopilot/fire control

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

- Torch entity queries
- Cameras/antennas/sensors
- Tactical
- Navigation
- Communications datalink
- Signature sources

---

## Sensors Dashboard

**Purpose**

Summarise detection capability, contacts, emissions, tasks, and faults.

**What this page contains**

- Summarise detection capability, contacts, emissions, tasks, and faults.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Sensors Dashboard** from the Sensors navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Contact List

**Purpose**

List correlated tracks with bearing, range estimate, velocity, class, confidence, and age.

**What this page contains**

- List correlated tracks with bearing, range estimate, velocity, class, confidence, and age.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Contact List** from the Contact navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sensor Fusion

**Purpose**

Explain contributing detections, residuals, duplicates, and conflicts.

**What this page contains**

- Explain contributing detections, residuals, duplicates, and conflicts.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Sensor Fusion** from the Sensor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Passive EM

**Purpose**

Detect antennas, radar, communications, and active emitters without range certainty.

**What this page contains**

- Detect antennas, radar, communications, and active emitters without range certainty.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Passive EM** from the Passive navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Active Radar

**Purpose**

Search defined volumes while accepting emission and power cost.

**What this page contains**

- Search defined volumes while accepting emission and power cost.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Active Radar** from the Active navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Optical Tracking

**Purpose**

Use camera line of sight for angular tracks and visual evidence.

**What this page contains**

- Use camera line of sight for angular tracks and visual evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Optical Tracking** from the Optical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Thermal Sensors

**Purpose**

Detect server-modelled engine, reactor, weapon, and environmental heat.

**What this page contains**

- Detect server-modelled engine, reactor, weapon, and environmental heat.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Thermal Sensors** from the Thermal navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Lidar and Rangefinding

**Purpose**

Provide precise short-range geometry for docking and inspection.

**What this page contains**

- Provide precise short-range geometry for docking and inspection.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Lidar and Rangefinding** from the Lidar navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Mass and Gravity Detection

**Purpose**

Optionally provide coarse large-object anomaly detection.

**What this page contains**

- Optionally provide coarse large-object anomaly detection.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Mass and Gravity Detection** from the Mass navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Long-Range Search

**Purpose**

Perform slow, sensitive sector sweeps.

**What this page contains**

- Perform slow, sensitive sector sweeps.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Long-Range Search** from the Long-Range navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Planetary Survey

**Purpose**

Map terrain, sites, and surface activity with age and resolution.

**What this page contains**

- Map terrain, sites, and surface activity with age and resolution.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Planetary Survey** from the Planetary navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Track Correlation

**Purpose**

Merge, split, or retain contradictory candidate tracks.

**What this page contains**

- Merge, split, or retain contradictory candidate tracks.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Track Correlation** from the Track navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Classification

**Purpose**

Assess object type, allegiance, and capability from evidence.

**What this page contains**

- Assess object type, allegiance, and capability from evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Classification** from the Classification navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Signature Management

**Purpose**

Show own EM, thermal, optical, communications, weapons, and thrust signature.

**What this page contains**

- Show own EM, thermal, optical, communications, weapons, and thrust signature.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Signature Management** from the Signature navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Emission Control

**Purpose**

Enforce passive, restricted, tactical, or unrestricted emission posture.

**What this page contains**

- Enforce passive, restricted, tactical, or unrestricted emission posture.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Emission Control** from the Emission navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Search Patterns

**Purpose**

Create reusable search volumes and schedules.

**What this page contains**

- Create reusable search volumes and schedules.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Search Patterns** from the Search navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Calibration

**Purpose**

Track sensor bias, drift, alignment, and test results.

**What this page contains**

- Track sensor bias, drift, alignment, and test results.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Calibration** from the Calibration navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sensor History

**Purpose**

Replay detection and track evolution.

**What this page contains**

- Replay detection and track evolution.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Sensor coverage and health
- Contact count and confidence
- Selected track and detections

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Sensor History** from the Sensor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

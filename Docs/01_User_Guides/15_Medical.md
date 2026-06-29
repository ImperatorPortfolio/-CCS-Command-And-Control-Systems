# Medical — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Coordinate gameplay-level casualty discovery, triage, treatment capacity, supplies, evacuation, quarantine, and environmental exposure without pretending to be a clinical simulator.

## 2. Typical Roles

- Chief Medical Officer
- Medic
- Triage Officer
- Medical Logistics Officer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Crew status and casualties
- Triage priority and locations
- Medical room availability
- Treatment and evacuation queue
- Supplies and exposure
- Quarantine state

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Medical Dashboard | Summarise known casualties, facilities, supplies, and quarantine. |
| Crew Medical Status | List supported gameplay condition, location, exposure, and treatment state. |
| Triage Board | Assign simple configurable response priority and destination. |
| Casualty Location Map | Display last-known positions with route hazards, age, and confidence. |
| Medical Bay Status | Show powered, accessible medical blocks and capacity. |
| Treatment Queue | Coordinate priority, destination, waiting, diversion, and completion. |
| Medical Supplies | Track configured medical items through Logistics. |
| Environmental Exposure | Track decompression, oxygen loss, heat, cold, or optional radiation bands. |
| Quarantine Control | Manage gameplay quarantine requests and zones. |
| Medical Evacuation | Coordinate movement to facilities or rescue craft. |
| Rescue Coordination | Coordinate retrieval of missing or trapped personnel. |
| Medical Incident Log | Record operational casualty and response history. |

## 5. Standard Operating Workflow

1. Locate and classify casualty using supported state
2. Assign triage and destination
3. Coordinate rescue, access, and evacuation
4. Reserve supplies and capacity
5. Close with auditable operational outcome

## 6. Alarm Responsibilities

- Crew incapacitated/lost
- Medical facility unavailable
- High-priority casualty has no route
- Medical supply low
- Quarantine violation
- Mass-casualty threshold

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

- Player state
- Security tracking
- Damage routes
- Life Support exposure
- Flight Operations evacuation
- Logistics supplies

---

## Medical Dashboard

**Purpose**

Summarise known casualties, facilities, supplies, and quarantine.

**What this page contains**

- Summarise known casualties, facilities, supplies, and quarantine.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Medical Dashboard** from the Medical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Crew Medical Status

**Purpose**

List supported gameplay condition, location, exposure, and treatment state.

**What this page contains**

- List supported gameplay condition, location, exposure, and treatment state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Crew Medical Status** from the Crew navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Triage Board

**Purpose**

Assign simple configurable response priority and destination.

**What this page contains**

- Assign simple configurable response priority and destination.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Triage Board** from the Triage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Casualty Location Map

**Purpose**

Display last-known positions with route hazards, age, and confidence.

**What this page contains**

- Display last-known positions with route hazards, age, and confidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Casualty Location Map** from the Casualty navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Medical Bay Status

**Purpose**

Show powered, accessible medical blocks and capacity.

**What this page contains**

- Show powered, accessible medical blocks and capacity.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Medical Bay Status** from the Medical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Treatment Queue

**Purpose**

Coordinate priority, destination, waiting, diversion, and completion.

**What this page contains**

- Coordinate priority, destination, waiting, diversion, and completion.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Treatment Queue** from the Treatment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Medical Supplies

**Purpose**

Track configured medical items through Logistics.

**What this page contains**

- Track configured medical items through Logistics.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Medical Supplies** from the Medical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Environmental Exposure

**Purpose**

Track decompression, oxygen loss, heat, cold, or optional radiation bands.

**What this page contains**

- Track decompression, oxygen loss, heat, cold, or optional radiation bands.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Environmental Exposure** from the Environmental navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Quarantine Control

**Purpose**

Manage gameplay quarantine requests and zones.

**What this page contains**

- Manage gameplay quarantine requests and zones.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Quarantine Control** from the Quarantine navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Medical Evacuation

**Purpose**

Coordinate movement to facilities or rescue craft.

**What this page contains**

- Coordinate movement to facilities or rescue craft.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Medical Evacuation** from the Medical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Rescue Coordination

**Purpose**

Coordinate retrieval of missing or trapped personnel.

**What this page contains**

- Coordinate retrieval of missing or trapped personnel.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Rescue Coordination** from the Rescue navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Medical Incident Log

**Purpose**

Record operational casualty and response history.

**What this page contains**

- Record operational casualty and response history.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Crew status and casualties
- Triage priority and locations
- Medical room availability

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Medical Incident Log** from the Medical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

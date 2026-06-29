# Fleet and Strategic Operations — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Coordinate multiple vessels and installations, formations, fleet orders, shared navigation and contacts, synchronised jumps, supply, territory, and campaign objectives without duplicating Scenarium authority.

## 2. Typical Roles

- Fleet Commander
- Flag Operations Officer
- Fleet Navigator
- Fleet Logistics Officer
- Strategic Intelligence Officer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Fleet roster and readiness
- Task groups and formation
- Strategic objectives
- Shared contacts/routes/comms
- Supply and reinforcements
- Territory, bases, losses, and events

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Fleet Dashboard | Summarise task-group and strategic state. |
| Fleet Tactical Picture | Combine trusted unit reports with source, age, and uncertainty. |
| Task-Group Organisation | Define command relationships, roles, units, and support links. |
| Formation Control | Manage leader, slots, separation, doctrine, and breakaway. |
| Fleet Orders | Issue versioned intent-based orders with acknowledgement and status. |
| Shared Contacts | Publish selected sensor tracks while retaining provenance. |
| Shared Navigation | Distribute routes, rendezvous, exclusion zones, and revisions. |
| Jump Synchronisation | Coordinate readiness, commit window, and abort without remotely firing drives. |
| Asset Readiness | Compare signed, age-qualified vessel capability reports. |
| Reinforcement Requests | Request and assign additional assets through authorised strategic source. |
| Supply Network | View bases, routes, stock bands, convoys, demand, and interdiction. |
| Territory and Conquest Map | Display Scenarium-owned nodes, status, ownership, and objectives. |
| Base and Installation Status | Show authorised allied installation services and readiness. |
| Campaign Objectives | Consume Scenarium objective state and submit evidence. |
| Faction Intelligence | Store evidence-based capability and intent assessments. |
| Loss and Replacement | Track confirmed losses, capability gaps, and replacement requests. |
| Strategic Event Timeline | Record orders, battles, node changes, arrivals, losses, and objectives. |

## 5. Standard Operating Workflow

1. Build roster and confirm data age
2. Organise groups and issue orders
3. Publish contacts/navigation with provenance
4. Coordinate timing, supply, and reinforcement
5. Submit evidence through Scenarium gateway

## 6. Alarm Responsibilities

- Fleet command link lost
- Critical order unacknowledged
- Formation separation unsafe
- Jump participant not ready
- Supply route interrupted
- Scenarium state stale/conflicted
- Readiness expired

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

- Communications/datalink
- Per-vessel departments
- ScenariumAPI
- MES through Scenarium
- Fleet logistics

---

## Fleet Dashboard

**Purpose**

Summarise task-group and strategic state.

**What this page contains**

- Summarise task-group and strategic state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Fleet Dashboard** from the Fleet navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fleet Tactical Picture

**Purpose**

Combine trusted unit reports with source, age, and uncertainty.

**What this page contains**

- Combine trusted unit reports with source, age, and uncertainty.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Fleet Tactical Picture** from the Fleet navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Task-Group Organisation

**Purpose**

Define command relationships, roles, units, and support links.

**What this page contains**

- Define command relationships, roles, units, and support links.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Task-Group Organisation** from the Task-Group navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Formation Control

**Purpose**

Manage leader, slots, separation, doctrine, and breakaway.

**What this page contains**

- Manage leader, slots, separation, doctrine, and breakaway.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Formation Control** from the Formation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fleet Orders

**Purpose**

Issue versioned intent-based orders with acknowledgement and status.

**What this page contains**

- Issue versioned intent-based orders with acknowledgement and status.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Fleet Orders** from the Fleet navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Shared Contacts

**Purpose**

Publish selected sensor tracks while retaining provenance.

**What this page contains**

- Publish selected sensor tracks while retaining provenance.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Shared Contacts** from the Shared navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Shared Navigation

**Purpose**

Distribute routes, rendezvous, exclusion zones, and revisions.

**What this page contains**

- Distribute routes, rendezvous, exclusion zones, and revisions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Shared Navigation** from the Shared navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Jump Synchronisation

**Purpose**

Coordinate readiness, commit window, and abort without remotely firing drives.

**What this page contains**

- Coordinate readiness, commit window, and abort without remotely firing drives.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Jump Synchronisation** from the Jump navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Asset Readiness

**Purpose**

Compare signed, age-qualified vessel capability reports.

**What this page contains**

- Compare signed, age-qualified vessel capability reports.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Asset Readiness** from the Asset navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Reinforcement Requests

**Purpose**

Request and assign additional assets through authorised strategic source.

**What this page contains**

- Request and assign additional assets through authorised strategic source.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Reinforcement Requests** from the Reinforcement navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Supply Network

**Purpose**

View bases, routes, stock bands, convoys, demand, and interdiction.

**What this page contains**

- View bases, routes, stock bands, convoys, demand, and interdiction.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Supply Network** from the Supply navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Territory and Conquest Map

**Purpose**

Display Scenarium-owned nodes, status, ownership, and objectives.

**What this page contains**

- Display Scenarium-owned nodes, status, ownership, and objectives.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Territory and Conquest Map** from the Territory navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Base and Installation Status

**Purpose**

Show authorised allied installation services and readiness.

**What this page contains**

- Show authorised allied installation services and readiness.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Base and Installation Status** from the Base navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Campaign Objectives

**Purpose**

Consume Scenarium objective state and submit evidence.

**What this page contains**

- Consume Scenarium objective state and submit evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Campaign Objectives** from the Campaign navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Faction Intelligence

**Purpose**

Store evidence-based capability and intent assessments.

**What this page contains**

- Store evidence-based capability and intent assessments.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Faction Intelligence** from the Faction navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Loss and Replacement

**Purpose**

Track confirmed losses, capability gaps, and replacement requests.

**What this page contains**

- Track confirmed losses, capability gaps, and replacement requests.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Loss and Replacement** from the Loss navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Strategic Event Timeline

**Purpose**

Record orders, battles, node changes, arrivals, losses, and objectives.

**What this page contains**

- Record orders, battles, node changes, arrivals, losses, and objectives.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Fleet roster and readiness
- Task groups and formation
- Strategic objectives

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Strategic Event Timeline** from the Strategic navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

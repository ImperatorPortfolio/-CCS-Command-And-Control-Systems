# Operations — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Coordinate departments, resources, procedures, and timelines so command intent becomes executable ship activity.

## 2. Typical Roles

- Operations Officer
- Executive Officer
- Department Liaison
- Mission Planner

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks
- Upcoming mission events
- Cross-department conflicts
- Unstaffed stations
- Automation and procedure queue

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Operations Dashboard | Show the vessel as a coordinated operating organisation. |
| Operational Picture | Combine mission, navigation, tactical, and engineering context. |
| Department Status | Collect structured readiness, risk, and request reports from every department. |
| Resource Allocation | Translate command priorities into power, fuel, crew, repair, and bandwidth budgets. |
| Task Board | Create and track operational tasks with dependencies and evidence. |
| Procedure Control | Start, monitor, pause, resume, or abort multi-station procedures. |
| Dependency Map | Explain exactly which systems, tasks, resources, or authorisations block an operation. |
| Watch Schedule | Plan station staffing and expose qualification gaps. |
| Maintenance Coordination | Schedule technical work without conflicting with operational demand. |
| Mission Timeline | Coordinate jumps, launches, rendezvous, maintenance, and deadlines. |
| Request Queue | Route departmental requests to the correct authority. |
| Automation Queue | Expose scheduled and triggered automation so it cannot surprise operators. |
| Readiness Reports | Generate formal preflight, precombat, prejump, docking, and post-damage reports. |

## 5. Standard Operating Workflow

1. Collect department status
2. Convert orders into tasks, procedures, and allocations
3. Resolve dependencies and schedule conflicts
4. Track execution until evidence confirms completion
5. Submit readiness reports to Command

## 6. Alarm Responsibilities

- Critical task overdue
- Procedure stalled
- Conflicting resource allocation
- Mandatory watch vacancy
- Maintenance lockout conflict
- Automation outside approved window

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

- Command orders
- Department readiness providers
- Procedure engine
- Rules engine
- Resource allocation
- Scenarium mission timeline

---

## Operations Dashboard

**Purpose**

Show the vessel as a coordinated operating organisation.

**What this page contains**

- Show the vessel as a coordinated operating organisation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Operations Dashboard** from the Operations navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Operational Picture

**Purpose**

Combine mission, navigation, tactical, and engineering context.

**What this page contains**

- Combine mission, navigation, tactical, and engineering context.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Operational Picture** from the Operational navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Department Status

**Purpose**

Collect structured readiness, risk, and request reports from every department.

**What this page contains**

- Collect structured readiness, risk, and request reports from every department.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Department Status** from the Department navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Resource Allocation

**Purpose**

Translate command priorities into power, fuel, crew, repair, and bandwidth budgets.

**What this page contains**

- Translate command priorities into power, fuel, crew, repair, and bandwidth budgets.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Resource Allocation** from the Resource navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Task Board

**Purpose**

Create and track operational tasks with dependencies and evidence.

**What this page contains**

- Create and track operational tasks with dependencies and evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Task Board** from the Task navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Procedure Control

**Purpose**

Start, monitor, pause, resume, or abort multi-station procedures.

**What this page contains**

- Start, monitor, pause, resume, or abort multi-station procedures.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Procedure Control** from the Procedure navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Dependency Map

**Purpose**

Explain exactly which systems, tasks, resources, or authorisations block an operation.

**What this page contains**

- Explain exactly which systems, tasks, resources, or authorisations block an operation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Dependency Map** from the Dependency navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Watch Schedule

**Purpose**

Plan station staffing and expose qualification gaps.

**What this page contains**

- Plan station staffing and expose qualification gaps.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Watch Schedule** from the Watch navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Maintenance Coordination

**Purpose**

Schedule technical work without conflicting with operational demand.

**What this page contains**

- Schedule technical work without conflicting with operational demand.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Maintenance Coordination** from the Maintenance navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Mission Timeline

**Purpose**

Coordinate jumps, launches, rendezvous, maintenance, and deadlines.

**What this page contains**

- Coordinate jumps, launches, rendezvous, maintenance, and deadlines.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Mission Timeline** from the Mission navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Request Queue

**Purpose**

Route departmental requests to the correct authority.

**What this page contains**

- Route departmental requests to the correct authority.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Request Queue** from the Request navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Automation Queue

**Purpose**

Expose scheduled and triggered automation so it cannot surprise operators.

**What this page contains**

- Expose scheduled and triggered automation so it cannot surprise operators.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Automation Queue** from the Automation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Readiness Reports

**Purpose**

Generate formal preflight, precombat, prejump, docking, and post-damage reports.

**What this page contains**

- Generate formal preflight, precombat, prejump, docking, and post-damage reports.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Department workload and readiness
- Active tasks and blocked work
- Resource bottlenecks

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Readiness Reports** from the Readiness navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

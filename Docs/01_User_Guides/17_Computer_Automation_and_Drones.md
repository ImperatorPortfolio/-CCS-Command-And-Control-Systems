# Computer, Automation and Drones — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Operate CommandShellOS core services, visible automation, failover, scheduling, autonomous craft, network health, data storage, and cybersecurity boundaries.

## 2. Typical Roles

- Computer Systems Officer
- Automation Engineer
- Drone Controller
- Systems Administrator

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Core and failover health
- Network nodes and consoles
- Active automation and jobs
- Drone missions and links
- Data storage
- Performance and security alerts

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Computer Dashboard | Summarise core, network, automation, drones, storage, performance, and security. |
| Computer Core | Show service state, dependencies, queues, errors, leader, and standby. |
| Network Map | Display logical nodes, subscriptions, sessions, latency, and stale endpoints. |
| Automation Rules | Manage event-condition-action rules with owner, scope, cooldown, and simulation. |
| Sequence Library | Store immutable published procedure versions. |
| Task Scheduler | Schedule idempotent future work with owner, expiry, and journal. |
| Drone Control | Assign bounded objectives, routes, autonomy, safe state, pause, recall, or terminate. |
| Autopilot Behaviours | Configure reusable data-driven patrol, escort, dock, and intercept profiles. |
| Remote Control | Manage least-privilege remote equipment or vessel sessions. |
| Module Status | Show installed modules, versions, dependencies, health, and compatibility. |
| Data Storage | Manage configuration, logs, maps, snapshots, backup, and restore. |
| Failover Control | Transfer critical services while preventing split-brain. |
| Performance | Expose tick cost, scan cost, message rate, queues, and slow handlers. |
| Cybersecurity | Detect invalid sessions, replay, rate abuse, and compromised modules. |
| Audit Logs | Review integrity-protected system and administrator actions. |

## 5. Standard Operating Workflow

1. Verify core, standby, network, storage, and automation
2. Simulate before enabling automation
3. Assign drones bounded missions
4. Monitor cost and stale subscriptions
5. Use controlled failover and verify one leader

## 6. Alarm Responsibilities

- Core unavailable
- Split-brain risk
- Automation repeated/stalled/out of scope
- Drone leaves boundary or loses link
- Backup verification fails
- Replay/rate attack
- Performance budget exceeded

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

- All CommandShell services
- Torch lifecycle
- Client protocol
- Communications remote sessions
- Security authority
- AI/remote blocks

---

## Computer Dashboard

**Purpose**

Summarise core, network, automation, drones, storage, performance, and security.

**What this page contains**

- Summarise core, network, automation, drones, storage, performance, and security.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Computer Dashboard** from the Computer navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Computer Core

**Purpose**

Show service state, dependencies, queues, errors, leader, and standby.

**What this page contains**

- Show service state, dependencies, queues, errors, leader, and standby.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Computer Core** from the Computer navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Network Map

**Purpose**

Display logical nodes, subscriptions, sessions, latency, and stale endpoints.

**What this page contains**

- Display logical nodes, subscriptions, sessions, latency, and stale endpoints.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Network Map** from the Network navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Automation Rules

**Purpose**

Manage event-condition-action rules with owner, scope, cooldown, and simulation.

**What this page contains**

- Manage event-condition-action rules with owner, scope, cooldown, and simulation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Automation Rules** from the Automation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sequence Library

**Purpose**

Store immutable published procedure versions.

**What this page contains**

- Store immutable published procedure versions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Sequence Library** from the Sequence navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Task Scheduler

**Purpose**

Schedule idempotent future work with owner, expiry, and journal.

**What this page contains**

- Schedule idempotent future work with owner, expiry, and journal.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Task Scheduler** from the Task navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Drone Control

**Purpose**

Assign bounded objectives, routes, autonomy, safe state, pause, recall, or terminate.

**What this page contains**

- Assign bounded objectives, routes, autonomy, safe state, pause, recall, or terminate.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Drone Control** from the Drone navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Autopilot Behaviours

**Purpose**

Configure reusable data-driven patrol, escort, dock, and intercept profiles.

**What this page contains**

- Configure reusable data-driven patrol, escort, dock, and intercept profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Autopilot Behaviours** from the Autopilot navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Remote Control

**Purpose**

Manage least-privilege remote equipment or vessel sessions.

**What this page contains**

- Manage least-privilege remote equipment or vessel sessions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Remote Control** from the Remote navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Module Status

**Purpose**

Show installed modules, versions, dependencies, health, and compatibility.

**What this page contains**

- Show installed modules, versions, dependencies, health, and compatibility.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Module Status** from the Module navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Data Storage

**Purpose**

Manage configuration, logs, maps, snapshots, backup, and restore.

**What this page contains**

- Manage configuration, logs, maps, snapshots, backup, and restore.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Data Storage** from the Data navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Failover Control

**Purpose**

Transfer critical services while preventing split-brain.

**What this page contains**

- Transfer critical services while preventing split-brain.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Failover Control** from the Failover navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Performance

**Purpose**

Expose tick cost, scan cost, message rate, queues, and slow handlers.

**What this page contains**

- Expose tick cost, scan cost, message rate, queues, and slow handlers.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Performance** from the Performance navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Cybersecurity

**Purpose**

Detect invalid sessions, replay, rate abuse, and compromised modules.

**What this page contains**

- Detect invalid sessions, replay, rate abuse, and compromised modules.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Cybersecurity** from the Cybersecurity navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Audit Logs

**Purpose**

Review integrity-protected system and administrator actions.

**What this page contains**

- Review integrity-protected system and administrator actions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Core and failover health
- Network nodes and consoles
- Active automation and jobs

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Audit Logs** from the Audit navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

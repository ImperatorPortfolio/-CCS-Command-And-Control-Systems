# Security — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Protect personnel, compartments, armories, computers, and command functions from unauthorised access, boarding, sabotage, and internal threats.

## 2. Typical Roles

- Chief of Security
- Security Officer
- Armory Officer
- Brig Officer
- Cybersecurity Officer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Security condition and lockdown
- Personnel aboard
- Access violations
- Door, camera, turret, armory, and brig status
- Teams and incidents
- Remote-access alerts

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Security Dashboard | Present internal security posture and active incidents. |
| Access Control | Manage role-based access and time-bound grants. |
| Deck Security Map | Show personnel, doors, cameras, zones, incidents, and teams by compartment. |
| Personnel Tracking | Track known identities, roles, credentials, and last known position. |
| Intrusion Detection | Detect unauthorised entry, terminal use, block changes, and ownership changes. |
| Lockdown Control | Apply and release zone, deck, or ship lockdown with consequence preview. |
| Security Cameras | View, route, and record authorised camera feeds. |
| Armory | Control weapon storage, issue, return, and sealing. |
| Brig | Manage secure detention while preserving life-safety override. |
| Security Teams | Assign, dispatch, reroute, and recall response teams. |
| Boarding Defence | Coordinate internal barriers, zones, and bounded defensive systems. |
| Sabotage Detection | Score suspicious disables, changes, explosives, and repeated faults with evidence. |
| Cybersecurity | Protect remote sessions and command services from invalid or replayed requests. |
| Visitor Control | Issue sponsored, zone-limited, expiring visitor access. |
| Security Log | Provide tamper-evident access and incident history. |

## 5. Standard Operating Workflow

1. Verify condition, personnel, and critical zones
2. Investigate evidence before escalation
3. Contain the smallest safe zone
4. Dispatch team and log incident
5. Release lockdown after review

## 6. Alarm Responsibilities

- Unauthorised critical-zone access
- Unknown occupant
- Armory opened without authority
- Brig life-safety failure
- Security sensor disabled during incident
- Remote-command attack
- Command-core tampering

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

- Crew identity
- Doors/cameras/sensors/turrets
- Damage compartments
- Computer security
- Command authorisation
- Audit

---

## Security Dashboard

**Purpose**

Present internal security posture and active incidents.

**What this page contains**

- Present internal security posture and active incidents.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Security Dashboard** from the Security navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Access Control

**Purpose**

Manage role-based access and time-bound grants.

**What this page contains**

- Manage role-based access and time-bound grants.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Access Control** from the Access navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Deck Security Map

**Purpose**

Show personnel, doors, cameras, zones, incidents, and teams by compartment.

**What this page contains**

- Show personnel, doors, cameras, zones, incidents, and teams by compartment.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Deck Security Map** from the Deck navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Personnel Tracking

**Purpose**

Track known identities, roles, credentials, and last known position.

**What this page contains**

- Track known identities, roles, credentials, and last known position.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Personnel Tracking** from the Personnel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Intrusion Detection

**Purpose**

Detect unauthorised entry, terminal use, block changes, and ownership changes.

**What this page contains**

- Detect unauthorised entry, terminal use, block changes, and ownership changes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Intrusion Detection** from the Intrusion navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Lockdown Control

**Purpose**

Apply and release zone, deck, or ship lockdown with consequence preview.

**What this page contains**

- Apply and release zone, deck, or ship lockdown with consequence preview.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Lockdown Control** from the Lockdown navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Security Cameras

**Purpose**

View, route, and record authorised camera feeds.

**What this page contains**

- View, route, and record authorised camera feeds.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Security Cameras** from the Security navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Armory

**Purpose**

Control weapon storage, issue, return, and sealing.

**What this page contains**

- Control weapon storage, issue, return, and sealing.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Armory** from the Armory navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Brig

**Purpose**

Manage secure detention while preserving life-safety override.

**What this page contains**

- Manage secure detention while preserving life-safety override.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Brig** from the Brig navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Security Teams

**Purpose**

Assign, dispatch, reroute, and recall response teams.

**What this page contains**

- Assign, dispatch, reroute, and recall response teams.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Security Teams** from the Security navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Boarding Defence

**Purpose**

Coordinate internal barriers, zones, and bounded defensive systems.

**What this page contains**

- Coordinate internal barriers, zones, and bounded defensive systems.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Boarding Defence** from the Boarding navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sabotage Detection

**Purpose**

Score suspicious disables, changes, explosives, and repeated faults with evidence.

**What this page contains**

- Score suspicious disables, changes, explosives, and repeated faults with evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Sabotage Detection** from the Sabotage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Cybersecurity

**Purpose**

Protect remote sessions and command services from invalid or replayed requests.

**What this page contains**

- Protect remote sessions and command services from invalid or replayed requests.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Cybersecurity** from the Cybersecurity navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Visitor Control

**Purpose**

Issue sponsored, zone-limited, expiring visitor access.

**What this page contains**

- Issue sponsored, zone-limited, expiring visitor access.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Visitor Control** from the Visitor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Security Log

**Purpose**

Provide tamper-evident access and incident history.

**What this page contains**

- Provide tamper-evident access and incident history.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Security condition and lockdown
- Personnel aboard
- Access violations

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Security Log** from the Security navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

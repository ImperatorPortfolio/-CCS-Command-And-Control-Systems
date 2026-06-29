# Weapons — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Prepare, aim, authorise, fire, reload, safe, and diagnose weapon systems while enforcing authority, arcs, ammunition, and friendly-fire protections.

## 2. Typical Roles

- Weapons Officer
- Fire-Control Officer
- Missile Officer
- Point-Defence Operator
- Armorer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Global armed/safe state
- Battery readiness
- Selected target and authority
- Ammunition and reload
- Firing solution and obstruction
- Point-defence coverage
- Faults and lockouts

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Weapons Dashboard | Summarise combat-system readiness and safety. |
| Fire Control | Build and verify target, lead, range, arc, obstruction, and ROE solution. |
| Weapon Groups | Define and operate named batteries. |
| Turrets | Inspect arcs, orientation, target filters, ammunition, and AI/manual mode. |
| Fixed Weapons | Coordinate alignment, convergence, target cue, and Helm release. |
| Missiles | Program seeker, target, route, datalink, warhead, and salvo. |
| Railguns and Artillery | Manage charge, reload, alignment, recoil constraints, and firing solution. |
| Point Defence | Manage coverage, target classes, sectors, doctrine, and saturation. |
| Ammunition | Separate stored, accessible, reserved, ready, and endurance values. |
| Firing Arcs | Display own-ship obstruction, friendly units, and inhibited sectors. |
| Target Profiles | Select desired effect, ammunition preference, and aim rules. |
| Weapon Safety | Control arming, safing, maintenance, and emergency inhibit. |
| Salvo Programming | Configure ripple, simultaneous, staggered, and saturation patterns. |
| Weapons Diagnostics | Find power, feed, targeting, mechanical, and network faults. |
| Weapons Maintenance | Manage servicing, optional wear, lockouts, tests, and release. |

## 5. Standard Operating Workflow

1. Verify weapons-safe and inventory
2. Receive valid target and plan
3. Build solution and pass safety checks
4. Confirm firing authority and execute
5. Cease fire, safe, reconcile ammunition, and log faults

## 6. Alarm Responsibilities

- Weapon unexpectedly armed
- Friendly/protected object in line
- Ammunition feed failure
- Weapon fired without authority
- Point-defence saturation
- Missile rogue/lost
- High-energy constraint violation

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

- Tactical targets
- Command ROE
- Sensors tracks
- Engineering power/structure
- Logistics ammunition
- Weapon block adapters

---

## Weapons Dashboard

**Purpose**

Summarise combat-system readiness and safety.

**What this page contains**

- Summarise combat-system readiness and safety.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Weapons Dashboard** from the Weapons navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fire Control

**Purpose**

Build and verify target, lead, range, arc, obstruction, and ROE solution.

**What this page contains**

- Build and verify target, lead, range, arc, obstruction, and ROE solution.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Fire Control** from the Fire navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Weapon Groups

**Purpose**

Define and operate named batteries.

**What this page contains**

- Define and operate named batteries.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Weapon Groups** from the Weapon navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Turrets

**Purpose**

Inspect arcs, orientation, target filters, ammunition, and AI/manual mode.

**What this page contains**

- Inspect arcs, orientation, target filters, ammunition, and AI/manual mode.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Turrets** from the Turrets navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fixed Weapons

**Purpose**

Coordinate alignment, convergence, target cue, and Helm release.

**What this page contains**

- Coordinate alignment, convergence, target cue, and Helm release.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Fixed Weapons** from the Fixed navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Missiles

**Purpose**

Program seeker, target, route, datalink, warhead, and salvo.

**What this page contains**

- Program seeker, target, route, datalink, warhead, and salvo.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Missiles** from the Missiles navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Railguns and Artillery

**Purpose**

Manage charge, reload, alignment, recoil constraints, and firing solution.

**What this page contains**

- Manage charge, reload, alignment, recoil constraints, and firing solution.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Railguns and Artillery** from the Railguns navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Point Defence

**Purpose**

Manage coverage, target classes, sectors, doctrine, and saturation.

**What this page contains**

- Manage coverage, target classes, sectors, doctrine, and saturation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Point Defence** from the Point navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ammunition

**Purpose**

Separate stored, accessible, reserved, ready, and endurance values.

**What this page contains**

- Separate stored, accessible, reserved, ready, and endurance values.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Ammunition** from the Ammunition navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Firing Arcs

**Purpose**

Display own-ship obstruction, friendly units, and inhibited sectors.

**What this page contains**

- Display own-ship obstruction, friendly units, and inhibited sectors.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Firing Arcs** from the Firing navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Target Profiles

**Purpose**

Select desired effect, ammunition preference, and aim rules.

**What this page contains**

- Select desired effect, ammunition preference, and aim rules.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Target Profiles** from the Target navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Weapon Safety

**Purpose**

Control arming, safing, maintenance, and emergency inhibit.

**What this page contains**

- Control arming, safing, maintenance, and emergency inhibit.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Weapon Safety** from the Weapon navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Salvo Programming

**Purpose**

Configure ripple, simultaneous, staggered, and saturation patterns.

**What this page contains**

- Configure ripple, simultaneous, staggered, and saturation patterns.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Salvo Programming** from the Salvo navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Weapons Diagnostics

**Purpose**

Find power, feed, targeting, mechanical, and network faults.

**What this page contains**

- Find power, feed, targeting, mechanical, and network faults.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Weapons Diagnostics** from the Weapons navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Weapons Maintenance

**Purpose**

Manage servicing, optional wear, lockouts, tests, and release.

**What this page contains**

- Manage servicing, optional wear, lockouts, tests, and release.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Global armed/safe state
- Battery readiness
- Selected target and authority

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Weapons Maintenance** from the Weapons navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

# Damage Control — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Detect, contain, prioritise, repair, and recover from hull, atmosphere, structural, power, fire-simulation, and system casualties by compartment.

## 2. Typical Roles

- Damage Control Officer
- Repair Coordinator
- Compartment Controller
- Emergency Team Leader

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties
- Isolation and emergency routes
- Repair teams and materials
- Survival estimate

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Damage Control Dashboard | Summarise all active casualties and the current survival problem. |
| Damage Map | Display casualties by deck, zone, and compartment. |
| Hull Integrity | Assess missing blocks, armour loss, breaches, and critical connections. |
| Atmosphere Loss | Track pressure zones, oxygen loss, leak rate, and adjacent compartments. |
| Compartment Isolation | Control doors, vents, power, and access while showing trapped personnel and affected systems. |
| Fire Control | Manage optional server-simulated fire, fuel, spread, venting, and suppression. |
| Structural Stability | Identify critical connections, unsafe assemblies, and conservative movement restrictions. |
| System Casualties | Show capability loss by department and downstream dependencies. |
| Damage Teams | Assign, route, recall, and monitor emergency teams. |
| Repair Queue | Prioritise concrete repair tasks, components, access, and verification. |
| Emergency Routing | Create alternate power, fuel, conveyor, access, and control routes. |
| Evacuation Routes | Publish safe personnel movement and muster routes. |
| Casualty Estimates | Estimate personnel at risk with source age and confidence. |
| Battle Damage Assessment | Summarise remaining mobility, sensors, weapons, defence, power, and endurance. |
| Recovery Procedures | Restore atmosphere, power, control, and equipment in safe dependency order. |

## 5. Standard Operating Workflow

1. Detect and locate casualty
2. Protect personnel and contain hazards
3. Assess system and structural consequences
4. Assign teams and repair materials
5. Test, restore, and obtain owning-department release

## 6. Alarm Responsibilities

- Hull breach
- Rapid pressure loss
- Critical compartment inaccessible
- Fire spreading
- Structural connection at risk
- Repair team enters unsafe zone
- Recovery verification failed

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

- Block damage/grid topology
- Life Support
- Engineering isolation
- Security doors
- Logistics components
- Crew tracking
- Command readiness

---

## Damage Control Dashboard

**Purpose**

Summarise all active casualties and the current survival problem.

**What this page contains**

- Summarise all active casualties and the current survival problem.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Damage Control Dashboard** from the Damage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Damage Map

**Purpose**

Display casualties by deck, zone, and compartment.

**What this page contains**

- Display casualties by deck, zone, and compartment.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Damage Map** from the Damage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hull Integrity

**Purpose**

Assess missing blocks, armour loss, breaches, and critical connections.

**What this page contains**

- Assess missing blocks, armour loss, breaches, and critical connections.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Hull Integrity** from the Hull navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Atmosphere Loss

**Purpose**

Track pressure zones, oxygen loss, leak rate, and adjacent compartments.

**What this page contains**

- Track pressure zones, oxygen loss, leak rate, and adjacent compartments.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Atmosphere Loss** from the Atmosphere navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Compartment Isolation

**Purpose**

Control doors, vents, power, and access while showing trapped personnel and affected systems.

**What this page contains**

- Control doors, vents, power, and access while showing trapped personnel and affected systems.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Compartment Isolation** from the Compartment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fire Control

**Purpose**

Manage optional server-simulated fire, fuel, spread, venting, and suppression.

**What this page contains**

- Manage optional server-simulated fire, fuel, spread, venting, and suppression.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Fire Control** from the Fire navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Structural Stability

**Purpose**

Identify critical connections, unsafe assemblies, and conservative movement restrictions.

**What this page contains**

- Identify critical connections, unsafe assemblies, and conservative movement restrictions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Structural Stability** from the Structural navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## System Casualties

**Purpose**

Show capability loss by department and downstream dependencies.

**What this page contains**

- Show capability loss by department and downstream dependencies.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **System Casualties** from the System navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Damage Teams

**Purpose**

Assign, route, recall, and monitor emergency teams.

**What this page contains**

- Assign, route, recall, and monitor emergency teams.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Damage Teams** from the Damage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Repair Queue

**Purpose**

Prioritise concrete repair tasks, components, access, and verification.

**What this page contains**

- Prioritise concrete repair tasks, components, access, and verification.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Repair Queue** from the Repair navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Emergency Routing

**Purpose**

Create alternate power, fuel, conveyor, access, and control routes.

**What this page contains**

- Create alternate power, fuel, conveyor, access, and control routes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Emergency Routing** from the Emergency navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Evacuation Routes

**Purpose**

Publish safe personnel movement and muster routes.

**What this page contains**

- Publish safe personnel movement and muster routes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Evacuation Routes** from the Evacuation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Casualty Estimates

**Purpose**

Estimate personnel at risk with source age and confidence.

**What this page contains**

- Estimate personnel at risk with source age and confidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Casualty Estimates** from the Casualty navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Battle Damage Assessment

**Purpose**

Summarise remaining mobility, sensors, weapons, defence, power, and endurance.

**What this page contains**

- Summarise remaining mobility, sensors, weapons, defence, power, and endurance.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Battle Damage Assessment** from the Battle navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Recovery Procedures

**Purpose**

Restore atmosphere, power, control, and equipment in safe dependency order.

**What this page contains**

- Restore atmosphere, power, control, and equipment in safe dependency order.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Hull and functional integrity
- Breaches and depressurised compartments
- Critical equipment casualties

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Recovery Procedures** from the Recovery navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

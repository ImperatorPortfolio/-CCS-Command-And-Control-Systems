# Flight Operations — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Manage attached craft, hangars, launch and recovery traffic, servicing, pilots, rescue readiness, and flight-deck safety.

## 2. Typical Roles

- Flight Operations Officer
- Air Boss
- Hangar Officer
- Launch Officer
- Recovery Officer
- Crew Chief

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Craft roster and readiness
- Hangar condition
- Launch/recovery queue
- Active sorties and traffic
- Fuel, ammunition, repair, and pilot readiness
- Rescue craft and deck safety

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Flight Operations Dashboard | Present all attached-craft activity and limiting readiness issue. |
| Craft Roster | Track each craft identity, pilot, location, readiness, fuel, ammunition, and damage. |
| Hangar Map | Show bay occupancy, doors, pressure, hazards, service points, and routes. |
| Launch Control | Run staged crew, power, fuel, weapons, bay, and clearance checks. |
| Recovery Control | Sequence inbound craft, bay assignment, emergency priority, and wave-off. |
| Approach Traffic | Display local craft tracks, corridors, closure, holding, and priority. |
| Bay Door and Pressure | Cycle hangars with personnel and life-safety interlocks. |
| Refuelling | Approve bounded fuel transfers without violating host reserves. |
| Rearming | Apply approved loadouts and reconcile ammunition transfer. |
| Repair and Servicing | Create service packages and release craft only after checks pass. |
| Pilot Assignments | Assign qualified players and record briefing acknowledgement. |
| Sortie Planning | Create objective, craft, loadout, route, timing, communications, and recovery plan. |
| Drone Launch Operations | Coordinate launch/recovery while Computer owns autonomous mission logic. |
| Search and Rescue | Maintain and scramble rescue capability. |
| Flight-Deck Safety | Control safe zones, engine/weapon state, personnel, and stop-work. |
| Tug and Retrieval | Coordinate towing and recovery of disabled craft. |

## 5. Standard Operating Workflow

1. Register craft, pilot, mission, bay, and loadout
2. Complete readiness and safety stages
3. Clear bay and launch corridor
4. Track sortie and prepare recovery
5. Recover, safe, reconcile, inspect, and close

## 6. Alarm Responsibilities

- Hangar interlock violation
- Launch without clearance
- Inbound craft below fuel reserve
- Assigned bay unsafe
- Servicing with engines/weapons unsafe
- Drone/craft link lost
- Rescue craft unavailable

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

- Attached-vessel registry
- Helm/Navigation
- Sensors traffic
- Engineering fuel
- Weapons/Logistics ammunition
- Computer drones
- Scenarium objectives

---

## Flight Operations Dashboard

**Purpose**

Present all attached-craft activity and limiting readiness issue.

**What this page contains**

- Present all attached-craft activity and limiting readiness issue.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Flight Operations Dashboard** from the Flight navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Craft Roster

**Purpose**

Track each craft identity, pilot, location, readiness, fuel, ammunition, and damage.

**What this page contains**

- Track each craft identity, pilot, location, readiness, fuel, ammunition, and damage.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Craft Roster** from the Craft navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hangar Map

**Purpose**

Show bay occupancy, doors, pressure, hazards, service points, and routes.

**What this page contains**

- Show bay occupancy, doors, pressure, hazards, service points, and routes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Hangar Map** from the Hangar navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Launch Control

**Purpose**

Run staged crew, power, fuel, weapons, bay, and clearance checks.

**What this page contains**

- Run staged crew, power, fuel, weapons, bay, and clearance checks.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Launch Control** from the Launch navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Recovery Control

**Purpose**

Sequence inbound craft, bay assignment, emergency priority, and wave-off.

**What this page contains**

- Sequence inbound craft, bay assignment, emergency priority, and wave-off.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Recovery Control** from the Recovery navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Approach Traffic

**Purpose**

Display local craft tracks, corridors, closure, holding, and priority.

**What this page contains**

- Display local craft tracks, corridors, closure, holding, and priority.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Approach Traffic** from the Approach navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Bay Door and Pressure

**Purpose**

Cycle hangars with personnel and life-safety interlocks.

**What this page contains**

- Cycle hangars with personnel and life-safety interlocks.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Bay Door and Pressure** from the Bay navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Refuelling

**Purpose**

Approve bounded fuel transfers without violating host reserves.

**What this page contains**

- Approve bounded fuel transfers without violating host reserves.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Refuelling** from the Refuelling navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Rearming

**Purpose**

Apply approved loadouts and reconcile ammunition transfer.

**What this page contains**

- Apply approved loadouts and reconcile ammunition transfer.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Rearming** from the Rearming navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Repair and Servicing

**Purpose**

Create service packages and release craft only after checks pass.

**What this page contains**

- Create service packages and release craft only after checks pass.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Repair and Servicing** from the Repair navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Pilot Assignments

**Purpose**

Assign qualified players and record briefing acknowledgement.

**What this page contains**

- Assign qualified players and record briefing acknowledgement.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Pilot Assignments** from the Pilot navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sortie Planning

**Purpose**

Create objective, craft, loadout, route, timing, communications, and recovery plan.

**What this page contains**

- Create objective, craft, loadout, route, timing, communications, and recovery plan.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Sortie Planning** from the Sortie navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Drone Launch Operations

**Purpose**

Coordinate launch/recovery while Computer owns autonomous mission logic.

**What this page contains**

- Coordinate launch/recovery while Computer owns autonomous mission logic.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Drone Launch Operations** from the Drone navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Search and Rescue

**Purpose**

Maintain and scramble rescue capability.

**What this page contains**

- Maintain and scramble rescue capability.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Search and Rescue** from the Search navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Flight-Deck Safety

**Purpose**

Control safe zones, engine/weapon state, personnel, and stop-work.

**What this page contains**

- Control safe zones, engine/weapon state, personnel, and stop-work.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Flight-Deck Safety** from the Flight-Deck navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Tug and Retrieval

**Purpose**

Coordinate towing and recovery of disabled craft.

**What this page contains**

- Coordinate towing and recovery of disabled craft.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Craft roster and readiness
- Hangar condition
- Launch/recovery queue

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Tug and Retrieval** from the Tug navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

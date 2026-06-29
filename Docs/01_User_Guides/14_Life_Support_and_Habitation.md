# Life Support and Habitation — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Maintain breathable, pressurised, powered, habitable compartments and provide credible survival endurance, airlock, gravity, lighting, and optional consumables management.

## 2. Typical Roles

- Life Support Officer
- Environmental Technician
- Habitation Officer
- Emergency Systems Operator

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks
- Gravity and lighting
- Emergency shelters
- Optional water, food, temperature, and waste

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Life Support Dashboard | Summarise habitability, reserves, zones, shelters, and survival. |
| Atmosphere Overview | Show pressure zones, oxygen, volume, leak rate, and vent state. |
| Compartment Pressure | Inspect one compartment, occupants, boundaries, and adjacent zones. |
| Oxygen Generation | Manage farms, generators, feed, power, health, and output. |
| Oxygen Storage | Manage tank groups, flow, roles, and protected emergency reserve. |
| Ventilation | Control zone circulation and pressure interfaces. |
| Airlock Management | Run explicit safe inner-door, pressure, outer-door state machines. |
| Gravity Zones | Manage vector, strength, generators, occupancy, and profiles. |
| Lighting Zones | Apply normal, dim, emergency, blackout, and alert profiles. |
| Emergency Shelters | Track pressure, oxygen, power, supplies, capacity, and muster. |
| Crew Quarters and Occupancy | Show habitation capacity and unsafe/restricted areas. |
| Water and Food | Optionally manage long-duration consumables and ration posture. |
| Waste Processing | Optionally manage environmental waste capacity and processing. |
| Temperature Model | Optionally simulate clearly labelled compartment thermal state. |
| Environmental Trends | Review pressure, oxygen, rate of change, and thresholds. |
| Survival Endurance | Estimate remaining habitable time under defined assumptions. |

## 5. Standard Operating Workflow

1. Verify zones, oxygen, generation, and shelters
2. Protect personnel and coordinate leak isolation
3. Balance generation and reserves
4. Use state machines for airlocks and transitions
5. Report survival endurance

## 6. Alarm Responsibilities

- Rapid pressure loss
- Occupied compartment unsafe
- Oxygen reserve low
- Airlock boundary violation
- Shelter unavailable
- Critical gravity/lighting loss
- Endurance below mission requirement

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

- Vents/tanks/generators/farms/doors/gravity/lights
- Damage Control
- Engineering
- Security occupancy
- Medical exposure
- Logistics consumables

---

## Life Support Dashboard

**Purpose**

Summarise habitability, reserves, zones, shelters, and survival.

**What this page contains**

- Summarise habitability, reserves, zones, shelters, and survival.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Life Support Dashboard** from the Life navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Atmosphere Overview

**Purpose**

Show pressure zones, oxygen, volume, leak rate, and vent state.

**What this page contains**

- Show pressure zones, oxygen, volume, leak rate, and vent state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Atmosphere Overview** from the Atmosphere navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Compartment Pressure

**Purpose**

Inspect one compartment, occupants, boundaries, and adjacent zones.

**What this page contains**

- Inspect one compartment, occupants, boundaries, and adjacent zones.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Compartment Pressure** from the Compartment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Oxygen Generation

**Purpose**

Manage farms, generators, feed, power, health, and output.

**What this page contains**

- Manage farms, generators, feed, power, health, and output.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Oxygen Generation** from the Oxygen navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Oxygen Storage

**Purpose**

Manage tank groups, flow, roles, and protected emergency reserve.

**What this page contains**

- Manage tank groups, flow, roles, and protected emergency reserve.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Oxygen Storage** from the Oxygen navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ventilation

**Purpose**

Control zone circulation and pressure interfaces.

**What this page contains**

- Control zone circulation and pressure interfaces.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Ventilation** from the Ventilation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Airlock Management

**Purpose**

Run explicit safe inner-door, pressure, outer-door state machines.

**What this page contains**

- Run explicit safe inner-door, pressure, outer-door state machines.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Airlock Management** from the Airlock navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Gravity Zones

**Purpose**

Manage vector, strength, generators, occupancy, and profiles.

**What this page contains**

- Manage vector, strength, generators, occupancy, and profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Gravity Zones** from the Gravity navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Lighting Zones

**Purpose**

Apply normal, dim, emergency, blackout, and alert profiles.

**What this page contains**

- Apply normal, dim, emergency, blackout, and alert profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Lighting Zones** from the Lighting navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Emergency Shelters

**Purpose**

Track pressure, oxygen, power, supplies, capacity, and muster.

**What this page contains**

- Track pressure, oxygen, power, supplies, capacity, and muster.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Emergency Shelters** from the Emergency navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Crew Quarters and Occupancy

**Purpose**

Show habitation capacity and unsafe/restricted areas.

**What this page contains**

- Show habitation capacity and unsafe/restricted areas.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Crew Quarters and Occupancy** from the Crew navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Water and Food

**Purpose**

Optionally manage long-duration consumables and ration posture.

**What this page contains**

- Optionally manage long-duration consumables and ration posture.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Water and Food** from the Water navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Waste Processing

**Purpose**

Optionally manage environmental waste capacity and processing.

**What this page contains**

- Optionally manage environmental waste capacity and processing.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Waste Processing** from the Waste navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Temperature Model

**Purpose**

Optionally simulate clearly labelled compartment thermal state.

**What this page contains**

- Optionally simulate clearly labelled compartment thermal state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Temperature Model** from the Temperature navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Environmental Trends

**Purpose**

Review pressure, oxygen, rate of change, and thresholds.

**What this page contains**

- Review pressure, oxygen, rate of change, and thresholds.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Environmental Trends** from the Environmental navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Survival Endurance

**Purpose**

Estimate remaining habitable time under defined assumptions.

**What this page contains**

- Estimate remaining habitable time under defined assumptions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Oxygen generation/storage/endurance
- Pressurised compartments and leaks
- Ventilation and airlocks

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Survival Endurance** from the Survival navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

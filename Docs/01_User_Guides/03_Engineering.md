# Engineering — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Operate, protect, diagnose, and restore power, propulsion support, resource distribution, mechanical, thermal, jump, shield, and structural systems.

## 2. Typical Roles

- Chief Engineer
- Engineering Officer
- Power Operator
- Propulsion Engineer
- Maintenance Technician

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed
- Hydrogen and oxygen storage
- Jump-drive readiness
- Conveyor health
- Thermal, shield, structural, and fault state

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Engineering Dashboard | Summarise vessel technical capability and the limiting technical fault. |
| Power Grid | Display generation, consumption, reserve, buses, breakers, and load flow. |
| Reactors | Operate reactor groups through guarded startup, shutdown, and scram sequences. |
| Batteries | Manage storage mode, reserve banks, input/output, and endurance. |
| Renewable Generation | Manage solar and wind generation, orientation, and efficiency. |
| Hydrogen Engines | Operate fuel-consuming generation under reserve policy. |
| Power Distribution | Control main, auxiliary, emergency, and isolated buses. |
| Load Shedding | Apply staged removal and restoration of nonessential loads. |
| Emergency Power | Perform black-start and survival-bus recovery. |
| Propulsion Overview | Show available thrust and acceleration by axis and engine family. |
| Thruster Groups | Inspect, test, isolate, and release thruster groups. |
| Fuel Feed | Manage hydrogen tank roles, feed paths, protected reserve, and leak isolation. |
| Attitude Control | Manage gyroscope availability and remove unsafe overrides. |
| Hydrogen Systems | Control production, storage, distribution, and endurance. |
| Oxygen Systems | Provide engineering control of oxygen plant and storage. |
| Conveyor Network | Find disconnected islands, blocked paths, and starved consumers. |
| Mechanical Systems | Operate named rotor, piston, hinge, crane, and deployment sequences. |
| Gravity Systems | Manage artificial gravity and mass-system availability. |
| Jump Drives | Charge, test, and release jump-drive groups to Navigation and Helm. |
| Thermal Management | Manage plugin-simulated heat sources, loops, radiators, and limits. |
| Shield Systems | Manage installed shield hardware, recharge, sectors, and power demand. |
| Structural Loads | Estimate conservative manoeuvre, landing, towing, and mechanism risk. |
| Maintenance and Diagnostics | Manage faults, lockouts, work orders, tests, and equipment release. |

## 5. Standard Operating Workflow

1. Assume engineering watch and review margin, fuel, faults, and lockouts
2. Set the plant posture requested by Operations
3. Use guided startup, shutdown, transfer, and black-start procedures
4. Diagnose root cause through dependencies and conveyors
5. Isolate, repair, test, and formally release equipment

## 6. Alarm Responsibilities

- Power reserve below minimum
- Critical bus lost
- Reactor fault or scram
- Hydrogen reserve/feed low
- Propulsion asymmetry
- Conveyor island starving critical consumer
- Jump drive late
- Thermal or shield overload

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

- Space Engineers block adapters
- Torch entity access
- Helm authority provider
- Life Support
- Damage Control
- Logistics
- Weapons feed
- Procedure engine

---

## Engineering Dashboard

**Purpose**

Summarise vessel technical capability and the limiting technical fault.

**What this page contains**

- Summarise vessel technical capability and the limiting technical fault.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Engineering Dashboard** from the Engineering navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Power Grid

**Purpose**

Display generation, consumption, reserve, buses, breakers, and load flow.

**What this page contains**

- Display generation, consumption, reserve, buses, breakers, and load flow.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Power Grid** from the Power navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Reactors

**Purpose**

Operate reactor groups through guarded startup, shutdown, and scram sequences.

**What this page contains**

- Operate reactor groups through guarded startup, shutdown, and scram sequences.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Reactors** from the Reactors navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Batteries

**Purpose**

Manage storage mode, reserve banks, input/output, and endurance.

**What this page contains**

- Manage storage mode, reserve banks, input/output, and endurance.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Batteries** from the Batteries navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Renewable Generation

**Purpose**

Manage solar and wind generation, orientation, and efficiency.

**What this page contains**

- Manage solar and wind generation, orientation, and efficiency.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Renewable Generation** from the Renewable navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hydrogen Engines

**Purpose**

Operate fuel-consuming generation under reserve policy.

**What this page contains**

- Operate fuel-consuming generation under reserve policy.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Hydrogen Engines** from the Hydrogen navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Power Distribution

**Purpose**

Control main, auxiliary, emergency, and isolated buses.

**What this page contains**

- Control main, auxiliary, emergency, and isolated buses.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Power Distribution** from the Power navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Load Shedding

**Purpose**

Apply staged removal and restoration of nonessential loads.

**What this page contains**

- Apply staged removal and restoration of nonessential loads.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Load Shedding** from the Load navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Emergency Power

**Purpose**

Perform black-start and survival-bus recovery.

**What this page contains**

- Perform black-start and survival-bus recovery.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Emergency Power** from the Emergency navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Propulsion Overview

**Purpose**

Show available thrust and acceleration by axis and engine family.

**What this page contains**

- Show available thrust and acceleration by axis and engine family.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Propulsion Overview** from the Propulsion navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Thruster Groups

**Purpose**

Inspect, test, isolate, and release thruster groups.

**What this page contains**

- Inspect, test, isolate, and release thruster groups.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Thruster Groups** from the Thruster navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fuel Feed

**Purpose**

Manage hydrogen tank roles, feed paths, protected reserve, and leak isolation.

**What this page contains**

- Manage hydrogen tank roles, feed paths, protected reserve, and leak isolation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Fuel Feed** from the Fuel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Attitude Control

**Purpose**

Manage gyroscope availability and remove unsafe overrides.

**What this page contains**

- Manage gyroscope availability and remove unsafe overrides.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Attitude Control** from the Attitude navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hydrogen Systems

**Purpose**

Control production, storage, distribution, and endurance.

**What this page contains**

- Control production, storage, distribution, and endurance.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Hydrogen Systems** from the Hydrogen navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Oxygen Systems

**Purpose**

Provide engineering control of oxygen plant and storage.

**What this page contains**

- Provide engineering control of oxygen plant and storage.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Oxygen Systems** from the Oxygen navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Conveyor Network

**Purpose**

Find disconnected islands, blocked paths, and starved consumers.

**What this page contains**

- Find disconnected islands, blocked paths, and starved consumers.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Conveyor Network** from the Conveyor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Mechanical Systems

**Purpose**

Operate named rotor, piston, hinge, crane, and deployment sequences.

**What this page contains**

- Operate named rotor, piston, hinge, crane, and deployment sequences.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Mechanical Systems** from the Mechanical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Gravity Systems

**Purpose**

Manage artificial gravity and mass-system availability.

**What this page contains**

- Manage artificial gravity and mass-system availability.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Gravity Systems** from the Gravity navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Jump Drives

**Purpose**

Charge, test, and release jump-drive groups to Navigation and Helm.

**What this page contains**

- Charge, test, and release jump-drive groups to Navigation and Helm.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Jump Drives** from the Jump navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Thermal Management

**Purpose**

Manage plugin-simulated heat sources, loops, radiators, and limits.

**What this page contains**

- Manage plugin-simulated heat sources, loops, radiators, and limits.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Thermal Management** from the Thermal navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Shield Systems

**Purpose**

Manage installed shield hardware, recharge, sectors, and power demand.

**What this page contains**

- Manage installed shield hardware, recharge, sectors, and power demand.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Shield Systems** from the Shield navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Structural Loads

**Purpose**

Estimate conservative manoeuvre, landing, towing, and mechanism risk.

**What this page contains**

- Estimate conservative manoeuvre, landing, towing, and mechanism risk.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Structural Loads** from the Structural navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Maintenance and Diagnostics

**Purpose**

Manage faults, lockouts, work orders, tests, and equipment release.

**What this page contains**

- Manage faults, lockouts, work orders, tests, and equipment release.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Generation, demand, reserve, and endurance
- Reactor and battery state
- Propulsion availability and fuel feed

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Maintenance and Diagnostics** from the Maintenance navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

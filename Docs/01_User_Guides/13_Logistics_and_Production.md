# Logistics and Production — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Know what the vessel has, where it is, how long it will last, what is needed, and how cargo, refining, assembly, transfer, salvage, and supply should operate.

## 2. Typical Roles

- Logistics Officer
- Quartermaster
- Production Officer
- Cargo Operator
- Supply Planner

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance
- Refinery and assembler state
- Production queue
- Loading and resupply operations

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Logistics Dashboard | Summarise stores, production, transfers, and supply risk. |
| Cargo Manifest | List item, quantity, mass, volume, location, access, and reservation. |
| Storage Locations | Map containers, capacity, category role, connection, and availability. |
| Inventory Search | Find specific items and valid transfer paths. |
| Ammunition Logistics | Manage stored, accessible, reserved, ready-demand, and production values. |
| Fuel Logistics | Manage ice, hydrogen, uranium, and configured fuel endurance and reserves. |
| Component Reserves | Protect repair and construction component minimums. |
| Ore and Ingot Reserves | Track raw and processed material and refinery demand. |
| Refinery Control | Coordinate refining jobs, inputs, outputs, yield, power, and path faults. |
| Assembler Control | Coordinate manufacturing queues and cooperative work. |
| Production Planning | Turn stock targets and forecast demand into bounded jobs. |
| Conveyor Distribution | Show logistics transfer intent and the physical path supplied by Engineering. |
| Automatic Stock Targets | Maintain minimum/preferred stock with cooldown and hysteresis. |
| Loading and Unloading | Execute journalled manifests between vessels or bases. |
| Supply Requests | Create, approve, fulfil, receive, and close strategic requests. |
| Salvage Intake | Inspect, quarantine, sort, and accept recovered material. |
| Waste and Scrap Processing | Manage stone, scrap, and configured waste without ejecting protected items. |
| Logistics History | Audit production, transfer, consumption, and discrepancies. |

## 5. Standard Operating Workflow

1. Review mission targets, reserves, and shortages
2. Confirm accessible rather than gross inventory
3. Generate and approve production/transfer plan
4. Monitor paths, power, and capacity
5. Reconcile completed work and discrepancies

## 6. Alarm Responsibilities

- Critical reserve low
- Cargo full
- Production blocked
- Inventory discrepancy
- Protected stock consumed
- Transfer destination full/disconnected
- Automatic rule oscillating

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

- Inventory/production blocks
- Engineering conveyors/power/fuel
- Weapons
- Damage Control
- Flight Operations
- Fleet supply
- Scenarium objectives

---

## Logistics Dashboard

**Purpose**

Summarise stores, production, transfers, and supply risk.

**What this page contains**

- Summarise stores, production, transfers, and supply risk.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Logistics Dashboard** from the Logistics navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Cargo Manifest

**Purpose**

List item, quantity, mass, volume, location, access, and reservation.

**What this page contains**

- List item, quantity, mass, volume, location, access, and reservation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Cargo Manifest** from the Cargo navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Storage Locations

**Purpose**

Map containers, capacity, category role, connection, and availability.

**What this page contains**

- Map containers, capacity, category role, connection, and availability.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Storage Locations** from the Storage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Inventory Search

**Purpose**

Find specific items and valid transfer paths.

**What this page contains**

- Find specific items and valid transfer paths.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Inventory Search** from the Inventory navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ammunition Logistics

**Purpose**

Manage stored, accessible, reserved, ready-demand, and production values.

**What this page contains**

- Manage stored, accessible, reserved, ready-demand, and production values.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Ammunition Logistics** from the Ammunition navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fuel Logistics

**Purpose**

Manage ice, hydrogen, uranium, and configured fuel endurance and reserves.

**What this page contains**

- Manage ice, hydrogen, uranium, and configured fuel endurance and reserves.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Fuel Logistics** from the Fuel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Component Reserves

**Purpose**

Protect repair and construction component minimums.

**What this page contains**

- Protect repair and construction component minimums.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Component Reserves** from the Component navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ore and Ingot Reserves

**Purpose**

Track raw and processed material and refinery demand.

**What this page contains**

- Track raw and processed material and refinery demand.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Ore and Ingot Reserves** from the Ore navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Refinery Control

**Purpose**

Coordinate refining jobs, inputs, outputs, yield, power, and path faults.

**What this page contains**

- Coordinate refining jobs, inputs, outputs, yield, power, and path faults.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Refinery Control** from the Refinery navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Assembler Control

**Purpose**

Coordinate manufacturing queues and cooperative work.

**What this page contains**

- Coordinate manufacturing queues and cooperative work.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Assembler Control** from the Assembler navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Production Planning

**Purpose**

Turn stock targets and forecast demand into bounded jobs.

**What this page contains**

- Turn stock targets and forecast demand into bounded jobs.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Production Planning** from the Production navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Conveyor Distribution

**Purpose**

Show logistics transfer intent and the physical path supplied by Engineering.

**What this page contains**

- Show logistics transfer intent and the physical path supplied by Engineering.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Conveyor Distribution** from the Conveyor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Automatic Stock Targets

**Purpose**

Maintain minimum/preferred stock with cooldown and hysteresis.

**What this page contains**

- Maintain minimum/preferred stock with cooldown and hysteresis.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Automatic Stock Targets** from the Automatic navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Loading and Unloading

**Purpose**

Execute journalled manifests between vessels or bases.

**What this page contains**

- Execute journalled manifests between vessels or bases.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Loading and Unloading** from the Loading navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Supply Requests

**Purpose**

Create, approve, fulfil, receive, and close strategic requests.

**What this page contains**

- Create, approve, fulfil, receive, and close strategic requests.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Supply Requests** from the Supply navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Salvage Intake

**Purpose**

Inspect, quarantine, sort, and accept recovered material.

**What this page contains**

- Inspect, quarantine, sort, and accept recovered material.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Salvage Intake** from the Salvage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Waste and Scrap Processing

**Purpose**

Manage stone, scrap, and configured waste without ejecting protected items.

**What this page contains**

- Manage stone, scrap, and configured waste without ejecting protected items.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Waste and Scrap Processing** from the Waste navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Logistics History

**Purpose**

Audit production, transfer, consumption, and discrepancies.

**What this page contains**

- Audit production, transfer, consumption, and discrepancies.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Cargo capacity and distribution
- Critical shortages and protected reserves
- Fuel, ammunition, components, ore, and ingot endurance

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Logistics History** from the Logistics navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

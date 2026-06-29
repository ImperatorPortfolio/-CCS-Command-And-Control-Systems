# External Systems and Docking — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Control docking ports, connectors, landing gear, airlocks, external lights, antennas, ramps, cranes, EVA support, umbilicals, towing, and attached-vessel interfaces.

## 2. Typical Roles

- Docking Officer
- External Systems Operator
- EVA Controller
- Cargo Transfer Operator

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms
- Lights, cameras, antennas, beacons
- EVA activity
- Towing and umbilicals

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| External Systems Dashboard | Summarise all hull-external interfaces and hazards. |
| Docking Port Control | Manage port identity, reservation, geometry, clearance, capture, and disconnect. |
| Docked Vessel Registry | Track attached vessel identity, ownership, services, and authority boundary. |
| Connector Transfer | Control manifest-linked item transfer and reconciliation. |
| External Power | Provide or receive bounded power under Engineering bus limits. |
| Fuel Umbilicals | Transfer fuel with source, destination, reserve, and emergency isolation. |
| Cargo Umbilicals | Deploy, lock, transfer, and retract dedicated cargo mechanisms. |
| Airlock Control | Operate external personnel access through Life Support rules. |
| Landing Gear | Manage deployment, contact, lock, load, unlock clearance, and retraction. |
| External Lighting | Control navigation, docking, work, search, and identification lighting. |
| Antennas and Beacons | Manage external identity and communications hardware profiles. |
| Ramps and Elevators | Run named deployment/stow sequences with obstruction checks. |
| Cranes and Manipulators | Operate bounded cargo and maintenance mechanisms. |
| EVA Support | Authorise, monitor, recall, and rescue external personnel using available telemetry. |
| Hull Cameras | Route and aim external operation feeds. |
| Towing and Salvage | Manage attachment, load, route, ownership, quarantine, and release. |
| External Maintenance Lockout | Protect workers from thrusters, mechanisms, doors, and automation. |

## 5. Standard Operating Workflow

1. Identify peer/task and reserve interface
2. Verify geometry, pressure, ownership, people, and mechanism
3. Run named sequence
4. Monitor load and transaction
5. Secure, reconcile, and release lockouts

## 6. Alarm Responsibilities

- Unexpected disconnect
- Port conflict/unregistered vessel
- Transfer exceeds limit
- Mechanism moves in hazard zone
- EVA overdue/lost
- Landing gear fault
- Maintenance lockout violation

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

- Helm docking
- Navigation approaches
- Flight Operations
- Engineering
- Life Support
- Logistics
- Security identity

---

## External Systems Dashboard

**Purpose**

Summarise all hull-external interfaces and hazards.

**What this page contains**

- Summarise all hull-external interfaces and hazards.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **External Systems Dashboard** from the External navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Docking Port Control

**Purpose**

Manage port identity, reservation, geometry, clearance, capture, and disconnect.

**What this page contains**

- Manage port identity, reservation, geometry, clearance, capture, and disconnect.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Docking Port Control** from the Docking navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Docked Vessel Registry

**Purpose**

Track attached vessel identity, ownership, services, and authority boundary.

**What this page contains**

- Track attached vessel identity, ownership, services, and authority boundary.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Docked Vessel Registry** from the Docked navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Connector Transfer

**Purpose**

Control manifest-linked item transfer and reconciliation.

**What this page contains**

- Control manifest-linked item transfer and reconciliation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Connector Transfer** from the Connector navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## External Power

**Purpose**

Provide or receive bounded power under Engineering bus limits.

**What this page contains**

- Provide or receive bounded power under Engineering bus limits.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **External Power** from the External navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fuel Umbilicals

**Purpose**

Transfer fuel with source, destination, reserve, and emergency isolation.

**What this page contains**

- Transfer fuel with source, destination, reserve, and emergency isolation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Fuel Umbilicals** from the Fuel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Cargo Umbilicals

**Purpose**

Deploy, lock, transfer, and retract dedicated cargo mechanisms.

**What this page contains**

- Deploy, lock, transfer, and retract dedicated cargo mechanisms.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Cargo Umbilicals** from the Cargo navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Airlock Control

**Purpose**

Operate external personnel access through Life Support rules.

**What this page contains**

- Operate external personnel access through Life Support rules.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Airlock Control** from the Airlock navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Landing Gear

**Purpose**

Manage deployment, contact, lock, load, unlock clearance, and retraction.

**What this page contains**

- Manage deployment, contact, lock, load, unlock clearance, and retraction.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Landing Gear** from the Landing navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## External Lighting

**Purpose**

Control navigation, docking, work, search, and identification lighting.

**What this page contains**

- Control navigation, docking, work, search, and identification lighting.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **External Lighting** from the External navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Antennas and Beacons

**Purpose**

Manage external identity and communications hardware profiles.

**What this page contains**

- Manage external identity and communications hardware profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Antennas and Beacons** from the Antennas navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ramps and Elevators

**Purpose**

Run named deployment/stow sequences with obstruction checks.

**What this page contains**

- Run named deployment/stow sequences with obstruction checks.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Ramps and Elevators** from the Ramps navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Cranes and Manipulators

**Purpose**

Operate bounded cargo and maintenance mechanisms.

**What this page contains**

- Operate bounded cargo and maintenance mechanisms.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Cranes and Manipulators** from the Cranes navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## EVA Support

**Purpose**

Authorise, monitor, recall, and rescue external personnel using available telemetry.

**What this page contains**

- Authorise, monitor, recall, and rescue external personnel using available telemetry.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **EVA Support** from the EVA navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hull Cameras

**Purpose**

Route and aim external operation feeds.

**What this page contains**

- Route and aim external operation feeds.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Hull Cameras** from the Hull navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Towing and Salvage

**Purpose**

Manage attachment, load, route, ownership, quarantine, and release.

**What this page contains**

- Manage attachment, load, route, ownership, quarantine, and release.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Towing and Salvage** from the Towing navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## External Maintenance Lockout

**Purpose**

Protect workers from thrusters, mechanisms, doors, and automation.

**What this page contains**

- Protect workers from thrusters, mechanisms, doors, and automation.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Port and connector availability
- Connected vessels and transfer links
- Airlocks, gear, ramps, mechanisms

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **External Maintenance Lockout** from the External navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

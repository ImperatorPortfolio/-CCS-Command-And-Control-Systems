# CommandShellOS — Master System Overview

**Product:** CommandShellOS for Space Engineers  
**Document type:** Product and information architecture  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Product Definition

CommandShellOS is a server-authoritative starship operating and command environment for Space Engineers. LCD surfaces provide the terminal interface and virtual pointer; Torch/server services own validation, simulation, persistence, equipment actuation, and verification.

It is not a skin over the vanilla terminal. It is a coherent vessel model in which stations, departments, authority, procedures, alarms, resources, damage, sensing, and campaign integrations interact through explicit contracts.

## 2. Governing Principles

1. One vessel has one authoritative operational state.
2. Consoles are role-specific views over that state.
3. Operators request outcomes; the server validates and executes.
4. Commanded state and verified actual state are always distinct.
5. Important transitions are validated, logged, evented, persisted, and inspectable.
6. Automation uses the same commands and permissions as human operators.
7. Scenarium owns campaign state; CommandShellOS owns vessel-local operational state.
8. MES owns encounter spawning and NPC behaviour.
9. No UI element may imply capability the server cannot verify.

## 3. Command Hierarchy

| Layer | Responsibility | Example |
| --- | --- | --- |
| Strategic Command | Mission intent and posture | Withdraw, defend, escort |
| Operational Coordination | Department priorities and schedule | Prioritise jump readiness |
| Department Control | Functional-domain decisions | Start emergency generation |
| Subsystem Control | Equipment-group coordination | Transfer an auxiliary bus |
| Equipment Execution | Actual block/plugin actuation | Set battery mode and verify |

## 4. Authority Levels

| Level | Name | Typical authority |
| --- | --- | --- |
| 0 | Observer | Public/read-only information |
| 1 | Crew | Routine local actions |
| 2 | Operator | Assigned department operation |
| 3 | Department Head | Department configuration and override |
| 4 | Executive Command | Shipwide priorities and coordination |
| 5 | Commanding Officer | Mission, ROE, alert condition |
| 6 | Emergency Authority | Multi-party destructive/exceptional action |
| 7 | Administration | Configuration, recovery, diagnostics |

## 5. Command Lifecycle

```text
Requested → Authorised → Queued → Executing → Verifying → Completed
```

Alternative terminal states: Blocked, Rejected, Partial, Failed, Cancelled, TimedOut, and Superseded.

## 6. Vessel Model

```text
Vessel
 ├── Deck → Zone → Compartment
 ├── Department → System → Subsystem → Equipment Group → Device
 └── Attached Vessel or Craft
```

Docked craft retain separate vessel identity and authority.

## 7. Equipment States

Offline, Standby, Starting, Online, Degraded, Fault, Isolated, Locked Out, Destroyed, and Unknown.

## 8. Readiness Conditions

White (maintenance), Green (normal), Blue (flight operations), Yellow (elevated readiness), Red (general quarters), and Black (catastrophic survival). Every transition is a versioned procedure with preview, ordered actions, timeouts, exceptions, and completion report.

## 9. Shared UI

All screens use a vessel/operator/alert header, category rail, breadcrumbs, workspace, context inspector, alarm strip, command bar, search, favourites, history, page routing, data-quality indicators, and pending-command state.

## 10. Top-Level Areas

| No. | Area | Responsibility |
| --- | --- | --- |
| 1 | Command | Give the Captain and executive officers an authoritative picture of mission status, vessel readiness, risk, and decisions requiring command authority. |
| 2 | Operations | Coordinate departments, resources, procedures, and timelines so command intent becomes executable ship activity. |
| 3 | Engineering | Operate, protect, diagnose, and restore power, propulsion support, resource distribution, mechanical, thermal, jump, shield, and structural systems. |
| 4 | Helm | Control immediate vessel motion safely within Navigation routes, Command intent, and Engineering control authority. |
| 5 | Navigation | Determine where the vessel is, where it should go, how it will get there, and what hazards or uncertainty affect that plan. |
| 6 | Tactical | Build the combat picture, assess threats, choose engagement plans, coordinate defence, and translate command rules into tactical intent. |
| 7 | Weapons | Prepare, aim, authorise, fire, reload, safe, and diagnose weapon systems while enforcing authority, arcs, ammunition, and friendly-fire protections. |
| 8 | Sensors | Create a server-authoritative, uncertain, multi-source contact picture using detection, correlation, classification, confidence, and emission trade-offs. |
| 9 | Security | Protect personnel, compartments, armories, computers, and command functions from unauthorised access, boarding, sabotage, and internal threats. |
| 10 | Communications | Provide authenticated, prioritised voice-equivalent, message, distress, broadcast, relay, datalink, and remote-command channels. |
| 11 | Damage Control | Detect, contain, prioritise, repair, and recover from hull, atmosphere, structural, power, fire-simulation, and system casualties by compartment. |
| 12 | Flight Operations | Manage attached craft, hangars, launch and recovery traffic, servicing, pilots, rescue readiness, and flight-deck safety. |
| 13 | Logistics and Production | Know what the vessel has, where it is, how long it will last, what is needed, and how cargo, refining, assembly, transfer, salvage, and supply should operate. |
| 14 | Life Support and Habitation | Maintain breathable, pressurised, powered, habitable compartments and provide credible survival endurance, airlock, gravity, lighting, and optional consumables management. |
| 15 | Medical | Coordinate gameplay-level casualty discovery, triage, treatment capacity, supplies, evacuation, quarantine, and environmental exposure without pretending to be a clinical simulator. |
| 16 | Science and Survey | Plan and execute planetary, asteroid, environmental, resource, anomaly, and mission-science surveys while preserving provenance, coverage, resolution, and age. |
| 17 | Computer, Automation and Drones | Operate CommandShellOS core services, visible automation, failover, scheduling, autonomous craft, network health, data storage, and cybersecurity boundaries. |
| 18 | External Systems and Docking | Control docking ports, connectors, landing gear, airlocks, external lights, antennas, ramps, cranes, EVA support, umbilicals, towing, and attached-vessel interfaces. |
| 19 | Fleet and Strategic Operations | Coordinate multiple vessels and installations, formations, fleet orders, shared navigation and contacts, synchronised jumps, supply, territory, and campaign objectives without duplicating Scenarium authority. |
| 20 | Administration and Configuration | Configure vessel identity, topology, departments, equipment groups, permissions, page definitions, integrations, diagnostics, backups, migrations, and safe mode without exposing administrative power as gameplay. |

## 11. Core Services

Vessel Registry, Topology Service, Equipment Registry, Telemetry/Projection Service, Command Bus, Authority/Station Service, Alarm Service, Procedure Engine, Rules Engine, Sensor Fusion, Logistics, Crew/Watch, Damage/Recovery, Persistence/Migration, Audit, Client Subscription, and Scenarium Gateway.

## 12. Recommended Solution Structure

```text
CommandShellOS.Contracts
CommandShellOS.Domain
CommandShellOS.Persistence
CommandShellOS.Server
CommandShellOS.Integrations
CommandShellOS.Torch
CommandShellOS.ClientMod
CommandShellOS.UI
CommandShellOS.Tools
CommandShellOS.Tests
```

## 13. Development Stages

1. Platform foundation.
2. Engineering and Operations.
3. Helm, Navigation, External/Docking.
4. Sensors, Tactical, Weapons.
5. Security, Communications, Damage Control, Life Support, Medical.
6. Flight Operations, Logistics, Science, Automation/Drones.
7. Fleet and Scenarium integration.
8. Product hardening, performance, migration, localisation, and diagnostics.

No stage advances until its acceptance criteria pass on a representative configured vessel.

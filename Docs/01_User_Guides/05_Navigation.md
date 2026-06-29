# Navigation — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Determine where the vessel is, where it should go, how it will get there, and what hazards or uncertainty affect that plan.

## 2. Typical Roles

- Navigator
- Astrogator
- Mission Planner
- Survey Navigator

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards
- Jump range and arrival uncertainty
- Rendezvous and approach state
- Navigation-sensor health

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Navigation Dashboard | Show position confidence, route, destination, next manoeuvre, and hazards. |
| Local Navigation Plot | Display nearby geometry, vectors, waypoints, contacts, and uncertainty. |
| Strategic Map | Provide planetary, orbital, and system-scale planning. |
| Route Planner | Build and validate constrained multi-leg routes. |
| Jump Planning | Prepare range, mass, charge, exclusion, and arrival-volume solutions. |
| Rendezvous | Compute intercept and relative-velocity matching options. |
| Orbital Operations | Support clearly labelled plugin-simulated orbit and burn planning. |
| Planetary Navigation | Use coordinates, terrain, slope, atmosphere, and landing areas. |
| Hazard Analysis | Evaluate asteroids, gravity, debris, hostile zones, and minefields. |
| Waypoint Database | Manage stable, shared, private, tactical, and mission waypoints. |
| Approach Procedures | Store versioned standard approaches for stations, carriers, and bases. |
| Navigation Sensors | Show sources, quality, disagreement, and calibration. |
| Course History | Review track, jumps, route deviation, and docking history. |
| Exclusion Zones | Manage approved no-fly and restricted volumes. |

## 5. Standard Operating Workflow

1. Verify position confidence
2. Build a constrained route
3. Validate each leg and resolve hazards
4. Submit plan to Command and Helm
5. Monitor actual track and publish revisions

## 6. Alarm Responsibilities

- Position confidence low
- Route enters hazard/restricted volume
- Jump arrival invalid or occupied
- Navigation sources disagree
- Route deviation excessive
- Approach procedure outdated

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

- Sensor fusion
- Helm autopilot
- Engineering jump readiness
- Scenarium strategic map
- Communications waypoint exchange
- Flight traffic

---

## Navigation Dashboard

**Purpose**

Show position confidence, route, destination, next manoeuvre, and hazards.

**What this page contains**

- Show position confidence, route, destination, next manoeuvre, and hazards.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Navigation Dashboard** from the Navigation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Local Navigation Plot

**Purpose**

Display nearby geometry, vectors, waypoints, contacts, and uncertainty.

**What this page contains**

- Display nearby geometry, vectors, waypoints, contacts, and uncertainty.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Local Navigation Plot** from the Local navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Strategic Map

**Purpose**

Provide planetary, orbital, and system-scale planning.

**What this page contains**

- Provide planetary, orbital, and system-scale planning.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Strategic Map** from the Strategic navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Route Planner

**Purpose**

Build and validate constrained multi-leg routes.

**What this page contains**

- Build and validate constrained multi-leg routes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Route Planner** from the Route navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Jump Planning

**Purpose**

Prepare range, mass, charge, exclusion, and arrival-volume solutions.

**What this page contains**

- Prepare range, mass, charge, exclusion, and arrival-volume solutions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Jump Planning** from the Jump navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Rendezvous

**Purpose**

Compute intercept and relative-velocity matching options.

**What this page contains**

- Compute intercept and relative-velocity matching options.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Rendezvous** from the Rendezvous navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Orbital Operations

**Purpose**

Support clearly labelled plugin-simulated orbit and burn planning.

**What this page contains**

- Support clearly labelled plugin-simulated orbit and burn planning.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Orbital Operations** from the Orbital navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Planetary Navigation

**Purpose**

Use coordinates, terrain, slope, atmosphere, and landing areas.

**What this page contains**

- Use coordinates, terrain, slope, atmosphere, and landing areas.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Planetary Navigation** from the Planetary navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Hazard Analysis

**Purpose**

Evaluate asteroids, gravity, debris, hostile zones, and minefields.

**What this page contains**

- Evaluate asteroids, gravity, debris, hostile zones, and minefields.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Hazard Analysis** from the Hazard navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Waypoint Database

**Purpose**

Manage stable, shared, private, tactical, and mission waypoints.

**What this page contains**

- Manage stable, shared, private, tactical, and mission waypoints.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Waypoint Database** from the Waypoint navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Approach Procedures

**Purpose**

Store versioned standard approaches for stations, carriers, and bases.

**What this page contains**

- Store versioned standard approaches for stations, carriers, and bases.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Approach Procedures** from the Approach navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Navigation Sensors

**Purpose**

Show sources, quality, disagreement, and calibration.

**What this page contains**

- Show sources, quality, disagreement, and calibration.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Navigation Sensors** from the Navigation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Course History

**Purpose**

Review track, jumps, route deviation, and docking history.

**What this page contains**

- Review track, jumps, route deviation, and docking history.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Course History** from the Course navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Exclusion Zones

**Purpose**

Manage approved no-fly and restricted volumes.

**What this page contains**

- Manage approved no-fly and restricted volumes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Current position and confidence
- Destination, route, and ETA
- Nearby bodies and hazards

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Exclusion Zones** from the Exclusion navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

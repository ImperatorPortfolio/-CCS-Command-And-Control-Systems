# Helm — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Control immediate vessel motion safely within Navigation routes, Command intent, and Engineering control authority.

## 2. Typical Roles

- Helmsman
- Pilot
- Docking Officer
- Command Pilot

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course
- Available thrust and gyro authority
- Braking distance and collision warnings
- Autopilot, dampener, formation, and docking state

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Helm Dashboard | Provide the complete immediate flight picture. |
| Flight Control | Manage control source, dampeners, speed limits, and flight mode. |
| Attitude | Display and command heading, pitch, roll, angular velocity, and vector alignment. |
| Thrust Management | Show thrust by axis and select approved boost or degraded profiles. |
| Flight Envelope | Display safe acceleration, turn, speed, braking, and terrain margins. |
| Inertial Dampening | Manage velocity hold and reference frame. |
| Atmospheric Flight | Support altitude, vertical speed, terrain clearance, and thrust-margin control. |
| Landing | Execute a corridor-based approach, gear deployment, touchdown, or wave-off. |
| Takeoff | Run clearance, lift, departure, and gear-retraction sequence. |
| Docking Control | Manage relative position, alignment, closure, capture, and abort. |
| Formation Flying | Maintain a shared relative slot with separation and breakaway. |
| Emergency Manoeuvres | Provide collision avoidance, emergency stop, and bounded evasive presets. |
| Helm Autopilot | Execute Navigation routes while monitoring deviation and abort conditions. |
| Control Transfer | Transfer helm through a positive request-and-accept handover. |

## 5. Standard Operating Workflow

1. Take helm through positive handover
2. Verify thrust and attitude authority
3. Load an approved route
4. Monitor envelope, collision margin, and automation deviation
5. Abort unsafe automation immediately

## 6. Alarm Responsibilities

- Loss of control authority
- Insufficient braking distance
- Collision predicted
- Atmospheric margin inadequate
- Docking limit exceeded
- Autopilot deviation
- Unexpected control transfer

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

- Engineering propulsion
- Navigation routes
- Sensors obstacles
- Flight Operations clearances
- Tactical manoeuvre requests
- Ship controller adapter

---

## Helm Dashboard

**Purpose**

Provide the complete immediate flight picture.

**What this page contains**

- Provide the complete immediate flight picture.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Helm Dashboard** from the Helm navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Flight Control

**Purpose**

Manage control source, dampeners, speed limits, and flight mode.

**What this page contains**

- Manage control source, dampeners, speed limits, and flight mode.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Flight Control** from the Flight navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Attitude

**Purpose**

Display and command heading, pitch, roll, angular velocity, and vector alignment.

**What this page contains**

- Display and command heading, pitch, roll, angular velocity, and vector alignment.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Attitude** from the Attitude navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Thrust Management

**Purpose**

Show thrust by axis and select approved boost or degraded profiles.

**What this page contains**

- Show thrust by axis and select approved boost or degraded profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Thrust Management** from the Thrust navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Flight Envelope

**Purpose**

Display safe acceleration, turn, speed, braking, and terrain margins.

**What this page contains**

- Display safe acceleration, turn, speed, braking, and terrain margins.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Flight Envelope** from the Flight navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Inertial Dampening

**Purpose**

Manage velocity hold and reference frame.

**What this page contains**

- Manage velocity hold and reference frame.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Inertial Dampening** from the Inertial navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Atmospheric Flight

**Purpose**

Support altitude, vertical speed, terrain clearance, and thrust-margin control.

**What this page contains**

- Support altitude, vertical speed, terrain clearance, and thrust-margin control.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Atmospheric Flight** from the Atmospheric navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Landing

**Purpose**

Execute a corridor-based approach, gear deployment, touchdown, or wave-off.

**What this page contains**

- Execute a corridor-based approach, gear deployment, touchdown, or wave-off.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Landing** from the Landing navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Takeoff

**Purpose**

Run clearance, lift, departure, and gear-retraction sequence.

**What this page contains**

- Run clearance, lift, departure, and gear-retraction sequence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Takeoff** from the Takeoff navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Docking Control

**Purpose**

Manage relative position, alignment, closure, capture, and abort.

**What this page contains**

- Manage relative position, alignment, closure, capture, and abort.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Docking Control** from the Docking navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Formation Flying

**Purpose**

Maintain a shared relative slot with separation and breakaway.

**What this page contains**

- Maintain a shared relative slot with separation and breakaway.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Formation Flying** from the Formation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Emergency Manoeuvres

**Purpose**

Provide collision avoidance, emergency stop, and bounded evasive presets.

**What this page contains**

- Provide collision avoidance, emergency stop, and bounded evasive presets.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Emergency Manoeuvres** from the Emergency navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Helm Autopilot

**Purpose**

Execute Navigation routes while monitoring deviation and abort conditions.

**What this page contains**

- Execute Navigation routes while monitoring deviation and abort conditions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Helm Autopilot** from the Helm navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Control Transfer

**Purpose**

Transfer helm through a positive request-and-accept handover.

**What this page contains**

- Transfer helm through a positive request-and-accept handover.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Velocity, acceleration, and attitude
- Gravity and altitude
- Current course

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Control Transfer** from the Control navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

# Tactical — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Build the combat picture, assess threats, choose engagement plans, coordinate defence, and translate command rules into tactical intent.

## 2. Typical Roles

- Tactical Officer
- Combat Systems Officer
- Threat Analyst
- Electronic Warfare Officer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence
- Own combat readiness
- Weapon options
- Defensive posture
- Rules of engagement

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Tactical Dashboard | Present the immediate combat decision picture. |
| Tactical Plot | Display tracks, vectors, ranges, uncertainty, sectors, and weapon envelopes. |
| Threat Assessment | Rank contacts using capability, intent, proximity, and evidence. |
| Target Management | Create, prioritise, share, protect, and release formal target designations. |
| Engagement Planning | Assign desired effect, weapon options, manoeuvre, timing, and authority. |
| Defensive Coordination | Coordinate point defence, shields, armour orientation, decoys, and manoeuvre. |
| Electronic Warfare | Plan jamming, deception, and emission-control effects. |
| Countermeasures | Manage expendable and active defensive patterns. |
| Tactical Zones | Define fire sectors, defended assets, and protected volumes. |
| Damage Projection | Estimate own and opponent combat capability with confidence bands. |
| Battle Timeline | Record track changes, shots, hits, damage, and orders. |
| ROE Compliance | Explain exactly why an engagement is permitted or blocked. |
| Combat Automation | Configure bounded defensive or command-approved doctrine. |

## 5. Standard Operating Workflow

1. Build a confidence-qualified contact picture
2. Confirm ROE and designate targets
3. Create an engagement plan with other departments
4. Execute while monitoring safety and endurance
5. Release obsolete targets and document the battle

## 6. Alarm Responsibilities

- Hostile contact inside boundary
- Incoming threat
- ROE conflict
- Friendly in firing solution
- Target track stale
- Point-defence gap
- EW emission exceeds posture

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
- Weapons fire control
- Helm manoeuvre
- Engineering shields/power
- Command ROE
- Communications datalink

---

## Tactical Dashboard

**Purpose**

Present the immediate combat decision picture.

**What this page contains**

- Present the immediate combat decision picture.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Tactical Dashboard** from the Tactical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Tactical Plot

**Purpose**

Display tracks, vectors, ranges, uncertainty, sectors, and weapon envelopes.

**What this page contains**

- Display tracks, vectors, ranges, uncertainty, sectors, and weapon envelopes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Tactical Plot** from the Tactical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Threat Assessment

**Purpose**

Rank contacts using capability, intent, proximity, and evidence.

**What this page contains**

- Rank contacts using capability, intent, proximity, and evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Threat Assessment** from the Threat navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Target Management

**Purpose**

Create, prioritise, share, protect, and release formal target designations.

**What this page contains**

- Create, prioritise, share, protect, and release formal target designations.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Target Management** from the Target navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Engagement Planning

**Purpose**

Assign desired effect, weapon options, manoeuvre, timing, and authority.

**What this page contains**

- Assign desired effect, weapon options, manoeuvre, timing, and authority.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Create a draft, add constraints and dependencies, validate it, resolve blockers, publish a version, and submit it to the executing station.

**How to use it**

Open **Engagement Planning** from the Engagement navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Defensive Coordination

**Purpose**

Coordinate point defence, shields, armour orientation, decoys, and manoeuvre.

**What this page contains**

- Coordinate point defence, shields, armour orientation, decoys, and manoeuvre.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Defensive Coordination** from the Defensive navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Electronic Warfare

**Purpose**

Plan jamming, deception, and emission-control effects.

**What this page contains**

- Plan jamming, deception, and emission-control effects.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Electronic Warfare** from the Electronic navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Countermeasures

**Purpose**

Manage expendable and active defensive patterns.

**What this page contains**

- Manage expendable and active defensive patterns.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Countermeasures** from the Countermeasures navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Tactical Zones

**Purpose**

Define fire sectors, defended assets, and protected volumes.

**What this page contains**

- Define fire sectors, defended assets, and protected volumes.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Tactical Zones** from the Tactical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Damage Projection

**Purpose**

Estimate own and opponent combat capability with confidence bands.

**What this page contains**

- Estimate own and opponent combat capability with confidence bands.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Damage Projection** from the Damage navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Battle Timeline

**Purpose**

Record track changes, shots, hits, damage, and orders.

**What this page contains**

- Record track changes, shots, hits, damage, and orders.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Battle Timeline** from the Battle navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## ROE Compliance

**Purpose**

Explain exactly why an engagement is permitted or blocked.

**What this page contains**

- Explain exactly why an engagement is permitted or blocked.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **ROE Compliance** from the ROE navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Combat Automation

**Purpose**

Configure bounded defensive or command-approved doctrine.

**What this page contains**

- Configure bounded defensive or command-approved doctrine.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Tactical plot and geometry
- Threat-ranked contacts
- Selected target and confidence

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Combat Automation** from the Combat navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

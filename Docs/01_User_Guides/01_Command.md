# Command — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Give the Captain and executive officers an authoritative picture of mission status, vessel readiness, risk, and decisions requiring command authority.

## 2. Typical Roles

- Commanding Officer
- Executive Officer
- Duty Commander
- Flag Officer
- Observer

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies
- Navigation destination and ETA
- Primary tactical contacts and rules of engagement
- Hull, power, propulsion, atmosphere, crew, and ammunition summary
- Pending authorisations and critical alarms

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Command Dashboard | Condense the shipwide situation into a decision-focused bridge view. |
| Mission Overview | Track mission intent, objectives, constraints, deadlines, and phase. |
| Ship Readiness | Explain whether the vessel can perform the ordered mission and why not. |
| Command Orders | Issue traceable orders with owners, priorities, acknowledgements, and completion evidence. |
| Alert Conditions | Set the shipwide readiness condition through a previewed transition procedure. |
| Rules of Engagement | Define when force may be used and which contacts or zones are protected. |
| Crew and Watch | Assign bridge roles and record positive watch handovers. |
| Resource Priorities | Set shipwide power, fuel, repair, sensor, and ammunition priorities without micromanaging equipment. |
| Command Authorisations | Approve or reject protected actions with exact consequence and target binding. |
| Incident Timeline | Review a unified chronology of contacts, commands, alarms, damage, and decisions. |
| Captain's Log | Record command intent and the reasons behind significant decisions. |
| Abandon Ship | Coordinate muster, escape craft, routes, and distress traffic. |
| Scuttle and Self-Destruct | Provide a multi-party, guarded last-resort denial sequence. |
| Main Viewscreen | Route tactical, navigation, camera, damage, communications, or mission views to shared displays. |

## 5. Standard Operating Workflow

1. Assume command and complete watch handover
2. Review mission, readiness, and critical alarms
3. Set alert condition and rules of engagement
4. Issue orders with named owners and completion criteria
5. Approve or reject protected requests
6. Review the incident timeline after major events

## 6. Alarm Responsibilities

- Loss of command core
- Mandatory bridge station unstaffed
- Department not ready for ordered operation
- ROE conflict
- Unauthorised command-level action
- Scuttle chain armed or authorisation lost

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

- Operations tasking
- All department summaries
- Scenarium mission gateway
- Communications
- Audit service
- Main viewscreen router

---

## Command Dashboard

**Purpose**

Condense the shipwide situation into a decision-focused bridge view.

**What this page contains**

- Condense the shipwide situation into a decision-focused bridge view.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Command Dashboard** from the Command navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Mission Overview

**Purpose**

Track mission intent, objectives, constraints, deadlines, and phase.

**What this page contains**

- Track mission intent, objectives, constraints, deadlines, and phase.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Mission Overview** from the Mission navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Ship Readiness

**Purpose**

Explain whether the vessel can perform the ordered mission and why not.

**What this page contains**

- Explain whether the vessel can perform the ordered mission and why not.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Ship Readiness** from the Ship navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Command Orders

**Purpose**

Issue traceable orders with owners, priorities, acknowledgements, and completion evidence.

**What this page contains**

- Issue traceable orders with owners, priorities, acknowledgements, and completion evidence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Command Orders** from the Command navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Alert Conditions

**Purpose**

Set the shipwide readiness condition through a previewed transition procedure.

**What this page contains**

- Set the shipwide readiness condition through a previewed transition procedure.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Alert Conditions** from the Alert navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Rules of Engagement

**Purpose**

Define when force may be used and which contacts or zones are protected.

**What this page contains**

- Define when force may be used and which contacts or zones are protected.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Rules of Engagement** from the Rules navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Crew and Watch

**Purpose**

Assign bridge roles and record positive watch handovers.

**What this page contains**

- Assign bridge roles and record positive watch handovers.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Crew and Watch** from the Crew navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Resource Priorities

**Purpose**

Set shipwide power, fuel, repair, sensor, and ammunition priorities without micromanaging equipment.

**What this page contains**

- Set shipwide power, fuel, repair, sensor, and ammunition priorities without micromanaging equipment.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Resource Priorities** from the Resource navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Command Authorisations

**Purpose**

Approve or reject protected actions with exact consequence and target binding.

**What this page contains**

- Approve or reject protected actions with exact consequence and target binding.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Command Authorisations** from the Command navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Incident Timeline

**Purpose**

Review a unified chronology of contacts, commands, alarms, damage, and decisions.

**What this page contains**

- Review a unified chronology of contacts, commands, alarms, damage, and decisions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Incident Timeline** from the Incident navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Captain's Log

**Purpose**

Record command intent and the reasons behind significant decisions.

**What this page contains**

- Record command intent and the reasons behind significant decisions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Captain's Log** from the Captain's navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Abandon Ship

**Purpose**

Coordinate muster, escape craft, routes, and distress traffic.

**What this page contains**

- Coordinate muster, escape craft, routes, and distress traffic.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Abandon Ship** from the Abandon navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Scuttle and Self-Destruct

**Purpose**

Provide a multi-party, guarded last-resort denial sequence.

**What this page contains**

- Provide a multi-party, guarded last-resort denial sequence.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Scuttle and Self-Destruct** from the Scuttle navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Main Viewscreen

**Purpose**

Route tactical, navigation, camera, damage, communications, or mission views to shared displays.

**What this page contains**

- Route tactical, navigation, camera, damage, communications, or mission views to shared displays.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Mission phase and active objectives
- Shipwide alert condition
- Department readiness and limiting deficiencies

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Main Viewscreen** from the Main navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

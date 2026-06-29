# Communications — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Provide authenticated, prioritised voice-equivalent, message, distress, broadcast, relay, datalink, and remote-command channels.

## 2. Typical Roles

- Communications Officer
- Signal Officer
- Datalink Operator
- Command Broadcaster

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Active channels
- Antenna and relay health
- Priority incoming traffic
- Encryption/authentication
- Datalink peers and latency
- Distress traffic and emission posture

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Communications Dashboard | Summarise links, channels, traffic, encryption, emissions, and faults. |
| Channel Control | Manage named logical channels and their members, medium, priority, and security. |
| Direct Communications | Establish authenticated ship-to-ship sessions. |
| Fleet Communications | Operate command, tactical, logistics, and support nets. |
| Command Broadcast | Send priority orders or announcements with acknowledgement and expiry. |
| Distress and Emergency | Transmit, receive, relay, and assign distress traffic. |
| Antenna Network | Manage hardware range, role, power, identity broadcast, and signature. |
| Relay Routing | Select multi-hop routes by quality, latency, trust, and failure state. |
| Encryption and Authentication | Manage peer trust, challenge, session identity, and operational key version. |
| Tactical Datalink | Exchange tracks, targets, routes, orders, and status with provenance. |
| Identification Challenge | Perform structured challenge-and-reply without treating failure as automatic hostility. |
| Message Queue | Deliver reliable, prioritised, expiring, idempotent messages. |
| Recorded Transmissions | Review authorised communication history. |
| Public Address | Send zone-based internal announcements. |
| Automated Announcements | Configure rate-limited event-driven announcements. |
| Remote Command Sessions | Grant least-privilege, expiring remote command scope. |

## 5. Standard Operating Workflow

1. Verify hardware, command net, distress guard, and encryption
2. Authenticate important peers
3. Use structured messages for operational objects
4. Monitor latency, age, and trust
5. Terminate and audit suspicious sessions

## 6. Alarm Responsibilities

- Command/distress channel lost
- Authentication failure
- Datalink stale
- Unexpected high-power emission
- Relay compromised or looping
- Remote command outside scope
- Message expires undelivered

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

- Antenna/laser/beacon
- Torch networking
- Sensors signature
- Tactical datalink
- Command orders
- Fleet and Scenarium

---

## Communications Dashboard

**Purpose**

Summarise links, channels, traffic, encryption, emissions, and faults.

**What this page contains**

- Summarise links, channels, traffic, encryption, emissions, and faults.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Communications Dashboard** from the Communications navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Channel Control

**Purpose**

Manage named logical channels and their members, medium, priority, and security.

**What this page contains**

- Manage named logical channels and their members, medium, priority, and security.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Channel Control** from the Channel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Direct Communications

**Purpose**

Establish authenticated ship-to-ship sessions.

**What this page contains**

- Establish authenticated ship-to-ship sessions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Direct Communications** from the Direct navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Fleet Communications

**Purpose**

Operate command, tactical, logistics, and support nets.

**What this page contains**

- Operate command, tactical, logistics, and support nets.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Fleet Communications** from the Fleet navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Command Broadcast

**Purpose**

Send priority orders or announcements with acknowledgement and expiry.

**What this page contains**

- Send priority orders or announcements with acknowledgement and expiry.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Command Broadcast** from the Command navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Distress and Emergency

**Purpose**

Transmit, receive, relay, and assign distress traffic.

**What this page contains**

- Transmit, receive, relay, and assign distress traffic.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Distress and Emergency** from the Distress navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Antenna Network

**Purpose**

Manage hardware range, role, power, identity broadcast, and signature.

**What this page contains**

- Manage hardware range, role, power, identity broadcast, and signature.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Antenna Network** from the Antenna navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Relay Routing

**Purpose**

Select multi-hop routes by quality, latency, trust, and failure state.

**What this page contains**

- Select multi-hop routes by quality, latency, trust, and failure state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Relay Routing** from the Relay navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Encryption and Authentication

**Purpose**

Manage peer trust, challenge, session identity, and operational key version.

**What this page contains**

- Manage peer trust, challenge, session identity, and operational key version.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Encryption and Authentication** from the Encryption navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Tactical Datalink

**Purpose**

Exchange tracks, targets, routes, orders, and status with provenance.

**What this page contains**

- Exchange tracks, targets, routes, orders, and status with provenance.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Tactical Datalink** from the Tactical navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Identification Challenge

**Purpose**

Perform structured challenge-and-reply without treating failure as automatic hostility.

**What this page contains**

- Perform structured challenge-and-reply without treating failure as automatic hostility.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Identification Challenge** from the Identification navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Message Queue

**Purpose**

Deliver reliable, prioritised, expiring, idempotent messages.

**What this page contains**

- Deliver reliable, prioritised, expiring, idempotent messages.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Create or accept work, assign an owner, change priority within authority, pause/cancel unstarted work, and verify completion evidence.

**How to use it**

Open **Message Queue** from the Message navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Recorded Transmissions

**Purpose**

Review authorised communication history.

**What this page contains**

- Review authorised communication history.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Recorded Transmissions** from the Recorded navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Public Address

**Purpose**

Send zone-based internal announcements.

**What this page contains**

- Send zone-based internal announcements.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Public Address** from the Public navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Automated Announcements

**Purpose**

Configure rate-limited event-driven announcements.

**What this page contains**

- Configure rate-limited event-driven announcements.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Automated Announcements** from the Automated navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Remote Command Sessions

**Purpose**

Grant least-privilege, expiring remote command scope.

**What this page contains**

- Grant least-privilege, expiring remote command scope.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Active channels
- Antenna and relay health
- Priority incoming traffic

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Remote Command Sessions** from the Remote navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

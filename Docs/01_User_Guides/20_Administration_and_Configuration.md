# Administration and Configuration — User Guide

**Product:** CommandShellOS for Space Engineers  
**Document type:** Department operator guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## 1. Purpose

Configure vessel identity, topology, departments, equipment groups, permissions, page definitions, integrations, diagnostics, backups, migrations, and safe mode without exposing administrative power as gameplay.

## 2. Typical Roles

- Server Administrator
- CommandShell Administrator
- Vessel Commissioner
- Configuration Maintainer
- Auditor

## 3. Main Dashboard

The dashboard is the department's starting page. It contains:

- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions
- Page/module/integration versions
- Persistence, backup, migration
- Safe mode and errors

Use the dashboard to find the limiting problem, then open the owning page before making a consequential decision.

## 4. Page Directory

| Page | Purpose |
| --- | --- |
| Administration Dashboard | Summarise vessel configuration, validation, modules, storage, errors, and backups. |
| Vessel Identity | Define stable vessel ID, name, class, faction, role, and commissioned version. |
| Grid and Subgrid Membership | Map primary grid, mechanical subgrids, exclusions, and attached craft. |
| Department Configuration | Enable vessel-appropriate departments, stations, readiness weights, and features. |
| Equipment Registry | Discover and map blocks by stable binding, group, compartment, adapter, and state. |
| Compartment and Deck Mapping | Define decks, zones, compartments, boundaries, routes, and equipment. |
| Console Station Assignment | Bind LCDs/controllers to verified station profiles and fallback pages. |
| Role and Permission Editor | Publish least-privilege claims, conditions, delegation, and revisions. |
| Alert Condition Profiles | Simulate, validate, publish, and roll back readiness transition profiles. |
| Automation Configuration | Define allowed triggers, actions, owners, quotas, and approval rules. |
| Sensor Model Configuration | Configure range, noise, confidence decay, occlusion, and signature cost. |
| Logistics Targets | Apply capacity-validated stock and reserve templates. |
| UI Themes and Localisation | Configure presentation without changing authority or relying on colour alone. |
| Data Backup and Restore | Create, verify, and restore compatible snapshots in safe mode. |
| Plugin and Integration Status | Show adapter version, capability, health, permissions, and compatibility. |
| Diagnostic Logging | Capture bounded structured diagnostics and support bundles. |
| Safe Mode | Run minimum recovery services with normal actuation and automation disabled. |
| Migration and Versioning | Apply ordered, idempotent, backed-up schema/configuration migrations. |

## 5. Standard Operating Workflow

1. Create/load stable vessel config
2. Discover and map equipment/topology/stations
3. Configure permissions, profiles, automation, and models
4. Resolve every blocking validation error
5. Back up, publish, and commission

## 6. Alarm Responsibilities

- Validation failed
- Unmapped critical equipment
- Duplicate identity
- Forbidden permission combination
- Schema mismatch
- Backup verification failed
- Integration incompatible
- Unsafe safe-mode exit

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

- All CommandShell services
- Torch lifecycle/config
- Client page registry
- Scenarium gateway config
- Persistence/migrations

---

## Administration Dashboard

**Purpose**

Summarise vessel configuration, validation, modules, storage, errors, and backups.

**What this page contains**

- Summarise vessel configuration, validation, modules, storage, errors, and backups.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select a status tile or alarm to open its owning detail page; request a formal report; route the view to another station where permitted.

**How to use it**

Open **Administration Dashboard** from the Administration navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Vessel Identity

**Purpose**

Define stable vessel ID, name, class, faction, role, and commissioned version.

**What this page contains**

- Define stable vessel ID, name, class, faction, role, and commissioned version.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Vessel Identity** from the Vessel navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Grid and Subgrid Membership

**Purpose**

Map primary grid, mechanical subgrids, exclusions, and attached craft.

**What this page contains**

- Map primary grid, mechanical subgrids, exclusions, and attached craft.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the target group, inspect commanded and actual state, review interlocks, submit the permitted command, and wait for server verification before proceeding.

**How to use it**

Open **Grid and Subgrid Membership** from the Grid navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Department Configuration

**Purpose**

Enable vessel-appropriate departments, stations, readiness weights, and features.

**What this page contains**

- Enable vessel-appropriate departments, stations, readiness weights, and features.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Department Configuration** from the Department navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Equipment Registry

**Purpose**

Discover and map blocks by stable binding, group, compartment, adapter, and state.

**What this page contains**

- Discover and map blocks by stable binding, group, compartment, adapter, and state.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Equipment Registry** from the Equipment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Compartment and Deck Mapping

**Purpose**

Define decks, zones, compartments, boundaries, routes, and equipment.

**What this page contains**

- Define decks, zones, compartments, boundaries, routes, and equipment.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Pan, zoom, filter layers, select an object or zone, measure geometry, open the context inspector, and publish an authorised selection.

**How to use it**

Open **Compartment and Deck Mapping** from the Compartment navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Console Station Assignment

**Purpose**

Bind LCDs/controllers to verified station profiles and fallback pages.

**What this page contains**

- Bind LCDs/controllers to verified station profiles and fallback pages.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Console Station Assignment** from the Console navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Role and Permission Editor

**Purpose**

Publish least-privilege claims, conditions, delegation, and revisions.

**What this page contains**

- Publish least-privilege claims, conditions, delegation, and revisions.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Role and Permission Editor** from the Role navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Alert Condition Profiles

**Purpose**

Simulate, validate, publish, and roll back readiness transition profiles.

**What this page contains**

- Simulate, validate, publish, and roll back readiness transition profiles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Alert Condition Profiles** from the Alert navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Automation Configuration

**Purpose**

Define allowed triggers, actions, owners, quotas, and approval rules.

**What this page contains**

- Define allowed triggers, actions, owners, quotas, and approval rules.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Automation Configuration** from the Automation navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Sensor Model Configuration

**Purpose**

Configure range, noise, confidence decay, occlusion, and signature cost.

**What this page contains**

- Configure range, noise, confidence decay, occlusion, and signature cost.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Edit a draft revision, run validation and simulation, review impact, publish with authority, and retain rollback to the previous version.

**How to use it**

Open **Sensor Model Configuration** from the Sensor navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Logistics Targets

**Purpose**

Apply capacity-validated stock and reserve templates.

**What this page contains**

- Apply capacity-validated stock and reserve templates.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Logistics Targets** from the Logistics navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## UI Themes and Localisation

**Purpose**

Configure presentation without changing authority or relying on colour alone.

**What this page contains**

- Configure presentation without changing authority or relying on colour alone.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **UI Themes and Localisation** from the UI navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Data Backup and Restore

**Purpose**

Create, verify, and restore compatible snapshots in safe mode.

**What this page contains**

- Create, verify, and restore compatible snapshots in safe mode.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Data Backup and Restore** from the Data navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Plugin and Integration Status

**Purpose**

Show adapter version, capability, health, permissions, and compatibility.

**What this page contains**

- Show adapter version, capability, health, permissions, and compatibility.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Inspect the limiting item, open its evidence and dependencies, request a refresh or diagnostic, assign corrective work, and re-run the assessment.

**How to use it**

Open **Plugin and Integration Status** from the Plugin navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Diagnostic Logging

**Purpose**

Capture bounded structured diagnostics and support bundles.

**What this page contains**

- Capture bounded structured diagnostics and support bundles.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Filter by time, source, severity, operator, or correlation ID; annotate without altering the original record; bookmark and export where permitted.

**How to use it**

Open **Diagnostic Logging** from the Diagnostic navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Safe Mode

**Purpose**

Run minimum recovery services with normal actuation and automation disabled.

**What this page contains**

- Run minimum recovery services with normal actuation and automation disabled.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Safe Mode** from the Safe navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

## Migration and Versioning

**Purpose**

Apply ordered, idempotent, backed-up schema/configuration migrations.

**What this page contains**

- Apply ordered, idempotent, backed-up schema/configuration migrations.
- Authoritative state revision and server timestamp
- Data-quality state: valid, estimated, stale, conflicted, or unavailable
- Active alarms, lockouts, dependencies, and pending commands relevant to the selected object
- Configuration validation
- Equipment discovery and unmapped items
- Roles, stations, and permissions

**Available actions**

Select the relevant object, review state quality and dependencies, use only enabled actions, and confirm the verified result or exact failure reason.

**How to use it**

Open **Migration and Versioning** from the Migration navigation or by selecting a linked dashboard item. Confirm the vessel, station, operator, target, and data quality. Investigate amber/red or stale values before acting. After submitting a command, keep the page open until the server reports Completed, Partial, Failed, Blocked, or another terminal result.

**Operating rule:** A visible control is not proof of authority. The server revalidates operator identity, station, target revision, lockouts, safety interlocks, and resource availability when the command is submitted.

---

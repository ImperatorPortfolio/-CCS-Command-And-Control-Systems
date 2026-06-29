# Authority, Stations, and Permission Architecture

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Inputs

Identity, faction/vessel relationship, watch role, physical/remote station, control ownership, claims, alert state, target state, lockouts, session scope, co-authorisation, expiry, and revision.

## Claims

Use granular claims such as engineering.power.operate, weapons.group.arm, weapons.fire.request, command.roe.set, security.lockdown.initiate, and admin.permissions.publish. Roles are data-defined claim bundles.

## Delegation

Delegation is bounded by time, vessel, station, target set, alert state, and the grantor's own authority.

## Multi-Party Authorisation

Approvals bind to exact payload hash, target revision, consequence, expiry, and distinct identities. A material change invalidates previous approvals.

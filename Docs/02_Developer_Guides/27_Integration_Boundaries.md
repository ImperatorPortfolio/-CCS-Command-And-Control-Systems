# Integration Boundaries

**Product:** CommandShellOS for Space Engineers  
**Document type:** Developer reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

## Space Engineers

Hide block/entity types behind adapters. Validate exact APIs against the installed dedicated-server assemblies and current ModAPI documentation.

## Torch

Torch hosts authoritative services, lifecycle, entity access, configuration, commands, and diagnostics. Keep Torch-specific code in its own project.

## Client Mod

The client renders LCD/UI, captures input, and exchanges messages. It is untrusted for authority and physical effects.

## ScenariumAPI

Scenarium owns campaign, scenario, quest, objective, conquest, faction, and world-state truth. CommandShellOS consumes projections and submits evidence through IScenariumGateway.

## MES

MES owns encounter spawning and NPC encounter behaviour. CommandShellOS does not become a parallel encounter director.

## Compatibility

Maintain an explicit matrix of Space Engineers DS build, Torch build, client protocol, CommandShell schema, Scenarium contract, and optional integration versions.

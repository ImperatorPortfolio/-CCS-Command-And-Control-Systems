# Naming and Identifier Conventions

**Product:** CommandShellOS for Space Engineers  
**Document type:** Reference  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

Use stable lowercase namespaced configuration IDs and strongly typed code IDs. Examples: vessel.iss_valiant, department.engineering, system.power.main, equipment.reactor.r1, compartment.deck03.port_engineering, station.bridge.helm_primary, procedure.power.black_start.v1.

Commands use imperative names such as SetAlertCondition; events use past-tense facts such as AlertConditionChanged; reason codes use category and exact cause such as Interlock.MaintenanceLockout. Display names are mutable and localisable; IDs are not.

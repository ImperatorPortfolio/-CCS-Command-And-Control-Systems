# CommandShellOS — Developer Companion Overview

**Product:** CommandShellOS for Space Engineers  
**Document type:** Platform implementation guide  
**Package version:** 0.1.0  
**Status:** Build-candidate  
**Prepared:** 2026-06-29

---

No connected repository named CommandShellOS was visible when this package was created. The connected ImperatorPortfolio/ScenariumAPI architecture was inspected and its boundary is retained: Scenarium owns campaign state; other systems consume, query, or react to it.

Build CommandShellOS as contracts, pure domain, persistence, server application, integrations, Torch host, client mod, UI, tools, and tests. Every feature requires contracts, domain tests, server implementation, adapter, persistence/recovery, projection, UI, permissions, alarms/audit, failure injection, and acceptance evidence.

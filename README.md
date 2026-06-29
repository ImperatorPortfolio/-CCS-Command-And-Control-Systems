# CommandOS Starter Build Candidate

**Status:** Build-candidate  
**Version:** 0.1.0  
**Target:** Space Engineers session mod with integrated touch runtime  
**Touch Runtime:** Integrated into CommandOS  
**Cockpit Dashboard Input:** Native inflight pointer and physical control interaction integrated into CommandOS

CommandOS is a server-authoritative command, operating-mode, thermal-management and weapon-control framework. This starter package delivers the first complete vertical slice for every block implementing `IMyUserControllableGun`, together with a public provider API for true per-instance integration into a custom weapon backend.

## Delivered functionality

- Automatic discovery of every user-controllable weapon on loaded grids.
- Persistent per-weapon mode, rate request, protection setting, temperature and cooldown state.
- SAFE, ECONOMY, STANDARD, OVERDRIVE and EMERGENCY operating modes.
- Server-side heat generation, passive cooling, thermal derating, thermal lockout and recovery.
- Enforced fallback rate governor for vanilla-style weapons at or below `1.0x`.
- Instrumented provider contract for genuine rate multipliers above `1.0x`, backend lockout and actual resource-sink demand.
- Terminal controls for mode, requested rate, thermal protection, forced cooldown and cooldown release.
- Eleven toolbar actions: five direct modes, cycle mode, rate up/down, thermal-protection toggle, forced cooldown and cooldown release.
- Integrated tactical console available from the LCD script selector as **CommandOS Tactical Console**.
- Construct-wide touchscreen commands.
- Native cockpit dashboard pointer with left-click, right-click and wheel interaction for `inflight_*` cockpit controls.
- Compatibility loader for existing cockpit button assignments stored with the earlier cockpit profile format.
- Secure multiplayer command routing, per-weapon construct-command filtering and access-filtered telemetry.
- Client telemetry cache and construct snapshots.
- World-storage persistence and automatic periodic saves.
- Full SDK file and a complete weapon-pack integration skeleton.

## Installation

1. Extract the `Mod` directory into:
   `%APPDATA%\SpaceEngineers\Mods\CommandOS`
2. Ensure the resulting path is:
   `%APPDATA%\SpaceEngineers\Mods\CommandOS\Data\Scripts\CommandOS`
3. Load a test world containing at least one weapon and one LCD or cockpit surface.
4. On the display surface, select **CommandOS Tactical Console** from the script list.
5. Sit in a compatible cockpit with `inflight_*` model dummies to use dashboard pointer controls on physical panel buttons.
6. Open a weapon terminal to access the CommandOS controls and toolbar actions.

No compiled DLL is required. Space Engineers compiles the C# files from `Data/Scripts` when the mod loads.

## Package boundaries

This starter is a complete weapon vertical slice, not the completed all-block CommandOS product. Every user-controllable gun is managed immediately. Genuine overdrive above the weapon's definition rate and genuine extra electrical draw require the weapon pack to register an instrumented provider using `SDK/CommandOsApi.cs`.

The final product doctrine and expansion roadmap are in `Docs/COMMAND_DOCTRINE.md` and `Docs/FOLLOW_UP_WORK.md`.

## Exact active mod files

- `Mod/metadata.mod`
- `Mod/Data/Scripts/CommandOS/CommandOsConstants.cs`
- `Mod/Data/Scripts/CommandOS/CommandOsLog.cs`
- `Mod/Data/Scripts/CommandOS/CommandOsSession.cs`
- `Mod/Data/Scripts/CommandOS/Domain/OperatingMode.cs`
- `Mod/Data/Scripts/CommandOS/Domain/Contracts.cs`
- `Mod/Data/Scripts/CommandOS/Core/AccessService.cs`
- `Mod/Data/Scripts/CommandOS/Core/CommandProcessor.cs`
- `Mod/Data/Scripts/CommandOS/Core/ExternalWeaponProviderRegistry.cs`
- `Mod/Data/Scripts/CommandOS/Core/ManagedWeapon.cs`
- `Mod/Data/Scripts/CommandOS/Core/WeaponRegistry.cs`
- `Mod/Data/Scripts/CommandOS/Integration/TerminalIntegration.cs`
- `Mod/Data/Scripts/CommandOS/Integration/TouchScreenBridge.cs`
- `Mod/Data/Scripts/CommandOS/Integration/CommandOsCockpitControlProfile.cs`
- `Mod/Data/Scripts/CommandOS/Integration/CommandOsCockpitActionRouter.cs`
- `Mod/Data/Scripts/CommandOS/Integration/CommandOsCockpitInteractionService.cs`
- `Mod/Data/Scripts/CommandOS/Networking/ClientSnapshotCache.cs`
- `Mod/Data/Scripts/CommandOS/Networking/NetworkService.cs`
- `Mod/Data/Scripts/CommandOS/Persistence/PersistenceStore.cs`
- `Mod/Data/Scripts/CommandOS/PublicApi/PublicApiHost.cs`
- `Mod/Data/Scripts/CommandOS/UI/CommandOsTextSurfaceScript.cs`

## SDK files

- `SDK/CommandOsApi.cs`
- `SDK/WeaponPackIntegrationExample.cs`

## Acceptance checkpoint

Do not call this package product-ready until the checklist in `Docs/ACCEPTANCE_CHECKLIST.md` has passed in single player, a listen server and a dedicated server using the actual weapon pack.


using System.Collections.Generic;
using System.Globalization;
using ProtoBuf;
using VRageMath;

namespace AGS
{
    // ─────────────────────────────────────────────────────────────────────────────
    // Program API — wire contracts (Step 1 of the OS foundation).
    //
    // Everything an external modpack contributes to the OS crosses a serialization
    // boundary (registration, screens, input, persisted state), so the contract is
    // plain data — no delegates, no live object references. These types are the wire
    // format shared by single-player, multiplayer clients, and dedicated servers.
    //
    // Nothing in the running shell consumes these yet; they are the keystone the
    // renderer (Step 2) and command routing (Step 3) build on.
    // ─────────────────────────────────────────────────────────────────────────────

    // Bumped when the contract shape changes incompatibly. A program states the API
    // version it targets in its manifest so the OS can refuse/adapt mismatches.
    public static class ProgramApi
    {
        public const int Version = 1;
    }

    // What a program is permitted to do. Enforced server-side (Step 6). Bitflags so a
    // single int travels in the manifest.
    public enum ProgramCapability
    {
        None = 0,
        ReadShipState = 1 << 0,
        IssueCommands = 1 << 1,
        PersistState = 1 << 2,
    }

    // Static description of a program, sent once at registration. Identity + how the
    // OS should present and gate it. No per-frame data lives here.
    [ProtoContract]
    public sealed class ProgramManifest
    {
        [ProtoMember(1)] public string Id;
        [ProtoMember(2)] public string Title;
        [ProtoMember(3)] public string Purpose;
        [ProtoMember(4)] public int Version;        // the program's own build/version
        [ProtoMember(5)] public int ApiVersion;     // ProgramApi.Version it was built against
        [ProtoMember(6)] public int RequiredRole;   // maps to RoleId; gates the launcher
        [ProtoMember(7)] public int Capabilities;   // OR-ed ProgramCapability flags
        [ProtoMember(8)] public string IconSprite;  // optional launcher icon sprite id

        public ProgramManifest() { }

        public bool Has(ProgramCapability capability)
        {
            return (Capabilities & (int)capability) != 0;
        }
    }

    public enum ProgramBindingKind
    {
        Text = 0,
        Number = 1,
        Bool = 2,
        Color = 3,
    }

    // One key/value a program supplies to fill a slot in its view markup. The value is
    // always carried as a string with a kind tag so it round-trips through serialization
    // and maps onto the typed UiXmlBindings setters.
    [ProtoContract]
    public sealed class ProgramBinding
    {
        [ProtoMember(1)] public string Key;
        [ProtoMember(2)] public ProgramBindingKind Kind;
        [ProtoMember(3)] public string Value;

        public ProgramBinding() { }

        public static ProgramBinding Text(string key, string value)
        {
            return new ProgramBinding { Key = key, Kind = ProgramBindingKind.Text, Value = value ?? string.Empty };
        }

        public static ProgramBinding Number(string key, float value)
        {
            return new ProgramBinding { Key = key, Kind = ProgramBindingKind.Number, Value = value.ToString("R", CultureInfo.InvariantCulture) };
        }

        public static ProgramBinding Bool(string key, bool value)
        {
            return new ProgramBinding { Key = key, Kind = ProgramBindingKind.Bool, Value = value ? "1" : "0" };
        }

        public static ProgramBinding Color(string key, Color value)
        {
            return new ProgramBinding { Key = key, Kind = ProgramBindingKind.Color, Value = value.PackedValue.ToString(CultureInfo.InvariantCulture) };
        }
    }

    // A single rendered screen state from a program: which view to draw plus the data to
    // fill it. ViewId names a markup view the OS knows; InlineMarkup carries the markup
    // verbatim when a program ships its own. The OS turns this into a live element tree
    // through the existing UiXmlLoader (Step 2).
    [ProtoContract]
    public sealed class ProgramFrame
    {
        [ProtoMember(1)] public string ViewId;
        [ProtoMember(2)] public string InlineMarkup;
        [ProtoMember(3)] public string Title;
        [ProtoMember(4)] public List<ProgramBinding> Bindings = new List<ProgramBinding>();

        public ProgramFrame() { }

        public ProgramFrame Text(string key, string value) { Bindings.Add(ProgramBinding.Text(key, value)); return this; }
        public ProgramFrame Number(string key, float value) { Bindings.Add(ProgramBinding.Number(key, value)); return this; }
        public ProgramFrame Bool(string key, bool value) { Bindings.Add(ProgramBinding.Bool(key, value)); return this; }
        public ProgramFrame Color(string key, Color value) { Bindings.Add(ProgramBinding.Color(key, value)); return this; }

        // Bridge to the existing XML binding bag so the renderer can feed UiXmlLoader.
        // Kept here so the string<->typed conversion lives with the wire format.
        public UiXmlBindings ToUiBindings()
        {
            var result = new UiXmlBindings();
            if (Bindings == null)
            {
                return result;
            }

            for (var i = 0; i < Bindings.Count; i++)
            {
                var binding = Bindings[i];
                if (binding == null || string.IsNullOrEmpty(binding.Key))
                {
                    continue;
                }

                switch (binding.Kind)
                {
                    case ProgramBindingKind.Number:
                        float number;
                        if (float.TryParse(binding.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                        {
                            result.Set(binding.Key, number);
                        }
                        break;

                    case ProgramBindingKind.Bool:
                        result.Set(binding.Key, binding.Value == "1" || binding.Value == "true");
                        break;

                    case ProgramBindingKind.Color:
                        uint packed;
                        if (uint.TryParse(binding.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out packed))
                        {
                            var color = new Color();
                            color.PackedValue = packed;
                            result.Set(binding.Key, color);
                        }
                        break;

                    default:
                        result.Set(binding.Key, binding.Value ?? string.Empty);
                        break;
                }
            }

            return result;
        }
    }

    // An input event from a screen, addressed to the program that owns the active app.
    // Action is a free-form string the program defines (generalises today's hardcoded
    // UiIntentType + GalleryAction). Routed client -> server -> owning program (Step 3).
    [ProtoContract]
    public sealed class ProgramCommand
    {
        [ProtoMember(1)] public string AppId;
        [ProtoMember(2)] public string Action;
        [ProtoMember(3)] public int Value;
        [ProtoMember(4)] public string Data;

        public ProgramCommand() { }

        public ProgramCommand(string appId, string action, int value = 0, string data = null)
        {
            AppId = appId ?? string.Empty;
            Action = action ?? string.Empty;
            Value = value;
            Data = data ?? string.Empty;
        }
    }

    // Opaque, program-owned persisted state, namespaced by app id and stored server-side
    // in the construct (Step 4). The OS never interprets Data.
    [ProtoContract]
    public sealed class ProgramStateBlob
    {
        [ProtoMember(1)] public string AppId;
        [ProtoMember(2)] public byte[] Data;

        public ProgramStateBlob() { }
    }
}

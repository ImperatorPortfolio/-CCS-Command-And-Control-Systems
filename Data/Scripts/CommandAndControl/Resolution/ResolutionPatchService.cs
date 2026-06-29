using System;
using System.Collections.Generic;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Definitions;
using Sandbox.ModAPI;
using VRage.Utils;

namespace AGS
{
    public sealed class ResolutionPatchService
    {
        private const int ResolutionMultiplier = 2;
        private const int MaximumLongestTextureAxis = 4096;
        private const string LogPrefix = "[AGS LCD Resolution x2] ";

        private static readonly List<ActiveScreenAreaPatch> ActiveScreenAreaPatches = new List<ActiveScreenAreaPatch>();
        private static readonly List<ActiveTextPanelPatch> ActiveTextPanelPatches = new List<ActiveTextPanelPatch>();

        private readonly List<ScreenAreaOriginal> _screenAreaOriginals = new List<ScreenAreaOriginal>();
        private readonly List<TextPanelOriginal> _textPanelOriginals = new List<TextPanelOriginal>();

        private bool _patchApplied;
        private bool _isDedicatedServer;

        public void Load()
        {
            try
            {
                _isDedicatedServer = MyAPIGateway.Utilities != null && MyAPIGateway.Utilities.IsDedicated;
                if (_isDedicatedServer)
                {
                    Log("Dedicated server detected; no render-target patch is required.");
                    return;
                }

                ApplyPatch();
            }
            catch (Exception exception)
            {
                Log("ERROR while applying LCD definition patch: " + exception);
            }
        }

        public void Unload()
        {
            try
            {
                if (!_isDedicatedServer)
                {
                    RestorePatch();
                }
            }
            catch (Exception exception)
            {
                Log("ERROR while restoring LCD definition patch: " + exception);
            }
            finally
            {
                _screenAreaOriginals.Clear();
                _textPanelOriginals.Clear();
                _patchApplied = false;
            }
        }

        private void ApplyPatch()
        {
            if (_patchApplied)
            {
                return;
            }

            foreach (var definition in MyDefinitionManager.Static.GetAllDefinitions())
            {
                var functionalDefinition = definition as MyFunctionalBlockDefinition;
                if (functionalDefinition == null || ShouldSkip(functionalDefinition))
                {
                    continue;
                }

                PatchScreenAreas(functionalDefinition);
                PatchTopLevelTextPanelResolution(functionalDefinition);
            }

            _patchApplied = true;
            Log("Applied LCD definition resolution patch.");
        }

        private static bool ShouldSkip(MyFunctionalBlockDefinition definition)
        {
            var subtypeName = definition.Id.SubtypeName ?? string.Empty;
            return subtypeName.IndexOf("Entertainment", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void PatchScreenAreas(MyFunctionalBlockDefinition definition)
        {
            if (definition.ScreenAreas == null || definition.ScreenAreas.Count == 0)
            {
                return;
            }

            for (var index = 0; index < definition.ScreenAreas.Count; index++)
            {
                var screenArea = definition.ScreenAreas[index];
                if (screenArea == null)
                {
                    continue;
                }

                ResolutionPatch activePatch;
                if (TryGetActivePatch(screenArea, out activePatch))
                {
                    TrackOriginal(screenArea, activePatch);
                    continue;
                }

                var originalResolution = screenArea.TextureResolution;
                var patchedResolution = CalculatePatchedResolution(originalResolution, screenArea.ScreenWidth, screenArea.ScreenHeight);
                if (patchedResolution <= originalResolution)
                {
                    continue;
                }

                var patch = new ResolutionPatch(originalResolution, patchedResolution);
                ActiveScreenAreaPatches.Add(new ActiveScreenAreaPatch(screenArea, patch));
                TrackOriginal(screenArea, patch);
                screenArea.TextureResolution = patchedResolution;
            }
        }

        private void PatchTopLevelTextPanelResolution(MyFunctionalBlockDefinition functionalDefinition)
        {
            var textPanelDefinition = functionalDefinition as MyTextPanelDefinition;
            if (textPanelDefinition == null)
            {
                return;
            }

            ResolutionPatch activePatch;
            if (TryGetActivePatch(textPanelDefinition, out activePatch))
            {
                TrackOriginal(textPanelDefinition, activePatch);
                return;
            }

            var originalResolution = textPanelDefinition.TextureResolution;
            var patchedResolution = CalculatePatchedResolution(originalResolution, textPanelDefinition.ScreenWidth, textPanelDefinition.ScreenHeight);
            if (patchedResolution <= originalResolution)
            {
                return;
            }

            var patch = new ResolutionPatch(originalResolution, patchedResolution);
            ActiveTextPanelPatches.Add(new ActiveTextPanelPatch(textPanelDefinition, patch));
            TrackOriginal(textPanelDefinition, patch);
            textPanelDefinition.TextureResolution = patchedResolution;
        }

        private void RestorePatch()
        {
            if (!_patchApplied)
            {
                return;
            }

            for (var index = 0; index < _screenAreaOriginals.Count; index++)
            {
                var original = _screenAreaOriginals[index];
                if (original.ScreenArea.TextureResolution == original.PatchedTextureResolution)
                {
                    original.ScreenArea.TextureResolution = original.OriginalTextureResolution;
                }
                RemoveActivePatch(original.ScreenArea);
            }

            for (var index = 0; index < _textPanelOriginals.Count; index++)
            {
                var original = _textPanelOriginals[index];
                if (original.Definition.TextureResolution == original.PatchedTextureResolution)
                {
                    original.Definition.TextureResolution = original.OriginalTextureResolution;
                }
                RemoveActivePatch(original.Definition);
            }

            _patchApplied = false;
        }

        private static int CalculatePatchedResolution(int originalResolution, int screenWidth, int screenHeight)
        {
            if (originalResolution <= 0)
            {
                return originalResolution;
            }

            long doubledResolution = (long)originalResolution * ResolutionMultiplier;
            var absoluteWidth = Math.Abs(screenWidth);
            var absoluteHeight = Math.Abs(screenHeight);
            var longestDimension = Math.Max(absoluteWidth, absoluteHeight);
            var shortestDimension = Math.Min(absoluteWidth, absoluteHeight);

            if (longestDimension <= 0 || shortestDimension <= 0)
            {
                return ClampToIntegerAndCeiling(doubledResolution);
            }

            var projectedLongestAxis = doubledResolution * longestDimension / shortestDimension;
            if (projectedLongestAxis > MaximumLongestTextureAxis)
            {
                doubledResolution = (long)MaximumLongestTextureAxis * shortestDimension / longestDimension;
            }

            if (doubledResolution <= originalResolution)
            {
                return originalResolution;
            }

            return ClampToIntegerAndCeiling(doubledResolution);
        }

        private static int ClampToIntegerAndCeiling(long resolution)
        {
            if (resolution <= 0)
            {
                return 0;
            }

            if (resolution > MaximumLongestTextureAxis)
            {
                return MaximumLongestTextureAxis;
            }

            if (resolution > int.MaxValue)
            {
                return int.MaxValue;
            }

            return (int)resolution;
        }

        private void TrackOriginal(ScreenArea screenArea, ResolutionPatch patch)
        {
            for (var index = 0; index < _screenAreaOriginals.Count; index++)
            {
                if (ReferenceEquals(_screenAreaOriginals[index].ScreenArea, screenArea))
                {
                    return;
                }
            }

            _screenAreaOriginals.Add(new ScreenAreaOriginal(screenArea, patch.OriginalTextureResolution, patch.PatchedTextureResolution));
        }

        private void TrackOriginal(MyTextPanelDefinition definition, ResolutionPatch patch)
        {
            for (var index = 0; index < _textPanelOriginals.Count; index++)
            {
                if (ReferenceEquals(_textPanelOriginals[index].Definition, definition))
                {
                    return;
                }
            }

            _textPanelOriginals.Add(new TextPanelOriginal(definition, patch.OriginalTextureResolution, patch.PatchedTextureResolution));
        }

        private static bool TryGetActivePatch(ScreenArea screenArea, out ResolutionPatch patch)
        {
            for (var index = 0; index < ActiveScreenAreaPatches.Count; index++)
            {
                var activePatch = ActiveScreenAreaPatches[index];
                if (ReferenceEquals(activePatch.ScreenArea, screenArea))
                {
                    patch = activePatch.Patch;
                    return true;
                }
            }

            patch = null;
            return false;
        }

        private static bool TryGetActivePatch(MyTextPanelDefinition definition, out ResolutionPatch patch)
        {
            for (var index = 0; index < ActiveTextPanelPatches.Count; index++)
            {
                var activePatch = ActiveTextPanelPatches[index];
                if (ReferenceEquals(activePatch.Definition, definition))
                {
                    patch = activePatch.Patch;
                    return true;
                }
            }

            patch = null;
            return false;
        }

        private static void RemoveActivePatch(ScreenArea screenArea)
        {
            for (var index = ActiveScreenAreaPatches.Count - 1; index >= 0; index--)
            {
                if (ReferenceEquals(ActiveScreenAreaPatches[index].ScreenArea, screenArea))
                {
                    ActiveScreenAreaPatches.RemoveAt(index);
                }
            }
        }

        private static void RemoveActivePatch(MyTextPanelDefinition definition)
        {
            for (var index = ActiveTextPanelPatches.Count - 1; index >= 0; index--)
            {
                if (ReferenceEquals(ActiveTextPanelPatches[index].Definition, definition))
                {
                    ActiveTextPanelPatches.RemoveAt(index);
                }
            }
        }

        private static void Log(string message)
        {
            MyLog.Default.WriteLine(LogPrefix + message);
        }

        private sealed class ResolutionPatch
        {
            public readonly int OriginalTextureResolution;
            public readonly int PatchedTextureResolution;

            public ResolutionPatch(int originalTextureResolution, int patchedTextureResolution)
            {
                OriginalTextureResolution = originalTextureResolution;
                PatchedTextureResolution = patchedTextureResolution;
            }
        }

        private sealed class ActiveScreenAreaPatch
        {
            public readonly ScreenArea ScreenArea;
            public readonly ResolutionPatch Patch;

            public ActiveScreenAreaPatch(ScreenArea screenArea, ResolutionPatch patch)
            {
                ScreenArea = screenArea;
                Patch = patch;
            }
        }

        private sealed class ActiveTextPanelPatch
        {
            public readonly MyTextPanelDefinition Definition;
            public readonly ResolutionPatch Patch;

            public ActiveTextPanelPatch(MyTextPanelDefinition definition, ResolutionPatch patch)
            {
                Definition = definition;
                Patch = patch;
            }
        }

        private sealed class ScreenAreaOriginal
        {
            public readonly ScreenArea ScreenArea;
            public readonly int OriginalTextureResolution;
            public readonly int PatchedTextureResolution;

            public ScreenAreaOriginal(ScreenArea screenArea, int originalTextureResolution, int patchedTextureResolution)
            {
                ScreenArea = screenArea;
                OriginalTextureResolution = originalTextureResolution;
                PatchedTextureResolution = patchedTextureResolution;
            }
        }

        private sealed class TextPanelOriginal
        {
            public readonly MyTextPanelDefinition Definition;
            public readonly int OriginalTextureResolution;
            public readonly int PatchedTextureResolution;

            public TextPanelOriginal(MyTextPanelDefinition definition, int originalTextureResolution, int patchedTextureResolution)
            {
                Definition = definition;
                OriginalTextureResolution = originalTextureResolution;
                PatchedTextureResolution = patchedTextureResolution;
            }
        }
    }
}

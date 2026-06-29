using System.Collections.Generic;
using System.Globalization;
using VRageMath;

namespace AGS
{
    public sealed class UiXmlBindings
    {
        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>();
        private readonly Dictionary<string, float> _floats = new Dictionary<string, float>();
        private readonly Dictionary<string, int> _ints = new Dictionary<string, int>();
        private readonly Dictionary<string, bool> _bools = new Dictionary<string, bool>();
        private readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>();

        public void Set(string key, string value) { _strings[key] = value ?? string.Empty; }
        public void Set(string key, float value) { _floats[key] = value; }
        public void Set(string key, int value) { _ints[key] = value; }
        public void Set(string key, bool value) { _bools[key] = value; }
        public void Set(string key, Color value) { _colors[key] = value; }

        public string ResolveString(string value)
        {
            var key = GetBindingKey(value);
            if (string.IsNullOrEmpty(key))
            {
                return value ?? string.Empty;
            }

            string stringValue;
            if (_strings.TryGetValue(key, out stringValue))
            {
                return stringValue;
            }

            float floatValue;
            if (_floats.TryGetValue(key, out floatValue))
            {
                return floatValue.ToString("0.##", CultureInfo.InvariantCulture);
            }

            int intValue;
            if (_ints.TryGetValue(key, out intValue))
            {
                return intValue.ToString(CultureInfo.InvariantCulture);
            }

            bool boolValue;
            if (_bools.TryGetValue(key, out boolValue))
            {
                return boolValue ? "true" : "false";
            }

            return string.Empty;
        }

        public float ResolveFloat(string value, float fallback)
        {
            var key = GetBindingKey(value);
            if (!string.IsNullOrEmpty(key))
            {
                float floatValue;
                if (_floats.TryGetValue(key, out floatValue))
                {
                    return floatValue;
                }

                int intValue;
                if (_ints.TryGetValue(key, out intValue))
                {
                    return intValue;
                }

                string stringValue;
                if (_strings.TryGetValue(key, out stringValue))
                {
                    return ParseFloat(stringValue, fallback);
                }

                return fallback;
            }

            return ParseFloat(value, fallback);
        }

        public int ResolveInt(string value, int fallback)
        {
            var key = GetBindingKey(value);
            if (!string.IsNullOrEmpty(key))
            {
                int intValue;
                if (_ints.TryGetValue(key, out intValue))
                {
                    return intValue;
                }

                float floatValue;
                if (_floats.TryGetValue(key, out floatValue))
                {
                    return (int)floatValue;
                }

                string stringValue;
                if (_strings.TryGetValue(key, out stringValue))
                {
                    return ParseInt(stringValue, fallback);
                }

                return fallback;
            }

            return ParseInt(value, fallback);
        }

        public bool ResolveBool(string value, bool fallback)
        {
            var key = GetBindingKey(value);
            if (!string.IsNullOrEmpty(key))
            {
                bool boolValue;
                if (_bools.TryGetValue(key, out boolValue))
                {
                    return boolValue;
                }

                string stringValue;
                if (_strings.TryGetValue(key, out stringValue))
                {
                    return ParseBool(stringValue, fallback);
                }

                return fallback;
            }

            return ParseBool(value, fallback);
        }

        public Color ResolveColor(string value, Color fallback)
        {
            var key = GetBindingKey(value);
            if (!string.IsNullOrEmpty(key))
            {
                Color colorValue;
                if (_colors.TryGetValue(key, out colorValue))
                {
                    return colorValue;
                }

                string stringValue;
                if (_strings.TryGetValue(key, out stringValue))
                {
                    return ParseColor(stringValue, fallback);
                }

                return fallback;
            }

            return ParseColor(value, fallback);
        }

        private static string GetBindingKey(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 3)
            {
                return null;
            }

            return value[0] == '{' && value[value.Length - 1] == '}' ? value.Substring(1, value.Length - 2) : null;
        }

        private static float ParseFloat(string value, float fallback)
        {
            float parsed;
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed) ? parsed : fallback;
        }

        private static int ParseInt(string value, int fallback)
        {
            int parsed;
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed) ? parsed : fallback;
        }

        private static bool ParseBool(string value, bool fallback)
        {
            if (string.IsNullOrEmpty(value))
            {
                return fallback;
            }

            if (value == "1")
            {
                return true;
            }

            if (value == "0")
            {
                return false;
            }

            bool parsed;
            return bool.TryParse(value, out parsed) ? parsed : fallback;
        }

        private static Color ParseColor(string value, Color fallback)
        {
            if (string.IsNullOrEmpty(value))
            {
                return fallback;
            }

            if (value[0] == '#')
            {
                var hex = value.Substring(1);
                uint parsed;
                if (!uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsed))
                {
                    return fallback;
                }

                if (hex.Length == 6)
                {
                    return new Color((byte)((parsed >> 16) & 0xFF), (byte)((parsed >> 8) & 0xFF), (byte)(parsed & 0xFF));
                }

                if (hex.Length == 8)
                {
                    return new Color((byte)((parsed >> 24) & 0xFF), (byte)((parsed >> 16) & 0xFF), (byte)((parsed >> 8) & 0xFF), (byte)(parsed & 0xFF));
                }
            }

            return fallback;
        }
    }
}

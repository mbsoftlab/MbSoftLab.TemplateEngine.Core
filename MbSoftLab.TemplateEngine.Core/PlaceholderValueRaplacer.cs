using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MbSoftLab.TemplateEngine.Core
{
    class PlaceholderValueRaplacer : IPlaceholderValueRaplacer
    {
        string _outputString;
        readonly string _nullStringValue;
        public string OutputString => _outputString;
        public CultureInfo CultureInfo { get; set; } = CultureInfo.CreateSpecificCulture("en-US");

        private Dictionary<string, Action<string, object>> replacementActions = new Dictionary<string, Action<string, object>>();

        public PlaceholderValueRaplacer(string outputString, string nullStringValue)
        {
            _outputString = outputString;
            _nullStringValue = nullStringValue;
            RegisterReplacementActions();
        }
        private void RegisterReplacementActions()
        {
            replacementActions.Add("String", (placeholderValueName, value) => ReplaceNullableStringValueInOutputString(placeholderValueName, value));
            replacementActions.Add("Byte", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (byte)value));
            replacementActions.Add("Short", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (short)value));
            replacementActions.Add("UShort", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (ushort)value));
            replacementActions.Add("Long", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (long)value));
            replacementActions.Add("ULong", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (ulong)value));
            replacementActions.Add("SByte", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (sbyte)value));
            replacementActions.Add("Char", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (char)value));
            replacementActions.Add("UInt16", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt16)value));
            replacementActions.Add("UInt32", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt32)value));
            replacementActions.Add("UInt64", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt64)value));
            replacementActions.Add("Int16", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int16)value));
            replacementActions.Add("Int32", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int32)value));
            replacementActions.Add("Int64", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int64)value));
            replacementActions.Add("Decimal", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Decimal)value));
            replacementActions.Add("Double", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (double)value));
            replacementActions.Add("DateTime", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (DateTime)value));
            replacementActions.Add("Boolean", (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, ((bool)value)));
        }
        private void ReplaceValueInOutputString(string placeholderValueName, object value)
        {
            string replacementForCulture = Convert.ToString(value, CultureInfo);
            _outputString = _outputString.Replace(placeholderValueName, replacementForCulture);
        }
        private void ReplaceNullableStringValueInOutputString(string placeholderValueName, object value)
        {
            if (value == null)
                _outputString = _outputString.Replace(placeholderValueName, _nullStringValue);
            else
                _outputString = _outputString.Replace(placeholderValueName, (String)value);  
        }
        public void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value)
        {
            if (IsNoCollection(valueType))
            {
                if (HasReplacementActionForType(valueType))
                    InvokeReplacementActionForType(valueType, placeholderValueName, value);
                else
                    throw new NotSupportedException($"Type '{valueType}' not supported by TemplateEngine.createStringFromTemplate().");
            }
        }

        private bool IsNoCollection(Type valueType) => valueType.FullName.Contains("System.Collections.Generic") == false;
        private bool HasReplacementActionForType(Type valueType) => replacementActions.ContainsKey(valueType.Name);
        private void InvokeReplacementActionForType(Type valueType, string placeholderValueName, object value)
        {
            replacementActions[valueType.Name].Invoke(placeholderValueName, value);
        }

    }
}

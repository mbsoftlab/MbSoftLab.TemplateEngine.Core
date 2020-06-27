using System;
using System.Collections.Generic;
using System.Text;

namespace MbSoftLab.TemplateEngine.Core
{
    interface IPlaceholderValueRaplacer
    {
        void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value);
        string OutputString { get; }
    }
    class PlaceholderValueRaplacer: IPlaceholderValueRaplacer
    {
        string _outputString, _nullStringValue;
        public string OutputString => _outputString;
        public PlaceholderValueRaplacer(string outputString,string nullStringValue)
        {
            _outputString = outputString;
            _nullStringValue = nullStringValue;
        } 
        private string SetStringValueToOutputstring(string placeholderValueName, object value){
            string result = _outputString;
            if (value == null)
                result = _outputString.Replace(placeholderValueName, _nullStringValue);
            else
                result = _outputString.Replace(placeholderValueName, (String)value);
            return result;
        }
        public void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value)
        {
            switch (valueType.Name)
            {
                case "String":
                    _outputString= SetStringValueToOutputstring(placeholderValueName, value);
                    break;
                case "Byte":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((byte)value));
                    break;
                case "Short":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((short)value));
                    break;
                case "UShort":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((ushort)value));
                    break;
                case "Long":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((long)value));
                    break;
                case "ULong":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((ulong)value));
                    break;
                case "SByte":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((sbyte)value));
                    break;
                case "Char":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((char)value));
                    break;
                case "UInt16":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((UInt16)value));
                    break;
                case "UInt32":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((UInt32)value));
                    break;
                case "UInt64":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((UInt64)value));
                    break;
                case "Int16":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((Int16)value));
                    break;
                case "Int32":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((Int32)value));
                    break;
                case "Int64":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((Int64)value));
                    break;
                case "Decimal":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((Decimal)value));
                    break;
                case "Double":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString((double)value));
                    break;
                case "DateTime":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString(((DateTime)value)));
                    break;
                case "Boolean":
                    _outputString = _outputString.Replace(placeholderValueName, Convert.ToString(((bool)value)));
                    break;
                default:
                    if (valueType.FullName.Contains("System.Collections.Generic"))
                    {
                        // TODO: MTC-1 - Support Types from System.Collections.Generic (List, Dictionary,...)
                    #if DEBUG
                    #warning types of System.Collections.Generic are not supported in this Version 
                    #endif
                  
                        break;
                    }
                    throw new NotSupportedException($"Type '{valueType.ToString()}' not supported by TemplateEngine.createStringFromTemplate().");
                    
            }

        }
    }
}

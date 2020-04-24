using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Framework
{
    public class TemplateEngine : TemplateEngine<object>
    {
        #region --- CONSTRUCTORS
        public TemplateEngine(object templateDataModel, string stringTemplate):base(templateDataModel, stringTemplate)
        {
           
        }
        public TemplateEngine(object templateDataModel):base(templateDataModel)
        {
           
        }
       
        public TemplateEngine()
        {

        }
        #endregion
    } 

    public class TemplateEngine<T>
    {
        private string _outputString;
        private List<String> _methodBlacklist = new List<string>() { "ToString", "GetType", "Equals", "GetHashCode" };

        #region --- PUBLIC PROPERTYS
        /// <summary>
        /// Startzeichen für eine PlaceholderProperty. Nach diesem Zeichen folgt der Propertyname. Der Defaultwert ist ${.
        /// </summary>
        public string OpeningDelimiter { get=>_openingDelimiter; set=>_openingDelimiter=value.Trim(); }
        private string _openingDelimiter = "${";
        /// <summary>
        /// Endzeichen für eine PlaceholderProperty. Dieses Zeichen markiert das ende des Propertynamens. Der Defaultwet ist }.
        /// </summary>
        public string CloseingDelimiter { get=>_closeingDelimiter; set=>_closeingDelimiter=value.Trim(); }
        private string _closeingDelimiter = "}";  
        /// <summary>
        /// Datenmodell mit Propertys zum befüllen der ${PlaceholderPropertys} im Template. Name der Propertys im DataModel muss den Namen in den ${Placeholder} entsprechen
        /// </summary>
        public T TemplateDataModel { get => _templateDataModel; set => _templateDataModel = value; }
        T _templateDataModel;
        /// <summary>
        /// Das Template für die Zeichenkette mit ${PlaceholderPropertys}
        /// </summary>
        public string TemplateString
        {
            get => _templateString;
            set {
                if (value != null && value != _templateString)
                    _templateString = value;
            }
        }
        string _templateString;
        /// <summary>
        /// Legt die Zeichenkette für NULL-Werte fest. Standard = NULL. 
        /// </summary>
        public string NullStringValue { get=>_nullStringValue; set=>_nullStringValue=value; }
        string _nullStringValue = "NULL";


        public ITemplateEngineConfig<T> Config {
            get=>_config; 
            set 
            {
                _config = value;
                this.NullStringValue = _config.NullStringValue;
                this.OpeningDelimiter = _config.OpeningDelimiter;
                this.CloseingDelimiter = _config.CloseingDelimiter;
                this.TemplateDataModel = _config.TemplateDataModel;
                this.TemplateString = _config.TemplateString;
            } 
        }
        private ITemplateEngineConfig<T> _config;
        #endregion


        #region --- CONSTRUCTORS
        public TemplateEngine(T templateDataModel, string stringTemplate)
        {
            _templateDataModel = templateDataModel;
            _templateString = stringTemplate;
        }
        public TemplateEngine(T templateDataModel)
        {
            _templateDataModel = templateDataModel;
        }
     
        public TemplateEngine()
        {

        }
         
        #endregion
        /// <summary>
        /// Läd eine Templatezeichenkette aus einer Datei
        /// </summary>
        /// <param name="path">Pfad zur Datei mit der Templatezeichenkette.</param>
        public void LoadTemplateFromFile(string path)
        {
            TemplateString = System.IO.File.ReadAllText(path);
        }

        /// <summary>
        /// Ersetzt alle Eigenschaften aus TemplateDataModel im StringTemplate. Name der Eigenschaften im TemplateDataModel und Name der ${Platzhalter} müssen gleich sein. 
        /// Beispiel: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>HTML Datei mit Daten aus dem TemplateDataModel </returns>
        public string CreateStringFromTemplate(string stringTemplate = null)
        {
            try
            {
            TemplateString = stringTemplate;
            return CreateStringFromTemplate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }
        /// <summary>
        /// Ersetzt alle Eigenschaften aus TemplateDataModel im StringTemplate. Name der Eigenschaften im TemplateDataModel und Name der ${Platzhalter} müssen gleich sein. 
        /// Beispiel: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>HTML Datei mit Daten aus dem TemplateDataModel </returns>
        public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
        {
            TemplateString = stringTemplate;
            _templateDataModel = templateDataModel;
            return CreateStringFromTemplate();
        }
        /// <summary>
        /// Ersetzt alle Eigenschaften aus TemplateDataModel im StringTemplate. Name der Eigenschaften im TemplateDataModel und Name der ${Platzhalter} müssen gleich sein. 
        /// Beispiel: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>HTML Datei mit Daten aus dem TemplateDataModel </returns>
        public string CreateStringFromTemplate(T templateDataModel)
        {
            _templateDataModel = templateDataModel;
            return CreateStringFromTemplate();
        }
        private void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value)
        {   
            switch (valueType.Name)
            {
                case "String": 
                    if (value == null)
                        _outputString = _outputString.Replace(placeholderValueName, _nullStringValue);
                    else
                        _outputString = _outputString.Replace(placeholderValueName, (String)value);
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
                        // TODO: Support Types from System.Collections.Generic (List, Dictionary,...)
                        #if DEBUG
                        #warning types of System.Collections.Generic are not supported in this Version 
                        #endif

                        break;
                    }
                    throw new NotSupportedException($"Type '{valueType.ToString()}' not supported by TemplateEngine.createStringFromTemplate().");
            }
          
        }
        private void ProcessTemplateDataModelClassMethods()
        {

            Type t = _templateDataModel.GetType();
            List<MethodInfo> methodInfos = t.GetMethods().Where(mi => mi.IsSpecialName == false
                                                            && !_methodBlacklist.Contains(mi.Name)
                                                            ).ToList();

            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.IsPublic)
                {
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Count() > 0) continue; // TODO: Methoden mit Parameter unterstüzen. Dazu über Methodenname die angegebenen Parameter in der HTML file auslesen
                   
                    var instance = Activator.CreateInstance(t);
                    object methodValue = methodInfo.Invoke(instance, parameters);
                    Type methodValueType = methodInfo.ReturnType;
                    string methodName = _openingDelimiter + methodInfo.Name + "()" + _closeingDelimiter;
                    try
                    {
                        this.ReplacePlaceholderWithValue(methodValueType, methodName, methodValue);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        private void ProcessTemplateDataModelClassPropertys()
        {
            Type t = _templateDataModel.GetType();
            PropertyInfo[] propertyInfos = t.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if ((propertyInfo.CanRead))
                {
                    object propertyValue = propertyInfo.GetValue(_templateDataModel, null);
                    string propertyName = _openingDelimiter + propertyInfo.Name + _closeingDelimiter;
                    Type propertyValueType = null;

                    if (propertyValue != null) propertyValueType = propertyValue?.GetType();
                    else propertyValueType = typeof(string);
                    try
                    {
                        this.ReplacePlaceholderWithValue(propertyValueType,  propertyName, propertyValue);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        /// <summary>
        /// Ersetzt alle Eigenschaften aus TemplateDataModel in der Templatezeichenkette. Name der Eigenschaften im TemplateDataModel und Name der ${Platzhalter} müssen gleich sein. 
        /// Beispiel: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>HTML Datei mit Daten aus dem TemplateDataModel </returns>
        private string CreateStringFromTemplate()
        {
            _outputString=_templateString;
            ProcessTemplateDataModelClassMethods();
            ProcessTemplateDataModelClassPropertys();           
            return _outputString;
        }

    }

}
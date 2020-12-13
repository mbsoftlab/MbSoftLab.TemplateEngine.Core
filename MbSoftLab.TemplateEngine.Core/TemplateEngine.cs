using System;
using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Core
{
    /// <summary>
    /// A simple StringTemplateengine for .NET. <br></br>
    /// See <a href="https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/blob/master/README.md"/> for more details
    /// </summary>
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
  
        #region --- PUBLIC PROPERTYS
        /// <summary>
        /// Beginning Char for a PlaceholderProperty. The Defaultvalue ist "${".
        /// </summary>
        public string OpeningDelimiter { get=>_openingDelimiter; set=>_openingDelimiter=value.Trim(); }
        private string _openingDelimiter = "${";
        /// <summary>
        /// Ending Char for a PlaceholderProperty. Der Default ist "}".
        /// </summary>
        public string CloseingDelimiter { get=>_closeingDelimiter; set=>_closeingDelimiter=value.Trim(); }
        private string _closeingDelimiter = "}";  
        /// <summary>
        /// Datenmodell mit Propertys zum befüllen der ${PlaceholderPropertys} im Template. Name der Propertys im DataModel muss den Namen in den ${Placeholder} entsprechen
        /// </summary>
        public T TemplateDataModel { get => _templateDataModel; set => _templateDataModel = value; }
        T _templateDataModel;
        /// <summary>
        /// The Templatestring with ${PlaceholderPropertys}
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
        /// Get or Set the string for NULL-Values. Default = NULL. 
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
        /// Loads a Templatestring from File
        /// </summary>
        /// <param name="path">Path to File with Templatestring.</param>
        public void LoadTemplateFromFile(string path)
        {
            TemplateString = System.IO.File.ReadAllText(path);
        }

        /// <summary>
        /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
        /// Example: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>File with Data from TemplateDataModel </returns>
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
        /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
        /// Example: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>File with Data from TemplateDataModel </returns>
        public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
        {
            TemplateString = stringTemplate;
            _templateDataModel = templateDataModel;
            return CreateStringFromTemplate();
        }
        /// <summary>
        /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
        /// Example: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>File with Data from TemplateDataModel </returns>
        public string CreateStringFromTemplate(T templateDataModel)
        {
            _templateDataModel = templateDataModel;
            return CreateStringFromTemplate();
        }

        /// <summary>
        /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
        /// Example: public string MyProperty  => ${MyProperty}
        /// </summary>
        /// <returns>File with Data from TemplateDataModel </returns>
        private string CreateStringFromTemplate()
        {
            _outputString=_templateString;
            
            IPlaceholderValueRaplacer placeholderValueRaplacer = new PlaceholderValueRaplacer(_outputString, _nullStringValue);
            TemplateDataModelProcessor templateDataModelProcessor = new TemplateDataModelProcessor(_openingDelimiter, _closeingDelimiter, placeholderValueRaplacer);
            templateDataModelProcessor.ProcessTemplateDataModell(_templateDataModel);

            return placeholderValueRaplacer.OutputString;
        }

    }

}
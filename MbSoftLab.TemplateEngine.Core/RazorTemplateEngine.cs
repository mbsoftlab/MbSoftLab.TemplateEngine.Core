using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MbSoftLab.TemplateEngine.Core
{
    public class RazorTemplateEngine<T> : ITemplateEngine<T> where T: TemplateDataModel
    {
        public string CloseingDelimiter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ITemplateEngineConfig<T> Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CultureInfo CultureInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string NullStringValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OpeningDelimiter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T TemplateDataModel { get; set; }
        public string TemplateString { get ; set ; }
        IRazorEngine razorEngine;
        public RazorTemplateEngine(IRazorEngine razorEngine)
        {
            this.razorEngine = razorEngine;
        }
        public RazorTemplateEngine()
        {
            razorEngine = new RazorEngine();
        }
        public string CreateStringFromTemplate(string csHtmlTemplate = null)
        {
            TemplateString = csHtmlTemplate ?? TemplateString;
            IRazorEngineCompiledTemplate template = razorEngine.Compile(TemplateString);

            string result = template.Run(TemplateDataModel);
            return result;
        }

        public string CreateStringFromTemplate(T templateDataModel)
        {
            TemplateDataModel = templateDataModel;
            return CreateStringFromTemplate();
        
        }

        public string CreateStringFromTemplate(T templateDataModel, string csHtmlTemplate)
        {
            TemplateDataModel = templateDataModel;
            TemplateString = csHtmlTemplate;
            return CreateStringFromTemplate();
        }
    }
}

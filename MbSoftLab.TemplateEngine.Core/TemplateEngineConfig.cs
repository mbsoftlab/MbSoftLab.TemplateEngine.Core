using System;
using System.Collections.Generic;
using System.Text;

namespace MbSoftLab.TemplateEngine.Core
{
    public interface ITemplateEngineConfig: ITemplateEngineConfig<object>{ }
    public interface ITemplateEngineConfig<T>
    {
        string OpeningDelimiter { get; set; }
        string CloseingDelimiter { get; set; }
        string TemplateString { get; set; }
        T TemplateDataModel { get; set; }
        string NullStringValue { get; set; }
    }
    public class TemplateEngineConfig : TemplateEngineConfig<object>{ }
    public class TemplateEngineConfig<T> : ITemplateEngineConfig<T>
    {
        public string OpeningDelimiter { get => _openingDelimiter; set => _openingDelimiter=value.Trim(); }
        private string _openingDelimiter;
        public string CloseingDelimiter { get => _closeingDelimiter; set => _closeingDelimiter=value.Trim(); }
        private string _closeingDelimiter;
        public string TemplateString { get => _templateString; set => _templateString=value; }
        private string _templateString;
        public T TemplateDataModel { get => _templateDataModel; set => _templateDataModel=value; }
        private T _templateDataModel;
        public string NullStringValue { get => _nullStringValue; set => _nullStringValue=value; }
        private string _nullStringValue;
    }
}

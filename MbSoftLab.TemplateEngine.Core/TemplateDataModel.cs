using RazorEngineCore;

namespace MbSoftLab.TemplateEngine.Core
{
    public class TemplateDataModel<T> : RazorEngineTemplateBase
    {
        public new T Model { get; set; }
    }
}

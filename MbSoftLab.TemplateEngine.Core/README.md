<?xml version="1.0"?>
<doc>
    <assembly>
        <name>MbSoftLab.TemplateEngine.Core</name>
    </assembly>
    <members>
        <member name="T:MbSoftLab.TemplateEngine.Core.TemplateEngine">
            <summary>
            A simple StringTemplateengine for .NET. <br></br>
            See <a href="https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/blob/master/README.md"/> for more details
            </summary>
        </member>
        <member name="P:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.OpeningDelimiter">
            <summary>
            Beginning Char for a PlaceholderProperty. The Defaultvalue ist "${".
            </summary>
        </member>
        <member name="P:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.CloseingDelimiter">
            <summary>
            Ending Char for a PlaceholderProperty. Der Default ist "}".
            </summary>
        </member>
        <member name="P:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.TemplateDataModel">
            <summary>
            Datenmodell mit Propertys zum befüllen der ${PlaceholderPropertys} im Template. Name der Propertys im DataModel muss den Namen in den ${Placeholder} entsprechen
            </summary>
        </member>
        <member name="P:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.TemplateString">
            <summary>
            The Templatestring with ${PlaceholderPropertys}
            </summary>
        </member>
        <member name="P:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.NullStringValue">
            <summary>
            Get or Set the string for NULL-Values. Default = NULL. 
            </summary>
        </member>
        <member name="M:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.LoadTemplateFromFile(System.String)">
            <summary>
            Loads a Templatestring from File
            </summary>
            <param name="path">Path to File with Templatestring.</param>
        </member>
        <member name="M:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.CreateStringFromTemplate(System.String)">
            <summary>
            Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
            Example: public string MyProperty  => ${MyProperty}
            </summary>
            <returns>File with Data from TemplateDataModel </returns>
        </member>
        <member name="M:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.CreateStringFromTemplate(`0,System.String)">
            <summary>
            Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
            Example: public string MyProperty  => ${MyProperty}
            </summary>
            <returns>File with Data from TemplateDataModel </returns>
        </member>
        <member name="M:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.CreateStringFromTemplate(`0)">
            <summary>
            Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
            Example: public string MyProperty  => ${MyProperty}
            </summary>
            <returns>File with Data from TemplateDataModel </returns>
        </member>
        <member name="M:MbSoftLab.TemplateEngine.Core.TemplateEngine`1.CreateStringFromTemplate">
            <summary>
            Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
            Example: public string MyProperty  => ${MyProperty}
            </summary>
            <returns>File with Data from TemplateDataModel </returns>
        </member>
    </members>
</doc>

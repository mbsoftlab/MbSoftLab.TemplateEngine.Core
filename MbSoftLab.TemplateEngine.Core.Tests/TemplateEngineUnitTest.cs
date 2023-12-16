using MbSoftLab.TemplateEngine.Core;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;

namespace TemplateEngineCore.Tests
{

    [TestFixture]
    public class TemplateEngineUnitTest : UnitTestBase
    {

        [Test]
        public void can_create_a_valid_template()
        {

            //Arrange  
            var sut = new TemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>DummyStringProp1Value</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_handle_null_Values_in_Propertys()
        {

            //Arrange
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>NULL</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString== ReturnString);
        }
        [Test]
        public void can_set_a_custom_null_value_String()
        {

            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_set_a_template()
        {
            //Arrange
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            string ShouldReturnString = "<MyTag>Nothing</MyTag>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_use_the_config()
        {
            //Arrange   

            TemplateEngineConfig templateEngineConfig = new TemplateEngineConfig()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "---",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>{{DummyStringProp2}}</TagName>"
            };
            var sut = new TemplateEngine(); //SUT = [S]ystem [U]nder [T]est
            sut.Config = templateEngineConfig;

            string ShouldReturnString = "<TagName>---</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.That(ShouldReturnString==ReturnString);
        }
        [Test]
        public void can_use_the_generic_config()
        {
            //Arrange 

            TemplateEngineConfig<TemplateDataModelDummy> templateEngineConfig = new TemplateEngineConfig<TemplateDataModelDummy>()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "---",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>{{DummyStringProp2}}</TagName>"
            };
            var sut = new TemplateEngine<TemplateDataModelDummy>();
            sut.Config = templateEngineConfig;

            string ShouldReturnString = "<TagName>---</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_set_a_template_and_model_on_creating()
        {
            //Arrange
            var sut = new TemplateEngine<TemplateDataModelDummy>();
            sut.NullStringValue = "Nothing";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>");


            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_set_a_model_on_creating()
        {
            //Arrange 
            var sut = new TemplateEngine();
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<TagName>${DummyStringProp2}</TagName>";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy());


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_set_a_DataModel_with_Annonymos_Type()
        {
            //Arrange   
            var sut = new TemplateEngine(new { DummyStringProp2 = "2" }); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            string ShouldReturnString = "<MyTag>2</MyTag>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_set_a_different_DataModel_with_annonymos_type_after_create_an_instance()
        {
            //Arrange
            var sut = new TemplateEngine(new { DummyStringProp2 = "2" }, "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            sut.TemplateDataModel = new { DummyStringProp2 = "5" };
            string ShouldReturnString = "<MyTag>5</MyTag>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void can_change_the_default_delimiters()
        {
            //Arrange  
            var sut = new TemplateEngine(new { DummyStringProp2 = "2" }, "<TagName>{{DummyStringProp2}}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.OpeningDelimiter = "{{";
            sut.CloseingDelimiter = "}}";
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>{{DummyStringProp2}}</MyTag>";
            sut.TemplateDataModel = new { DummyStringProp2 = "5" };
            string ShouldReturnString = "<MyTag>5</MyTag>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        public void throws_exeption_if_type_not_supported()
        {
            //Arrange 
            var sut = new TemplateEngine(GetAWrongTemplateDataModelDummy(), "<TagName>${DummyObjectProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est


            //Assert
            Assert.Throws<NotSupportedException>(delegate
            {
                //Act 
                sut.CreateStringFromTemplate();
            });

        }
        [Test]
        public void throws_excepton_if_file_load_fail()
        {
            //Arrange  
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyObjectProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            //Assert 
            Assert.Throws<FileNotFoundException>(delegate
            {

                //Act
                sut.LoadTemplateFromFile("NonExistingFile.txt");
            });
        }
        [Test]
        public void can_handle_a_DataModel_with_a_list_property()
        {
            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>${DummyStringListProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            //Act / Assert 
            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });

        }
        [Test]
        public void can_handle_a_Method_in_DataModel_without_Exeption()
        {
            //Arrange   
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>${StringReturningMethod}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });
        }
        [Test]
        [TestCase("StringReturningMethod()", "StringReturnValue")]
        [TestCase("BoolReturningMethod()", "True")]
        public void can_handle_return_values_from_a_method(string methodName, string returnValue)
        {
            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${" + methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [TestCase]
        public void can_handle_return_values_from_IntReturningMethod()
        {
            //Arrange 
            string methodName = "IntReturningMethod()";
            string returnValue = Convert.ToString(Convert.ToInt32("12"));
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${" + methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [TestCase]
        public void can_handle_return_values_from_DoubleReturningMethod()
        {
            //Arrange 
            string methodName = "DoubleReturningMethod()";
            double value = 1.2;
            string returnValue = Convert.ToString(value, CultureInfo.CreateSpecificCulture("en-US"));
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${" + methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [Test]
        [TestCase("DummyByteProp", "255")]
        [TestCase("DummySByteProp", "-5")]
        [TestCase("DummyCharProp", "c")]
        [TestCase("DummyUInt16", "8")]
        [TestCase("DummyInt16", "-8")]
        [TestCase("DummyIntProp1", "1")]
        [TestCase("DummyInt64Prop", "1234567")]
        [TestCase("DummyDecimalProp", "55")]
        [TestCase("DummBoolProp1", "True")]
        [TestCase("DummyBoolQProp1", "True")]// Bool With Null 
        [TestCase("DummyBoolProp2", "False")]
        [TestCase("DummyBoolQProp2", "NULL")]
        //[TestCase("DummyDoubleProp1", "1.75")]
        public void can_handle_values_from_propertys(string propertyName, string expectedOutput)
        {
            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();

            //Assert  
            Assert.That(ShouldReturnString == ReturnString);
        }
        [TestCase]
        public void can_handle_double_values_from_propertys()
        {
            //Arrange   
            string propertyName = "DummyDoubleProp1";
            double value = 1.75;
            string expectedOutput = Convert.ToString(value, CultureInfo.CreateSpecificCulture("en-US"));
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [TestCase]
        public void can_handle_date_values_from_propertys()
        {
            string propertyName = "DummyDateTimeProp1";
            string expectedOutput = Convert.ToString(Convert.ToDateTime("01.01.2020 00:00:00"), CultureInfo.CreateSpecificCulture("en-US"));

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.That(ShouldReturnString == ReturnString);
        }
        [TestCase]
        public void can_create_and_use_SpecificCulture()
        {
            //Arrange   
            string propertyName = "DummyDoubleProp1";
            double value = 1.75;
            string expectedOutput = Convert.ToString(value, CultureInfo.CreateSpecificCulture("de-DE"));
            TemplateEngine sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>");
            sut.CultureInfo = CultureInfo.CreateSpecificCulture("de-DE");
            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.That(ShouldReturnString == ReturnString);
        }

    }
}

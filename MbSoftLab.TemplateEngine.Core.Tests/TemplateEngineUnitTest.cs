using NUnit.Framework;
using System;
using MbSoftLab.TemplateEngine.Core;
using System.Collections.Generic;
using System.IO;

namespace TemplateEngineCore.Tests
{

    [TestFixture]
    public class TemplateEngineUnitTest
    {
 
        private TemplateDataModelDummy GetTemplateDataModelDummy()
        {
            TemplateDataModelDummy templateDataModelFake = new TemplateDataModelDummy
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyIntProp1 = 1,
                DummBoolProp1 = true,
                DummyBoolQProp1 = true,
                DummyDoubleProp1 = 1.75,
                DummyDateTimeProp1 = DateTime.Parse("01.01.2020 00:00:00"),
                //DummyObjectProp1 = new DummyCustomObject(),
                DummyInt64Prop = 1234567,
                DummyUInt16 = 8,
                DummyInt16 = -8,
                DummyDecimalProp = 55,
                DummyCharProp = 'c',
                DummyByteProp = 255,
                DummySByteProp = -5

            };

            return templateDataModelFake;
        }
        private TemplateDataModelDummy GetAWrongTemplateDataModelDummy()
        {
            TemplateDataModelDummy templateDataModelFake = new TemplateDataModelDummy
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyIntProp1 = 1,
                DummBoolProp1 = true,
                DummyBoolQProp1 = true,
                DummyDoubleProp1 = 1.75,
                DummyObjectProp1 = new DummyCustomObject(),
                DummyInt64Prop = 1234567,
                DummyUInt16 = 8,
                DummyInt16 = -8,
                DummyDecimalProp = 55,
                DummyCharProp = 'c',
                DummyByteProp = 255,
                DummySByteProp = -5

            };

            return templateDataModelFake;
        }
        private TemplateDataModelDummyWithList GetTemplateDataModelDummyWithListAndMethod()
        {
            TemplateDataModelDummyWithList templateDataModelFake = new TemplateDataModelDummyWithList
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyStringListProp2 = new List<string>() { "asd", "asd" }

            };

            return templateDataModelFake;
        }
        private TemplateDataModelDummyWithMethods GetTemplateDataModelDummyWithMethods()
        {
            TemplateDataModelDummyWithMethods templateDataModelFake = new TemplateDataModelDummyWithMethods();

            return templateDataModelFake;
        }

        [Test]
        public void can_create_a_valid_template()
        {

            //Arrange  
            var sut = new TemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>DummyStringProp1Value</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
        [TestCase("IntReturningMethod()", "12")]
        public void can_handle_return_values_from_a_method(string methodName,string returnValue)
        {
            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${"+ methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>"+returnValue+"</TagName>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }

        [TestCase]
        public void can_handle_return_values_from_a_method()
        {
            //Arrange 
            string methodName = "DoubleReturningMethod()";
            string returnValue = Convert.ToString(Convert.ToDouble("1,2"));
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${" + methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert 
            Assert.AreEqual(ShouldReturnString, ReturnString);
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
  
        public void can_handle_values_from_propertys(string propertyName, string expectedOutput)
        {
            //Arrange 
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert  
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }

        [TestCase]
        public void can_handle_double_values_from_propertys()
        {
            string propertyName = "DummyDoubleProp1";
            string expectedOutput= Convert.ToString(Convert.ToDouble("1,75"));

            //Arrange   
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [TestCase] 
        public void can_handle_date_values_from_propertys()
        {
            string propertyName = "DummyDateTimeProp1";
            string expectedOutput = Convert.ToString(Convert.ToDateTime("01.01.2020 00:00:00"));

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }


    }
}

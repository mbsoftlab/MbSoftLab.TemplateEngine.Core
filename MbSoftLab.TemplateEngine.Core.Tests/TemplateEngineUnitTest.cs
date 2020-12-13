using NUnit.Framework;
using System;
using MbSoftLab.TemplateEngine.Core;
using System.Collections.Generic;

namespace TemplateEngineCore.Tests
{
    public class DummyCustomObject
    {
        public String Name { get; set; } = "MyName";
    }
    public class TemplateDataModelDummy
    {

        public string DummyStringProp1 { get; set; }
        public string DummyStringProp2 { get; set; }

        public DateTime DummyDateTimeProp1 { get; set; }

        public int DummyIntProp1 { get; set; }
        public int DummyIntProp2 { get; set; }

        public Int64 DummyInt64Prop { get; set; }

        public UInt16 DummyUInt16 { get; set; }

        public Int16 DummyInt16 { get; set; }
        public Decimal DummyDecimalProp { get; set; }

        public byte DummyByteProp { get; set; }
        public sbyte DummySByteProp { get; set; }
        public char DummyCharProp { get; set; }

        public double DummyDoubleProp1 { get; set; }
        public double DummyDoubleProp2 { get; set; }

        public bool DummBoolProp1 { get; set; }
        public bool DummyBoolProp2 { get; set; }

        public bool? DummyBoolQProp1 { get; set; }
        public bool? DummyBoolQProp2 { get; set; }

        public object DummyObjectProp1 { get; set; }
        public object DummyObjectProp2 { get; set; }
    }
    public class TemplateDataModelDummyWithList
    {
        public string DummyStringProp1 { get; set; }
        public List<string> DummyStringListProp2 { get; set; }
        public IList<string> DummyStringIListProp { get; set; }
        public Dictionary<string, int> DummyStringDicProp { get; set; }
        public IDictionary<string, int> DummyStringIDicProp { get; set; }

        public IEnumerable<string> DummyStringIEnumerableProp { get; set; }

        public string StringReturningMethod()
        {
            return "StringReturnValue";
        }

    }
    public class TemplateDataModelDummyWithMethods
    {

        public string StringReturningMethod()
        {
            return "StringReturnValue";
        }
        public string StringReturningMethod(string parameter)
        {
            return parameter;
        }
        public bool BoolReturningMethod()
        {
            return true;
        }
        public int IntReturningMethod()
        {
            return 12;
        }
        public double DoubleReturningMethod()
        {
            return 1.2;
        }
        private double PrivateDoubleReturningMethod() // Test if has a effect
        {
            return 1.2;
        }


    }

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

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>DummyStringProp1Value</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_handle_null_Values_in_Propertys()
        {

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string ShouldReturnString = "<TagName>NULL</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_custom_null_value_String()
        {

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_template()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            string ShouldReturnString = "<MyTag>Nothing</MyTag>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_use_the_config()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   

            TemplateEngineConfig templateEngineConfig = new TemplateEngineConfig()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "---",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>{{DummyStringProp2}}</TagName>"
            };
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(); //SUT = [S]ystem [U]nder [T]est
            sut.Config = templateEngineConfig;

            string ShouldReturnString = "<TagName>---</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_use_the_generic_config()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   

            TemplateEngineConfig<TemplateDataModelDummy> templateEngineConfig = new TemplateEngineConfig<TemplateDataModelDummy>()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "---",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>{{DummyStringProp2}}</TagName>"
            };
            var sut = new TemplateEngine<TemplateDataModelDummy>(); //SUT = [S]ystem [U]nder [T]est
            sut.Config = templateEngineConfig;

            string ShouldReturnString = "<TagName>---</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_template_and_model_on_creating()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine<TemplateDataModelDummy>(); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy(), "<TagName>${DummyStringProp2}</TagName>");


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_model_on_creating()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<TagName>${DummyStringProp2}</TagName>";
            string ShouldReturnString = "<TagName>Nothing</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy());


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_DataModel_with_Annonymos_Type()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(new { DummyStringProp2 = "2" }); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            string ShouldReturnString = "<MyTag>2</MyTag>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_set_a_different_DataModel_with_annonymos_type_after_create_an_instance()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(new { DummyStringProp2 = "2" }, "<TagName>${DummyStringProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>${DummyStringProp2}</MyTag>";
            sut.TemplateDataModel = new { DummyStringProp2 = "5" };
            string ShouldReturnString = "<MyTag>5</MyTag>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void can_change_the_default_delimiters()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(new { DummyStringProp2 = "2" }, "<TagName>{{DummyStringProp2}}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.OpeningDelimiter = "{{";
            sut.CloseingDelimiter = "}}";
            sut.NullStringValue = "Nothing";
            sut.TemplateString = "<MyTag>{{DummyStringProp2}}</MyTag>";
            sut.TemplateDataModel = new { DummyStringProp2 = "5" };
            string ShouldReturnString = "<MyTag>5</MyTag>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }
        [Test]
        public void throws_exeption_if_type_not_supported()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetAWrongTemplateDataModelDummy(), "<TagName>${DummyObjectProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est


            //Assert Prüfen der Ergebnisse 
            Assert.Throws<NotSupportedException>(delegate
            {
                //Act Ausführen der zu testenden Funktion
                sut.CreateStringFromTemplate();
            });

        }
        [Test]
        public void throws_excepton_if_file_load_fail()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${DummyObjectProp1}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            //Assert Prüfen der Ergebnisse 
            Assert.Throws<System.IO.FileNotFoundException>(delegate
            {

                //Act Ausführen der zu testenden Funktion
                sut.LoadTemplateFromFile("NonExistingFile.txt");
            });
        }
        [Test]
        public void can_handle_a_DataModel_with_a_list_property()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>${DummyStringListProp2}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });

        }
        [Test]
        public void can_handle_a_Method_in_DataModel_without_Exeption()
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new MbSoftLab.TemplateEngine.Core.TemplateEngine(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>${StringReturningMethod}</TagName>"); //SUT = [S]ystem [U]nder [T]est
            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });
        }


        [Test]
        [TestCase("StringReturningMethod()", "StringReturnValue")]
        [TestCase("BoolReturningMethod()", "True")]
        [TestCase("IntReturningMethod()", "12")]  
        [TestCase("DoubleReturningMethod()", "1,2")]
        public void can_handle_return_values_from_a_method(string methodName,string returnValue)
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine(GetTemplateDataModelDummyWithMethods(), "<TagName>${"+ methodName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>"+returnValue+"</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
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
        [TestCase("DummyDateTimeProp1", "01.01.2020 00:00:00")]
        public void can_handle_values_from_propertys(string propertyName, string expectedOutput)
        {
            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new TemplateEngine(GetTemplateDataModelDummy(), "<TagName>${" + propertyName + "}</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string ShouldReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act Ausführen der zu testenden Funktion
            string ReturnString = sut.CreateStringFromTemplate();


            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(ShouldReturnString, ReturnString);
        }

        [TestCase]
        public void can_handle_double_values_from_propertys()
        {
            string propertyName = "DummyDoubleProp1";
            string expectedOutput= Convert.ToString(Convert.ToDouble("1,75"));

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

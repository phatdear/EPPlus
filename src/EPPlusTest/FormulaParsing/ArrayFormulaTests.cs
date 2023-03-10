﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPPlusTest.FormulaParsing
{
    [TestClass]

    public class ArrayFormulaTests : TestBase
    {
        private static ExcelPackage _package;
        private static ExcelWorksheet _ws;
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _package = OpenPackage("ArrayFormulas.xlsx",true);
            _ws = _package.Workbook.Worksheets.Add("Data");
            LoadTestdata(_ws);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            SaveAndCleanup(_package);
        }

        [TestMethod]
        public void ArrayFormulaSimpleRange()
        {
            _ws.Cells["F1"].Formula = "F10";
            _ws.Cells["F2:F10"].CreateArrayFormula("A1:A9");
            _ws.Calculate();

            Assert.AreEqual(_ws.GetValue(1, 1), _ws.GetValue(2, 6));
            Assert.AreEqual(_ws.GetValue(2, 1), _ws.GetValue(3, 6));
            Assert.AreEqual(_ws.GetValue(9, 1), _ws.GetValue(10, 6));

            Assert.AreEqual(((DateTime)_ws.GetValue(10, 6)).ToOADate(), _ws.GetValue(1, 6));    //A1 is converted to AO-date. Correct?
        }
        [TestMethod]
        public void ArrayFormulaMultiplyRange()
        {
            _ws.Cells["G1"].Formula = "G10";
            _ws.Cells["G2:G20"].CreateArrayFormula("(B2:B10 + 1) * 2");
            _ws.Calculate();

            Assert.AreEqual((_ws.Cells["B2"].GetValue<double>() + 1) * 2, _ws.Cells["G2"].Value);
            Assert.AreEqual((_ws.Cells["B10"].GetValue<double>() + 1) * 2, _ws.Cells["G10"].Value);
            Assert.AreEqual(_ws.Cells["G10"].Value, _ws.Cells["G1"].Value);

            Assert.AreEqual(((ExcelErrorValue)_ws.Cells["G11"].Value).Type, eErrorType.NA);
            Assert.AreEqual(((ExcelErrorValue)_ws.Cells["G20"].Value).Type, eErrorType.NA);
        }
        [TestMethod]
        public void ArrayFormulaTranspose()
        {
            _ws.Cells["G1"].Formula = "G10";
            _ws.Cells["H5:P5"].CreateArrayFormula("Transpose(C2:C10)");
            _ws.Calculate();

            Assert.AreEqual("Value 2", _ws.Cells["H5"].Value);
            Assert.AreEqual("Value 3", _ws.Cells["I5"].Value);
            Assert.AreEqual("Value 10", _ws.Cells["P5"].Value);
        }

    }
}

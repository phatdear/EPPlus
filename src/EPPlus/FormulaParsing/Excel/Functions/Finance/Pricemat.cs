﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  15/08/2023         EPPlus Software AB           EPPlus v7
 *************************************************************************************************/

using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.FinancialDayCount;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Metadata;
using OfficeOpenXml.FormulaParsing.FormulaExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeOpenXml.FormulaParsing.Excel.Functions.Finance
{
    [FunctionMetadata(
    Category = ExcelFunctionCategory.Financial,
    EPPlusVersion = "7.0",
    Description = "Calculates the price of a security that pays interest maturity (per 100 dollars)")]
    internal class Pricemat : ExcelFunction
    {
        public override int ArgumentMinLength => 5;

        public override CompileResult Execute(IList<FunctionArgument> arguments, ParsingContext context)
        {
            var settlementDate = System.DateTime.FromOADate(ArgToInt(arguments, 0));
            var maturityDate = System.DateTime.FromOADate(ArgToInt(arguments, 1));
            var issueDate = System.DateTime.FromOADate(ArgToInt(arguments, 2));
            var rate = ArgToDecimal(arguments, 3);
            var yield = ArgToDecimal(arguments, 4);
            var basis = 0d;

            if (arguments.Count() > 5) basis = ArgToInt(arguments, 5);

            if (rate < 0 || yield < 0) return CreateResult(eErrorType.Num);
            if (basis < 0 || basis > 4) return CreateResult(eErrorType.Num);
            if (settlementDate >= maturityDate) return CreateResult(eErrorType.Num);

            var b = (DayCountBasis)basis;

            var daysDefinition = FinancialDaysFactory.Create(b);
            var B = daysDefinition.DaysPerYear;
            var DSM = daysDefinition.GetDaysBetweenDates(settlementDate, maturityDate);
            var DIM = daysDefinition.GetDaysBetweenDates(issueDate, maturityDate);
            var A = daysDefinition.GetDaysBetweenDates(issueDate, settlementDate);

            var term1 = 100d + DIM / B * rate * 100d;
            var term2 = 1d + DSM / B * yield;
            var term3 = A / B * rate * 100d;

            return CreateResult(term1 / term2 - term3, DataType.Decimal);
        }
    }
}
﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  10/04/2022         EPPlus Software AB       Initial release EPPlus 6.1
 *************************************************************************************************/
using OfficeOpenXml.Core.CellStore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace OfficeOpenXml.Export.ToCollection
{
    /// <summary>
    /// An object that represents a row in the callback function in <see cref="ExcelRangeBase.ToCollection{T}(Func{ToCollectionRow, T}, ToCollectionOptions)"/>
    /// </summary>
    public class ToCollectionRow
    {
        private ExcelWorkbook _workbook;
        internal ToCollectionRow(List<string> headers, ExcelWorkbook workbook)
        {
            _workbook = workbook;
            Headers = headers;

            for(int i = 0; i < headers.Count; i++)
            {
                _headers.Add(headers[i], i);
            }
        }

        internal Dictionary<string, int> _headers=new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
        /// <summary>
        /// Headers used to access cell values.
        /// </summary>
        public List<string> Headers { get; }
        /// <summary>
        /// The rows values
        /// </summary>
        internal List<ExcelValue> _cellValues{ get; set; }
        /// <summary>
        /// Returns the value of the row at the column index
        /// </summary>
        /// <param name="index">the column index</param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= _cellValues.Count)
                {
                    throw (new ArgumentOutOfRangeException("index"));
                }
                return _cellValues[index];
            }
        }
        /// <summary>
        /// Returns the value of the row at the column index
        /// </summary>
        /// <param name="columnName">the column index</param>
        /// <returns></returns>
        public object this[string columnName]
        {
            get
            {
                if (_headers.Count == 0)
                {
                    throw (new ArgumentException($"This range has no headers. Please specify headers or use GetValue<T>(int)."));
                }
                if (_headers.ContainsKey(columnName) == false)
                {
                    throw (new ArgumentException($"Column name {columnName} does not exist in the range."));
                }
                return this[_headers[columnName]];
            }
        }
        /// <summary>
        /// Returns the typed value of the cell at the column index within the row of the range.
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="index">The column index</param>
        /// <returns>The value</returns>
        public T GetValue<T>(int index)
        {
            if(index < 0 || index >= _cellValues.Count)
            {
                throw(new ArgumentOutOfRangeException("index"));
            }
            return ConvertUtil.GetTypedCellValue<T>(_cellValues[index]._value);
        }
        /// <summary>
        /// Returns the typed value of the cell at the column index within the row of the range.
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="columnName">The column name</param>
        /// <returns>The value</returns>
        public T GetValue<T>(string columnName)
        {
            if(_headers.Count==0)
            {
                throw (new ArgumentException($"This range has no headers. Please specify headers or use GetValue<T>(int)."));
            }
            if (_headers.ContainsKey(columnName)==false)
            {
                throw (new ArgumentException($"Column name {columnName} does not exist in the range."));
            }
            return GetValue<T>(_headers[columnName]);
        }
        /// <summary>
        /// Returns formatted value of the cell at the column index within the row of the range.
        /// </summary>
        /// <param name="index">The column index</param>
        /// <returns>The formatted value</returns>
        public string GetText(int index)
        {
            if (index < 0 || index >= _cellValues.Count)
            {
                throw (new ArgumentOutOfRangeException("index"));
            }

            return ValueToTextHandler.GetFormattedText(_cellValues[index]._value, _workbook, _cellValues[index]._styleId, false);
        }
        /// <summary>
        /// Returns formatted value of the cell at the column index within the row of the range.
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <returns>The formatted value</returns>
        public string GetText(string columnName)
        {
            if (_headers.Count == 0)
            {
                throw (new ArgumentException($"This range has no headers. Please specify headers or use GetValue<T>(int)."));
            }
            if (_headers.ContainsKey(columnName) == false)
            {
                throw (new ArgumentException($"Column name {columnName} does not exist in the range."));
            }
            
            return GetText(_headers[columnName]);
        }
    }
}
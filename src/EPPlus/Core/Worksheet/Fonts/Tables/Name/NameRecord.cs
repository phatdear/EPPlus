﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  12/26/2021         EPPlus Software AB       EPPlus 6.0
 *************************************************************************************************/
using OfficeOpenXml.Core.Worksheet.Core.Worksheet.Fonts.FontLocalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeOpenXml.Core.Worksheet.Core.Worksheet.Fonts.Tables.Name
{
    public class NameRecord
    {
        /// <summary>
        /// Platform identifier code.
        /// </summary>
        public ushort platformId { get; set; }

        /// <summary>
        /// Platform-specific encoding identifier.
        /// </summary>
        public ushort encodingId { get; set; }

        /// <summary>
        /// Language identifier.
        /// </summary>
        public ushort languageID { get; set; }

        /// <summary>
        /// Name identifier.
        /// </summary>
        public ushort nameId { get; set; }

        public NameRecordTypes RecordType { get; set; }

        /// <summary>
        /// Name string length in bytes.
        /// </summary>
        public ushort length { get; set; }

        /// <summary>
        /// Name string offset in bytes from stringOffset.
        /// </summary>
        public ushort offset { get; set; }


        public string Name { get; set; }

        public LanguageMapping LanguageMapping { get; set; }

        public override string ToString()
        {
            if(!string.IsNullOrEmpty(Name))
            {
                return Name;
            }
            else
            {
                return base.ToString();
            }
        }

    }
}
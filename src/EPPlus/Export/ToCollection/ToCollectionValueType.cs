﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  01/27/2020         EPPlus Software AB       Initial release EPPlus 5
 *************************************************************************************************/
#if !NET35 && !NET40
#endif
namespace OfficeOpenXml
{
    /// <summary>
    /// The type of value returned for the cells.
    /// </summary>
    public enum ToCollectionValueType
    {
        /// <summary>
        /// The cells value will be returned. Default.
        /// </summary>
        Value,
        /// <summary>
        /// The cells formatted value will be returned.
        /// </summary>
        Text
    }
}
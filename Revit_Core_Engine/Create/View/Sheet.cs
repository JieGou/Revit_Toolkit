﻿/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2023, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using Autodesk.Revit.DB;
using BH.oM.Base.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BH.Revit.Engine.Core
{
    public static partial class Create
    {
        /***************************************************/
        /****               Public Methods              ****/
        /***************************************************/

        [Description("Creates and returns a new Sheet in the current Revit file.")]
        [Input("document", "The current Revit document to be processed.")]
        [Input("sheetName", "Name of the new sheet.")]
        [Input("sheetNumber", "Number of the new sheet.")]
        [Input("viewTemplateId", "The Title Block Id to be applied to the sheet.")]
        [Output("newSheet", "The new sheet.")]
        public static ViewSheet Sheet(this Document document, string sheetName, string sheetNumber, ElementId titleBlockId)
        {
            List<string> sheetNumbersInModel = new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().Select(x => x.SheetNumber).ToList();
            ViewSheet newSheet = ViewSheet.Create(document, titleBlockId);

            if (!string.IsNullOrEmpty(sheetName))
            {
                newSheet.Name = sheetName;
            }

            if (!string.IsNullOrEmpty(sheetNumber))
            {
                int number = 0;
                string uniqueNumber = sheetNumber;

                while (sheetNumbersInModel.Contains(uniqueNumber))
                {
                    number++;
                    uniqueNumber = $"{sheetNumber} ({number})";
                }

                newSheet.SheetNumber = uniqueNumber;
                if (uniqueNumber != sheetNumber)
                {
                    BH.Engine.Base.Compute.RecordWarning($"Sheet named '{sheetNumber}' already exists in the document. Newly created has been named '{uniqueNumber}' instead.");
                }
            }

            return newSheet;
        }

        /***************************************************/
    }
}




/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
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
using BH.oM.Environment.Elements;
using BH.oM.Reflection.Attributes;
using System.ComponentModel;

namespace BH.Revit.Engine.Core
{
    public static partial class Query
    {
        /***************************************************/
        /****              Public methods               ****/
        /***************************************************/

        [Description("Returns the BHoM environment opening type enum value relevant to a given Revit built-in category.")]
        [Input("category", "Revit built-in category to be queried for its correspondent opening type enum value.")]
        [Output("openingType", "BHoM environment opening type enum value relevant to the input Revit built-in category.")]
        public static oM.Environment.Elements.OpeningType? OpeningType(this BuiltInCategory builtInCategory)
        {
            switch (builtInCategory)
            {
                case Autodesk.Revit.DB.BuiltInCategory.OST_Windows:
                    return oM.Environment.Elements.OpeningType.Window;
                case Autodesk.Revit.DB.BuiltInCategory.OST_Doors:
                    return oM.Environment.Elements.OpeningType.Door;
                case Autodesk.Revit.DB.BuiltInCategory.OST_CurtainWallPanels:
                    return oM.Environment.Elements.OpeningType.Window;
            }

            return null;
        }

        /***************************************************/

        [Description("Returns the BHoM environment opening type enum value relevant to a given Revit category.")]
        [Input("category", "Revit category to be queried for its correspondent opening type enum value.")]
        [Output("openingType", "BHoM environment opening type enum value relevant to the input Revit category.")]
        public static oM.Environment.Elements.OpeningType? OpeningType(this Category category)
        {
            if (category == null)
                return null;

            return OpeningType((BuiltInCategory)category.Id.IntegerValue);
        }

        /***************************************************/
    }
}

﻿/*
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
using BH.Engine.Adapters.Revit;
using BH.oM.Adapters.Revit;
using BH.oM.Adapters.Revit.Settings;
using BH.oM.Base;
using BH.oM.Base.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace BH.Revit.Engine.Core
{
    public static partial class Convert
    {
        /***************************************************/
        /****               Public Methods              ****/
        /***************************************************/

        //[Description("Converts a Revit Element to a generic BHoM object, either ModelInstance or DraftingInstance (if the element has location in space) or a BHoMObject otherwise.")]
        //[Input("element", "Revit Element to be converted.")]
        //[Input("settings", "Revit adapter settings to be used while performing the convert.")]
        //[Input("refObjects", "Optional, a collection of objects already processed in the current adapter action, stored to avoid processing the same object more than once.")]
        //[Output("object", "BHoM object resulting from converting the given Revit Element.")]
        public static RevitTypeFragment TypeFragmentFromRevit(this ElementType elementType, RevitSettings settings = null, Dictionary<string, List<IBHoMObject>> refObjects = null)
        {
            if (elementType == null)
                return null;

            settings = settings.DefaultIfNull();

            RevitTypeFragment typeFragment = refObjects.GetValue<RevitTypeFragment>(elementType.Id);
            if (typeFragment != null)
                return typeFragment;

            typeFragment = new RevitTypeFragment();
            typeFragment.Name = elementType.Name;
            typeFragment.SetIdentifiers(elementType);
            typeFragment.CopyParameters(elementType, settings.MappingSettings);
            refObjects.AddOrReplace(elementType.Id, typeFragment);

            return typeFragment;
        }

        /***************************************************/
    }
}




using DbConfigurator.Model.DTOs.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DbConfigurator.DataAccess
{
    public class AreaBusinessUnitComparer : IEqualityComparer<AreaBusinessUnitForParserDto>
    {
        public bool Equals(AreaBusinessUnitForParserDto? x, AreaBusinessUnitForParserDto? y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y))
                return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Area == y.Area && x.BusinessUnit == y.BusinessUnit;
        }

        public int GetHashCode([DisallowNull] AreaBusinessUnitForParserDto obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashAreaName = obj.Area == null ? 0 : obj.Area.GetHashCode();

            //Get hash code for the Code field.
            int hashBusinessUnit = obj.BusinessUnit == null ? 0 : obj.BusinessUnit.GetHashCode();

            //Calculate the hash code for the product.
            return hashAreaName ^ hashBusinessUnit;
        }
    }
}

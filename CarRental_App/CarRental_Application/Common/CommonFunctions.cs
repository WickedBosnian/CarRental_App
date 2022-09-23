using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Common
{
    public static class CommonFunctions
    {
        public static bool ArePropertiesNull(object myObject)
        {
            return !myObject.GetType()
                             .GetProperties()
                             .Select(pi => pi.GetValue(myObject))
                             .Any(value => value != null);
        }
    }
}

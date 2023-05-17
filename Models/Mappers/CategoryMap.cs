using Borigines.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Mappers
{
    internal static class CategoryMap
    {
        internal static Category ToCategory(this IDataRecord record)
        {
            return new Category((int)record["Id"], (string)record["Name_Category"]);
        }
    }
}

using Borigines.Models.Entities;
using System.Data;

namespace Models.Mappers
{
    internal static partial class UserMap
    {
        internal static User ToUser(this IDataRecord record)
        {
            return new User((int)record["Id"], (string)record["First_name"], (string)record["Last_name"], (string)record["Login"], (bool)record["Is_Admin"]);
        }
    }
}

using Borigines.Models.Entities;
using System.Data;


namespace Models.Mappers
{
    internal static class PictureMap
    {
        internal static Picture ToPicture(this IDataRecord record)
        {
            return new Picture((int)record["Id"], (string)record["Name_picture"]);
        }
    }
}



using Borigines.Models.Entities;
using System.Data;


namespace Models.Mappers
{
    internal static class AlbumMap
    {
        internal static Album ToAlbum (this IDataRecord record)
        {
            return new(
                (int)record["Id"], 
                (string)record["Title"], 
                (DateTime)record["Date_Album"],
                new((int)record["uId"], (string)record["First_Name"], (string)record["Last_Name"], (string)record["Login"], (bool)record["Is_Admin"])
                );
        }


    }//end class 
}//end name space 

using Borigines.Models.Entities;
using System.Data;

namespace Models.Mappers
{
    internal static class FullArticleMap
    {
        internal static Article ToFullArticle(this IDataRecord record)
        {
            return new Article(
                (int)record["artId"],
                new Category(
                    (int)record["catId"],
                    (string)record["catName_Category"]),
                new Content(
                    (int)record["IdFr"],
                    (string)record["TitleFr"],
                    (string)record["TextFr"]),
                 new Content(
                    (int)record["IdEn"],
                    (string)record["TitleEn"],
                    (string)record["TextEn"]),
                 new Content(
                    (int)record["IdNl"],
                    (string)record["TitleNl"],
                    (string)record["TextNl"]),
                (DateTime)record["artDate_Article"],
                new User(
                    (int)record["uId"],
                    (string)record["uFirst_name"],
                    (string)record["uLast_name"],
                    (string)record["uLogin"],
                    (bool)record["uIs_Admin"]));
        }
    }
}

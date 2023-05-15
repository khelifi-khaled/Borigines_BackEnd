using Borigines.Models.Entities;
using System.Data;


namespace Models.Mappers
{
    internal static partial class ArticleMap
    {
        internal static Article ToArticle(this IDataRecord record)
        {
            return new Article(
                (int)record["artId"], 
                new Category(
                    (int)record["catId"],
                    (string)record["catName_Category"]),
                new Content(
                    (int)record["frId"], 
                    (string)record["frTitel"],
                    (string)record["frContent"]),
                new Content(
                    (int)record["enId"],
                    (string)record["enTitel"], 
                    (string)record["enContent"]),
                new Content(
                    (int)record["nlId"],
                    (string)record["nlTitel"],
                    (string)record["nlContent"]), 
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

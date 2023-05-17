using BiriginesAPI.DTO;
using Borigines.Models.Entities;

namespace BiriginesAPI.Mappers
{
    internal static class ArticleMap
    {
        internal static GetArticlesByCategoryDTO ToDtoGetArticlesByCategory(this Article article)
        {
            return new GetArticlesByCategoryDTO(article.Id,article.User.Id,article.User.Last_name,article.User.First_name,article.Date,article.CategoryArticle.Id,article.Content.Title , article.Content.Text);
        }
    }
}

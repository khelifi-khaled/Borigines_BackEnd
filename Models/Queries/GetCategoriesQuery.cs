using Borigines.Models.Entities;
using Models.Mappers;

using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetCategoriesQuery : IQuery<IEnumerable<Category>>
    {
        public GetCategoriesQuery()
        {

        }



    }//end GetCategoriesQuery

    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly IDbConnection _dbconnection;

        public GetCategoriesQueryHandler(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public IEnumerable<Category>? Execute(GetCategoriesQuery query)
        {
            string sql = "SELECT Id , Name_Category FROM Categorys ;";
            IEnumerable<Category>? categories = _dbconnection.ExecuteReader(sql , dr => dr.ToCategory());
            return categories;
        }
    }

}//end name space 

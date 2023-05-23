

using Microsoft.AspNetCore.Http;
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public  class CreatePictureAlbumCommand : ICommand
    {
        public CreatePictureAlbumCommand(int idAlbum, string pictureName)
        {
            FK_Album = idAlbum;
            Name_picture = pictureName;
        }

        public int FK_Album { get; init; }

        public string Name_picture { get; init; }


    }//end CreatePictureAlbumCommand

    public class CreatePictureAlbumCommandHandler : ICommandHandler<CreatePictureAlbumCommand>
    {
        private readonly IDbConnection _dbconnection;
        private readonly IHttpContextAccessor _contex;

        public CreatePictureAlbumCommandHandler(IDbConnection dbConnection, IHttpContextAccessor contex)
        {
            _dbconnection = dbConnection;
            _contex = contex;
        }

        public IResult Execute(CreatePictureAlbumCommand command)
        {
            try
            {
                string sql = "INSERT INTO Picture (Name_picture) OUTPUT INSERTED.Id VALUES (@Name_picture)";
                int? FK_Picture = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.Name_picture

                }) as int?;

                sql = "INSERT INTO Album_Picture  (FK_Album , FK_Picture) VALUES (@FK_Album , @FK_Picture)";

                _dbconnection.ExecuteNonQuery(sql, parameters: new { FK_Picture, command.FK_Album });

                string url = _contex.HttpContext!.Request.Scheme + "://" + _contex.HttpContext!.Request.Host.Value + "/Images/" + command.Name_picture;

                return Result.Success(url);
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }

        }//end Execute

        
    }//end CreatePictureAlbumCommandHandler




}//end name space 

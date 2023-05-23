
using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class DeletePictureAlbumCommand : ICommand 
    {
        public DeletePictureAlbumCommand(int idAlbum, string name_Picture)
        {
            IdAlbum = idAlbum;
            Name_Picture = name_Picture;
        }

        public int IdAlbum { get; init; }

        public string Name_Picture { get; init; }

    }//end DeletePictureAlbumCommand

    public class DeletePictureAlbumCommandHandler : ICommandHandler<DeletePictureAlbumCommand>
    {
        private readonly IDbConnection _dbConnection;

        public DeletePictureAlbumCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(DeletePictureAlbumCommand command)
        {
            try
            {
                //geting pic Id from db 
                string sql = "SELECT Id , Name_picture from Picture WHERE Name_picture = @Name_Picture ; ";

                Picture? pictureToDelete = _dbConnection.ExecuteReader(sql, dr => dr.ToPicture(), parameters: new { command.Name_Picture }).SingleOrDefault();
                if (pictureToDelete is null)
                {
                    return Result.Failure("Picture to delete not found");
                }
                //delete pic from Article_Picture
                sql = "DELETE FROM Album_Picture WHERE FK_Picture = @id AND FK_Album = @IdAlbum ; ";
                _dbConnection.ExecuteNonQuery(sql, parameters: new { pictureToDelete.Id, command.IdAlbum });

                //delete pic from Picture table on DB 
                sql = "DELETE FROM Picture WHERE Id = @Id";
                _dbConnection.ExecuteNonQuery(sql, parameters: new { pictureToDelete.Id });



                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }//end Execute 

    }//end DeletePictureCommandHandler



}//end name space 

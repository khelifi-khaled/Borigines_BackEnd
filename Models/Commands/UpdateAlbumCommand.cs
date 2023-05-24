using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class UpdateAlbumCommand : ICommand
    {
        public UpdateAlbumCommand(string title, int albumId)
        {
            Title = title;
            AlbumId = albumId;
        }

        public string  Title { get; init; }

        public int AlbumId { get; init; }

    }//end UpdateAlbumCommand

    public class UpdateAlbumCommandHandler : ICommandHandler<UpdateAlbumCommand>
    {
        private readonly IDbConnection _dbConnection;

        public UpdateAlbumCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(UpdateAlbumCommand command)
        {
            try
            {
                string sql = "UPDATE Albums SET Title = @Title WHERE Id = @AlbumId ; ";
                _dbConnection.ExecuteNonQuery(sql, parameters: command);
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }
    }




}//end name space 

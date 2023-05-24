using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class DeleteAlbumCommand : ICommand
    {
        public DeleteAlbumCommand(int albumId)
        {
            AlbumId = albumId;
        }

        public int AlbumId { get; init; }

    }//end DeleteAlbumCommand

    public class DeleteAlbumCommandHandler : ICommandHandler<DeleteAlbumCommand>
    {
        private readonly IDbConnection _dbConnection;

        public DeleteAlbumCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(DeleteAlbumCommand command)
        {
            try
            {
                string sql = "DELETE FROM Albums WHERE Id = @AlbumId ; ";
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

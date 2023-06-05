
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class CreateAlbumCommand : ICommand
    {
        public CreateAlbumCommand(string title, int id_User)
        {
            Title = title;
            Date_Album = DateTime.Now;
            Id_User = id_User;
        }

        public string Title { get; init; }

        public DateTime Date_Album { get; init; }

        public int Id_User { get; init; }

    }//end CreateAlbumCommand

    public class CreateAlbumCommandHandler : ICommandHandler<CreateAlbumCommand>
    {
        private readonly IDbConnection _dbConnection ;

        public CreateAlbumCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(CreateAlbumCommand command)
        {
            try
            {
                string sql = @"INSERT INTO Albums (Title, Date_Album , Id_user) 
                                              OUTPUT INSERTED.Id VALUES 
                                              (@Title,@Date_Album,@Id_user) ; ";
                int? IdAlbum = _dbConnection.ExecuteScalar(sql, parameters: command) as int?;

                if (IdAlbum is null)
                {
                    return Result.Failure("L'id de l'album est null");
                }
                return Result.Success(IdAlbum.ToString());

            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }//end Execute
    }//end CreateAlbumCommandHandler

}//end name space 

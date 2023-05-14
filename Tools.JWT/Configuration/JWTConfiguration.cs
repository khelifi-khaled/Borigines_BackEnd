
namespace ToolBox.JWT.Configuration
{
    public class JWTConfiguration
    {

        public string Signature { get; set; } = string.Empty;

        public string? Audience  { get; set; }

        public string? Issuer { get; set; }

        //token live Duration  in sec
        public int? Duration { get; set; } 



    }//end class
}//end namespace 

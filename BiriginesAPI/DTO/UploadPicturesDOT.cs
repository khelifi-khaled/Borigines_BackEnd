using System.Text.Json.Serialization;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class UploadPicturesDOT
    {
        public byte[] ArticlePicture
        {
            get; set;
        }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

    }
}

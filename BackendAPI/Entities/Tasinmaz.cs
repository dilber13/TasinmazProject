using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendAPI.Entities
{
    public class Tasinmaz
    {
        public int Id { get; set; }
        public int MahalleId { get; set; }
        public int Ada { get; set; } // Ada numarası
        public int Parsel { get; set; } // Parsel numarası
        public string Nitelik { get; set; } // Tasinmazın niteliği (örneğin, arsa, bina vb.)
        public string KoordinatBilgileri { get; set; } // Koordinat bilgileri (string olarak tutabilirsin)
        public string Adres { get; set; }

        [JsonIgnore] // Sonsuz döngüyü önlemek için
        public Mahalle Mahalle { get; set; }

        public int userId { get; set; }

        public User User { get; set; }
    }
}

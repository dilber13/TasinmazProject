using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackendAPI.Entities
{
    public class Mahalle
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int IlceId { get; set; }

        [ForeignKey("IlceId")]

        [JsonIgnore] // Sonsuz döngüyü önlemek için
        public virtual Ilce? Ilce { get; set; }

        public ICollection<Tasinmaz> Tasinmazlar { get; set; } = new List<Tasinmaz>();
    }

}

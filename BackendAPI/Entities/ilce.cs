using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendAPI.Entities
{
    public class Ilce
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int IlId { get; set; }

        [JsonIgnore] // Sonsuz döngüyü önlemek için
        public Il? Il { get; set; }

        public ICollection<Mahalle> Mahalleler { get; set; } = new List<Mahalle>();
    }

}

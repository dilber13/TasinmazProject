using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackendAPI.Entities
{
    public class Ilce
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int IlId { get; set; }
        
        [ForeignKey("IlId")]

        [JsonIgnore] // Sonsuz döngüyü önlemek için
        public virtual Il? Il { get; set; }

        public ICollection<Mahalle> Mahalleler { get; set; } = new List<Mahalle>();
    }

}

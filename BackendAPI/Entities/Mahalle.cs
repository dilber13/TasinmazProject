using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Entities
{
    public class Mahalle
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int IlceId { get; set; }
        public Ilce Ilce { get; set; }
        public ICollection<Tasinmaz> Tasinmazlar { get; set; }
    }
}

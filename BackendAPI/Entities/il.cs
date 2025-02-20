using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Entities
{
    public class Il
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public ICollection<Ilce> Ilceler { get; set; }
    }
}

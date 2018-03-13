using System;
using System.Threading;

namespace Contactos.Data
{
    public class Contacto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public bool IsFavorite { get; set; }
    }
}

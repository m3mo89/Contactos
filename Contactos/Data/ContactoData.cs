using System;
using System.Collections.Generic;
using System.Linq;

namespace Contactos.Data
{
    public static class ContactoData
    {
        public static List<Contacto> Contacts { get; set; }

        static ContactoData()
        {
            var temp = new List<Contacto>();

            AddContacts(temp);
            AddContacts(temp);
            AddContacts(temp);
            AddContacts(temp);
            AddContacts(temp);
            AddContacts(temp);

            Contacts = temp.OrderBy(i => i.Name).ToList();
        }

        static void AddContacts(List<Contacto> contactos)
        {
            contactos.Add(new Contacto()
            {
                Name = "Bad Luck Brian",
                Phone = "1234567890",
                ImageUrl = "images/badluck.png"
            });

            contactos.Add(new Contacto()
            {
                Name = "Kimmo Km",
                Phone = "1234567890",
                ImageUrl = "images/kimmokm.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Genne Wilder",
                Phone = "1234567890",
                ImageUrl = "images/genewilder.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Jackie Chan",
                Phone = "1234567890",
                ImageUrl = "images/jackiechan.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Trad Cat",
                Phone = "1234567890",
                ImageUrl = "images/tradecat.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Blake Boston",
                Phone = "1234567890",
                ImageUrl = "images/blakeboston.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Laina Morris",
                Phone = "1234567890",
                ImageUrl = "images/lainamorris.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Mia Talerico",
                Phone = "1234567890",
                ImageUrl = "images/miatalerico.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Neil DeGrasse Tyson",
                Phone = "1234567890",
                ImageUrl = "images/neil.jpg"
            });

            contactos.Add(new Contacto()
            {
                Name = "Giorgio A. Tsoukalos",
                Phone = "1234567890",
                ImageUrl = "images/giorgio.jpg",
            });

            contactos.Add(new Contacto()
            {
                Name = "Connor Sinclair",
                Phone = "1234567890",
                ImageUrl = "images/connor.jpg",
            });

            contactos.Add(new Contacto()
            {
                Name = "Yao Ming",
                Phone = "1234567890",
                ImageUrl = "images/yaoming.jpg"
            });
        }
    }
}

using System;
using System.Collections.Generic;
using Contactos.Data;

namespace Contactos.Helpers
{
    public static class SectionIndexerBuilder
    {
        // builds an array of unique section headers, data must be sorted by name
        public static Java.Lang.Object[] BuildSectionHeaders(List<Contacto> data)
        {
            var results = new List<string>();
            var used = new SortedSet<string>();

            foreach (var item in data)
            {
                var letter = item.Name[0].ToString();

                if (!used.Contains(letter))
                    results.Add(letter);

                used.Add(letter);
            }

            var jobjects = new Java.Lang.Object[results.Count];

            for (int i = 0; i < results.Count; i++)
            {
                jobjects[i] = results[i];
            }

            return jobjects;
        }

        // builds a map to answer: position --> section, data must be sorted by name
        public static Dictionary<int, int> BuildSectionForPositionMap(List<Contacto> contactos)
        {
            var results = new Dictionary<int, int>();
            var used = new SortedSet<string>();
            int section = -1;

            for (int i = 0; i < contactos.Count; i++)
            {
                var letter = contactos[i].Name[0].ToString();

                if (!used.Contains(letter))
                {
                    section++;
                    used.Add(letter);
                }

                results.Add(i, section);
            }

            return results;
        }

        // builds a map to answer: section --> position, data must be sorted by name
        public static Dictionary<int, int> BuildPositionForSectionMap(List<Contacto> contactos)
        {
            var results = new Dictionary<int, int>();
            var used = new SortedSet<string>();
            int section = -1;

            for (int i = 0; i < contactos.Count; i++)
            {
                var letter = contactos[i].Name[0].ToString();

                if (!used.Contains(letter))
                {
                    section++;
                    used.Add(letter);
                    results.Add(section, i);
                }
            }

            return results;
        }
    }
}

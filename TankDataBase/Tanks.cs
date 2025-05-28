using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace TankDataBase
{
    public class Tanks
    {
        public Tanks(string name, string from, int age, string category)
        {
            Name = name;
            From = from;
            Age = age;
            Category = category;
        }

        public string Name { get; set; }
        public string From { get; set; }
        public int Age { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Name} | {From} | {Age} | {Category}";
        }

        public static Tanks FromString(string userData)
        {
            var parts = userData.Split(" | ");
            if (parts.Length != 4)
            {
                throw new FormatException("A felhasználói adatok formátuma nem megfelelő");
            }
            return new Tanks(parts[0], parts[1], Int32.Parse(parts[2]), parts[3]);
        }
        public static Tanks FromObject(object userData)
        {
            string[] parts = userData.ToString().Split(" | ");
            if (parts.Length != 4)
            {
                throw new FormatException("A felhasználói adatok formátuma nem megfelelő");
            }
            return new Tanks(parts[0], parts[1], Int32.Parse(parts[2]), parts[3]);
        }
    }
}

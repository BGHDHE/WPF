using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projekt
{
    public class Ability
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{Name} ({Damage} DMG)";
    }

}

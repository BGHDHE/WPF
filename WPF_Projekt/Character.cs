using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projekt
{
    public class Character
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Attack { get; set; }
        public List<Ability> Abilities { get; set; } = new List<Ability>();
 


        public bool IsAlive => HP > 0;

        public string ImagePath { get; set; }


        public void TakeDamage(int damage)
        {
            HP = Math.Max(HP - damage, 0);
        }
    }
}

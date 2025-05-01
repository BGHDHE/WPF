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
        public int Mana { get; set; } // Új tulajdonság
        public int MaxMana { get; set; } // Új tulajdonság
        public int Attack { get; set; }
        public List<Ability> Abilities { get; set; } = new List<Ability>();
        public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();
        public bool IsAlive => HP > 0;
        public string ImagePath { get; set; }

        public void TakeDamage(int damage)
        {
            HP = Math.Max(HP - damage, 0);
        }

        public void ApplyStatusEffects()
        {
            foreach (var effect in StatusEffects.ToArray())
            {
                effect.ApplyEffect(this);
                effect.Duration--;

                if (effect.Duration <= 0)
                    StatusEffects.Remove(effect);
            }
        }
    }
}

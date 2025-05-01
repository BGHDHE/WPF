using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projekt
{
    public enum AbilityEffectType
    {
        Damage,
        Heal,
        Buff
    }

    public class Ability
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
        public string AnimationPath { get; set; }
        public AbilityEffectType EffectType { get; set; }
        public int EffectValue { get; set; } // Gyógyítás vagy bónusz értéke
        public int ManaCost { get; set; } // Új tulajdonság: Mana költség

        public override string ToString() => $"{Name} (Mana: {ManaCost})";
    }
}

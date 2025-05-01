using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Projekt
{
    public class StatusEffect
    {
        public string Name { get; set; } // Az effekt neve
        public int Duration { get; set; } // Az effekt időtartama (körök száma)
        public Action<Character> ApplyEffect { get; set; } // Az effekt hatásának logikája

        public StatusEffect(string name, int duration, Action<Character> applyEffect)
        {
            Name = name;
            Duration = duration;
            ApplyEffect = applyEffect;
        }
    }
}

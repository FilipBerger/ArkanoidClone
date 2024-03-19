using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public abstract class PowerUps : Entity
    {
        public abstract void ApplyEffect();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public abstract class Enemy : Destroyable
    {
        public abstract void Update();
    }
}
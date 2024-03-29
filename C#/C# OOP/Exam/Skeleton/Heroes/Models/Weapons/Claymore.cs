﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Weapons
{
    public class Claymore : Weapon
    {
        public Claymore(string _name, int _durability)
            : base(_name, _durability)
        {
        }

        public override int DoDamage()
        {
            durability--;

            if (durability == 0)
            {
                return 0;
            }

            return 20;
        }
    }
}

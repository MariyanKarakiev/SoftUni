﻿using Military.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Military.Models
{
    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
       protected SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, Corps corps)
           : base(id, firstName, lastName, salary)
        {
            this.Corps = corps;
        }
        public Corps Corps { get; }
    }
}

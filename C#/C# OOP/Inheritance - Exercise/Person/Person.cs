﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
   public class Person
    {
        private int age;
       
        public string Name { get; set; }
       
        public virtual int Age
        {
            get { return this.age; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age cannot be negative");
                }
                else
                {
                    this.age = value;
                }
            }       
        }
        public Person()
        {

        }
        public Person(string name,int age)
        {
            Name = name;          
            Age = age;           
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Age: {age}";
        }
    }
}

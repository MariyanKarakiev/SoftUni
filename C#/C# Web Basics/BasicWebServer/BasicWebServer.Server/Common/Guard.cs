﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Common
{
    public class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "Value";

                throw new ArgumentException($"{name} cannot be null");
            }

        }

        public static void AgainstDuplicatedKey<T,V>(IDictionary<T,V> dictionary, T key, string name)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"{name} already contains key {key.ToString()}");
            }
        }
    }
}

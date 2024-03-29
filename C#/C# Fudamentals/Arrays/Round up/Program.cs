﻿using System;
using System.Linq;

namespace Round_up
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] nums = Console.ReadLine()
                .Split(" ")
                .Select(double.Parse)
                .ToArray();
            int[] rounded = new int[nums.Length];
           

            for (int i = 0; i < nums.Length; i++)
            {
                Convert.ToDecimal(rounded[i] = Math.Round(nums[i], MidpointRounding.AwayFromZero));
            }
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine($"{nums[i]} => {rounded[i]}" );
            }

        }
    }
}

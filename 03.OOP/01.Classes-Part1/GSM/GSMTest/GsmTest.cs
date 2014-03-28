﻿namespace GsmTest
{
    using System;
    using GSM;

    public class GsmTester
    {
        public static void Main()
        {
            GSM[] mobiles = 
            {
                new GSM(
                    "Asha 205 ",
                    "Nokia",
                    "Nikolay Kostadinov",
                    120m,
                    "Nokia",
                    200,
                    10,
                    4,
                    65000u),
                 new GSM(
                    "Asha 205 ",
                    "Nokia",
                    "Hristo Grigorov",
                    120m,
                    "Nokia",
                    200,
                    6,
                    4,
                    65000u),
                  new GSM(
                    "Asha 205 ",
                    "Nokia",
                    "Detelin Yolov",
                    120m,
                    "Nokia",
                    200,
                    15,
                    4,
                    65000u),
            };

            foreach (GSM gsm in mobiles)
            {
                Console.WriteLine(gsm.ToString());
            }

            Console.WriteLine(GSM.Iphone4S);
        }
    }
}
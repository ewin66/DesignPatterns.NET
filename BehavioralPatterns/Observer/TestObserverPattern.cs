﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehavioralPatterns.Observer
{
    public class TestObserverPattern
    {
        public static void Run()
        {
            WeatherData data = new WeatherData(13);

            CurrentConditionDisplay display1 = new CurrentConditionDisplay("Display1", data);
            // OUTPUT --> Display1 - Temperature: 13
            CurrentConditionDisplay display2 = new CurrentConditionDisplay("Display2", data);
            // OUTPUT --> Display2 - Temperature: 13

            // update the temperature.
            data.Temperature = 14;

            // Display 1 and 2 show the new temperature
            // OUTPUT: 
            /* Weather data is updated
               Display1 - Temperature: 14
               Weather data is updated
               Display2 - Temperature: 14
            */

            data.removeObserver(display2);
            data.Temperature = 15;

            // Only Display 1 shows the new temperature
            // OUTPUT: 
            /* Weather data is updated
               Display1 - Temperature: 15
            */

            Console.ReadLine();
        }
    }
}

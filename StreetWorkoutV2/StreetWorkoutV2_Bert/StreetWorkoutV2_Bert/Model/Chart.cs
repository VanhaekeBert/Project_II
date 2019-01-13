using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StreetWorkoutV2_Bert.Model
{
    public class Chart
    {
        public string Month { get; set; }

        public double Target { get; set; }

        public Chart(string xValue, double yValue)
        {
            Month = xValue;
            Target = yValue;
        }
    }
    public class ViewModel
        {
            public ObservableCollection<Chart> Data { get; set; }

            public ViewModel()
            {
                Data = new ObservableCollection<Chart>()
        {
            new Chart("Jan", 50),
            new Chart("Feb", 70),
            new Chart("Mar", 65),
            new Chart("Apr", 57),
            new Chart("May", 48),
        };
            }
        }
    }
    
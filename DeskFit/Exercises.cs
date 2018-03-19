using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskFit
{
    public class Exercises
    {
        List<string> exercises = new List<string>
        {
            "Get a drink",
            "Run up and down the stairs twice",
            "Stretch",
            "10 crunches",
            "10 situps",
            "10 squats",
            "20 walking lunges",
            "10s plank",
            "10 jumping jacks"
        };

        static Random rand = new Random();

        public string GetExercise()
        {
            int i = rand.Next(exercises.Count);

            return(exercises[i]);

        }
    }
}

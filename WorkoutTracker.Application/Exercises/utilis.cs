using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Exercises
{
    public class utilis<T>
    {
        public List<T> ShuffleArr(List<T> arr)
        {
            // Creating a object
            // for Random class
            Random r = new Random();
            int n = arr.Count();

            // Start from the last element and
            // swap one by one. We don't need to
            // run for the first element
            // that's why i > 0
            for (int i = n - 1; i > 0; i--)
            {

                // Pick a random index
                // from 0 to i
                int j = r.Next(0, i + 1);

                // Swap arr[i] with the
                // element at random index
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            

            return arr;
        }

    }

    }

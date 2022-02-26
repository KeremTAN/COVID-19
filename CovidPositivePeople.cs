using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_19
{
    class CovidPositivePeople : People
    {
        public CovidPositivePeople(string name, int age, string gender) : base(name, age, gender) {    }
        public override void Display()
        {
            Console.WriteLine($"The COVID-19 positive patient who his/her name is {base.GetName()}, his/her gender is {base.GetGender()}  and is age {base.GetAge()}");
        }
    }
}

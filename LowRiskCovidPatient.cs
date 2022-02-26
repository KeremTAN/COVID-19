using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_19
{
    class LowRiskCovidPatient : People
    {
        public LowRiskCovidPatient(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display()
        {
            Console.WriteLine($"The Low risk COVID-19 patient who his/her name is {base.GetName()}, his/her gender is {base.GetGender()}  and is age {base.GetAge()}");
        }
    }
}

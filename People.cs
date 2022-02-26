using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_19
{
    class People
    {
        private string name;
        private int age;
        private string gender;
        public People(string name, int age, string gender) {
            SetName(name);
            SetAge(age);
            SetGender(gender);
        }
        ~People()
        {
            Console.WriteLine("Your PERSON values which is NOT valid has entered!");
        }
        public virtual void Display()
        {
            Console.WriteLine($"The people who his/her name is {this.name}, his/her gender is {this.gender}  and is age {this.age}");
        }
        public string GetName() => name;
        public void SetName(string name) => this.name = name;
        public string GetGender() => gender;
        public void SetGender(string gender) {
            if (gender.ToLower().Equals("male") || gender.ToLower().Equals("female"))
                this.gender = gender;
            else {
                Console.WriteLine("Your gender is not valid!");
            }
        }
        public int GetAge() => age;
        public void SetAge(int age) {
            if (age > 0 || age < 125)
                this.age = age;
            else
            {
                Console.WriteLine("Your age is not valid!");
            }
        
        }
        public static bool operator >(People p1, People p2) => p1.age > p2.age ? true : false;
        public static bool operator <(People p1, People p2) => p1.age > p2.age ? false : true;
        public static bool operator ==(People p1, People p2) => p1.GetName() == p2.GetName() ? true : false;
        public static bool operator !=(People p1, People p2) => p1.GetName() == p2.GetName() ? false : true;

    }
    

}


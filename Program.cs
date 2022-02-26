using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace covid_19
{
    class Program
    {
        static List<People> GeneratePatientList()
        {
            #region Generate People list
            List<People> patientPeopleList = new List<People>();
            string name;
            int age, nameIndex, surnameIndex;
            string[] maleNameArr = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas" };
            string[] femaleNameArr = { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Susan", "Margaret", "Lisa", "Nancy" };
            string[] surnameArr = { "Adams", "Allen", "Anderson", "Atkins", "Baker", "Barnes", "Bell", "Bennet", "Cooper", "Forester", "Foster", "Fox", "Gardener", "Hamilton", "Harris", "Marshall", "Murphy", "Parker", "Richardson", "Simpson"};
            //Generate Females
            age = 10;
            for (int i = 0; i < 100; i++)
            {
                nameIndex = i % femaleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = femaleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age++;
                if (age > 80)
                {
                    age = 10;
                }
                People newPeople = new People(name, age, "female");
                patientPeopleList.Add(newPeople);
            }
            //Generate Males
            age = 80;
            for (int i = 0; i < 100; i++)
            {
                nameIndex = i % maleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = maleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age--;
                if (age < 10)
                {
                    age = 80;
                }
                People newPeople = new People(name, age, "male");
                patientPeopleList.Add(newPeople);
            }
            //Order people by age
            patientPeopleList = patientPeopleList.OrderBy(p => p.GetAge()).ToList();
            return patientPeopleList;
            #endregion
        }
        static void Main(string[] args)
        {
            List<People> patientPeopleList = GeneratePatientList(); /*Generate patients*/
            List<HighRiskCovidPatient> highRiskPatientPeopleList = new List<HighRiskCovidPatient>(); //High risk exposure patients
            List<LowRiskCovidPatient> lowRiskPatientPeopleList = new List<LowRiskCovidPatient>(); //Low risk exposure patients
            List<CovidSelfIzolatePatient> covidSelfIzolatePeopleList = new List<CovidSelfIzolatePatient>(); //COVID-19 self isolate people
            List<CovidNegativePeople> covidNegativePeopleList = new List<CovidNegativePeople>(); //COVID-19 negative or inconclusive people
            List<CovidPositivePeople> covidPossitivePeopleList = new List<CovidPositivePeople>(); //COVID-19 positive people
            int high_risk_exposure_rate=30;
            int high_risk_exposure_symptoms_rate=60;
            int low_risk_exposure_symptoms_rate=25;
            int laboratory_testing_positive_rate=30;
            try
            {
                high_risk_exposure_rate = args.Length != 0 ? Convert.ToInt32(args[0]) > 0 && Convert.ToInt32(args[0]) < 100 ? Convert.ToInt32(args[0]) : 30 : 30;
                high_risk_exposure_symptoms_rate = args.Length > 1 ? Convert.ToInt32(args[1]) > 0 && Convert.ToInt32(args[1]) < 100 ? Convert.ToInt32(args[1]) : 60 : 60;
                low_risk_exposure_symptoms_rate = args.Length > 2 ? Convert.ToInt32(args[2]) > 0 && Convert.ToInt32(args[2]) < 100 ? Convert.ToInt32(args[2]) : 25 : 25;
                laboratory_testing_positive_rate = args.Length > 4 ? Convert.ToInt32(args[3]) > 0 && Convert.ToInt32(args[3]) < 100 ? Convert.ToInt32(args[3]) : 30 : 30;
            }
            catch (Exception)
            {
                Console.WriteLine(@"You send value/values except for integer! The program will be ended later 3 seconds.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            //----------------------------------
            HighRiskCovidPatient hrp;
            LowRiskCovidPatient lrp;
            CovidSelfIzolatePatient csi;
            CovidNegativePeople cnp;
            CovidPositivePeople cpp;
            Random randomP = new Random();
            //---------------------------------- Percent Sizes ---------------------
            int peopleSize = patientPeopleList.Count();
            int percentOfHigh_r_exposure = (peopleSize * high_risk_exposure_rate) / 100;
            int percentOfLow_r_exposure = peopleSize - percentOfHigh_r_exposure;
            int percentOfHigh_symptoms = (percentOfHigh_r_exposure * high_risk_exposure_symptoms_rate) / 100;
            int percentOfLow_symptoms = (percentOfLow_r_exposure * low_risk_exposure_symptoms_rate) / 100;
            //-- Temp to keep Hihg Risk Covid ids.
            List<int> temp=new List<int>();
            //-- Temp to keep Isolated people ids.
            List<int> temp2 = new List<int>();
            //-- Temp to keep Possitive people ids.
            List<int> temp3 = new List<int>();
            //-----------------------------------------------------------------------------

            // We decide who is *High* Risk Covid Patient
            for (int i = 0; i < percentOfHigh_r_exposure; i++)
            {
                int id = randomP.Next(0, peopleSize);
                if (!temp.Contains(id))
                {
                    temp.Add(id);
                    hrp = new HighRiskCovidPatient(patientPeopleList[id].GetName(), patientPeopleList[id].GetAge(), patientPeopleList[id].GetGender());
                    highRiskPatientPeopleList.Add((hrp));
                }
                else
                    i--;
            }

            // High Risk people display!
            {
                Console.WriteLine($@"
High pisk people's number of size {highRiskPatientPeopleList.Count()}
------------------------------------------------------------------------------------------------------
                 ");
                highRiskPatientPeopleList = highRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();
                highRiskPatientPeopleList.ForEach(p => p.Display());
            }

            // We decide who is ^Low^ Risk Covid Patient
            for (int i = 0; i < peopleSize; i++)
                if (!temp.Contains(i)) {
                    lrp = new LowRiskCovidPatient(patientPeopleList[i].GetName(), patientPeopleList[i].GetAge(), patientPeopleList[i].GetGender());
                    lowRiskPatientPeopleList.Add(lrp);
                }

            // Low Risk people display!
            {
                Console.WriteLine($@"
High pisk people's number of size {lowRiskPatientPeopleList.Count()}
------------------------------------------------------------------------------------------------------
                 ");
                lowRiskPatientPeopleList = lowRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();
                lowRiskPatientPeopleList.ForEach(p => p.Display());
            }

            // We decide who is self izloated from *High* Risk Covid Patient
            for (int i = 0; i < percentOfHigh_symptoms; i++)
            {
                int id = randomP.Next(0, peopleSize);
                if (temp.Contains(id))
                {
                    temp2.Add(id);
                    csi = new CovidSelfIzolatePatient(patientPeopleList[id].GetName(), patientPeopleList[id].GetAge(), patientPeopleList[id].GetGender());
                    covidSelfIzolatePeopleList.Add(csi);
                }
                else
                    i--;
            }

            // We decide who is self izloated from ^Low^ Risk Covid Patient
            for (int i = 0; i < percentOfLow_symptoms; i++)
            {
                int id = randomP.Next(0, peopleSize);
                if (!temp.Contains(id))
                {
                    temp2.Add(id);
                    csi = new CovidSelfIzolatePatient(patientPeopleList[id].GetName(), patientPeopleList[id].GetAge(), patientPeopleList[id].GetGender());
                    covidSelfIzolatePeopleList.Add(csi);
                }
                else
                    i--;
            }
            temp.Clear();

            //  Self Izloated people display!
            {
                Console.WriteLine($@"
High pisk people's number of size {covidSelfIzolatePeopleList.Count()}
------------------------------------------------------------------------------------------------------
                 ");
                covidSelfIzolatePeopleList = covidSelfIzolatePeopleList.OrderBy(p => p.GetAge()).ToList();
                covidSelfIzolatePeopleList.ForEach(p => p.Display());
            }

            // We decide who is covid positive people
            int lab_rate = (covidSelfIzolatePeopleList.Count() * laboratory_testing_positive_rate) / 100;
            for (int i = 0; i < lab_rate ; i++)
            {
                int id = randomP.Next(0, peopleSize);
                if (temp2.Contains(id) && !temp3.Contains(id))
                {  
                    cpp = new CovidPositivePeople(patientPeopleList[id].GetName(), patientPeopleList[id].GetAge(), patientPeopleList[id].GetGender());
                    covidPossitivePeopleList.Add(cpp);
                    temp3.Add(id);
                }
                else
                    i--;
            }


            // We decide who is covid negative people
            for (int id = 0; id < peopleSize; id++)
            {
                if (!temp3.Contains(id))
                {
                    cnp = new CovidNegativePeople(patientPeopleList[id].GetName(), patientPeopleList[id].GetAge(), patientPeopleList[id].GetGender());
                    covidNegativePeopleList.Add(cnp);
                }
            }

            // Covid Possitive Risk people display!
            {
                Console.WriteLine($@"
Covid Possitive people's number of size {covidPossitivePeopleList.Count()}
------------------------------------------------------------------------------------------------------
                 ");
                covidPossitivePeopleList = covidPossitivePeopleList.OrderBy(p => p.GetAge()).ToList();
                covidPossitivePeopleList.ForEach(p => p.Display());
            }
            // Covid Negative people display!
            {
                Console.WriteLine($@"
Covid Negative people's number of size {covidNegativePeopleList.Count()}
------------------------------------------------------------------------------------------------------
                 ");
                covidNegativePeopleList = covidNegativePeopleList.OrderBy(p => p.GetAge()).ToList();
                covidNegativePeopleList.ForEach(p => p.Display());
            }
            Console.ReadLine();
        }
    }
}

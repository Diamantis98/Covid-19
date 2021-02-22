using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid_19
{
    class Cases
    {
        private string name, fname, num, mail, adress, diseases, gender,time;
        private DateTime bdate, recordDate;

        public Cases(string name, string fname, string num, string mail, string adress, string diseases, string gender, DateTime bdate, DateTime recordDate, string time)
        {
            this.Name = name;
            this.Fname = fname;
            this.Num = num;
            this.Mail = mail;
            this.Adress = adress;
            this.Diseases = diseases;
            this.Gender = gender;
            this.Bdate = bdate;
            this.RecordDate = recordDate;
            this.Time = time;
        }

        public string Name { get => name; set => name = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Num { get => num; set => num = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Diseases { get => diseases; set => diseases = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime Bdate { get => bdate; set => bdate = value; }
        public DateTime RecordDate { get => recordDate; set => recordDate = value; }
        public string Time { get => time; set => time = value; }
    }
}

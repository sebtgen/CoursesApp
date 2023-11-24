using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Operator : User
    {
        string operatorName;

        public string OperatorName1 { get => operatorName; set => operatorName = value; }

        public Operator() { }

        public Operator(string email, string password, string phoneNumber, string operatorName) : base(password, email, phoneNumber)
        {
            this.operatorName = operatorName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_09_02
{
   public class FinanceReport
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Balance { get; set; }
        public Dictionary<string, decimal> CategoryStatistics { get; set; }
    }
}

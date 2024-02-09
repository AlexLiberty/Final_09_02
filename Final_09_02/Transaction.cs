using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_09_02
{
    public enum TransactionType
    {
        Income,
        Expense
    }
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

}

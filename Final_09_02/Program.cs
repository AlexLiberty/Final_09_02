using Final_09_02;

using (var db = new ApplicationContext())
{

    //var user = new User
    //{
    //    FirstName = "Firs1",
    //    LastName = "Last1"
    //};

    //db.Users.Add(user);
    //db.SaveChanges();

    //var user = db.Users.FirstOrDefault(u => u.Id == 1);
    //if (user == null)
    //{
    //    user = new User { Id = 1, FirstName = "Firs1", LastName = "Last1" };
    //    db.Users.Add(user);
    //}
    //1
    //void AddTransaction(ApplicationContext db, User user, TransactionType type, decimal amount, DateTime date, string description)
    //{
    //    var transaction = new Transaction
    //    {
    //        Type = type,
    //        Amount = amount,
    //        Description = description,
    //        Date = date,
    //        User = user
    //    };
    //    db.Transactions.Add(transaction);
    //    db.SaveChanges();
    //}

    //AddTransaction(db, user, TransactionType.Income, 100, DateTime.Now, "Trans1");
    //AddTransaction(db, user, TransactionType.Income, 500, DateTime.Now, "Trans2");

    //2
    List<Transaction> GetAllTransactions(ApplicationContext db)
    {
        return db.Transactions.ToList();
    }

    var allTransactions = GetAllTransactions(db);
    Console.WriteLine("List all transactions:");
    foreach (var transaction in allTransactions)
    {
        Console.WriteLine($"{transaction.TransactionId}: {transaction.Type}, {transaction.Amount}, {transaction.Description}, {transaction.Date}");
    }

    //3
    decimal GetTotalAmount(ApplicationContext db, TransactionType type, DateTime startDate, DateTime endDate)
    {
        return db.Transactions
            .Where(t => t.Type == type && t.Date >= startDate && t.Date <= endDate)
            .Sum(t => t.Amount);
    }

    var startDate = new DateTime(2024, 2, 1);
    var endDate = new DateTime(2024, 2, 10);
    var income = GetTotalAmount(db, TransactionType.Income, startDate, endDate);
    var expenses = GetTotalAmount(db, TransactionType.Expense, startDate, endDate);
    Console.WriteLine();
    Console.WriteLine($"Total Income: {income}");
    Console.WriteLine($"Total Expense: {expenses}");

    //4
    List<Transaction> GetFilteredTransactions(ApplicationContext db, TransactionType type, DateTime startDate, DateTime endDate)
    {
        return db.Transactions
            .Where(t => t.Type == type && t.Date >= startDate && t.Date <= endDate)
            .ToList();
    }

    var filteredTransactions = GetFilteredTransactions(db, TransactionType.Expense, startDate, endDate);
    Console.WriteLine();
    Console.WriteLine("List Filtred Transactios:");
    foreach (var transaction in filteredTransactions)
    {
        Console.WriteLine($"{transaction.TransactionId}: {transaction.Type}, {transaction.Amount}, {transaction.Description}, {transaction.Date}");
    }

    //5
    var financeReport = GenerateFinanceReport(db, startDate, endDate);
    Console.WriteLine();
    Console.WriteLine("Finance Report:");
    Console.WriteLine($"Total Income: {financeReport.TotalIncome}");
    Console.WriteLine($"Total Expense: {financeReport.TotalExpenses}");
    Console.WriteLine($"Balance: {financeReport.Balance}");
    Console.WriteLine();
    Console.WriteLine("Statistics:");
    foreach (var category in financeReport.CategoryStatistics)
    {
        Console.WriteLine($"{category.Key}: {category.Value}");
    }

    FinanceReport GenerateFinanceReport(ApplicationContext context, DateTime startDate, DateTime endDate)
    {
        var report = new FinanceReport();
        report.TotalIncome = GetTotalAmount(context, TransactionType.Income, startDate, endDate);
        report.TotalExpenses = GetTotalAmount(context, TransactionType.Expense, startDate, endDate);
        report.Balance = report.TotalIncome - report.TotalExpenses;

     //6
        report.CategoryStatistics = context.Transactions
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .GroupBy(t => t.Description)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        return report;
    }
}
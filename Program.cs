Console.WriteLine("BankAccount!");

BankAccount bankAccount = new BankAccount();
bankAccount.Deposit(300);
Console.WriteLine("Saldo: " + bankAccount.GetBalance());
bankAccount.Withdraw(100);
Console.WriteLine("Saldo: " + bankAccount.GetBalance());
Console.WriteLine("Hej ");


class BankAccount
{
    private decimal balance;

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string AccountNumber { get; set; }

    public string AccountType { get; set; }

    public decimal InterestRate { get; set; }

    public decimal MinimumBalance { get; set; }

    public decimal CreditLimit { get; set; }

    public BankAccount(decimal initialBalance = 0)
    {
        balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }
        balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.");
        }
        if (amount > balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }
        balance -= amount;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public decimal GetInterestRate()
    {
        return InterestRate;
    }
}
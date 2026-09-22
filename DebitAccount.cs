namespace BankAccountProject
{
    // DebitAccount extends a normal account with a controlled overdraft limit.
    public class DebitAccount : BankAccount
    {
        // Label used when displaying this account.
        public override string AccountType => "Debit";
        // Maximum amount that may be withdrawn below zero.
        public decimal OverdraftLimit { get; }

        // Creates a debit account.
        // firstName, lastName, and accountNumber identify the customer and account.
        // initialBalance is the starting amount; overdraftLimit is the allowed negative balance.
        public DebitAccount(
            string firstName,
            string lastName,
            string accountNumber,
            decimal initialBalance = 0,
            decimal overdraftLimit = 0)
            : base(firstName, lastName, accountNumber, initialBalance)
        {
            if (overdraftLimit < 0)
            {
                throw new ArgumentException("Overdraft limit cannot be negative.");
            }

            OverdraftLimit = overdraftLimit;
        }

        // Withdraws money as long as the overdraft limit is not exceeded.
        // amount is the money to withdraw.
        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount, "Withdrawal");
            if (amount > Balance + OverdraftLimit)
            {
                throw new InvalidOperationException("The debit account limit has been reached.");
            }

            Balance -= amount;
        }

        // Returns the normal account summary with the overdraft limit included.
        public override string GetSummary()
        {
            return $"{base.GetSummary()} (overdraft limit {OverdraftLimit:C})";
        }
    }
}
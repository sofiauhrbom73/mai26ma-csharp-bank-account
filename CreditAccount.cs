namespace BankAccountProject
{
    // CreditAccount allows spending below zero up to a defined credit limit.
    public class CreditAccount : Account
    {
        // Label used when displaying this account.
        public override string AccountType => "Credit";
        // Maximum permitted debt on the account.
        public decimal CreditLimit { get; }

        // Creates a credit account.
        // firstName, lastName, and accountNumber identify the customer and account.
        // creditLimit is the maximum debt; initialBalance is the starting amount.
        public CreditAccount(
            string firstName,
            string lastName,
            string accountNumber,
            decimal creditLimit,
            decimal initialBalance = 0)
            : base(firstName, lastName, accountNumber, initialBalance)
        {
            if (creditLimit <= 0)
            {
                throw new ArgumentException("Credit limit must be positive.");
            }

            CreditLimit = creditLimit;
        }

        // Withdraws money as long as the credit limit is not exceeded.
        // amount is the money to withdraw.
        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount, "Withdrawal");
            if (Balance - amount < -CreditLimit)
            {
                throw new InvalidOperationException("The credit limit has been reached.");
            }

            Balance -= amount;
        }

        // Returns the account summary with the credit limit included.
        public override string GetSummary()
        {
            return $"{base.GetSummary()} (credit limit {CreditLimit:C})";
        }
    }
}
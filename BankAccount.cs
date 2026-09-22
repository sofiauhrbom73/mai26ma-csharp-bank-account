namespace BankAccountProject
{
    // BankAccount is a normal account that cannot withdraw more than its balance.
    public class BankAccount : Account
    {
        // Label used when displaying this account.
        public override string AccountType => "Normal";
        // Optional interest rate associated with the account.
        public decimal InterestRate { get; }

        // Creates a normal account.
        // firstName, lastName, and accountNumber identify the customer and account.
        // initialBalance is the starting amount; interestRate is the account's interest rate.
        public BankAccount(
            string firstName = "Unknown",
            string lastName = "Customer",
            string accountNumber = "UNASSIGNED",
            decimal initialBalance = 0,
            decimal interestRate = 0)
            : base(firstName, lastName, accountNumber, initialBalance)
        {
            if (interestRate < 0)
            {
                throw new ArgumentException("Interest rate cannot be negative.");
            }

            InterestRate = interestRate;
        }

        // Withdraws money only when the account has enough available balance.
        // amount is the money to withdraw.
        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount, "Withdrawal");
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            Balance -= amount;
        }
    }
}
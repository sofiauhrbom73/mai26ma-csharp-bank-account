namespace BankAccountProject
{
    // Account defines the common behavior shared by all account types.
    public abstract class Account
    {
        // Protected balance can be changed by this class and its derived account types.
        protected decimal Balance { get; set; }

        // Customer's first name.
        public string FirstName { get; }
        // Customer's last name.
        public string LastName { get; }
        // Unique number identifying the account.
        public string AccountNumber { get; }
        // Account type shown in summaries and the menu.
        public abstract string AccountType { get; }

        // Creates an account with customer details and an optional starting balance.
        // firstName, lastName, and accountNumber identify the account owner and account.
        // initialBalance is the amount already in the account and cannot be negative.
        protected Account(string firstName, string lastName, string accountNumber, decimal initialBalance = 0)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }

            FirstName = RequireValue(firstName, "First name");
            LastName = RequireValue(lastName, "Last name");
            AccountNumber = RequireValue(accountNumber, "Account number");
            Balance = initialBalance;
        }

        // Adds a positive amount to the account balance.
        // amount is the money to deposit.
        public virtual void Deposit(decimal amount)
        {
            ValidateAmount(amount, "Deposit");
            Balance += amount;
        }

        // Removes money according to the rules of the specific account type.
        // amount is the money to withdraw.
        public abstract void Withdraw(decimal amount);

        // Returns the current account balance.
        public decimal GetBalance() => Balance;

        // Returns a readable description of the account and its balance.
        public virtual string GetSummary()
        {
            return $"{AccountType} {AccountNumber} - {FirstName} {LastName}: balance {Balance:C}";
        }

        // Checks that a deposit or withdrawal amount is positive.
        // amount is the value being checked; operation is used in the error message.
        protected static void ValidateAmount(decimal amount, string operation)
        {
            if (amount <= 0)
            {
                throw new ArgumentException($"{operation} amount must be positive.");
            }
        }

        // Checks required text fields and removes extra whitespace.
        // value is the text to validate; fieldName identifies it in an error message.
        private static string RequireValue(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} is required.");
            }

            return value.Trim();
        }
    }
}
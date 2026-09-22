using BankAccountProject;

// Menu controls the console user interface and the current account session.
public class Menu
{
    // All accounts created during this run of the program.
    private readonly List<Account> accounts = [];
    // Messages that will be written to the session file.
    private readonly List<string> sessionEntries = [];
    // Account currently used by deposit, withdrawal, and balance actions.
    private Account? selectedAccount;

    // Starts the menu loop until the user chooses Exit.
    public void Run()
    {
        bool running = true;
        Console.WriteLine("Bank account manager");

        while (running)
        {
            ShowOptions();
            string choice = Console.ReadLine()?.Trim() ?? string.Empty;

            try
            {
                running = HandleChoice(choice);
            }
            catch (Exception exception) when (exception is ArgumentException or InvalidOperationException)
            {
                WriteResult($"Operation failed: {exception.Message}");
            }
        }
    }

    // Displays the available account operations.
    private void ShowOptions()
    {
        Console.WriteLine();
        Console.WriteLine("1. Create normal account");
        Console.WriteLine("2. Create debit account");
        Console.WriteLine("3. Create credit account");
        Console.WriteLine("4. Select account");
        Console.WriteLine("5. Deposit");
        Console.WriteLine("6. Withdraw");
        Console.WriteLine("7. Show selected balance");
        Console.WriteLine("8. List accounts");
        Console.WriteLine("9. Save session");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    // Executes the selected menu command and returns whether the menu should continue.
    // choice is the option entered by the user.
    private bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1": CreateNormalAccount(); break;
            case "2": CreateDebitAccount(); break;
            case "3": CreateCreditAccount(); break;
            case "4": SelectAccount(); break;
            case "5": Deposit(); break;
            case "6": Withdraw(); break;
            case "7": ShowBalance(); break;
            case "8": ListAccounts(); break;
            case "9": SaveSession(); break;
            case "0": SaveSession(); return false;
            default: Console.WriteLine("Unknown option."); break;
        }

        return true;
    }

    // Reads normal account details and adds a new account.
    private void CreateNormalAccount()
    {
        AddAccount(new BankAccount(ReadRequired("First name: "), ReadRequired("Last name: "), ReadRequired("Account number: "), ReadDecimal("Initial balance: ")));
    }

    // Reads debit account details and adds a new account.
    private void CreateDebitAccount()
    {
        AddAccount(new DebitAccount(ReadRequired("First name: "), ReadRequired("Last name: "), ReadRequired("Account number: "), ReadDecimal("Initial balance: "), ReadDecimal("Overdraft limit: ")));
    }

    // Reads credit account details and adds a new account.
    private void CreateCreditAccount()
    {
        AddAccount(new CreditAccount(ReadRequired("First name: "), ReadRequired("Last name: "), ReadRequired("Account number: "), ReadDecimal("Credit limit: "), ReadDecimal("Initial balance: ")));
    }

    // Stores an account and selects it for the next operation.
    // account is the new account to store.
    private void AddAccount(Account account)
    {
        accounts.Add(account);
        selectedAccount = account;
        WriteResult($"Created account: {account.GetSummary()}");
    }

    // Displays available accounts and selects one by its list number.
    private void SelectAccount()
    {
        ListAccounts();
        if (accounts.Count == 0) return;
        int accountIndex = ReadInteger("Account number in the list: ") - 1;
        if (accountIndex < 0 || accountIndex >= accounts.Count) throw new ArgumentException("That account selection does not exist.");
        selectedAccount = accounts[accountIndex];
        WriteResult($"Selected account: {selectedAccount.GetSummary()}");
    }

    // Deposits an amount into the selected account.
    private void Deposit()
    {
        Account account = RequireSelectedAccount();
        decimal amount = ReadDecimal("Deposit amount: ");
        account.Deposit(amount);
        WriteResult($"Deposited {amount:C}. New balance: {account.GetBalance():C}");
    }

    // Withdraws an amount from the selected account.
    private void Withdraw()
    {
        Account account = RequireSelectedAccount();
        decimal amount = ReadDecimal("Withdrawal amount: ");
        account.Withdraw(amount);
        WriteResult($"Withdrew {amount:C}. New balance: {account.GetBalance():C}");
    }

    // Writes the selected account's details to the console and session log.
    private void ShowBalance() => WriteResult(RequireSelectedAccount().GetSummary());

    // Lists every account created during this program run.
    private void ListAccounts()
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts have been created.");
            return;
        }

        for (int index = 0; index < accounts.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {accounts[index].GetSummary()}");
        }
    }

    // Saves all recorded session messages to a text file.
    private void SaveSession()
    {
        string path = FileHandler.SaveSession(sessionEntries);
        Console.WriteLine($"Session saved to {path}");
    }

    // Returns the selected account or reports that one must be selected first.
    private Account RequireSelectedAccount() => selectedAccount ?? throw new InvalidOperationException("Select or create an account first.");

    // Displays a result and records it for the session file.
    // message is the result shown to the user.
    private void WriteResult(string message)
    {
        Console.WriteLine(message);
        sessionEntries.Add($"{DateTime.Now:O} {message}");
    }

    // Reads a non-empty text value from the console.
    // prompt is the question displayed to the user.
    private static string ReadRequired(string prompt)
    {
        Console.Write(prompt);
        string value = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A value is required.");
        return value;
    }

    // Reads a decimal number from the console.
    // prompt is the question displayed to the user.
    private static decimal ReadDecimal(string prompt)
    {
        Console.Write(prompt);
        if (!decimal.TryParse(Console.ReadLine(), out decimal value)) throw new ArgumentException("Enter a valid number.");
        return value;
    }

    // Reads a whole number from the console.
    // prompt is the question displayed to the user.
    private static int ReadInteger(string prompt)
    {
        Console.Write(prompt);
        if (!int.TryParse(Console.ReadLine(), out int value)) throw new ArgumentException("Enter a valid whole number.");
        return value;
    }
}

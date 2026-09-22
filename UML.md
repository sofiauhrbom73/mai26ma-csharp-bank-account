# Bank Account UML Diagram

The diagram below shows the current classes, inheritance, access levels, and the main relationships in the application.

```mermaid
classDiagram
	class Account {
		<<abstract>>
		#decimal Balance
		+string FirstName
		+string LastName
		+string AccountNumber
		+string AccountType
		#Account(firstName, lastName, accountNumber, initialBalance)
		+Deposit(amount) void
		+Withdraw(amount) void
		+GetBalance() decimal
		+GetSummary() string
		#ValidateAmount(amount, operation) void
		-RequireValue(value, fieldName) string
	}

	class BankAccount {
		+string AccountType
		+decimal InterestRate
		+BankAccount(firstName, lastName, accountNumber, initialBalance, interestRate)
		+Withdraw(amount) void
	}

	class DebitAccount {
		+string AccountType
		+decimal OverdraftLimit
		+DebitAccount(firstName, lastName, accountNumber, initialBalance, overdraftLimit)
		+Withdraw(amount) void
		+GetSummary() string
	}

	class CreditAccount {
		+string AccountType
		+decimal CreditLimit
		+CreditAccount(firstName, lastName, accountNumber, creditLimit, initialBalance)
		+Withdraw(amount) void
		+GetSummary() string
	}

	class Menu {
		-List~Account~ accounts
		-List~string~ sessionEntries
		-Account selectedAccount
		+Run() void
		-HandleChoice(choice) bool
		-CreateNormalAccount() void
		-CreateDebitAccount() void
		-CreateCreditAccount() void
		-SelectAccount() void
		-Deposit() void
		-Withdraw() void
		-ShowBalance() void
		-ListAccounts() void
		-SaveSession() void
	}

	class FileHandler {
		+SaveSession(entries) string
	}

	class Person {
		-string name
		+int age
		+GetName() string
		+getAge() int
	}

	Account <|-- BankAccount
	Account <|-- CreditAccount
	BankAccount <|-- DebitAccount
	Menu o-- "0..*" Account : manages
	Menu ..> FileHandler : saves session
```

## Relationship notes

- `Account` is abstract, so the menu creates concrete account types instead of creating an `Account` directly.
- `BankAccount` is the normal account and does not allow withdrawals below zero.
- `DebitAccount` inherits normal account behavior and adds an overdraft limit.
- `CreditAccount` allows a negative balance up to its credit limit.
- `Menu` stores zero or more accounts and keeps track of the selected account.
- `FileHandler` is used by `Menu` to save the current session as a text file.
- `Person` currently exists separately and is not used by the account workflow.

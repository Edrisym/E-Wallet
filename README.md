# E-Wallet Project

## Overview
This E-Wallet project is developed using the **Test-Driven Development (TDD)** approach. It is built with **.NET 8** and uses **SQL Server** for database management. The project utilizes **XUnit** for unit testing and **FluentAssertions** for expressive and readable assertions in tests. The system includes core functionalities such as wallet creation, balance management, withdrawal operations, and handling of multiple currencies.

## Features
- **Wallet Creation**: Create a wallet with an initial balance.
- **Withdrawals**: Withdraw funds from the wallet while ensuring the balance is sufficient.
- **Currency Support**: Support for handling multiple currencies and exchange rates.
- **TDD**: The entire development follows the **Test-Driven Development (TDD)** approach, ensuring a high level of code quality and test coverage.
- **SQL Server Integration**: Utilizes SQL Server as the primary database for persistence.
- **Custom Exceptions**: Throwing and handling exceptions for invalid operations like insufficient funds or invalid balances.

## Technologies Used
- **C#**
- **.NET 8**
- **SQL Server**: Used for data storage and persistence.
- **XUnit**: Framework for writing and running unit tests.
- **FluentAssertions**: Library to enhance assertions in tests with readable and expressive syntax.
- **Entity Framework Core**: Used for data access and interactions with SQL Server.

## Project Structure
- **`Models`**: Contains classes representing business entities like Wallet and Currency.
- **`Exceptions`**: Custom exceptions used for specific error scenarios (e.g., insufficient funds, invalid balance).
- **`Data`**: Contains database access logic, including Entity Framework DbContext.
- **`Tests`**: Unit tests for business logic, written using **XUnit** and **FluentAssertions**.

## Setup and Installation

### Prerequisites
To run and contribute to this project, ensure the following are installed:
- **.NET 8 SDK**: [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Local or cloud-based instance of SQL Server.
- **XUnit**: [XUnit](https://xunit.net/) for unit testing.
- **FluentAssertions**: [FluentAssertions](https://fluentassertions.com/) for enhanced assertions.

### Steps to Run the Project

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/edrisym/e-wallet.git
   cd e-wallet
   ```

2. **Restore Dependencies**:
   Ensure that all NuGet packages are restored:
   ```bash
   dotnet restore
   ```

3. **Set Up SQL Server**:
   Make sure you have access to a **SQL Server** instance and configure your connection string in the `appsettings.json` file.

   Example of the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EWalletDb;Trusted_Connection=True;"
     }
   }
   ```

4. **Apply Migrations**:
   Apply Entity Framework Core migrations to create the necessary database tables:
   ```bash
   dotnet ef database update
   ```

5. **Build the Project**:
   ```bash
   dotnet build
   ```

6. **Run the Tests**:
   To run all unit tests:
   ```bash
   dotnet test
   ```

7. **Run the Application** (if applicable):
   To run the main application (for example, if there is a web API or service):
   ```bash
   dotnet run
   ```

## Test-Driven Development Approach

This project strictly follows the **Test-Driven Development (TDD)** approach:
1. **Write a Test First**: Write a test to specify the expected behavior of the functionality before implementing it.
2. **Implement the Code**: Write the minimal code necessary to pass the test.
3. **Refactor**: Refactor the code to improve its design, while ensuring all tests continue to pass.

### Benefits of TDD in This Project:
- **High Code Coverage**: Every functionality, from wallet creation to withdrawal, is fully tested.
- **Early Bug Detection**: Errors are caught early due to continuous testing.
- **Clearer Design**: The TDD approach forces developers to think about the design and functionality before implementation.

## Example Test Case

Here’s an example test case for the **withdrawal functionality**:

```csharp
[Fact]
    public void Should_Throw_Exception_If_Balance_Is_Negative()
    {
        var balance = -0.75m;
        var currency = Currency.Create("USD", "United States Dollar", 1.0m);
        var walletCreation = () => Wallet.Create(balance, currency);

        walletCreation.Should().ThrowExactly<NegativeBalanceException>();
    }
```

This test ensures that the wallet throws an exception if the user tries to withdraw more than the available balance.

## Contributing

We welcome contributions to improve this project. If you’d like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push the branch to your fork (`git push origin feature-branch`).
5. Open a pull request to merge your changes.

Please make sure that all new features are covered by tests, as we follow TDD in this project.

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for more information.

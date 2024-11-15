### E-Wallet Project

---

## **Overview**
This E-Wallet project is developed using the **Test-Driven Development (TDD)** approach. It is built with **.NET 8** and uses **SQL Server** for database management. The project leverages **JWT (JSON Web Tokens)** for secure user authentication and authorization. The development is thoroughly tested using **XUnit** with enhanced readability provided by **FluentAssertions**. Core functionalities include wallet creation, balance management, withdrawal operations, and handling of multiple currencies.

---

## **Features**
- **Wallet Creation**: Create a wallet with an initial balance.
- **Withdrawals**: Withdraw funds from the wallet while ensuring sufficient balance.
- **Currency Support**: Handle multiple currencies and exchange rates seamlessly.
- **User Authentication**: Secure user authentication and authorization using **JWT**.
- **TDD**: Adheres to the **Test-Driven Development (TDD)** approach, ensuring high code quality and comprehensive test coverage.
- **SQL Server Integration**: Utilizes **SQL Server** as the primary database for data persistence.
- **Custom Exceptions**: Provides tailored exception handling for scenarios like insufficient funds and invalid balances.

---

## **Technologies Used**
- **C#**
- **.NET 8**
- **SQL Server**: Used for data storage and persistence.
- **Entity Framework Core**: For database interactions and migrations.
- **JWT (JSON Web Tokens)**: Secures authentication and authorization processes.
- **XUnit**: Framework for unit testing.
- **FluentAssertions**: Library for expressive and readable assertions in tests.

---

## **Project Structure**
- **`Models`**: Contains classes representing business entities like `Wallet` and `Currency`.
- **`Exceptions`**: Custom exceptions for specific error scenarios, such as insufficient funds.
- **`Data`**: Contains database logic, including `DbContext` for Entity Framework.
- **`Services`**: Implements core business logic for wallet operations.
- **`Authentication`**: Includes JWT configuration and token generation.
- **`Tests`**: Unit tests written with **XUnit** and **FluentAssertions**.

---

## **Setup and Installation**

### **Prerequisites**
To run this project, ensure the following are installed:
- **.NET 8 SDK**: [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Local or cloud-based SQL Server instance.
- **XUnit**: [XUnit](https://xunit.net/) for running unit tests.
- **FluentAssertions**: [FluentAssertions](https://fluentassertions.com/) for enhanced test assertions.

---

### **Steps to Run the Project**

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/edrisym/e-wallet.git
   cd e-wallet
   ```

2. **Restore Dependencies**:
   Restore the required NuGet packages:
   ```bash
   dotnet restore
   ```

3. **Set Up SQL Server**:
   Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EWalletDb;Trusted_Connection=True;"
     },
     "JwtSettings": {
       "Issuer": "YourIssuerValue",
       "Audience": "YourAudienceValue",
       "Key": "YourSuperSecretKey12345"
     }
   }
   ```

4. **Apply Migrations**:
   Run migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```

5. **Build the Project**:
   Compile the application:
   ```bash
   dotnet build
   ```

6. **Run the Tests**:
   Execute all unit tests:
   ```bash
   dotnet test
   ```

7. **Run the Application**:
   Launch the application:
   ```bash
   dotnet run
   ```

---

## **Test-Driven Development (TDD) Approach**
This project strictly adheres to the **TDD** methodology:
1. **Write Tests First**: Define the expected behavior with tests before implementation.
2. **Implement Code**: Write only the code required to pass the test.
3. **Refactor**: Improve the code structure while ensuring tests pass.

---

### **Example Test Case**

Here’s an example test case for validating negative balances:

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

This ensures the wallet creation logic enforces valid balance constraints.

---

## **JWT Authentication**

The project uses **JWT** for secure authentication and authorization:
- **Issuer** and **Audience** validate the source and target of the token.
- **Key** ensures the token's integrity.
- **Token Validation**: Configured to ensure validity, expiration, and signature.

---

## **Contributing**

Contributions are welcome! Follow these steps to contribute:
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push the branch (`git push origin feature-branch`).
5. Submit a pull request.

All contributions should adhere to the **TDD** methodology and include corresponding unit tests.

---

## **License**

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for more details.
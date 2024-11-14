Here’s a list of tests to consider when starting with TDD for an e-wallet/account domain:

1. **Create Wallet/Account**
    - [x] Test that a new wallet can be created with a unique ID and valid initial balance.
    - [x] Test that a wallet cannot be created with a negative initial balance.
    - [x] Test that a wallet has a default currency upon creation.

2. **Deposit Funds**
    - [x] Test that depositing a positive amount increases the wallet balance correctly.
    - [x] Test that depositing a zero or negative amount results in an error or is not allowed.

3. **Withdraw Funds**
    - [x] Test that withdrawing a valid amount decreases the wallet balance correctly.
    - [x] Test that attempting to withdraw more than the available balance results in an error or is not allowed.
    - [x] Test that withdrawing a zero or negative amount results in an error or is not allowed.

4. **Transfer Funds Between Wallets**
    - Test that a valid transfer from one wallet to another decreases the sender’s balance and increases the receiver’s
      balance by the correct amounts.
    - Test that a transfer fails if the sender’s balance is insufficient.
    - Test that a transfer between wallets with different currencies adjusts balances according to the correct exchange
      rate.
    - Test that transferring a zero or negative amount results in an error or is not allowed.

5. **Check Balance**
    - Test that the wallet balance can be retrieved and matches the expected value after deposits, withdrawals, or
      transfers.

6. **Currency Conversion**
    - Test that the wallet’s balance can be converted to a different currency accurately based on a given exchange rate.

7. **Transaction History**
    - Test that each deposit, withdrawal, or transfer is recorded in the wallet’s transaction history.
    - Test that the transaction history includes correct details (e.g., amount, date, type of transaction).

8. **Account Locking/Status**
    - Test that a wallet can be locked or deactivated, preventing further transactions.
    - Test that attempting to transact on a locked/deactivated wallet results in an error.

9. **Event Logging/Notification**
    - Test that each transaction (deposit, withdrawal, transfer) triggers the appropriate event or notification.

10. **Daily/Monthly Limits**
    - Test that deposits or withdrawals exceeding daily or monthly limits are not allowed.
    - Test that transactions within the limit are processed successfully.

These tests will give a strong foundation for building and validating the core functionality of an e-wallet/account
domain.
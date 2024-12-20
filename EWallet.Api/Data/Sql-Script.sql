BEGIN TRANSACTION;
CREATE TABLE [Currencies] (
    [Id] NVARCHAR(36) NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Code] varchar(10) NOT NULL,
    [Ratio] DECIMAL(18,2) NOT NULL,
    [ModifiedOnUtc] datetime2 NULL,
    [CreatedOnUtc] datetime2 NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY ([Id])
    );

CREATE TABLE [Wallets] (
    [Id] NVARCHAR(36) NOT NULL,
    [Balance] DECIMAL(18,2) NOT NULL,
    [StatusId] int NOT NULL,
    [CurrencyId] NVARCHAR(36) NOT NULL,
    [ModifiedOnUtc] datetime2 NULL,
    [CreatedOnUtc] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Wallets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Wallets_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE NO ACTION
    );

CREATE NONCLUSTERED INDEX [IX_Currencies_Code] ON [Currencies] ([Code]);

CREATE UNIQUE INDEX [IX_Currencies_Code_Name] ON [Currencies] ([Code], [Name]);

CREATE INDEX [IX_Wallets_CurrencyId] ON [Wallets] ([CurrencyId]);

CREATE NONCLUSTERED INDEX [IX_Wallets_StatusId] ON [Wallets] ([StatusId]);
COMMIT;
GO

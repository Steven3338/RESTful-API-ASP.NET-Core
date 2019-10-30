IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [LogIn] bit NOT NULL,
        [DateOfBirth] nvarchar(max) NULL,
        [Gender] nvarchar(max) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [Addresses] (
        [Id] int NOT NULL IDENTITY,
        [Country] nvarchar(max) NULL,
        [Street] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [County] nvarchar(max) NULL,
        [Zip] nvarchar(max) NULL,
        [MoveInDate] nvarchar(max) NULL,
        [MoveOutDate] nvarchar(max) NULL,
        [CustomerId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [CustomerMessage] (
        [Id] int NOT NULL IDENTITY,
        [Message] nvarchar(max) NULL,
        [DateTimeOfMessage] nvarchar(max) NULL,
        [Resolved] bit NOT NULL,
        [CustomerId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_CustomerMessage] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CustomerMessage_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [Email] (
        [Id] int NOT NULL IDENTITY,
        [EmailAddress] nvarchar(max) NULL,
        [CustomerId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Email] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Email_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [Employeer] (
        [Id] nvarchar(450) NOT NULL,
        [CompanyName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [AddressId] int NULL,
        [Phone] nvarchar(max) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Employeer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employeer_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE TABLE [CustomerEmployeer] (
        [CustomerId] int NOT NULL,
        [EmployeerId] int NOT NULL,
        [EmployeerId1] nvarchar(450) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_CustomerEmployeer] PRIMARY KEY ([CustomerId], [EmployeerId]),
        CONSTRAINT [FK_CustomerEmployeer_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CustomerEmployeer_Employeer_EmployeerId1] FOREIGN KEY ([EmployeerId1]) REFERENCES [Employeer] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE INDEX [IX_CustomerEmployeer_EmployeerId1] ON [CustomerEmployeer] ([EmployeerId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE INDEX [IX_CustomerMessage_CustomerId] ON [CustomerMessage] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE INDEX [IX_Email_CustomerId] ON [Email] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    CREATE INDEX [IX_Employeer_AddressId] ON [Employeer] ([AddressId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319180837_PeopleModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190319180837_PeopleModel', N'2.2.3-servicing-35854');
END;

GO


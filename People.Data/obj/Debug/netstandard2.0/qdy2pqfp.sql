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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeer] DROP CONSTRAINT [FK_CustomerEmployeer_Customers_CustomerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeer] DROP CONSTRAINT [FK_CustomerEmployeer_Employeer_EmployeerId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerMessage] DROP CONSTRAINT [FK_CustomerMessage_Customers_CustomerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Email] DROP CONSTRAINT [FK_Email_Customers_CustomerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Employeer] DROP CONSTRAINT [FK_Employeer_Addresses_AddressId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Employeer] DROP CONSTRAINT [PK_Employeer];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Email] DROP CONSTRAINT [PK_Email];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerMessage] DROP CONSTRAINT [PK_CustomerMessage];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeer] DROP CONSTRAINT [PK_CustomerEmployeer];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[Employeer]', N'Employeers';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[Email]', N'Emails';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[CustomerMessage]', N'CustomerMessages';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[CustomerEmployeer]', N'CustomerEmployeers';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[Employeers].[IX_Employeer_AddressId]', N'IX_Employeers_AddressId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[Emails].[IX_Email_CustomerId]', N'IX_Emails_CustomerId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[CustomerMessages].[IX_CustomerMessage_CustomerId]', N'IX_CustomerMessages_CustomerId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    EXEC sp_rename N'[CustomerEmployeers].[IX_CustomerEmployeer_EmployeerId1]', N'IX_CustomerEmployeers_EmployeerId1', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Addresses] ADD [VitalSigynEmployeeId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Employeers] ADD CONSTRAINT [PK_Employeers] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Emails] ADD CONSTRAINT [PK_Emails] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerMessages] ADD CONSTRAINT [PK_CustomerMessages] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeers] ADD CONSTRAINT [PK_CustomerEmployeers] PRIMARY KEY ([CustomerId], [EmployeerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    CREATE TABLE [VitalSigynEmployees] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [LogIn] bit NOT NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Gender] int NOT NULL,
        [EmailsId] int NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_VitalSigynEmployees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VitalSigynEmployees_Emails_EmailsId] FOREIGN KEY ([EmailsId]) REFERENCES [Emails] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    CREATE TABLE [VitalSigynMessages] (
        [Id] int NOT NULL IDENTITY,
        [Message] nvarchar(max) NULL,
        [DateTimeOfMessage] datetime2 NOT NULL,
        [Resolved] bit NOT NULL,
        [VitalSigynEmployeeId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_VitalSigynMessages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VitalSigynMessages_VitalSigynEmployees_VitalSigynEmployeeId] FOREIGN KEY ([VitalSigynEmployeeId]) REFERENCES [VitalSigynEmployees] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    CREATE INDEX [IX_Addresses_VitalSigynEmployeeId] ON [Addresses] ([VitalSigynEmployeeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    CREATE INDEX [IX_VitalSigynEmployees_EmailsId] ON [VitalSigynEmployees] ([EmailsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    CREATE INDEX [IX_VitalSigynMessages_VitalSigynEmployeeId] ON [VitalSigynMessages] ([VitalSigynEmployeeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Addresses] ADD CONSTRAINT [FK_Addresses_VitalSigynEmployees_VitalSigynEmployeeId] FOREIGN KEY ([VitalSigynEmployeeId]) REFERENCES [VitalSigynEmployees] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeers] ADD CONSTRAINT [FK_CustomerEmployeers_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerEmployeers] ADD CONSTRAINT [FK_CustomerEmployeers_Employeers_EmployeerId1] FOREIGN KEY ([EmployeerId1]) REFERENCES [Employeers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [CustomerMessages] ADD CONSTRAINT [FK_CustomerMessages_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Emails] ADD CONSTRAINT [FK_Emails_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    ALTER TABLE [Employeers] ADD CONSTRAINT [FK_Employeers_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190319182351_PeopleModelRev1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190319182351_PeopleModelRev1', N'2.2.3-servicing-35854');
END;

GO


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Admin] bit NOT NULL,
        [Phone] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Gender] nvarchar(max) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [Cases] (
        [Id] int NOT NULL IDENTITY,
        [Subject] nvarchar(max) NULL,
        [DateTimeOfInitialMessage] datetime2 NOT NULL,
        [TimeToResolution] datetime2 NOT NULL,
        [Resolved] bit NOT NULL,
        [UserId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Cases] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Cases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [Emails] (
        [Id] int NOT NULL IDENTITY,
        [EmailAddress] nvarchar(max) NULL,
        [UserId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Emails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Emails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [Messages] (
        [Id] int NOT NULL IDENTITY,
        [DateTimeOfMessage] datetime2 NOT NULL,
        [MessageText] nvarchar(max) NULL,
        [CaseId] int NOT NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [ServiceProviderEmployees] (
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
        CONSTRAINT [PK_ServiceProviderEmployees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceProviderEmployees_Emails_EmailsId] FOREIGN KEY ([EmailsId]) REFERENCES [Emails] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
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
        [ServiceProviderEmployeeId] int NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Addresses_Users_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Addresses_ServiceProviderEmployees_ServiceProviderEmployeeId] FOREIGN KEY ([ServiceProviderEmployeeId]) REFERENCES [ServiceProviderEmployees] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [Employeers] (
        [Id] nvarchar(450) NOT NULL,
        [CompanyName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [AddressId] int NULL,
        [Phone] nvarchar(max) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_Employeers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employeers_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE TABLE [UserEmployeers] (
        [UserId] int NOT NULL,
        [EmployeerId] int NOT NULL,
        [EmployeerId1] nvarchar(450) NULL,
        [LastModified] datetime2 NOT NULL,
        CONSTRAINT [PK_UserEmployeers] PRIMARY KEY ([UserId], [EmployeerId]),
        CONSTRAINT [FK_UserEmployeers_Employeers_EmployeerId1] FOREIGN KEY ([EmployeerId1]) REFERENCES [Employeers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserEmployeers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Addresses_ServiceProviderEmployeeId] ON [Addresses] ([ServiceProviderEmployeeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Cases_UserId] ON [Cases] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Emails_UserId] ON [Emails] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Employeers_AddressId] ON [Employeers] ([AddressId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_Messages_CaseId] ON [Messages] ([CaseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_ServiceProviderEmployees_EmailsId] ON [ServiceProviderEmployees] ([EmailsId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    CREATE INDEX [IX_UserEmployeers_EmployeerId1] ON [UserEmployeers] ([EmployeerId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190422144915_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190422144915_init', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190428200025_actualSqlDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190428200025_actualSqlDb', N'2.2.3-servicing-35854');
END;

GO


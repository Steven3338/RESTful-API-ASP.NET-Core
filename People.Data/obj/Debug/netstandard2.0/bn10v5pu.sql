IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

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

GO

CREATE TABLE [Emails] (
    [Id] int NOT NULL IDENTITY,
    [EmailAddress] nvarchar(max) NULL,
    [CustomerId] int NOT NULL,
    [LastModified] datetime2 NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Emails_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

GO

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

GO

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
    [GetServiceProviderEmployeeId] int NULL,
    [ServiceProviderId] int NOT NULL,
    [LastModified] datetime2 NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Addresses_VitalSigynEmployees_GetServiceProviderEmployeeId] FOREIGN KEY ([GetServiceProviderEmployeeId]) REFERENCES [VitalSigynEmployees] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CustomerMessages] (
    [Id] int NOT NULL IDENTITY,
    [CustomerMessage] nvarchar(max) NULL,
    [DateTimeOfCustomerMessage] nvarchar(max) NULL,
    [VitalSigynMessage] nvarchar(max) NULL,
    [DateTimeOfVitalSigynMessage] nvarchar(max) NULL,
    [TimeToResolution] nvarchar(max) NULL,
    [Resolved] bit NOT NULL,
    [CustomerId] int NOT NULL,
    [VitalSigynEmployeeId] int NOT NULL,
    [LastModified] datetime2 NOT NULL,
    CONSTRAINT [PK_CustomerMessages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerMessages_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerMessages_VitalSigynEmployees_VitalSigynEmployeeId] FOREIGN KEY ([VitalSigynEmployeeId]) REFERENCES [VitalSigynEmployees] ([Id]) ON DELETE CASCADE
);

GO

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

GO

CREATE TABLE [CustomerEmployeers] (
    [CustomerId] int NOT NULL,
    [EmployeerId] int NOT NULL,
    [EmployeerId1] nvarchar(450) NULL,
    [LastModified] datetime2 NOT NULL,
    CONSTRAINT [PK_CustomerEmployeers] PRIMARY KEY ([CustomerId], [EmployeerId]),
    CONSTRAINT [FK_CustomerEmployeers_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerEmployeers_Employeers_EmployeerId1] FOREIGN KEY ([EmployeerId1]) REFERENCES [Employeers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);

GO

CREATE INDEX [IX_Addresses_GetServiceProviderEmployeeId] ON [Addresses] ([GetServiceProviderEmployeeId]);

GO

CREATE INDEX [IX_CustomerEmployeers_EmployeerId1] ON [CustomerEmployeers] ([EmployeerId1]);

GO

CREATE INDEX [IX_CustomerMessages_CustomerId] ON [CustomerMessages] ([CustomerId]);

GO

CREATE INDEX [IX_CustomerMessages_VitalSigynEmployeeId] ON [CustomerMessages] ([VitalSigynEmployeeId]);

GO

CREATE INDEX [IX_Emails_CustomerId] ON [Emails] ([CustomerId]);

GO

CREATE INDEX [IX_Employeers_AddressId] ON [Employeers] ([AddressId]);

GO

CREATE INDEX [IX_VitalSigynEmployees_EmailsId] ON [VitalSigynEmployees] ([EmailsId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190320145203_init', N'2.2.3-servicing-35854');

GO


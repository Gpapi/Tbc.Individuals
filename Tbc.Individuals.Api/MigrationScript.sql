IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'Tbc.Individuals')
BEGIN
    CREATE DATABASE [Tbc.Individuals];
END;
GO

USE [Tbc.Individuals];
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Translations] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Translations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cities] (
    [Id] int NOT NULL IDENTITY,
    [NameId] int NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cities_Translations_NameId] FOREIGN KEY ([NameId]) REFERENCES [Translations] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TranslationValues] (
    [Id] int NOT NULL IDENTITY,
    [Language] nvarchar(2) NOT NULL,
    [Value] nvarchar(250) NOT NULL,
    [TranslationId] int NULL,
    CONSTRAINT [PK_TranslationValues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TranslationValues_Translations_TranslationId] FOREIGN KEY ([TranslationId]) REFERENCES [Translations] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Individuals] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] date NOT NULL,
    [PersonalId] nvarchar(11) NOT NULL,
    [CityId] int NOT NULL,
    [ProfileImage] nvarchar(128) NOT NULL,
    CONSTRAINT [PK_Individuals] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Individuals_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PhoneNumbers] (
    [Number] nvarchar(50) NOT NULL,
    [Type] int NOT NULL,
    [IndividualId] int NOT NULL,
    CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY ([IndividualId], [Number], [Type]),
    CONSTRAINT [FK_PhoneNumbers_Individuals_IndividualId] FOREIGN KEY ([IndividualId]) REFERENCES [Individuals] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RelatedIndividuals] (
    [Id] int NOT NULL IDENTITY,
    [RelationshipType] int NOT NULL,
    [RelatedIndividualId] int NOT NULL,
    [IndividualId] int NULL,
    CONSTRAINT [PK_RelatedIndividuals] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RelatedIndividuals_Individuals_IndividualId] FOREIGN KEY ([IndividualId]) REFERENCES [Individuals] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RelatedIndividuals_Individuals_RelatedIndividualId] FOREIGN KEY ([RelatedIndividualId]) REFERENCES [Individuals] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id') AND [object_id] = OBJECT_ID(N'[Translations]'))
    SET IDENTITY_INSERT [Translations] ON;
INSERT INTO [Translations] ([Id])
VALUES (1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11),
(12),
(13),
(14),
(15),
(16),
(17),
(18),
(19),
(20);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id') AND [object_id] = OBJECT_ID(N'[Translations]'))
    SET IDENTITY_INSERT [Translations] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NameId') AND [object_id] = OBJECT_ID(N'[Cities]'))
    SET IDENTITY_INSERT [Cities] ON;
INSERT INTO [Cities] ([Id], [NameId])
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NameId') AND [object_id] = OBJECT_ID(N'[Cities]'))
    SET IDENTITY_INSERT [Cities] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Language', N'TranslationId', N'Value') AND [object_id] = OBJECT_ID(N'[TranslationValues]'))
    SET IDENTITY_INSERT [TranslationValues] ON;
INSERT INTO [TranslationValues] ([Id], [Language], [TranslationId], [Value])
VALUES (1, N'ka', 1, N'თბილისი'),
(2, N'en', 1, N'Tbilisi'),
(3, N'ka', 2, N'ბათუმი'),
(4, N'en', 2, N'Batumi'),
(5, N'ka', 3, N'ქუთაისი'),
(6, N'en', 3, N'Kutaisi'),
(7, N'ka', 4, N'რუსთავი'),
(8, N'en', 4, N'Rustavi'),
(9, N'ka', 5, N'გორი'),
(10, N'en', 5, N'Gori'),
(11, N'ka', 6, N'ზუგდიდი'),
(12, N'en', 6, N'Zugdidi'),
(13, N'ka', 7, N'ფოთი'),
(14, N'en', 7, N'Poti'),
(15, N'ka', 8, N'თელავი'),
(16, N'en', 8, N'Telavi'),
(17, N'ka', 9, N'ახალციხე'),
(18, N'en', 9, N'Akhaltsikhe'),
(19, N'ka', 10, N'ოზურგეთი'),
(20, N'en', 10, N'Ozurgeti'),
(21, N'ka', 11, N'ამბროლაური'),
(22, N'en', 11, N'Ambrolauri'),
(23, N'ka', 12, N'ახალქალაქი'),
(24, N'en', 12, N'Akhalkalaki'),
(25, N'ka', 13, N'ბორჯომი'),
(26, N'en', 13, N'Borjomi'),
(27, N'ka', 14, N'ლანჩხუთი'),
(28, N'en', 14, N'Lanchkhuti'),
(29, N'ka', 15, N'მარნეული'),
(30, N'en', 15, N'Marneuli'),
(31, N'ka', 16, N'საჩხერე'),
(32, N'en', 16, N'Sachkhere'),
(33, N'ka', 17, N'საგარეჯო'),
(34, N'en', 17, N'Sagarejo'),
(35, N'ka', 18, N'ჩხოროწყუ'),
(36, N'en', 18, N'Chkhorotsku'),
(37, N'ka', 19, N'ხაშური'),
(38, N'en', 19, N'Khashuri'),
(39, N'ka', 20, N'წალკა'),
(40, N'en', 20, N'Tsalka');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Language', N'TranslationId', N'Value') AND [object_id] = OBJECT_ID(N'[TranslationValues]'))
    SET IDENTITY_INSERT [TranslationValues] OFF;
GO

CREATE INDEX [IX_Cities_NameId] ON [Cities] ([NameId]);
GO

CREATE INDEX [IX_Individuals_CityId] ON [Individuals] ([CityId]);
GO

CREATE INDEX [IX_RelatedIndividuals_IndividualId] ON [RelatedIndividuals] ([IndividualId]);
GO

CREATE INDEX [IX_RelatedIndividuals_RelatedIndividualId] ON [RelatedIndividuals] ([RelatedIndividualId]);
GO

CREATE INDEX [IX_TranslationValues_TranslationId] ON [TranslationValues] ([TranslationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507114851_Initial', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Individuals]') AND [c].[name] = N'ProfileImage');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Individuals] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Individuals] ALTER COLUMN [ProfileImage] nvarchar(128) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507145342_ProfileImageNull', N'8.0.15');
GO

COMMIT;
GO



-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2023 23:27:46
-- Generated from EDMX file: C:\Users\dbasi\Desktop\PPPK\Projects\PPPK_MVCApp\PPPK_MVCApp\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PPPK_MVCAppDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Veterinarians'
CREATE TABLE [dbo].[Veterinarians] (
    [IDVeterinarian] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Email] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [IDOwner] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Email] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Pets'
CREATE TABLE [dbo].[Pets] (
    [IDPet] int IDENTITY(1,1) NOT NULL,
    [PetName] nvarchar(20)  NOT NULL,
    [Species] nvarchar(50)  NOT NULL,
    [Age] int  NOT NULL,
    [VeterinarianIDVeterinarian] int  NOT NULL,
    [OwnerIDOwner] int  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [PetIDPet] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDVeterinarian] in table 'Veterinarians'
ALTER TABLE [dbo].[Veterinarians]
ADD CONSTRAINT [PK_Veterinarians]
    PRIMARY KEY CLUSTERED ([IDVeterinarian] ASC);
GO

-- Creating primary key on [IDOwner] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([IDOwner] ASC);
GO

-- Creating primary key on [IDPet] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [PK_Pets]
    PRIMARY KEY CLUSTERED ([IDPet] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [VeterinarianIDVeterinarian] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [FK_VeterinarianPet]
    FOREIGN KEY ([VeterinarianIDVeterinarian])
    REFERENCES [dbo].[Veterinarians]
        ([IDVeterinarian])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VeterinarianPet'
CREATE INDEX [IX_FK_VeterinarianPet]
ON [dbo].[Pets]
    ([VeterinarianIDVeterinarian]);
GO

-- Creating foreign key on [OwnerIDOwner] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [FK_OwnerPet]
    FOREIGN KEY ([OwnerIDOwner])
    REFERENCES [dbo].[Owners]
        ([IDOwner])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerPet'
CREATE INDEX [IX_FK_OwnerPet]
ON [dbo].[Pets]
    ([OwnerIDOwner]);
GO

-- Creating foreign key on [PetIDPet] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_PetUploadedFile]
    FOREIGN KEY ([PetIDPet])
    REFERENCES [dbo].[Pets]
        ([IDPet])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PetUploadedFile'
CREATE INDEX [IX_FK_PetUploadedFile]
ON [dbo].[UploadedFiles]
    ([PetIDPet]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/24/2022 17:14:17
-- Generated from EDMX file: C:\Users\Zita Cathcart\source\repos\Assignment1\VetPractice.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Assigment1.VetPractice];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PracticeVet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vets] DROP CONSTRAINT [FK_PracticeVet];
GO
IF OBJECT_ID(N'[dbo].[FK_VetVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_VetVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerPet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pets] DROP CONSTRAINT [FK_OwnerPet];
GO
IF OBJECT_ID(N'[dbo].[FK_PetVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_PetVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitTreatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treatments] DROP CONSTRAINT [FK_VisitTreatment];
GO
IF OBJECT_ID(N'[dbo].[FK_TreatmentMedication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Medications] DROP CONSTRAINT [FK_TreatmentMedication];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Practices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Practices];
GO
IF OBJECT_ID(N'[dbo].[Vets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vets];
GO
IF OBJECT_ID(N'[dbo].[Visits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visits];
GO
IF OBJECT_ID(N'[dbo].[Pets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pets];
GO
IF OBJECT_ID(N'[dbo].[Owners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Owners];
GO
IF OBJECT_ID(N'[dbo].[Treatments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treatments];
GO
IF OBJECT_ID(N'[dbo].[Medications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medications];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Practices'
CREATE TABLE [dbo].[Practices] (
    [RegNum] int IDENTITY(1,1) NOT NULL,
    [PracticeName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [TelNo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Vets'
CREATE TABLE [dbo].[Vets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [StaffNo] int  NOT NULL,
    [ContactNo] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PracticeRegNum] int  NOT NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [VetId] int  NOT NULL,
    [PetId] int  NOT NULL
);
GO

-- Creating table 'Pets'
CREATE TABLE [dbo].[Pets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Breed] nvarchar(max)  NOT NULL,
    [OwnerId] int  NOT NULL,
    [Num] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [TelNo] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Treatments'
CREATE TABLE [dbo].[Treatments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Cost] int  NOT NULL,
    [VisitId] int  NOT NULL
);
GO

-- Creating table 'Medications'
CREATE TABLE [dbo].[Medications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Dose] int  NOT NULL,
    [TreatmentId] int  NOT NULL,
    [Cost] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RegNum] in table 'Practices'
ALTER TABLE [dbo].[Practices]
ADD CONSTRAINT [PK_Practices]
    PRIMARY KEY CLUSTERED ([RegNum] ASC);
GO

-- Creating primary key on [Id] in table 'Vets'
ALTER TABLE [dbo].[Vets]
ADD CONSTRAINT [PK_Vets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [PK_Pets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [PK_Treatments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Medications'
ALTER TABLE [dbo].[Medications]
ADD CONSTRAINT [PK_Medications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PracticeRegNum] in table 'Vets'
ALTER TABLE [dbo].[Vets]
ADD CONSTRAINT [FK_PracticeVet]
    FOREIGN KEY ([PracticeRegNum])
    REFERENCES [dbo].[Practices]
        ([RegNum])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeVet'
CREATE INDEX [IX_FK_PracticeVet]
ON [dbo].[Vets]
    ([PracticeRegNum]);
GO

-- Creating foreign key on [VetId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_VetVisit]
    FOREIGN KEY ([VetId])
    REFERENCES [dbo].[Vets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VetVisit'
CREATE INDEX [IX_FK_VetVisit]
ON [dbo].[Visits]
    ([VetId]);
GO

-- Creating foreign key on [OwnerId] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [FK_OwnerPet]
    FOREIGN KEY ([OwnerId])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerPet'
CREATE INDEX [IX_FK_OwnerPet]
ON [dbo].[Pets]
    ([OwnerId]);
GO

-- Creating foreign key on [PetId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_PetVisit]
    FOREIGN KEY ([PetId])
    REFERENCES [dbo].[Pets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PetVisit'
CREATE INDEX [IX_FK_PetVisit]
ON [dbo].[Visits]
    ([PetId]);
GO

-- Creating foreign key on [VisitId] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [FK_VisitTreatment]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitTreatment'
CREATE INDEX [IX_FK_VisitTreatment]
ON [dbo].[Treatments]
    ([VisitId]);
GO

-- Creating foreign key on [TreatmentId] in table 'Medications'
ALTER TABLE [dbo].[Medications]
ADD CONSTRAINT [FK_TreatmentMedication]
    FOREIGN KEY ([TreatmentId])
    REFERENCES [dbo].[Treatments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TreatmentMedication'
CREATE INDEX [IX_FK_TreatmentMedication]
ON [dbo].[Medications]
    ([TreatmentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
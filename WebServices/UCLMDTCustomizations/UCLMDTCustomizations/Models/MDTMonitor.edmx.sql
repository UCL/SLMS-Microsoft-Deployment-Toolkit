
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2012 17:23:08
-- Generated from EDMX file: C:\Users\rmhanje\Dropbox\workspace\UCLMDTCustomizations\UCLMDTCustomizations\UCLMDTCustomizations\Models\MDTMonitor.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Inventory];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ComputerHasIdentities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ComputerIdentities] DROP CONSTRAINT [FK_ComputerHasIdentities];
GO
IF OBJECT_ID(N'[dbo].[FK_ComputerLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logs] DROP CONSTRAINT [FK_ComputerLog];
GO
IF OBJECT_ID(N'[dbo].[FK_ComputerProperty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_ComputerProperty];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ComputerIdentities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComputerIdentities];
GO
IF OBJECT_ID(N'[dbo].[Computers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Computers];
GO
IF OBJECT_ID(N'[dbo].[NextIDs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NextIDs];
GO
IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[Properties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Properties];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ComputerIdentities'
CREATE TABLE [dbo].[ComputerIdentities] (
    [ID] int  NOT NULL,
    [Identifier] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Computers'
CREATE TABLE [dbo].[Computers] (
    [Name] nvarchar(100)  NULL,
    [PercentComplete] smallint  NULL,
    [Settings] nvarchar(4000)  NULL,
    [Warnings] int  NULL,
    [Errors] int  NULL,
    [DeploymentStatus] int  NULL,
    [StartTime] datetime  NULL,
    [EndTime] datetime  NULL,
    [ID] int IDENTITY(1,1) NOT NULL,
    [UniqueID] uniqueidentifier  NOT NULL,
    [CurrentStep] int  NULL,
    [TotalSteps] int  NULL,
    [StepName] nvarchar(256)  NULL,
    [LastTime] datetime  NULL,
    [DartIP] nvarchar(100)  NULL,
    [DartPort] nvarchar(8)  NULL,
    [DartTicket] nvarchar(16)  NULL,
    [VMHost] nvarchar(256)  NULL,
    [VMName] nvarchar(256)  NULL,
    [TestColumn] nvarchar(100)  NULL,
    [Severity] nvarchar(max)  NULL,
    [Message] nvarchar(max)  NULL,
    [MessageID] nvarchar(max)  NULL,
    [Custodian] nvarchar(max)  NULL,
    [Room] nvarchar(max)  NULL,
    [Location] nvarchar(max)  NULL,
    [ComputerName] nvarchar(max)  NULL,
    [UUID] uniqueidentifier  NULL,
    [MacAddresses] nvarchar(max)  NULL,
    [Operator] nvarchar(max)  NULL
);
GO

-- Creating table 'NextIDs'
CREATE TABLE [dbo].[NextIDs] (
    [ID] int  NOT NULL,
    [NextValue] int  NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [File] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [ComputerID] int  NOT NULL
);
GO

-- Creating table 'Properties'
CREATE TABLE [dbo].[Properties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PropertyKey] nvarchar(max)  NULL,
    [PropertyValue] nvarchar(max)  NULL,
    [ComputerID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID], [Identifier] in table 'ComputerIdentities'
ALTER TABLE [dbo].[ComputerIdentities]
ADD CONSTRAINT [PK_ComputerIdentities]
    PRIMARY KEY CLUSTERED ([ID], [Identifier] ASC);
GO

-- Creating primary key on [ID] in table 'Computers'
ALTER TABLE [dbo].[Computers]
ADD CONSTRAINT [PK_Computers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'NextIDs'
ALTER TABLE [dbo].[NextIDs]
ADD CONSTRAINT [PK_NextIDs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Properties'
ALTER TABLE [dbo].[Properties]
ADD CONSTRAINT [PK_Properties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID] in table 'ComputerIdentities'
ALTER TABLE [dbo].[ComputerIdentities]
ADD CONSTRAINT [FK_ComputerHasIdentities]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[Computers]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ComputerID] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [FK_ComputerLog]
    FOREIGN KEY ([ComputerID])
    REFERENCES [dbo].[Computers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ComputerLog'
CREATE INDEX [IX_FK_ComputerLog]
ON [dbo].[Logs]
    ([ComputerID]);
GO

-- Creating foreign key on [ComputerID] in table 'Properties'
ALTER TABLE [dbo].[Properties]
ADD CONSTRAINT [FK_ComputerProperty]
    FOREIGN KEY ([ComputerID])
    REFERENCES [dbo].[Computers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ComputerProperty'
CREATE INDEX [IX_FK_ComputerProperty]
ON [dbo].[Properties]
    ([ComputerID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
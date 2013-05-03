
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/02/2012 15:33:25
-- Generated from EDMX file: C:\Users\rmhanje\Dropbox\workspace\UCLMDTCustomizations\UCLMDTCustomizations\UCLMDTCustomizations\Models\MDTStatus.edmx
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

IF OBJECT_ID(N'[dbo].[FK_StatusLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logs] DROP CONSTRAINT [FK_StatusLog];
GO
IF OBJECT_ID(N'[dbo].[FK_StatusVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables] DROP CONSTRAINT [FK_StatusVariable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[Variables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Variables];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [File] varbinary(max)  NOT NULL,
    [StatusId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [Id] uniqueidentifier  NOT NULL,
    [Engineer] nvarchar(max)  NULL,
    [Property] nvarchar(max)  NULL,
    [MachineId] nvarchar(max)  NULL,
    [ComputerName] nvarchar(max)  NULL,
    [Severity] nvarchar(max)  NULL,
    [StepName] nvarchar(max)  NULL,
    [DartIP] nvarchar(max)  NULL,
    [DartPort] nvarchar(max)  NULL,
    [DartTicket] nvarchar(max)  NULL,
    [CurrentStep] nvarchar(max)  NULL,
    [TotalSteps] nvarchar(max)  NULL,
    [Message] nvarchar(max)  NULL,
    [SmsUniqueID] nvarchar(max)  NULL,
    [MessageID] nvarchar(max)  NULL,
    [VmHost] nvarchar(max)  NULL,
    [VmName] nvarchar(max)  NULL,
    [Custodian] nvarchar(max)  NULL,
    [Room] nvarchar(max)  NULL,
    [Location] nvarchar(max)  NULL,
    [StartTime] datetime  NULL,
    [Timestamp] datetime  NULL
);
GO

-- Creating table 'Variables'
CREATE TABLE [dbo].[Variables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StatusId] uniqueidentifier  NOT NULL,
    [Property] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [PK_Variables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StatusId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [FK_StatusLog]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusLog'
CREATE INDEX [IX_FK_StatusLog]
ON [dbo].[Logs]
    ([StatusId]);
GO

-- Creating foreign key on [StatusId] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [FK_StatusVariable]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusVariable'
CREATE INDEX [IX_FK_StatusVariable]
ON [dbo].[Variables]
    ([StatusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
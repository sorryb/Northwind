CREATE TABLE [dbo].[Persons] (
    [ID]        INT           NOT NULL,
    [LastName]  VARCHAR (255) NOT NULL,
    [FirstName] VARCHAR (255) NULL,
    [Age]       INT           NULL,
    CHECK ([Age]>=(18))
);


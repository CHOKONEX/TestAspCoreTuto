SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--IF OBJECT_ID('dbo.[Customer_Seed]', 'P') IS NOT NULL
--	DROP PROCEDURE [Customer_Seed]
--GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Customer_Seed') AND type in (N'P', N'PC'))
BEGIN 
	DROP PROCEDURE [dbo].[Customer_Seed]
END

--IF EXISTS ( SELECT * FROM SYS.OBJECTS 
--	WHERE NAME='Customer_Seed' AND OBJECTPROPERTY(OBJECT_ID,'ISPROCEDURE')=1 )
--	DROP PROCEDURE [Customer_Seed]

IF OBJECT_ID('dbo.[Customer]', 'U') IS NOT NULL
BEGIN
	DROP TABLE [Customer]
END

IF TYPE_ID('dbo.TVP_Customer') IS NOT NULL
BEGIN
	DROP TYPE TVP_Customer
END

CREATE TABLE [Customer]
(
    [CustomerID] [INT] IDENTITY(1,1) NOT NULL,
    [Code] [VARCHAR](20) NULL,
    [Name] [VARCHAR](20) NULL,

    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
    (
        [CustomerID] ASC
    )
)


CREATE TYPE TVP_Customer AS TABLE
(
    [Code] [VARCHAR](20) NULL,
    [Name] [VARCHAR](20) NULL
)

GO
CREATE PROCEDURE [Customer_Seed] 
	@Customers TVP_Customer READONLY
AS
BEGIN
    INSERT INTO Customer (Code, Name)
    SELECT Code, Name
    FROM @Customers
END

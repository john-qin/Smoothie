USE [Smoothie]
GO

/****** Object:  StoredProcedure [dbo].[SearchIngredients]    Script Date: 09/17/2012 08:33:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		John Qin
-- Create date: 9/12/2012
-- Description:	Search Ingredients
-- =============================================
CREATE PROCEDURE [dbo].[SearchIngredients] 
	@Term NVARCHAR(50) 
AS
BEGIN
	SET NOCOUNT ON;

	SET @Term = '%' + @Term + '%'
	
    SELECT  *
	FROM    dbo.FoodAbbrev
	WHERE   Name LIKE @Term
	ORDER BY Name
	
END

GO



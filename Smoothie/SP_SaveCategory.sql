USE [Smoothie]
GO

/****** Object:  StoredProcedure [dbo].[SaveCategory]    Script Date: 09/17/2012 08:32:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		John Qin
-- Create date: 9/16/2012
-- Description:	insert a new category or edit the existing one
-- =============================================
CREATE PROCEDURE [dbo].[SaveCategory]
    @Id INT ,
    @Name NVARCHAR(50) ,
    @ReOrder INT ,
    @Status INT
AS 
    BEGIN
        SET NOCOUNT ON;

        IF @Id > 0 
            BEGIN
				UPDATE dbo.Category
				SET Name = @Name, ReOrder = @ReOrder, [Status] = @Status
				WHERE Id = @Id;
				
            END
        ELSE 
            BEGIN
				INSERT INTO dbo.Category
				        ( Name, ReOrder, [Status] )
				VALUES  ( @Name, @ReOrder, @Status );
				
				SET @Id = SCOPE_IDENTITY();
            END
            
         RETURN @Id  
    END

GO



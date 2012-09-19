USE [Smoothie]
GO

/****** Object:  StoredProcedure [dbo].[AddSmoothieIngredients]    Script Date: 09/17/2012 08:31:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<John Qin>
-- Create date: <8/27/2012>
-- Description:	<Add a new smoothie>
-- =============================================
CREATE PROCEDURE [dbo].[AddSmoothieIngredients]
	-- Add the parameters for the stored procedure here
    @Query NVARCHAR(4000) ,
    @SmoothieId INT ,
    @CreatedDate DATETIME ,
    @Status INT ,
    @UserId INT
AS 
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON;

        BEGIN TRY
            BEGIN TRAN
		
            IF @SmoothieId > 0 
                BEGIN
                    DELETE  FROM dbo.SmoothieIngredients
                    WHERE   SmoothieId = @SmoothieId;
                    
                    EXECUTE (@Query);
                END
            ELSE 
                BEGIN
				
                    IF @UserId = 0 
                        SET @UserId = NULL;
						
                    INSERT  INTO dbo.Smoothie
                            ( Name, CreatedDate, Status, UserId )
                    VALUES  ( N'', @CreatedDate, @Status, @UserId );
					
                    SET @SmoothieId = SCOPE_IDENTITY();
					
                    SET @Query = REPLACE(@Query, 'sId', @SmoothieId);
                    EXECUTE (@Query);
					
                END	
                
                	
            COMMIT TRAN
            
            RETURN @SmoothieId
	
        END TRY
	
        BEGIN CATCH
            ROLLBACK
        END CATCH

    END

GO



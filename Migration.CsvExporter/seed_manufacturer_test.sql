USE [ECommerceShopDB];
GO

INSERT INTO [dbo].[Manufacturers]
           ([Name]
           ,[Description]
           ,[AddressID]
           ,[IsActive]
           ,[DisplaySeqNo]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[ModifiedOn]
           ,[ModifiedBy])
     VALUES
           (N'Test Manufacturer'
           ,N'Sample manufacturer for migration testing.'
           ,1
           ,1
           ,1.00
           ,GETDATE()
           ,1
           ,NULL
           ,NULL);
GO

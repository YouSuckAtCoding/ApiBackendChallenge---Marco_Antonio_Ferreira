CREATE TABLE [dbo].[Product]
(
	[Code] BIGINT NOT NULL PRIMARY KEY, 
    [Barcode] NVARCHAR(50) NOT NULL, 
    [Status] NVARCHAR(50) NOT NULL, 
    [Imported_t] DATETIME NOT NULL, 
    [Quantity] NVARCHAR(50) NOT NULL, 
    [Url] NVARCHAR(300) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Categories] NVARCHAR(300) NOT NULL, 
    [Packaging] NVARCHAR(300) NOT NULL, 
    [Brands] NVARCHAR(300) NOT NULL, 
    [Image_Url] NVARCHAR(300) NOT NULL
)

CREATE PROCEDURE [dbo].[spProduct_Insert]
	@Code bigint,
	@Barcode nvarchar(50),
	@Status nvarchar(50),
	@Imported_t datetime,
	@Quantity nvarchar(50),
	@Url nvarchar(300),
	@Name nvarchar(100),
	@Categories nvarchar(300),
	@Packaging nvarchar(300), 
	@Brands nvarchar(300),
	@Image_url nvarchar(300)

AS
Begin
	
	Insert into dbo.[Product] (Code, Barcode, Status, Imported_t, Quantity, Url, Name, Categories, Packaging, Brands, Image_Url)
	values (@Code, @Barcode, @Status, @Imported_t, @Quantity, @Url, @Name, @Categories, @Packaging, @Brands, @Image_Url)

End

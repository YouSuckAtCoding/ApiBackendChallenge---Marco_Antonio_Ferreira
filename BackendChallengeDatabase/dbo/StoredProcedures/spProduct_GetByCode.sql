CREATE PROCEDURE [dbo].[spProduct_GetByCode]
	@Code bigint
AS
Begin

	Select Code, Barcode, [Status], Imported_t, Quantity, [Url], [Name], Categories, Packaging, Brands, Image_Url from dbo.[Product] 
	Where Code = @Code;
	
End

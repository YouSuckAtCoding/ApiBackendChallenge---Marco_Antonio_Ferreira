CREATE PROCEDURE [dbo].[spProduct_GetAll]
	
AS
Begin

	Select Code, Barcode, [Status], Imported_t, Quantity, [Url], [Name], Categories, Packaging, Brands, Image_Url from dbo.[Product] 
	
End
namespace api_jet.Models.ViewModels.Products
{
    public class ListProductsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public bool Status { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    
}
}
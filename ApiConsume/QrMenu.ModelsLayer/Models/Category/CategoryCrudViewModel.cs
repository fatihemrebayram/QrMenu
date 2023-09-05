namespace QrMenu.ModelsLayer.Models.Category;

public class CategoryCrudViewModel
{
    public EntityLayer.Concrete.Category? CategoryViewModel { get; set; }
    public List<EntityLayer.Concrete.Category>? CategoriesViewModel { get; set; }
}
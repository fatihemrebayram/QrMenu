using System.ComponentModel.DataAnnotations;

namespace QrMenu.EntityLayer.Concrete;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public ICollection<Meal>? Meals { get; set; }
    public DateTime AddedDate { get; set; }
}
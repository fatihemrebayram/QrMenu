using System.ComponentModel.DataAnnotations;

namespace QrMenu.EntityLayer.Concrete;

public class Meal
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public DateTime AddedDate { get; set; }

}
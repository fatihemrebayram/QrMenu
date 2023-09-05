using FluentValidation;

namespace QrMenu.BusinessLayer.ValiditonRules.Meal;

public class MealValidator : AbstractValidator<EntityLayer.Concrete.Meal>
{
    public MealValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adını Boş Geçemezsiniz");
        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Kategori Adı 2 Karakterden Aşağı Olamaz");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Kategori Adı 100 Karakterden Fazla Olamaz");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklamayı Boş Geçemezsiniz");
        RuleFor(x => x.Description).MinimumLength(2).WithMessage("Açıklama 2 Karakterden Aşağı Olamaz");
        RuleFor(x => x.Description).MaximumLength(100).WithMessage("Açıklama 100 Karakterden Fazla Olamaz");


        RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyatı Boş Geçemezsiniz");
      
    }
}
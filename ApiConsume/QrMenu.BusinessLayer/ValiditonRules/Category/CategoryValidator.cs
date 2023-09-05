using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QrMenu.BusinessLayer.ValiditonRules.Category;

    public class CategoryValidator : AbstractValidator<EntityLayer.Concrete.Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adını Boş Geçemezsiniz");
        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Kategori Adı 2 Karakterden Aşağı Olamaz");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Kategori Adı 100 Karakterden Fazla Olamaz");
    }
}


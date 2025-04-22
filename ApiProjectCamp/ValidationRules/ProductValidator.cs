using FluentValidation;
using ApiProjectCamp.WebApi.Entities;

namespace ApiProjectCamp.WebApi.ValidationRules;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adını boş geçmeyiniz.").MinimumLength(2).WithMessage("En az iki karakter girişi yapınız.").MaximumLength(50).WithMessage("En fazla 50 karakter girişi yapınız."); ;

        RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez.").GreaterThan(0).WithMessage("Fiyatı 0'dan büyük giriniz.").LessThan(1000).WithMessage("Ürün fiyatı bu kadar yüksek olamaz, girdiğiniz değeri kontrol ediniz!");

        RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş geçilemez.");
    }
}

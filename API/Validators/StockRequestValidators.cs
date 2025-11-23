using FluentValidation;
using Model.Requests;

namespace API.Validators
{
    public class BuyStockRequestValidator : AbstractValidator<BuyStockRequest>
    {
        public BuyStockRequestValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.StockId).NotEmpty().WithMessage("StockId is required.");
            RuleFor(x => x.Shares).GreaterThan(0).WithMessage("Shares must be greater than zero.");
        }
    }

    public class SellStockRequestValidator : AbstractValidator<SellStockRequest>
    {
        public SellStockRequestValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.StockId).NotEmpty().WithMessage("StockId is required.");
            RuleFor(x => x.Shares).GreaterThan(0).WithMessage("Shares must be greater than zero.");
        }
    }
}

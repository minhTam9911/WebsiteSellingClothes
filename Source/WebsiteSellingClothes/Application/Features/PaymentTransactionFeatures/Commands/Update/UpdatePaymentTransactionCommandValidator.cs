using Application.DTOs.Responses;
using FluentValidation;

namespace Application.Features.PaymentTransactionFeatures.Commands.Update;

public class UpdatePaymentTransactionCommandValidator :AbstractValidator<UpdatePaymentTransactionCommand>
{
    public UpdatePaymentTransactionCommandValidator()
    {
    }

}
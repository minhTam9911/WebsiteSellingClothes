using Application.DTOs.Momos;
using Application.DTOs.Requests;
using Application.DTOs.VnPays;
using Application.DTOs.ZaloPays;
using Application.Features.OrderFeatures.Commands.Create;
using Application.Features.OrderFeatures.Commands.SetPaid;
using Application.Features.PaymentFeatures.Commands.Create;
using Application.Features.PaymentFeatures.Commands.ProcessMomo;
using Application.Features.PaymentFeatures.Commands.ProcessMomoPaymentIpn;
using Application.Features.PaymentFeatures.Commands.ProcessVnPay;
using Application.Features.PaymentFeatures.Commands.ProcessVnPayIpn;
using Application.Features.PaymentFeatures.Commands.ProcessZaloPay;
using Application.Features.PaymentFeatures.Commands.ProcessZaloPayIpn;
using Application.Features.PaymentSignatureFeatures.Commands.Create;
using Asp.Versioning;
using AutoMapper;
using Common.Extensions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class PaymentsController : ApiControllerBase
{
    private readonly IMapper mapper;

    public PaymentsController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OrderRequestDto orderRequestDto)
    {

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var order = await Sender.Send(new CreateOrderCommand() { OrderRequestDto = orderRequestDto, UserId = userId });
        var payment = new PaymentRequestDto()
        {
            OrderId = order.Id,
            MerchantId = orderRequestDto.MerchantId,
            PaymentContent = $"ORDER # {order.Id}",
            PaymentCurrency = orderRequestDto.PaymentCurrency,
            PaymentDestinationId = orderRequestDto.PaymentDestinationId,
            PaymentDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddMinutes(15),
            PaymentLanguage = orderRequestDto.PaymentLanguage,
            RequiredAmount = order.Amount,
        };
        var result = await Sender.Send(new CreatePaymentCommand() { UserId = userId, Order = order, PaymentRequestDto = payment });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("vnpay-return")]
    public async Task<IActionResult> VnPayReturn([FromQuery] VnPayResponseDto vnPayResponseDto)
    {
        var returnUrl = string.Empty;
        var processResult = await Sender.Send(vnPayResponseDto.Adapt<ProcessVnPayPaymentCommand>());
        var returnMode = new PaymentReturnDto();
        if (processResult.Flag)
        {
            returnMode = processResult.Data.Item1 as PaymentReturnDto;
            returnUrl = processResult.Data.Item2 as string;
        }
        var paymentProcessIpn = await Sender.Send(vnPayResponseDto.Adapt<ProcessVnPayIpnCommand>());
        if (paymentProcessIpn.Flag)
        {
            await Sender.Send(new SetPaidOrderCommand()
            {
                PaidAmount = decimal.Parse(vnPayResponseDto.vnp_Amount.ToString()!),
                PaymentId = vnPayResponseDto.vnp_TxnRef
            });
            if (returnUrl.EndsWith("/")) returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"{returnUrl}?{returnMode.ToQueryString()}");
        }
        else
        {
            return BadRequest(paymentProcessIpn);
        }

    }

    [HttpGet("momo-return")]
    public async Task<IActionResult> MomoReturn([FromQuery] MomoOneTimePaymentResultRequest momoOneTimePaymentResultRequest)
    {
        var returnUrl = string.Empty;
        var processResult = await Sender.Send(momoOneTimePaymentResultRequest.Adapt<ProcessMomoPaymentCommand>());
        var returnMode = new PaymentReturnDto();
        if (processResult.Flag)
        {
            returnMode = processResult.Data.Item1 as PaymentReturnDto;
            returnUrl = processResult.Data.Item2 as string;
        }
        var paymentProcessIpn = await Sender.Send(momoOneTimePaymentResultRequest.Adapt<ProcessMomoPaymentIpnCommand>());
        if (paymentProcessIpn.Flag)
        {
            await Sender.Send(new SetPaidOrderCommand()
            {
                PaidAmount = decimal.Parse(momoOneTimePaymentResultRequest.amount.ToString()!),
                PaymentId = momoOneTimePaymentResultRequest.requestId
            });
            if (returnUrl.EndsWith("/")) returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"{returnUrl}?{returnMode.ToQueryString()}");
        }
        else
        {
            return BadRequest(paymentProcessIpn);
        }

    }

    [HttpGet("zalopay-return")]
    public async Task<IActionResult> ZaloPayReturn([FromQuery] ZaloPayResultRequest zaloPayResultRequest)
    {
        var returnUrl = string.Empty;
        var processResult = await Sender.Send(zaloPayResultRequest.Adapt<ProcessZaloPayCommand>());
        var returnMode = new PaymentReturnDto();
        if (processResult.Flag)
        {
            returnMode = processResult.Data.Item1 as PaymentReturnDto;
            returnUrl = processResult.Data.Item2 as string;
        }
        var paymentProcessIpn = await Sender.Send(zaloPayResultRequest.Adapt<ProcessZaloPayIpnCommand>());
        
        if (paymentProcessIpn.Flag)
        {
            string[] paymentId = zaloPayResultRequest.apptransid.Split("_");
            await Sender.Send(new SetPaidOrderCommand()
            {
                PaidAmount = decimal.Parse(zaloPayResultRequest.amount.ToString()!),
                PaymentId = paymentId[1]
            });
            
            if (returnUrl.EndsWith("/")) returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"{returnUrl}?{returnMode.ToQueryString()}");
        }
        else
        {
            return BadRequest(paymentProcessIpn);
        }

    }
}

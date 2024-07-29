using Application.DTOs.Responses;
using MediatR;

namespace Application.Features.OrderDetailFeatures.Queries.GetAll;
public class GetAllOrderDetailQuery : IRequest<List<OrderDetailResponseDto>>
{
}

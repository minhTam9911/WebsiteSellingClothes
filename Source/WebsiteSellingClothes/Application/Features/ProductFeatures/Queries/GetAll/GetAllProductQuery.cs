using Application.DTOs.Responses;
using MediatR;

namespace Application.Features.ProductFeatures.Queries.GetAll;
public class GetAllProductQuery : IRequest<List<ProductResponseDto>>
{
}

namespace Kernel.Categories.Commands;
public record CategoriesStatusChangeCommand(List<Guid> CategoriesId, bool Active) : IRequest<CategoriesListPayload>;
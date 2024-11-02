using API.RequestHelper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo, ISpecification<T> spec, int PageIndex, int PageSize ) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);
        var pagination = new Pagination<T>(PageIndex, PageSize, count, items);
        return Ok(pagination);
    }
}

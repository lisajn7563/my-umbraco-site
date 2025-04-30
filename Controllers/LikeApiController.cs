using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace MyUmbracoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeApiController : ControllerBase
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public LikeApiController(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        [HttpGet("content/{contentId}")]
        public IActionResult GetContentById(int contentId)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content?.GetById(contentId);
                if (content != null)
                {
                    return Ok(new { 
                        id = content.Id,
                        name = content.Name,
                        url = content.Url()
                    });
                }
            }
            
            return NotFound(new { error = "Content not found" });
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_MemoryCaching_Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public HomeController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{id}")]
        public IActionResult SetData([FromRoute]int id)
        {
            //در تنظیمات میتوان اولویت(priority) وانواع زمان انقضا و زمان غیر فعال بودن پیش از انتضا را تعیین کرد 
            //این اولویت برای آن است که وقتی حافظه دارد پر می شود بر اساس اولویت پاک میکند یا نگه میدارد
            var option = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1))
                .SetPriority(CacheItemPriority.Normal)
                .SetSlidingExpiration(TimeSpan.FromSeconds(1));


            var toSave1 = $"this is my first number{ id }";
            var toSave2 = $"this is my second number{id}";

            var test = _cache.Set("key1", toSave1);
            var test3 = _cache.Set("key2", toSave2, option);
            var tets2 = _cache.Get("key1");

            return Ok();
        }
    }
}

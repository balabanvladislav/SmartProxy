using Microsoft.AspNetCore.Mvc;
using UTM.Domain.Models;
using UTM.SyncNode.Services;

namespace UTM.SyncNode.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : Controller
    {
        private readonly SyncWorkJobService _workJobService;

        public SyncController(SyncWorkJobService workJobService)
        {
            _workJobService = workJobService;
        }
        [HttpPost]
        public IActionResult Sync(SyncEntity entity)
        {
            _workJobService.AddItem(entity);

            return Ok();
        }
    }
}

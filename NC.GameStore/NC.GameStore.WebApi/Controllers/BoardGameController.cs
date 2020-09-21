using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NC.GameStore.Exception;
using NC.GameStore.Mapper;
using NC.GameStore.Service.Interfaces;
using NC.GameStore.ViewModel;
using System.Linq;

namespace NC.GameStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IBoardGameService _boardGameService;
        private readonly ILogger<BoardGameController> _logger;
        private readonly string _unavailable = "Api unavailable";

        public BoardGameController(IBoardGameService boardGameService, ILogger<BoardGameController> logger)
        {
            _boardGameService = boardGameService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return GetViewModelByVerbName(nameof(Get), null);
        }

        [HttpGet("{id}")]
        [Produces("application/json", "application/xml")]
        public IActionResult Get(int id)
        {
            return GetViewModelByVerbName(nameof(Get), id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BoardGameViewModel viewModel)
        {
            return SetViewModelByVerbName(nameof(Post), viewModel, null);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BoardGameViewModel viewModel)
        {
            return SetViewModelByVerbName(nameof(Put), viewModel, null);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return SetViewModelByVerbName(nameof(Delete), null, id);
        }

        private IActionResult SendFeedback(string message)
        {
            return BadRequest(new { Message = message });
        }

        private IActionResult GetViewModelByVerbName(string verbName, int? id)
        {
            try
            {
                if (verbName.Equals(nameof(Get)) && !(id is null))
                    return Ok(_boardGameService.Get(id.Value).ToBoardGameViewModel());
                else
                    return Ok(_boardGameService.GetAll().Select(s => s.ToBoardGameViewModel()));
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        private IActionResult SetViewModelByVerbName(string verbName, BoardGameViewModel viewModel, int? id)
        {
            try
            {
                switch (verbName)
                {
                    case nameof(Delete):
                        _boardGameService.Delete(_boardGameService.Get(id.Value));
                        break;

                    case nameof(Post):
                        _boardGameService.Create(viewModel.ToEntity());
                        break;

                    case nameof(Put):
                        _boardGameService.Edit(viewModel.ToEntity());
                        break;
                }

                return Ok();
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }
    }
}
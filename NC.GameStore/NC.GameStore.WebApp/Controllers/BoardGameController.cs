using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NC.GameStore.Exception;
using NC.GameStore.Mapper;
using NC.GameStore.Service.Interfaces;
using NC.GameStore.ViewModel;
using System.Linq;

namespace NC.GameStore.WebApp.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly IBoardGameService _boardGameService;
        private readonly ILogger<BoardGameController> _logger;

        public BoardGameController(IBoardGameService boardGameService, ILogger<BoardGameController> logger)
        {
            _boardGameService = boardGameService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return GetViewModelByViewName(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return GetViewModelByViewName(nameof(Details), id);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BoardGameViewModel viewModel)
        {
            return SetViewModelByViewName(nameof(Create), viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return GetViewModelByViewName(nameof(Edit), id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BoardGameViewModel viewModel)
        {
            return SetViewModelByViewName(nameof(Edit), viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return GetViewModelByViewName(nameof(Delete), id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BoardGameViewModel viewModel)
        {
            return SetViewModelByViewName(nameof(Delete), viewModel);
        }

        private void SendFeedback(bool isError, string message)
        {
            if (isError)
                TempData["MessageError"] = message;
            else
                TempData["Message"] = message;
        }

        private IActionResult GetViewModelByViewName(string viewName, int? id = null)
        {
            try
            {
                if (viewName.Equals(nameof(Index)))
                {
                    return View(_boardGameService.GetAll().Select(s => s.ToBoardGameViewModel()).OrderBy(o => o.Title));
                }
                else if ((viewName.Equals(nameof(Details)) || (viewName.Equals(nameof(Delete)) || (viewName.Equals(nameof(Edit))))))
                {
                    if (!(id is null))
                        return View(_boardGameService.Get(id.Value).ToBoardGameViewModel());
                }

                return View();
            }
            catch (AppException ex)
            {
                SendFeedback(true, ex.Message);
                return View();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return View();
            }
        }

        private IActionResult SetViewModelByViewName(string viewName, BoardGameViewModel viewModel)
        {
            try
            {
                if (viewName.Equals(nameof(Create)))
                {
                    if (ModelState.IsValid)
                    {
                        _boardGameService.Create(viewModel.ToEntity());
                        SendFeedback(false, $"BoardGame { viewModel.Title?.ToUpper()} successfully created.");
                        return RedirectToAction(nameof(Index));
                    }

                    return View(viewModel);
                }
                else if (viewName.Equals(nameof(Edit)))
                {
                    if (ModelState.IsValid)
                    {
                        _boardGameService.Edit(viewModel.ToEntity());
                        SendFeedback(false, $"BoardGame {viewModel.Title?.ToUpper()} successfully edited.");
                        return RedirectToAction(nameof(Index));
                    }

                    return View(viewModel);
                }
                else if (viewName.Equals(nameof(Delete)))
                {
                    _boardGameService.Delete(_boardGameService.Get(viewModel.Id));
                    SendFeedback(false, $"BoardGame {viewModel.Title?.ToUpper()} successfully deleted.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (AppException ex)
            {
                SendFeedback(true, ex.Message);
                return View();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return View();
            }
        }
    }
}
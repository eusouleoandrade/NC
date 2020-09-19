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
            try
            {
                return View(_boardGameService.GetAll().Select(s => s.ToBoardGameViewModel()).OrderBy(o => o.Title));
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                return View(_boardGameService.Get(id).ToBoardGameViewModel());
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BoardGameViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _boardGameService.Create(viewModel.ToEntity());
                    SendFeedback(false, $"BoardGame { viewModel.Title?.ToUpper()} successfully created.");
                    return RedirectToAction(nameof(Index));
                }

                return View(viewModel);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                return View(_boardGameService.Get(id).ToBoardGameViewModel());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BoardGameViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _boardGameService.Edit(viewModel.ToEntity());
                    SendFeedback(false, $"BoardGame {viewModel.Title?.ToUpper()} successfully edited.");
                    return RedirectToAction(nameof(Index));
                }

                return View(viewModel);
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_boardGameService.Get(id).ToBoardGameViewModel());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BoardGameViewModel viewModel)
        {
            try
            {
                _boardGameService.Delete(_boardGameService.Get(viewModel.Id));
                SendFeedback(false, $"BoardGame {viewModel.Title?.ToUpper()} successfully deleted.");
                return RedirectToAction(nameof(Index));
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

        private void SendFeedback(bool isError, string message)
        {
            if (isError)
                TempData["MessageError"] = message;
            else
                TempData["Message"] = message;
        }
    }
}
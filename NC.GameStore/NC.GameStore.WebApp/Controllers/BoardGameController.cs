using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NC.GameStore.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NC.GameStore.WebApp.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly ILogger<BoardGameController> _logger;
        private readonly HttpClient _client;
        private readonly string urlApi = "http://boardgamestoreapi.azurewebsites.net/api/";

        public BoardGameController(ILogger<BoardGameController> logger)
        {
            _logger = logger;

            _client = new HttpClient
            {
                BaseAddress = new Uri(urlApi),
            };
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return GetListBoardGameViewModel();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return GetBoardGameViewModelById(id);
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
            if (ModelState.IsValid)
            {
                return SetViewModelByActionName(nameof(Create), viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return GetBoardGameViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BoardGameViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return SetViewModelByActionName(nameof(Edit), viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return GetBoardGameViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BoardGameViewModel viewModel)
        {
            return SetViewModelByActionName(nameof(Delete), viewModel);
        }

        private void SendFeedback(bool isError, string message)
        {
            if (isError)
                TempData["MessageError"] = message;
            else
                TempData["Message"] = message;
        }

        private IActionResult SendFeedback(HttpResponseMessage response)
        {
            var feedbackResponse = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = "" });
            SendFeedback(true, feedbackResponse.Message);
            return View();
        }

        private IActionResult SetViewModelByActionName(string actionName, BoardGameViewModel viewModel)
        {
            HttpResponseMessage response = null;
            string messageFeedback = "";

            try
            {
                switch (actionName)
                {
                    case nameof(Delete):
                        response = _client.DeleteAsync($"boardgame/{viewModel.Id}").Result;
                        messageFeedback = $"BoardGame {viewModel.Title?.ToUpper()} successfully deleted.";
                        break;

                    case nameof(Create):
                        var contentCreate = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PostAsync("boardgame", contentCreate).Result;
                        messageFeedback = $"BoardGame { viewModel.Title?.ToUpper()} successfully created.";
                        break;

                    case nameof(Edit):
                        var contentEdit = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PutAsync($"boardgame", contentEdit).Result;
                        messageFeedback = $"BoardGame {viewModel.Title?.ToUpper()} successfully edited.";
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    SendFeedback(false, messageFeedback);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
            finally
            {
                _client.Dispose();
            }
        }

        private IActionResult GetBoardGameViewModelById(int id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"boardgame/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<BoardGameViewModel>(response.Content.ReadAsStringAsync().Result);
                    return View(viewModel);
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
            finally
            {
                _client.Dispose();
            }
        }

        private IActionResult GetListBoardGameViewModel()
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"boardgame").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<IEnumerable<BoardGameViewModel>>(response.Content.ReadAsStringAsync().Result);
                    return View(viewModel.OrderBy(o => o.Title));
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
            finally
            {
                _client.Dispose();
            }
        }
    }
}
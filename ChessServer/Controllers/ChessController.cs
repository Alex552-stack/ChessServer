using ChessLogic;
using ChessServer.Models.Moves;
using ChessServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ChessServer.Controllers
{
    [ApiController]
    [Route("api/chess")]
    public class ChessController : Controller
    {
        private readonly ChessService chessService;

        public ChessController(ChessService chessService)
        {
            this.chessService = chessService;
        }

        [HttpPost]
        public IActionResult StartGame()
        {
            string gameId = chessService.StartGame();
            return Ok(new { GameId = gameId });
        }

        [HttpPost("{gameId}/MakeMove")]
        public IActionResult MakeMove(string gameId, [FromBody] Move move)
        {
            chessService.MakeMove(gameId, move);
            return Ok();
        }

        [HttpGet("TestMove")]
        public IActionResult TestMove()
        {
            return Ok(JsonSerializer.Serialize(new NormalMove(new Position(1, 1), new Position(1, 2))));
        }
    }
}

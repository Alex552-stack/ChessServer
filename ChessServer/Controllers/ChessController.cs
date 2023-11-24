using ChessLogic;
using ChessServer.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{gameId}/GetBoard")]
        public IActionResult GetBoard(string gameId)
        {
            return Ok( chessService.GetBoard(gameId) );
        }

        [HttpPost("{gameId}/GetPossibleMoves")]
        public IActionResult GetPossibleMoves(string gameId, [FromBody] Position piecePosition)
        {
            return Ok(chessService.GetPossibleMoves(gameId,piecePosition));
        }
        

        [HttpGet("TestMove")]
        public IActionResult TestMove()
        {
            return base.Ok(System.Text.Json.JsonSerializer.Serialize(new NormalMove(new Position(1, 1), new Position(1, 2))));
        }
    }
}

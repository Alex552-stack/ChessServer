using ChessLogic;

namespace ChessServer.Services
{
    public class ChessService
    {
        private Dictionary<String, GameState> activeGames = new Dictionary<string, GameState>();

        public string StartGame()
        {
            string gameId = Guid.NewGuid().ToString();

            GameState newGame = new GameState(Player.White, new Board());
            activeGames.Add(gameId, newGame);
            return gameId;
        }

        internal void MakeMove(string gameId, Move move)
        {
            activeGames[gameId].MakeMove(move);
        }
    }
}

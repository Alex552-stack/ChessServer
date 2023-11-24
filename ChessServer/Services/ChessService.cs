using ChessLogic;

namespace ChessServer.Services
{
    public class ChessService
    {
        private Dictionary<String, GameState> ActiveGames = new Dictionary<string, GameState>();

        public string StartGame()
        {
            string gameId = Guid.NewGuid().ToString();

            Board board = Board.Initial();
            GameState newGame = new GameState(Player.White, board);
            ActiveGames.Add(gameId, newGame);
            return gameId;
        }

        internal void MakeMove(string gameId, Move move)
        {
            ActiveGames[gameId].MakeMove(move);
        }

        public Board GetBoard(string gameId)
        {
            return ActiveGames[gameId].Board;
        }

        public Move[] GetPossibleMoves(string gameId, Position position)
        {
            return ActiveGames[gameId].LegalMovesForPiece(position).ToArray();
        }
    }
}

using ChessLogic;

namespace ChessServer.Models.Moves
{
    public class ConcreteMove : Move
    {
        public override MoveType Type { get; }
        public override Position FromPos { get ; set ; }
        public override Position ToPos { get ; set ; }

        public override void Execute(Board board)
        {
            throw new NotImplementedException();
        }
    }
}

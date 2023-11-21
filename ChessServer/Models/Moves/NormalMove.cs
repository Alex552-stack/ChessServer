using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class NormalMove : Move
    {
        public NormalMove( Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
        }

        public override MoveType Type { get; } = MoveType.Normal;
        public override Position FromPos { get; set; }
        public override Position ToPos { get; set;}



        public override void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type{ get; }
        public abstract Position FromPos { get; set; }
        public abstract Position ToPos { get; set; }



        public override int GetHashCode()
        {
            return HashCode.Combine(Type, FromPos, ToPos);
        }

        public abstract void Execute(Board board);

        public int AbsoluteColumnDistance()
        {
            return Math.Abs(FromPos.Row-ToPos.Row);
        }


    }
}

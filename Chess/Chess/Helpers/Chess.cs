using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.Helpers
{
    public class Chess
    {
    }

    public class Name
    {
        public String rook { get { return "rook";  } }
        public String knight { get { return "knight"; } }
        public String bishop { get { return "bishop"; } }
        public String queen { get { return "queen"; } }
        public String king { get { return "king"; } }
        public String pawn { get { return "pawn"; } }
    }
    public class Properties
    {
        Name name = new Name();
        Color color = new Color(); 
    }
}
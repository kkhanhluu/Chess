using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.Helpers
{
    public class Chess
    {
    }

    public class Properties
    {
        public String name { get; set; }
        public String color { get; set; }
        public int row { get; set; }
        public int column { get; set; }
        public bool isStart { get; set; }
    }

    public class Piece
    {
        Properties prop = new Properties(); 
        public Properties rook {
            get
            {
                prop.name = "rook";
                return prop; 
            }
        }

        public Properties knight
        {
            get
            {
                prop.name = "knight";
                return prop; 
            }
        }

        public Properties bishop
        {
            get
            {
                prop.name = "bishop";
                return prop; 
            }
        }
        public Properties queen
        {
            get
            {
                prop.name = "queen";
                return prop;
            }
        }

        public Properties king
        {
            get
            {
                prop.name = "king";
                return prop;
            }
        }

        public Properties pawn
        {
            get
            {
                prop.name = "pawn";
                return prop;
            }
        }
    }

    public class Move
    {
        public int row { get; set; }
        public int column { get; set; }
        public String id { get; set; }
        public String target { get; set; }
    }
}
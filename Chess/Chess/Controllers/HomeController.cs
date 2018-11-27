using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chess.Models;
using Chess.Helpers; 

namespace Chess.Controllers
{
    public class HomeController : Controller
    {
        public static ChessModel chessModel = new ChessModel();
        public ActionResult Index()
        {
            chessModel.allPieces.Clear();
            int x1 = 2, x2 = 1;
            String color = "black"; 

            for (int i = 0; i < 2; i++)
            {
                // white pieces
                if (i == 1)
                {
                    x1 = 7;
                    x2 = 8;
                    color = "white"; 
                }
                for (int j = 1; j <= 8; j++)
                {
                    Piece piece = new Piece(); // pawn
                    piece.pawn.color = color;
                    piece.pawn.column = j;
                    piece.pawn.row = x1;
                    piece.pawn.id = j.ToString() + color + piece.pawn.name;
                    chessModel.allPieces.Add(piece.pawn);
                    switch(j)
                    {
                        case 1: // rook
                            piece = new Piece();
                            piece.rook.color = color;
                            piece.rook.column = j;
                            piece.rook.row = x2;
                            piece.rook.id = j.ToString() + color + piece.rook.name;
                            chessModel.allPieces.Add(piece.rook);
                            break;
                        case 2: // knight
                            piece = new Piece();
                            piece.knight.color = color;
                            piece.knight.column = j;
                            piece.knight.row = x2;
                            piece.knight.id = j.ToString() + color + piece.knight.name;
                            chessModel.allPieces.Add(piece.knight);
                            break;
                        case 3: // bishop
                            piece = new Piece();
                            piece.bishop.color = color;
                            piece.bishop.column = j;
                            piece.bishop.row = x2;
                            piece.bishop.id = j.ToString() + color + piece.bishop.name;
                            chessModel.allPieces.Add(piece.bishop);
                            break;
                        case 4: // queen
                            piece = new Piece();
                            piece.queen.color = color;
                            piece.queen.column = j;
                            piece.queen.row = x2;
                            piece.queen.id = j.ToString() + color + piece.queen.name;
                            chessModel.allPieces.Add(piece.queen);
                            break;
                        case 5: // king
                            piece = new Piece();
                            piece.king.color = color;
                            piece.king.column = j;
                            piece.king.row = x2;
                            piece.king.id = j.ToString() + color + piece.king.name;
                            chessModel.allPieces.Add(piece.king);
                            break;
                        case 6: // bishop
                            piece = new Piece();
                            piece.bishop.color = color;
                            piece.bishop.column = j;
                            piece.bishop.row = x2;
                            piece.bishop.id = j.ToString() + color + piece.bishop.name;
                            chessModel.allPieces.Add(piece.bishop);
                            break;
                        case 7: // knight
                            piece = new Piece();
                            piece.knight.color = color;
                            piece.knight.column = j;
                            piece.knight.row = x2;
                            piece.knight.id = j.ToString() + color + piece.knight.name;
                            chessModel.allPieces.Add(piece.knight);
                            break;
                        case 8: // rook
                            piece = new Piece();
                            piece.rook.color = color;
                            piece.rook.column = j;
                            piece.rook.row = x2;
                            piece.rook.id = j.ToString() + color + piece.rook.name;
                            chessModel.allPieces.Add(piece.rook);
                            break;
                    }
                }
            }
            return View(chessModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
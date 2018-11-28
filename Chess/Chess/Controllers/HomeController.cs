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
        public static int[,] board = new int[8, 8]; // 0: empty, 1: black, 2:white
        public ActionResult Index()
        {
            chessModel.allPieces.Clear();
            int x1 = 1, x2 = 0;
            String color = "black";
            int cell = 1;

            for (int i = 0; i < 2; i++)
            {
                // white pieces
                if (i == 1)
                {
                    x1 = 6;
                    x2 = 7;
                    color = "white";
                    cell = 2;
                }
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = new Piece(); // pawn
                    piece.pawn.color = color;
                    piece.pawn.column = j;
                    piece.pawn.row = x1;
                    piece.pawn.id = j.ToString() + color + piece.pawn.name;
                    piece.pawn.isStart = true;
                    board[x1, j] = cell;
                    chessModel.allPieces.Add(piece.pawn);
                    switch (j)
                    {
                        case 0: // rook
                            piece = new Piece();
                            piece.rook.color = color;
                            piece.rook.column = j;
                            piece.rook.row = x2;
                            piece.rook.id = j.ToString() + color + piece.rook.name;
                            chessModel.allPieces.Add(piece.rook);
                            board[x2, j] = cell;
                            break;
                        case 1: // knight
                            piece = new Piece();
                            piece.knight.color = color;
                            piece.knight.column = j;
                            piece.knight.row = x2;
                            piece.knight.id = j.ToString() + color + piece.knight.name;
                            chessModel.allPieces.Add(piece.knight);
                            board[x2, j] = cell;
                            break;
                        case 2: // bishop
                            piece = new Piece();
                            piece.bishop.color = color;
                            piece.bishop.column = j;
                            piece.bishop.row = x2;
                            piece.bishop.id = j.ToString() + color + piece.bishop.name;
                            chessModel.allPieces.Add(piece.bishop);
                            board[x2, j] = cell;
                            break;
                        case 3: // queen
                            piece = new Piece();
                            piece.queen.color = color;
                            piece.queen.column = j;
                            piece.queen.row = x2;
                            piece.queen.id = j.ToString() + color + piece.queen.name;
                            chessModel.allPieces.Add(piece.queen);
                            board[x2, j] = cell;
                            break;
                        case 4: // king
                            piece = new Piece();
                            piece.king.color = color;
                            piece.king.column = j;
                            piece.king.row = x2;
                            piece.king.id = j.ToString() + color + piece.king.name;
                            chessModel.allPieces.Add(piece.king);
                            board[x2, j] = cell;
                            break;
                        case 5: // bishop
                            piece = new Piece();
                            piece.bishop.color = color;
                            piece.bishop.column = j;
                            piece.bishop.row = x2;
                            piece.bishop.id = j.ToString() + color + piece.bishop.name;
                            chessModel.allPieces.Add(piece.bishop);
                            board[x2, j] = cell;
                            break;
                        case 6: // knight
                            piece = new Piece();
                            piece.knight.color = color;
                            piece.knight.column = j;
                            piece.knight.row = x2;
                            piece.knight.id = j.ToString() + color + piece.knight.name;
                            chessModel.allPieces.Add(piece.knight);
                            board[x2, j] = cell;
                            break;
                        case 7: // rook
                            piece = new Piece();
                            piece.rook.color = color;
                            piece.rook.column = j;
                            piece.rook.row = x2;
                            piece.rook.id = j.ToString() + color + piece.rook.name;
                            chessModel.allPieces.Add(piece.rook);
                            board[x2, j] = cell;
                            break;
                    }
                }
            }
            return View(chessModel);
        }

        public JsonResult Move(int targetRow, int targetColumn, String pieceId)
        {
            var piece = chessModel.allPieces.SingleOrDefault(x => x.id == pieceId);
            var currentRow = piece.row;
            var currentColumn = piece.column;
            if (piece == null)
            {
                return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
            }
            // current number in chess board
            int cell = piece.color == "black" ? 1 : 2;
            if (board[targetRow, targetColumn] == cell)
            {
                return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
            }
            switch (piece.name)
            {
                case "pawn":
                    // whte pawn 
                    if (piece.color == "white")
                    {
                        if (piece.isStart)
                        {
                            if (currentRow - targetRow <= 2 && targetColumn == currentColumn)
                            {
                                UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                                return Json(new { isMoveable = true, canDelete = false }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (targetRow - currentRow == -1 && targetColumn == currentColumn)
                        {
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            return Json(new { isMoveable = true, canDelete = false }, JsonRequestBehavior.AllowGet);
                        }
                        if (targetRow - currentRow == -1 && Math.Abs(targetColumn - currentColumn) == 1 && board[targetRow, targetColumn] == 1)
                        {
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                            return Json(new { isMoveable = true, canDelete = deletedPiece.id }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (piece.color == "black")
                    {
                        if (piece.isStart)
                        {
                            if (targetRow - currentRow <= 2 && targetColumn == currentColumn)
                            {
                                UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                                return Json(new { isMoveable = true, canDelete = false }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (targetRow - currentRow == 1 && targetColumn == currentColumn)
                        {
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            return Json(new { isMoveable = true, canDelete = false }, JsonRequestBehavior.AllowGet);
                        }
                        if (targetRow - currentRow == 1 && Math.Abs(targetColumn - currentColumn) == 1 && board[targetRow, targetColumn] == 2)
                        {
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                            return Json(new { isMoveable = true, canDelete = deletedPiece.id }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    break;
                case "knight":
                    if (((targetColumn - currentColumn) == 2 && (targetRow - currentRow) == 1)
                        || ((targetColumn - currentColumn) == 1 && (targetRow - currentRow) == 2)
                        || ((-targetColumn + currentColumn) == 1 && (targetRow - currentRow) == 2)
                        || ((-targetColumn + currentColumn) == 2 && (targetRow - currentRow) == 1)
                        || ((targetColumn - currentColumn) == 1 && (-targetRow + currentRow) == 2)
                        || ((targetColumn - currentColumn) == 2 && (-targetRow + currentRow) == 1)
                        || ((-targetColumn + currentColumn) == 1 && (-targetRow + currentRow) == 2)
                        || ((-targetColumn + currentColumn) == 2 && (-targetRow + currentRow) == 1))
                    {
                        if (board[targetRow, targetColumn] != 0 && board[targetRow, targetColumn] != board[currentRow, currentColumn])
                        {
                            Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            return Json(new { isMoveable = true, canDelete = deletedPiece.id }, JsonRequestBehavior.AllowGet);
                        }
                        UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                        return Json(new { isMoveable = true }, JsonRequestBehavior.AllowGet);
                    }
                    break;
                case "rook":
                    String[] rookResult = LogicForRook(targetColumn, targetRow, currentColumn, currentRow, piece, pieceId).Split(' ');
                    if (rookResult[0] == "false")
                    {
                        return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (rookResult.Length > 1)
                        {
                            return Json(new { isMoveable = true, canDelete = rookResult[1] }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { isMoveable = true }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    break;
                case "bishop":
                    String[] bisHopResult = LogicForBishop(targetColumn, targetRow, currentColumn, currentRow, piece, pieceId).Split(' ');
                    if (bisHopResult[0] == "false")
                    {
                        return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (bisHopResult.Length > 1)
                        {
                            return Json(new { isMoveable = true, canDelete = bisHopResult[1] }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { isMoveable = true }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    break;
                case "queen":
                    String a = LogicForRook(targetColumn, targetRow, currentColumn, currentRow, piece, pieceId);
                    if (a == "false")
                    {
                        a = LogicForBishop(targetColumn, targetRow, currentColumn, currentRow, piece, pieceId);
                    }
                    String[] queenResult = a.Split(' ');
                    if (queenResult[0] == "false")
                    {
                        return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (queenResult.Length > 1)
                        {
                            return Json(new { isMoveable = true, canDelete = queenResult[1] }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { isMoveable = true }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    break;
                case "king":
                    if (((targetColumn - currentColumn) == 1 && (targetRow - currentRow) == 0)
                        || ((targetColumn - currentColumn) == 1 && (targetRow - currentRow) == 1)
                        || ((-targetColumn + currentColumn) == 0 && (targetRow - currentRow) == 1)
                        || ((-targetColumn + currentColumn) == -1 && (targetRow - currentRow) == 1)
                        || ((targetColumn - currentColumn) == -1 && (-targetRow + currentRow) == 0)
                        || ((targetColumn - currentColumn) == -1 && (-targetRow + currentRow) == 1)
                        || ((-targetColumn + currentColumn) == 0 && (-targetRow + currentRow) == 1)
                        || ((-targetColumn + currentColumn) == 1 && (-targetRow + currentRow) == 1))
                    {
                        if (board[targetRow, targetColumn] != 0 && board[targetRow, targetColumn] != board[currentRow, currentColumn])
                        {
                            Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                            UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                            return Json(new { isMoveable = true, canDelete = deletedPiece.id }, JsonRequestBehavior.AllowGet);
                        }
                        UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                        return Json(new { isMoveable = true }, JsonRequestBehavior.AllowGet);
                    }
                    break;
            }
            return Json(new { isMoveable = false }, JsonRequestBehavior.AllowGet);
        }
        private static String LogicForBishop(int targetColumn, int targetRow, int currentColumn, int currentRow, Properties piece, String pieceId)
        {
            if (Math.Abs(targetColumn - currentColumn) == Math.Abs(targetRow - currentRow))
            {
                int stepSize = Math.Abs(targetColumn - currentColumn);
                if (targetColumn > currentColumn && targetRow < currentRow)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow - i, currentColumn + i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                else if (targetColumn < currentColumn && targetRow < currentRow)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow - i, currentColumn - i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                else if (targetColumn < currentColumn && targetRow > currentRow)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow + i, currentColumn - i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                else if (targetColumn > currentColumn && targetRow > currentRow)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow + i, currentColumn + i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                if (board[targetRow, targetColumn] != 0 && board[targetRow, targetColumn] != board[currentRow, currentColumn])
                {
                    Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                    UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                    return "true " + deletedPiece.id;
                }
                UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                return "true";
            }
            return "false";
        }
        private static String LogicForRook(int targetColumn, int targetRow, int currentColumn, int currentRow, Properties piece, String pieceId)
        {
            if (targetColumn != currentColumn && targetRow == currentRow)
            {
                int stepSize = Math.Abs(targetColumn - currentColumn);
                if (targetColumn > currentColumn)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow, currentColumn + i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                else
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow, currentColumn - i] != 0)
                        {
                            return "false";
                        }

                    }
                }
                if (board[targetRow, targetColumn] != 0 && board[targetRow, targetColumn] != board[currentRow, currentColumn])
                {
                    Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                    UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                    return "true " + deletedPiece.id;
                }
                UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                return "true";
            }
            else if (targetColumn == currentColumn && targetRow != currentRow)
            {
                int stepSize = Math.Abs(targetRow - currentRow);
                if (targetRow > currentRow)
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow + i, currentColumn] != 0)
                        {
                            return "false";
                        }

                    }
                }
                else
                {
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (board[currentRow - i, currentColumn] != 0)
                        {
                            return "false";
                        }

                    }
                }
                if (board[targetRow, targetColumn] != 0 && board[targetRow, targetColumn] != board[currentRow, currentColumn])
                {
                    Properties deletedPiece = FindDeletedPiece(targetRow, targetColumn, pieceId);
                    UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                    return "true " + deletedPiece.id;
                }
                UpdateBoard(targetRow, targetColumn, piece, currentRow, currentColumn);
                return "true";
            }
            return "false";
        }

        private static Properties FindDeletedPiece(int targetRow, int targetColumn, string pieceId)
        {
            var deletedPiece = chessModel.allPieces.Where(x => x.row == targetRow && x.column == targetColumn && x.id != pieceId).FirstOrDefault();
            chessModel.allPieces.Remove(deletedPiece);
            return deletedPiece;
        }

        private static void UpdateBoard(int targetRow, int targetColumn, Properties piece, int currentRow, int currentColumn)
        {
            piece.row = targetRow;
            piece.column = targetColumn;
            board[currentRow, currentColumn] = 0;
            board[targetRow, targetColumn] = piece.color == "black" ? 1 : 2;
            piece.isStart = false;
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
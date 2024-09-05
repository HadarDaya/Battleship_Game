using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BattleshipGame
{
    class Board
    {
        private Cell[,] board; //cell מטריצה מטיפוס  

        public Board(int x1, int y1)
        {
            board = new Cell[10, 10]; // הגדרת מטריצה
            int x = x1; //מאיפה מתחיל לצייר 
            int y = y1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Cell c = new Cell();
                    c.SetX(x);
                    c.SetY(y);
                    board[i, j] = c;
                    x = x + 50; //CELL כל פעם נתקדם ליצור את התא הבא, ע"י הוספת הרוחב של ציור התא לפי מה שהגדרנו  
                }
                y = y + 50; // כל פעם נתקדם ליצור את התא הבא, ע"י הוספת האורך של ציור התא
                x = x1; // איפוס מחדש מאיפה יתחיל 
            }
        }

        // פעולה המקבלת משתנה גרפי ומציירת את לוח השחקן
        public void PaintBoard(Graphics g)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j].PaintCell(g);
            }
            // ציור הצוללת
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    if (board[i, j].GetHeadSubmarine() != null)
                        // אם אין צוללת במשבצת הנל, ניתן לצייר את הצוללת
                        board[i, j].PaintSub(g);
            }
        }

        // פעולה המקבלת  משתנה גרפי ומציירת את לוח המחשב
        public void PaintComBoard(Graphics g)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j].PaintCell(g);
            }
            // ציור הצוללת
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (board[i, j].GetHeadSubmarine() != null)
                        // אם אין צוללת במשבצת הנל, ניתן לצייר את הצוללת
                        board[i, j].PaintSub(g); 
        }

        // פעולה המחזירה את מספר המשבצות שפוצצו בלוח הנוכחי
        public int ExplodedCount()
        {
            int count = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // אם המשבצת הנוכחית בלוח פוצצה, נעלה את המונה
                    if (board[i, j].GetStatus() == 2) 
                        count++;
                }
            }
            return count;
        }

        public Cell[,] GetBoard()
        {
            return this.board;
        }
        
        // פעולה המעדכנת את לוח השחקן
        // הפעולה מזיזה את לוח השחקן שמאלה כאשר עוברים לדף המשחק שלאחר הצבת צוללות השחקן
        public void SetPlayerBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j].SetX(board[i, j].GetX() - 250); 
                //מכיוון שרק הוא יזוז שמאלה X מעדכנים רק את ציר ה
                // לא משתנה בהזזה Yציר ה      
            }
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BattleshipGame
{
    class Cell
    {
        private int x; // הקואורדינטות של התא 
        private int y;
        private bool isShip;
        //- תוכן המשבצת
        // ממוקמת בתא צוללת -true 
        //לא ממוקמת בתא צוללת false
        private int status;
        //  - אופי המשבצת מבחינת פגיעה  
        // טרם ניסו לפגוע במשבצת-0 
        // פגעו במשבצת ולא הצליחו-1
        // פגעו במשבצת וכן הצליחו לפגוע-2
        private Submarine s; // יהיה שונה מNULL כאשר מדובר בראש הצוללת
        private int isBetween; 
        // משתנה המסמל האם המשבצת צמודה לצוללת
        //  המשבצת לא צמודה לצוללת -0
        //  המשבצת צמודה לצוללת -9 

        // פעולה בונה 
        public Cell()
        {
            this.x = 0;
            this.y = 0;
            this.isShip = false;
            this.status = 0;
            this.s = null; // צוללת שבינתיים ריקה
            this.isBetween = 0;
        }


        public void SetX(int x)
        {
            this.x = x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public void SetIsShip(bool isShip)
        {
            this.isShip = isShip;
        }

        public void SetStatus(int status)
        {
            this.status = status;
        }

        public void SetSubmarine(Submarine s)
        {
            this.s = s;
        }

        public void SetIsBetween(int isBetween)
        {
            this.isBetween = isBetween;
        }

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

        public bool GetIsShip()
        {
            return this.isShip;
        }

        public int GetStatus()
        {
            return this.status;
        }

        public Submarine GetHeadSubmarine()
        {
            return this.s;
        }

        public int GetIsBetween()
        {
            return this.isBetween;
        }

        // פעולה המקבלת משתנה גרפי ומציירת את התא
        public void PaintCell(Graphics g)
        {
            Pen pen = new Pen(Color.AliceBlue, 7);
            g.DrawRectangle(pen, this.x, this.y, 50, 50);
            Point p = new Point(this.x, this.y);
            switch (this.status)
            {
                case 1: Image pic1 = Image.FromFile("X.png");
                    g.DrawImage(pic1, p);
                    break;
                case 2: Image pic2 = Image.FromFile("Fire.png");
                    g.DrawImage(pic2, p);
                    break;
            }

        }

        public void PaintSub(Graphics g)
        {
            Point p = new Point(this.x, this.y);
            s.PaintSubmarine(g, p);
        }  
        
    }
}

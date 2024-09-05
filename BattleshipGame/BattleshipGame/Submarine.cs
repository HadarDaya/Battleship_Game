using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace BattleshipGame
{
    class Submarine
    {
        private int num; //מספר המשבצות שהצוללת תופסת      
        private bool standingY;
        //איך הצוללת ממוקמת 
        // הצוללת מונחת לאורך-true
        // הצוללת מונחת לרוחב- false

        public Submarine(int size)
        {
            this.num = size;
            this.standingY = false;
        }

        // פעולה המקבלת משתנה גרפי ונקודה
        // הפעולה מציירת צוללת, בהתאם למנח
        public void PaintSubmarine(Graphics g, Point p)
        {
            Image pic = Image.FromFile("ship" + num + ".png");
            if (this.standingY == true) // אם הצוללת ממוקמת לאורך 
                pic = Image.FromFile("shipV" + num + ".png");
            g.DrawImage(pic, p);
        }

        public void SetNum(int num)
        {
            this.num = num;
        }

        public void SetStandingY(bool standing)
        {
            this.standingY = standing;
        }

        public int GetNum()
        {
            return this.num;
        }

        public bool GetStandingY()
        {
            return this.standingY;
        }

    }
}

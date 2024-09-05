using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipGame
{
    public partial class StartGame : Form
    {
        Graphics g;
        Board game;
        Board computer;
        Submarine s; // משתנה עבור הצוללת הדומינטטית של השחקן
        Submarine sComputer; // משתנה עבור הצוללת הדומינטטית של המחשב
        int size = 0;//גודל הצוללת שנבחרה
        int countButtles = 0; // משתנה המונה את מספר הצוללות שסודרו בלוח
        bool sw = false;
        //משתנה הבודק האם סיימנו להניח את הצוללות 
        // "כאשר סיימנו להניח, רק אחרי לחיצה על התחל משחק הערך יהיה "אמת
        Random rnd = new Random();
        int subBlow = 0;
        int subComBlow = 0;
        // שמירת המיקום האחרון שבו המחשב הצליח לפגוע
        int i_comp_last_bomb = -1;
        int j_comp_last_bomb = -1;
        int count_comp_bomb = 0; // כמה משבצות פוצצו בצוללת הנוכחית
        bool comp_last_standingY = false;
        public StartGame()
        {
            InitializeComponent();
        }

        private void StartGame_Load(object sender, EventArgs e)
        {
            int x = this.Width / 4;
            int y = this.Height / 6;
            game = new Board(x, y);
            s = new Submarine(-1);
        }

        private void StartGame_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (sw == true) // אם סיימנו להניח את כל הצוללות
            {
                computer.PaintComBoard(g); //נצייר את לוח המחשב  
                computer.PaintBoard(g);

            }
            game.PaintBoard(g); //נצייר את לוח השחקן
        }

        // :פעולה המעלימה במסך המשחק הבא את הכפתורים  
        // כפתור סיבוב צוללת וכפתור לדף הבא
        public void HideButtons()
        {
            button1.Visible = false;
            button2.Visible = false;
        }

        // כפתור חזרה לדף הבית
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            if (countButtles == 4) // אם סיימנו להניח את כל הצוללות
            {
                //תחילת המשחק                               
                sw = true;
                computer = new Board(700, 124); // יצירת לוח המחשב
                game.SetPlayerBoard(); // הזזת לוח השחקו שמאלה 
                ComputerButtles(); // הגרלת מיקומי הצוללות של המחשב 
                HideButtons(); // העלמת הכפתורים 
                pictureBox9.Visible = false; // העלמת הוראות הצבת הצוללות
                Refresh(); //  העלמת המטריצה של הצבת הצוללות של השחקן 
                MessageBox.Show("You start the game");
            }
            else
                MessageBox.Show("The game can not be started because you have not finished placing all the submarines in the board");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.size = 5;
            s = new Submarine(this.size);
            s.SetStandingY(false);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.size = 5;
            s = new Submarine(this.size);
            s.SetStandingY(true);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.size = 4;
            s = new Submarine(this.size);
            s.SetStandingY(false);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.size = 4;
            s = new Submarine(this.size);
            s.SetStandingY(true);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.size = 3;
            s = new Submarine(this.size);
            s.SetStandingY(false);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.size = 3;
            s = new Submarine(this.size);
            s.SetStandingY(true);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.size = 2;
            s = new Submarine(this.size);
            s.SetStandingY(false);
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.size = 2;
            s = new Submarine(this.size);
            s.SetStandingY(true);
        }

        private void StartGame_MouseClick(object sender, MouseEventArgs e)
        {
            g = CreateGraphics();
            if (countButtles == 4) // אם מיקמנו את כל הצוללות, ניתן להתחיל במשחק
            {
                if (subBlow == 13) // ברגע שפוצצנו את כל הצוללות של היריב, ניצחנו
                {
                    MessageBox.Show("You are the WINNER!!");
                    WinnerScreen a = new WinnerScreen();
                    a.Show();
                    this.Close();
                }
                else
                {
                    int x = e.X;
                    int y = e.Y;
                    if (x >= computer.GetBoard()[0, 0].GetX() && x <= computer.GetBoard()[0, 9].GetX() + 500) // זיהוי שמיקום הצוללת הרצוי הוא בתחומי המטריצה
                        if (y >= computer.GetBoard()[0, 0].GetY() && y <= computer.GetBoard()[9, 9].GetY() + 500)
                        {
                            //מציאת שורה ועמודה במטריצה
                            int j = (x - computer.GetBoard()[0, 0].GetX()) / 50;
                            int i = (y - computer.GetBoard()[0, 0].GetY()) / 50;
                            if (computer.GetBoard()[i, j].GetStatus() == 0) // אם לא ניסו לפגוע במשבצת הנוכחית
                            {
                                if (computer.GetBoard()[i, j].GetIsShip() == true) // המשבצת הנוכחית עם צוללת 
                                {
                                    computer.GetBoard()[i, j].SetStatus(2); // עדכון סטטוס המשבצת שהצליחו לפגוע בצוללת
                                    //computer.PaintComBoard(g); // ציור 
                                    subBlow++; // מספר המשבצות שפוצצו והיה בהן חלק מצוללת
                                    // מציאת ראש הצוללת
                                    while (computer.GetBoard()[i, j].GetIsShip() == true
                                                        && computer.GetBoard()[i, j].GetHeadSubmarine() == null)
                                    {
                                        // האם יש ללכת למעלה כדי להגיע לראש הצוללת-כאשר היא מונחת במאונך
                                        if (i > 0 && computer.GetBoard()[i - 1, j].GetIsShip() == true)
                                            i = i - 1;
                                        //  האם יש ללכת שמאלה כדי להגיע לראש הצוללת-כאשר היא מונחת במאוזן
                                        else if (j > 0 && computer.GetBoard()[i, j - 1].GetIsShip() == true)
                                            j = j - 1;
                                    }
                                    // בשלב הזה יש ביד את המיקום של ראש הצוללת
                                    s = computer.GetBoard()[i, j].GetHeadSubmarine();

                                    if (CheckAllSub(i, j, computer)) // מי שתמיד יישלח זה המיקום של ראש הצוללת הנוכחית
                                    {
                                        MessageBox.Show("Congratulations, the whole buttle was blown up!");
                                        CellUpdate(i, j, computer); // איקסים
                                    }
                                    computer.PaintComBoard(g);
                                }
                                else // המשבצת הנוכחית לא הייתה עם צוללת
                                {
                                    computer.GetBoard()[i, j].SetStatus(1);// עדכון סטטוס המשבצת שלא הצליחו לפגוע בצוללת
                                    computer.PaintComBoard(g);
                                    MessageBox.Show("computer turn");
                                    Comp();//התור עובר למחשב
                                }
                            }

                            else
                                MessageBox.Show("This cell can not be blown");
                        }
                }
            }
            else
            {               
                //האם הלחיצה בוצעה בתחומי המטריצה
                int x = e.X;
                int y = e.Y;
                bool isShip = false; // משתנה בוליאני המסמל האם קיימת במשבצת הנוכחית צוללת
                if (x >= game.GetBoard()[0, 0].GetX() && x <= game.GetBoard()[0, 9].GetX() + 500) // זיהוי שמיקום הצוללת הרצוי הוא בתחומי המטריצה
                    if (y >= game.GetBoard()[0, 0].GetY() && y <= game.GetBoard()[9, 9].GetY() + 500)
                    {
                        //מציאת שורה ועמודה במטריצה
                        int j = (x - game.GetBoard()[0, 0].GetX()) / 50;
                        int i = (y - game.GetBoard()[0, 0].GetY()) / 50;
                        // אם הצוללת אמורה לשבת לאורך
                        if (s != null && s.GetStandingY() == true)
                        {
                            // בדיקות
                            // נרוץ על כל האורך שלה ונראה שכל התאים ריקים ושאין צוללת צמודה על מנת שנוכל להוסיף את הצוללת
                            int temp_i = i;
                            for (int count = 1; count <= this.size && (isShip == false) && i + this.size - 1 <= 9; count++)
                            {
                                if (game.GetBoard()[temp_i, j].GetIsShip() == true || game.GetBoard()[temp_i, j].GetIsBetween() == 9)
                                    isShip = true;                    
                                temp_i++; // מתקדמים בשורות
                            }
                            // עדכונים
                            // המקום שבו רוצים להניח את הצוללת אינו חורג
                            if (i + this.size - 1 <= 9 && (isShip == false))
                            {
                                // עדכון של התא הראשון עליו מופיעה הצוללת
                                game.GetBoard()[i, j].SetIsShip(true);
                                game.GetBoard()[i, j].SetSubmarine(s);
                                // עדכון אחד למעלה
                                if (i >= 1)
                                {
                                    game.GetBoard()[i - 1, j].SetIsBetween(9);
                                    if (j >= 1)
                                        game.GetBoard()[i - 1, j - 1].SetIsBetween(9);
                                    if (j <= 8)
                                        game.GetBoard()[i - 1, j + 1].SetIsBetween(9);
                                }
                                // עדכון של שאר התאים לאורך הסירה
                                for (int count = 1; count <= this.size; count++)
                                {
                                    game.GetBoard()[i, j].SetIsShip(true);// ניתן להניח במשבצת זו צוללת, ודרוש עדכון
                                    if (j >= 1)
                                        game.GetBoard()[i, j - 1].SetIsBetween(9);
                                    if (j <= 8)
                                        game.GetBoard()[i, j + 1].SetIsBetween(9);
                                    i++;
                                }
                                // עדכון אחד למטה
                                if (i <= 9)
                                {
                                    game.GetBoard()[i, j].SetIsBetween(9);
                                    if (j >= 1)
                                        game.GetBoard()[i, j - 1].SetIsBetween(9);
                                    if (j <= 8)
                                        game.GetBoard()[i, j + 1].SetIsBetween(9);
                                }
                                // לצייר את הצוללת 
                                game.PaintBoard(g);
                                // להעלים אותה כדי שלא יוכלו להניחה שוב
                                ShipAlreadyInUse(s.GetNum());
                                s = null;
                                countButtles++; // מעלים את מונה מספר הצוללות שסודרו בלוח
                            }
                            else
                                MessageBox.Show("The submarine can not be placed in this position");
                        }
                        // אם הצוללת אמורה לשבת לרוחב
                        else if (s != null && s.GetStandingY() == false)
                        {
                            // בדיקות
                            // נרוץ על כל האורך שלה ושאין צוללת צמודה ונראה שכל התאים ריקים
                            int temp_j = j;
                            for (int count = 1; count <= this.size && (isShip == false) && j + this.size - 1 <= 9; count++)
                            {
                                if (game.GetBoard()[i, temp_j].GetIsShip() == true || game.GetBoard()[i, temp_j].GetIsBetween() == 9)
                                    isShip = true;
                                temp_j++; // מתקדמים בעמודות
                            }
                            // עדכונים
                            // המקום שבו רוצים להניח את הצוללת אינו חורג
                            if (j + this.size - 1 <= 9 && (isShip == false))
                            {

                                // עדכון של התא הראשון עליו מופיעה הצוללת
                                game.GetBoard()[i, j].SetIsShip(true);
                                game.GetBoard()[i, j].SetSubmarine(s);
                                // עדכון אחד שמאלה
                                if (j >= 1)
                                {
                                    game.GetBoard()[i, j - 1].SetIsBetween(9);
                                    if (i >= 1)
                                        game.GetBoard()[i - 1, j - 1].SetIsBetween(9);
                                    if (i <= 8)
                                        game.GetBoard()[i + 1, j - 1].SetIsBetween(9);
                                }
                                // עדכון של שאר התאים לרוחב הצוללת
                                for (int count = 1; count <= this.size; count++)
                                {
                                    game.GetBoard()[i, j].SetIsShip(true);// ניתן להניח במשבצת זו צוללת, ודרוש עדכון
                                    if (i >= 1)
                                        game.GetBoard()[i - 1, j].SetIsBetween(9);
                                    if (i <= 8)
                                        game.GetBoard()[i + 1, j].SetIsBetween(9);
                                    j++;
                                }
                                // עדכון אחד ימינה
                                if (j <= 9)
                                {
                                    game.GetBoard()[i, j].SetIsBetween(9);
                                    if (i >= 1)
                                        game.GetBoard()[i - 1, j].SetIsBetween(9);
                                    if (i <= 8)
                                        game.GetBoard()[i + 1, j].SetIsBetween(9);
                                }
                                // לצייר את הצוללת 
                                game.PaintBoard(g);
                                // להעלים אותה כדי שלא יוכלו להניחה שוב
                                ShipAlreadyInUse(s.GetNum());
                                s = null;
                                countButtles++; // מעלים את מונה מספר הצוללות שסודרו בלוח
                            }
                            else
                                MessageBox.Show("The submarine can not be placed in this position");
                        }

                    }
            }
        }

        // פעולה המשנה את מנח הצוללת הנוכחית, כאשר לוחצים על כפתור הסיבוב
        private void button2_Click(object sender, EventArgs e)
        {
            // נבדוק שיש צוללת שלחצנו עליה, כדי למנוע מצב שבו מנסים להכניס את אותה הצוללת שוב
            // משנים לnull אחרי ששמנו אותה
            if (s != null && s.GetNum() == 5)
            {
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                if (s.GetStandingY() == false)
                {
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                    s = new Submarine(5);
                    s.SetStandingY(true);
                }
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                else
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    s = new Submarine(5);
                    s.SetStandingY(false);
                }
            }
            if (s != null && s.GetNum() == 4)
            {
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                if (s.GetStandingY() == false)
                {
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                    s = new Submarine(4);
                    s.SetStandingY(true);
                }
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                else
                {
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = false;
                    s = new Submarine(4);
                    s.SetStandingY(false);
                }
            }
            if (s != null && s.GetNum() == 3)
            {
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                if (s.GetStandingY() == false)
                {
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    s = new Submarine(3);
                    s.SetStandingY(true);
                }
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                else
                {
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                    s = new Submarine(3);
                    s.SetStandingY(false);
                }
            }
            if (s != null && s.GetNum() == 2)
            {
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                if (s.GetStandingY() == false)
                {
                    pictureBox7.Visible = false;
                    pictureBox8.Visible = true;
                    s = new Submarine(2);
                    s.SetStandingY(true);
                }
                // אם הסטטוס האחרון שלה היה לרוחב, השינוי יגרום לה להיות לאורך
                else
                {
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = false;
                    s = new Submarine(2);
                    s.SetStandingY(false);
                }
            }

        }

        //פעולה המקבלת גודל של צוללת
        // הפעולה מעלימה את הצוללת בהתאם לגודלה על מנת שלא יוכלו להניחה שוב 
        public void ShipAlreadyInUse(int size)
        {
            if(size == 5)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }
            if (size == 4)
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
            }
            if (size == 3)
            {
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
            }
            if (size == 2)
            {
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
            }
        }

        // פעולה המגרילה מיקום צוללות ומעדכנת את לוח המחשב 
        private void ComputerButtles()
        {
            int buttleNum = 5; // משתנה המסמל את אורך הצוללת
            Random rnd = new Random();
            while(buttleNum>=2) // עד שהמחשב סיים למקם את כל ארבעת הצוללות
            {
                bool isShip = false; // משתנה בוליאני המסמל האם קיימת במשבצת הנוכחית צוללת 
                sComputer = new Submarine(buttleNum); // ****** רק לבדיקת המשחק ******
                int standingY = rnd.Next(2) + 1;//האם הצוללת תעמוד באופן מאוזן או מאונך
                                                // 1- הצוללת תמוקם לאורך
                                                // 2- הצוללת תמוקם לרוחב
                int i = rnd.Next(10);
                int j = rnd.Next(10);
                // אם הצוללת אמורה לשבת לאורך
                if (standingY == 1)
                {
                    sComputer.SetStandingY(true); // ****** רק לבדיקת המשחק ******
                    // בדיקות
                    // נרוץ על כל האורך שלה ונראה שכל התאים ריקים ושאין צוללת צמודה על מנת שנוכל להוסיף את הצוללת
                    int temp_i = i;
                    for (int count = 1; count <= buttleNum && (isShip == false) && i + buttleNum - 1 <= 9; count++)
                    {
                        if (computer.GetBoard()[temp_i, j].GetIsShip() == true || computer.GetBoard()[temp_i, j].GetIsBetween() == 9)
                            isShip = true;
                        temp_i++; // מתקדמים בשורות
                    }
                    // עדכונים
                    // המקום שבו רוצים להניח את הצוללת אינו חורג
                    if (i + buttleNum - 1 <= 9 && (isShip == false))
                    {
                        // עדכון של התא הראשון עליו מופיעה הצוללת
                        computer.GetBoard()[i, j].SetIsShip(true);
                        computer.GetBoard()[i, j].SetSubmarine(sComputer); // ****** רק לבדיקת המשחק ******
                        // עדכון אחד למעלה
                        if (i >= 1)
                        {
                            computer.GetBoard()[i - 1, j].SetIsBetween(9);
                            if (j >= 1)
                                computer.GetBoard()[i - 1, j - 1].SetIsBetween(9);
                            if (j <= 8)
                                computer.GetBoard()[i - 1, j + 1].SetIsBetween(9);
                        }
                        // עדכון של שאר התאים לאורך הסירה
                        for (int count = 1; count <= buttleNum; count++)
                        {
                            computer.GetBoard()[i, j].SetIsShip(true);// ניתן להניח במשבצת זו צוללת, ודרוש עדכון
                            if (j >= 1)
                                computer.GetBoard()[i, j - 1].SetIsBetween(9);
                            if (j <= 8)
                                computer.GetBoard()[i, j + 1].SetIsBetween(9);
                            i++;
                        }
                        // עדכון אחד למטה
                        if (i <= 9)
                        {
                            computer.GetBoard()[i, j].SetIsBetween(9);
                            if (j >= 1)
                                computer.GetBoard()[i, j - 1].SetIsBetween(9);
                            if (j <= 8)
                                computer.GetBoard()[i, j + 1].SetIsBetween(9);
                        }
                        // לצייר את הצוללת 
                        computer.PaintBoard(g); // ****** רק לבדיקת המשחק ******
                        buttleNum--;
                    }
                }
                // אם הצוללת אמורה לשבת לרוחב
                else if (standingY == 2)
                {
                    sComputer.SetStandingY(false); // ****** רק לבדיקת המשחק ******
                    // בדיקות
                    // נרוץ על כל האורך שלה ושאין צוללת צמודה ונראה שכל התאים ריקים
                    int temp_j = j;
                    for (int count = 1; count <= buttleNum && (isShip == false) && j + buttleNum - 1 <= 9; count++)
                    {
                        if (computer.GetBoard()[i, temp_j].GetIsShip() == true || computer.GetBoard()[i, temp_j].GetIsBetween() == 9)
                            isShip = true;
                        temp_j++; // מתקדמים בעמודות
                    }
                    // עדכונים
                    // המקום שבו רוצים להניח את הצוללת אינו חורג
                    if (j + buttleNum - 1 <= 9 && (isShip == false))
                    {

                        // עדכון של התא הראשון עליו מופיעה הצוללת
                        computer.GetBoard()[i, j].SetIsShip(true);
                        computer.GetBoard()[i, j].SetSubmarine(sComputer); // ****** רק לבדיקת המשחק ******
                        // עדכון אחד שמאלה
                        if (j >= 1)
                        {
                            computer.GetBoard()[i, j - 1].SetIsBetween(9);
                            if (i >= 1)
                                computer.GetBoard()[i - 1, j - 1].SetIsBetween(9);
                            if (i <= 8)
                                computer.GetBoard()[i + 1, j - 1].SetIsBetween(9);
                        }
                        // עדכון של שאר התאים לרוחב הצוללת
                        for (int count = 1; count <= buttleNum; count++)
                        {
                            computer.GetBoard()[i, j].SetIsShip(true);// ניתן להניח במשבצת זו צוללת, ודרוש עדכון
                            if (i >= 1)
                                computer.GetBoard()[i - 1, j].SetIsBetween(9);
                            if (i <= 8)
                                computer.GetBoard()[i + 1, j].SetIsBetween(9);
                            j++;
                        }
                        // עדכון אחד ימינה
                        if (j <= 9)
                        {
                            computer.GetBoard()[i, j].SetIsBetween(9);
                            if (i >= 1)
                                computer.GetBoard()[i - 1, j].SetIsBetween(9);
                            if (i <= 8)
                                computer.GetBoard()[i + 1, j].SetIsBetween(9);
                        }
                        // לצייר את הצוללת 
                        computer.PaintBoard(g); // ****** רק לבדיקת המשחק ******
                        buttleNum--;
                    }
                }
            }


            
        }

        //  פעולה המפעילה את מהלך המשחק של המחשב
       public void Comp()
        {
            //המחשב משחק
            if (subComBlow == 13)
            {
                MessageBox.Show("GAME OVER");
                this.Close();
            }
            else
                CompEasy();
            
        }

        //***************************************************************************

        // מהרגע שהמחשב עלה בצורה רנדומלית על מיקום עם צוללת-
        // הוא יתעקש למצוא את כל הצוללת ולפוצץ אותה
        // ברגע שסיים לפוצץ- יחזור להיות רנדומלי
        public void CompEasy()
        {
            g = CreateGraphics();
            bool sw = true; //התור של המחשב 
            int i = -1, j = -1;
            while (sw == true) // כל עוד המחשב עדיין פוגע במשבצת עם צוללת
            {
                // ********************** המשך פיצוץ צוללת **********************
                // האם יש לי מיקום אחרון שפוצצתי- אם כן- נחפש ממנו את הסמוכים לו
                if (i_comp_last_bomb != -1 && j_comp_last_bomb != -1)
                {
                    if (count_comp_bomb < 2) // עדיין לא ידוע המנח של הצוללת לכן נמשיך לבדוק את כל האופציות הצמודות
                    {
                        if (i_comp_last_bomb + 1 <= 9 &&
                            game.GetBoard()[i_comp_last_bomb + 1, j_comp_last_bomb].GetStatus() == 0) // האם חוקי לפוצץ תא בשורה למטה
                        {
                            i = i_comp_last_bomb + 1;
                            j = j_comp_last_bomb;
                        }
                        else if (i_comp_last_bomb - 1 >= 0 &&
                            game.GetBoard()[i_comp_last_bomb - 1, j_comp_last_bomb].GetStatus() == 0) // האם חוקי לפוצץ תא בשורה למעלה
                        {
                            i = i_comp_last_bomb - 1;
                            j = j_comp_last_bomb;
                        }
                        else if (j_comp_last_bomb + 1 <= 9 &&
                            game.GetBoard()[i_comp_last_bomb, j_comp_last_bomb + 1].GetStatus() == 0) // האם חוקי לפוצץ תא בעמודה ימינה
                        {
                            j = j_comp_last_bomb + 1;
                            i = i_comp_last_bomb;
                        }
                        else if (j_comp_last_bomb - 1 >= 0 &&
                            game.GetBoard()[i_comp_last_bomb, j_comp_last_bomb - 1].GetStatus() == 0) // האם חוקי לפוצץ תא בעמודה שמאלה
                        {
                            j = j_comp_last_bomb - 1;
                            i = i_comp_last_bomb;
                        }
                    }
                    else // אחרת- אפשר להבין כבר מה המנח של הצוללת
                    {
                        // אם שני המקומות שפוצצו הם באותו טור
                        if ((i_comp_last_bomb + 1 <= 9 && game.GetBoard()[i_comp_last_bomb + 1, j_comp_last_bomb].GetStatus() == 2)
                            || (i_comp_last_bomb - 1 >= 0 && game.GetBoard()[i_comp_last_bomb - 1, j_comp_last_bomb].GetStatus() == 2))
                            comp_last_standingY = true; // אז הסירה עומדת במאונך
                        // אם שני המקומות שפוצצו הם באותה שורה
                        else if ((j_comp_last_bomb + 1 <= 9 && game.GetBoard()[i_comp_last_bomb, j_comp_last_bomb + 1].GetStatus() == 2)
                            || (j_comp_last_bomb - 1 >= 0 && game.GetBoard()[i_comp_last_bomb, j_comp_last_bomb - 1].GetStatus() == 2))
                            comp_last_standingY = false; // אז הסירה עומדת במאוזן

                        // משתנים שבהמשך יכילו את קצוות הפיצוצים
                        int i_edge = i_comp_last_bomb;
                        int j_edge = j_comp_last_bomb;
                        bool tryOtherEdge = false; // ללכת בכיוון ההפוך מאיפה שחיפשנו 
                        // אם גילינו שהצוללת יושבת במאונך
                        if (comp_last_standingY == true)
                        {
                            // נלך לשני הקצוות ונמשיך לבדוק משם
                            // קצה תחתון -המשבצת הכי תחתונה שבה לא ניסינו לפגוע
                            while (i_edge + 1 <= 9 &&
                                    game.GetBoard()[i_edge, j_comp_last_bomb].GetStatus() == 2)
                            {
                                i_edge = i_edge + 1;
                            }
                            // עצרתי במישהו שהוא לא פיצוץ
                            // אם היה ניסיון לפגוע בו והוא לא הצליח
                            if (game.GetBoard()[i_edge, j_comp_last_bomb].GetStatus() == 1)
                                tryOtherEdge = true;
                            else
                                tryOtherEdge = false;

                            // אם התברר שהמשבצת האחרונה שבה פגענו היא הקצה העליון- נלך לקצה התחתון
                            if (tryOtherEdge == true)
                            {
                                i_edge = i_comp_last_bomb;
                                // קצה עליון -המשבצת הכי עליונה שבה לא ניסינו לפגוע
                                while (i_edge - 1 >= 0 &&
                                    game.GetBoard()[i_edge, j_comp_last_bomb].GetStatus() == 2)
                                {
                                    i_edge = i_edge - 1;
                                }
                            }
                        }
                        // אם גילינו שהצוללת יושבת במאוזן
                        else
                        {
                            // נלך לשני הקצוות ונמשיך לבדוק משם
                            // קצה ימני -המשבצת הכי ימנית שבה לא ניסינו לפגוע
                            while (j_edge + 1 <= 9 &&
                                    game.GetBoard()[i_comp_last_bomb, j_edge].GetStatus() == 2)
                            {
                                    j_edge = j_edge + 1;
                            }

                            // עצרתי במישהו שהוא לא פיצוץ
                            // אם היה ניסיון לפגוע בו והוא לא הצליח
                            if (game.GetBoard()[i_comp_last_bomb, j_edge].GetStatus() == 1)
                                tryOtherEdge = true;
                            else
                                tryOtherEdge = false;

                            // אם התברר שהמשבצת האחרונה שבה פגענו היא הקצה העליון- נלך לקצה התחתון
                            if (tryOtherEdge == true)
                            {
                                j_edge = j_comp_last_bomb;
                                // קצה שמאלי -המשבצת הכי שמאלית שבה לא ניסינו לפגוע
                                while (j_edge - 1 >= 0 &&
                                        game.GetBoard()[i_comp_last_bomb, j_edge].GetStatus() == 2)
                                {
                                    j_edge = j_edge - 1;
                                }
                            }
                        }
                        i = i_edge;
                        j = j_edge;
                    }
                }
                // ********************** ניסיון חיפוש צוללת בצורה רנדומלית **********************
                else
                {
                    i = rnd.Next(10);
                    j = rnd.Next(10);
                    while (game.GetBoard()[i, j].GetStatus() != 0) // כל עוד לא הגיע למשבצת שלא ניסו לפגוע בה ימשיך להגריל מיקום
                    {
                        i = rnd.Next(10);
                        j = rnd.Next(10);
                    }
                }

                // ********************** בדיקת המיקומים שנבחרו **********************
                if (game.GetBoard()[i, j].GetIsShip() == true) // יש צוללת במשבצת הנוכחית
                {
                    //מסמנים את התא ובודקים אם חיסלנו את הצוללת
                    game.GetBoard()[i, j].SetStatus(2); // עדכון המשבצת שפגעו בצוללת
                    subComBlow++; // מספר המשבצות שפוצצו והיה בהן חלק מצוללת
                    // שמירת המיקום שבו פגענו
                    i_comp_last_bomb = i;
                    j_comp_last_bomb = j;
                    // מציאת ראש הצוללת
                    while (game.GetBoard()[i, j].GetIsShip() == true
                                    && game.GetBoard()[i, j].GetHeadSubmarine() == null)
                    {
                        // האם יש ללכת למעלה כדי להגיע לראש הצוללת-כאשר היא מונחת במאונך
                        if (i > 0 && game.GetBoard()[i - 1, j].GetIsShip() == true)
                            i = i - 1;
                        //  האם יש ללכת שמאלה כדי להגיע לראש הצוללת-כאשר היא מונחת במאוזן
                        else if (j > 0 && game.GetBoard()[i, j - 1].GetIsShip() == true)
                            j = j - 1;
                    }
                    // בשלב הזה יש ביד את המיקום של ראש הצוללת
                    s = game.GetBoard()[i, j].GetHeadSubmarine();

                    // אם כל הצוללת פוצצה
                    if (CheckAllSub(i, j, game)) // מי שתמיד יישלח זה המיקום של ראש הצוללת הנוכחית
                    {
                        CellUpdate(i, j, game); // איקסים
                                                // לא נדרשת התעקשות על מציאת שאר הספינה כי היא כולה פוצצה
                        i_comp_last_bomb = -1;
                        j_comp_last_bomb = -1;
                        count_comp_bomb = 0;
                    }
                    // אם לא כל הצוללת פוצצה- יעלה את המונה שסופר את מספר החלקים שפוצצו ממנה
                    // חשוב לדעת כמה חלקים פוצצו כי 2 ומעלה יגידו לנו את המנח של הצוללת
                    else
                        count_comp_bomb++;
                    game.PaintBoard(g);
                }
                else
                {
                    game.GetBoard()[i, j].SetStatus(1);//עדכון משבצת אין פגיעה
                    game.PaintBoard(g);
                    sw = false;
                    MessageBox.Show("your turn");

                }
            }
        }

        // פעולה המקבלת את מיקום ראש הצוללת ולוח
         // הפעולה יוצרת איקסים מסביב לצוללת שפוצצה כולה
        private void CellUpdate(int i,int j,Board board)
        {
            g = CreateGraphics();
            int size = board.GetBoard()[i,j].GetHeadSubmarine().GetNum(); //גודל של הצוללת
            bool dir = board.GetBoard()[i, j].GetHeadSubmarine().GetStandingY(); //המנח של הצוללת
            if (dir == true) // אם הצוללת ממוקמת במאונך
            {
                if (i >= 1)
                {
                    board.GetBoard()[i - 1, j].SetStatus(1);
                    if (j >= 1)
                        board.GetBoard()[i - 1, j - 1].SetStatus(1);
                    if (j <= 8)
                        board.GetBoard()[i - 1, j + 1].SetStatus(1);
                }
                // עדכון של שאר התאים לאורך הסירה
                for (int count = 1; count <= size; count++)
                {
                    if (j >= 1)
                        board.GetBoard()[i, j - 1].SetStatus(1);
                    if (j <= 8)
                        board.GetBoard()[i, j + 1].SetStatus(1);
                    i++;
                }
                // עדכון אחד למטה
                if (i <= 9)
                {
                    board.GetBoard()[i, j].SetStatus(1);
                    if (j >= 1)
                        board.GetBoard()[i, j - 1].SetStatus(1);
                    if (j <= 8)
                        board.GetBoard()[i, j + 1].SetStatus(1);
                }
            }
            else // הצוללת ממוקמת במאוזן
            {
                // עדכון אחד שמאלה
                if (j >= 1)
                {
                    board.GetBoard()[i, j - 1].SetStatus(1);
                    if (i >= 1)
                        board.GetBoard()[i - 1, j - 1].SetStatus(1);
                    if (i <= 8)
                        board.GetBoard()[i + 1, j - 1].SetStatus(1);
                }
                // עדכון של שאר התאים לרוחב הצוללת
                for (int count = 1; count <= size; count++)
                {
                    if (i >= 1)
                        board.GetBoard()[i - 1, j].SetStatus(1);
                    if (i <= 8)
                        board.GetBoard()[i + 1, j].SetStatus(1);
                    j++;
                }
                // עדכון אחד ימינה
                if (j <= 9)
                {
                    board.GetBoard()[i, j].SetStatus(1);
                    if (i >= 1)
                        board.GetBoard()[i - 1, j].SetStatus(1);
                    if (i <= 8)
                        board.GetBoard()[i + 1, j].SetStatus(1);
                }
            }
            
        }
        
        // פעולה המקבלת את מיקום ראש הצוללת הנוכחית ולוח 
        //   הפעולה מחזירה אמת אם הייתה פגיעה בכלל הצוללת
        // אחרת- הפעולה מחזירה שקר
        private bool CheckAllSub(int iHead, int jHead, Board board) // המקום האחרון שלחצנו עליו בצוללת שנפגעה
        {

                int size = s.GetNum();//גודל של הצוללת
                int mySize = 0;
                bool dir = s.GetStandingY();//המנח של הצוללת
                if (dir == true)//הצוללת מונחת במאונך
                {
                    for (int i = iHead; i < iHead + size; i++)
                    {
                        if (board.GetBoard()[i, jHead].GetStatus() == 2)//אם הייתה פגיעה בחלק מהצוללת
                            mySize++;
                    }
                    if (size == mySize) // אם מספר המשבצות שבפועל פוצצו שווה למספר המשבצות המסמלות את הפגיעה בכל הצוללת
                        return true; // כל הצוללת פוצצה
                    return false;
                }
                else // הצוללת מונחת במאוזן
                {
                    for (int j= jHead ; j < jHead + size; j++)
                    {
                        if (board.GetBoard()[iHead, j].GetStatus() == 2)//אם הייתה פגיעה בחלק מהצוללת
                            mySize++;
                    }
                    if (size == mySize) //אם מספר המשבצות שבפועל פוצצו שווה למספר המשבצות המסמלות את הפגיעה בכל הצוללת
                        return true;// כל הצוללת פוצצה
                    return false;
                }
        }
    }
}

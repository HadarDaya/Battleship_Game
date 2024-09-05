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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // הפנייה להתחלת המשחק-הצבת הצוללות בלחיצה על כפתור התחל
            StartGame a = new StartGame();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // הפנייה לדף ההוראות בלחיצה על כפתור ההוראות
            Instruction a = new Instruction();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // יציאה מהמשחק בלחיצה על כפתור יציאה
            DialogResult result;
            result = MessageBox.Show("Are you sure you want to leave the game?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result== DialogResult.Yes)
                this.Close();
        }

    }
}

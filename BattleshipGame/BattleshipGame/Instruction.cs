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
    public partial class Instruction : Form
    {
        public Instruction()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // הפנייה למסך הפתיחה בלחיצה על כפתור חזרה
            this.Close();
        }

        private void Instruction_Load(object sender, EventArgs e)
        {

        }
    }
}

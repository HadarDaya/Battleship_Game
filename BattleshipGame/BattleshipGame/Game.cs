using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BattleshipGame
{
    public partial class Game : Form
    {
        Graphics g;
        Board game = new Board(0,0);
        
        public Game()
        {
            InitializeComponent();
            
        }

        private void Game_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();           
            //game.SetPlayerBoard(); // הזזת לוח השחקן שמאלה
            game.PaintBoard(g); // ציור לוח השחקן, לוח ריק שיופיעו בו הפגיעות
            
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            game.PaintBoard(g);
        }

    }
}

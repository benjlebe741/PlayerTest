using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Player_Test
{
    public partial class Form1 : Form
    {
        List<Rectangle> tiles = new List<Rectangle>();
        List<int> tilePallette = new List<int>();

        SolidBrush brush1 = new SolidBrush(Color.Black);
        SolidBrush brush0 = new SolidBrush(Color.DarkSlateGray);
        SolidBrush brush2 = new SolidBrush(Color.FloralWhite);

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        Rectangle player1 = new Rectangle(10, 170, 27, 27);
        Rectangle upDownCheck = new Rectangle(10, 170, 15, 31);
        Rectangle rightLeftCheck = new Rectangle(10, 170, 31, 15);
      
        int playerSpeed = 4;
        int playerSize;

        private bool wDown;
        private bool aDown;
        private bool sDown;
        private bool dDown;

        int playerStartX;
        int playerStartY;

        bool dontMoveUpDown;

       bool bottomBorderTouching = false;
        bool topBorderTouching = false; 
       bool leftBorderTouching = false;
       bool rightBorderTouching = false;

        public Form1()
        {
            InitializeComponent();
        }
        private void movePlayerTo(int x, int y)
        {
            player1.X = x;
            player1.Y = y;
        }
        private void createLevel(int[] level, int rectangleDimension, int spawnX, int spawnY)
        {
            tiles.Clear();
            tilePallette.Clear();
            int y = 1;
            for (int n = 1; n < level.Length; n++)
            {
                int x = n;
                while (x > level[0])
                {
                    x -= level[0];
                }
                tiles.Add(new Rectangle((x - 1) * rectangleDimension, (y - 1) * rectangleDimension, rectangleDimension, rectangleDimension));
                tilePallette.Add(level[n]);
                if ((n) % (level[0]) == 0)
                {
                    y++;
                    x = 1;
                }

            }
            playerStartY = (spawnY);
            playerStartX = (spawnX);
            movePlayerTo(playerStartX, playerStartY);
            playerSize = player1.Width;
        }


        //WHILE LOOP
        private void playerTimer_Tick(object sender, EventArgs e)
        {
            //coordinate updating
            label1.Text = $"X:{player1.X}\nY:{player1.Y}";

            //player boarder interacts with tiles
            for (int n = 0; n < tiles.Count; n++)
            {
                if (rightLeftCheck.IntersectsWith(tiles[n]) && (tilePallette[n] == 4) && (rightLeftCheck.Left + 20 > tiles[n].Right))
                {
                    leftBorderTouching = true;
                }

                if (upDownCheck.IntersectsWith(tiles[n]) && (tilePallette[n] == 4) && (upDownCheck.Top + 20 > tiles[n].Bottom))
                {
                    topBorderTouching = true;
                }
                
                if (rightLeftCheck.IntersectsWith(tiles[n]) && (tilePallette[n] == 4) && (rightLeftCheck.Right - 20 < tiles[n].Left))
                {
                    rightBorderTouching = true;
                }

                if (upDownCheck.IntersectsWith(tiles[n]) && (tilePallette[n] == 4) && (upDownCheck.Bottom - 20 < tiles[n].Top))
                {
                   bottomBorderTouching = true;
                }
            }
            
            //tell me whats happening
            label2.Text = $"{bottomBorderTouching}\n{topBorderTouching}\n{leftBorderTouching}\n{rightBorderTouching}";

            //move the player first
            if (wDown == true && sDown == false && player1.Y > 0 + playerSpeed && (topBorderTouching == false))
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && wDown == false && player1.Y < Size.Height - playerSpeed - player1.Height && (bottomBorderTouching == false))
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && dDown == false && player1.X > 0 + playerSpeed && (leftBorderTouching == false))
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && aDown == false && player1.X < Size.Width - playerSpeed - player1.Width && (rightBorderTouching == false))
            {
                player1.X += playerSpeed;
            }


            //check if the player is currently in a boarder tile, if so, set the player outside the border tile WHEN APPROPRIATE
            for (int n = 0; n < tiles.Count; n++)
            {
                if (player1.IntersectsWith(tiles[n]) && (tilePallette[n] == 7))
                {
                    movePlayerTo(playerStartX, playerStartY);
                }
                if (player1.IntersectsWith(tiles[n]) && (tilePallette[n] == 4))
                {
                    //UP AND DOWN
                    if ((player1.Top < tiles[n].Bottom) && (player1.Bottom < tiles[n].Bottom) && (sDown == true) && (dontMoveUpDown == false)) //Player entering from ABOVE
                    {
                        player1.Y = tiles[n].Top - playerSize;
                        dontMoveUpDown = false;
                    }
                    else if ((player1.Top > tiles[n].Top) && (player1.Bottom > tiles[n].Top) && (wDown == true) && (dontMoveUpDown == false)) //Player entering from BELOW
                    {
                        player1.Y = tiles[n].Bottom;
                        dontMoveUpDown = false;
                    }
                    //LEFT AND RIGHT
                    else if ((player1.Left < tiles[n].Left) && (dDown == true)) //Player entering from LEFT
                    {
                        player1.X = tiles[n].Left - playerSize;
                        dontMoveUpDown = true;
                    }
                    else if ((player1.Right > tiles[n].Right) && (aDown == true)) //Player entering from RIGHT
                    {
                        player1.X = tiles[n].Right;
                        dontMoveUpDown = true;
                    }
                    else
                    {
                        dontMoveUpDown = false;
                    }
                }

            }

            //keep track of player boarder
            upDownCheck.X = player1.X + 6;
            upDownCheck.Y = player1.Y - 2;
            rightLeftCheck.Y = player1.Y + 6;
            rightLeftCheck.X = player1.X - 2;

            bottomBorderTouching = false;
            topBorderTouching = false;
            leftBorderTouching = false;
            rightBorderTouching = false;

            //refresh and do the paint function
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
                for (int n = 0; n < tiles.Count; n++)
                {
                if (tilePallette[n] == 4) { 
                    e.Graphics.FillRectangle(brush0, tiles[n]);              
                }
                    if (tilePallette[n] == 7)
                    {
                        e.Graphics.FillRectangle(brush2, tiles[n]);
                    }
                    if (tilePallette[n] == 1)
                    {
                        e.Graphics.FillRectangle(brush1, tiles[n]);

                    }

                }
            e.Graphics.FillRectangle(brush2, rightLeftCheck);
            e.Graphics.FillRectangle(brush2, upDownCheck);
            e.Graphics.FillRectangle(blueBrush, player1);
            }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Space:
                    playerSpeed = 4;
                    break;
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Space:
                    playerSpeed = 8;
                    break;
            }
        }

        private void playLabel_Click(object sender, EventArgs e)
        {
            playerTimer.Enabled = true;
            playLabel.Visible = false;
            int[] level1 =
      {
                5,
                4,4,4,4,4,
                4,1,1,1,4,
                4,1,1,1,4,
                4,1,1,1,4,
                4,4,4,4,4
            };
            int[] level2 =
          {
                10,
                1,7,4,7,1,7,4,7,1,7,
                4,1,1,7,4,7,1,1,1,4,
                4,1,1,7,4,1,1,1,1,4,
                4,4,4,7,4,1,1,1,1,4,
                4,4,4,7,7,1,1,1,1,4,
                4,4,4,4,4,1,7,7,1,4
            };
            int[] level3 =
           {
                28,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,4,4,4,1,4,4,
                1,7,1,1,7,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,4,4,4,4,4,4,
                1,7,1,1,7,1,1,7,7,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                1,7,7,7,7,1,1,7,7,7,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,7,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,7,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,
                1,7,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,7,1,1,7,1,7,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,
            };
            //createLevel(level1, Size.Width / level1[0]);
            //createLevel(level2, Size.Width / level2[0]);
            createLevel(level3, Size.Width / level3[0], Size.Width/2,Size.Height/2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pastPlayerTimer_Tick(object sender, EventArgs e)
        {
           
        }
    }
}

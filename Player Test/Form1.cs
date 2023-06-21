using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Player_Test
{
    public partial class zeldaClone : Form
    {
        #region Global Variables
        //Lists to store the current loaded tiles, and current tiles Colour Pallette:
        List<Rectangle> currentTiles = new List<Rectangle>();
        List<int> currentTilePallette = new List<int>();
        
        //storing current Level
        int currentLevel;
        
        //WHAT DOES THIS DO??
        int layoutWidth = 3;

        //Paint Brushes
        SolidBrush brush1 = new SolidBrush(Color.DarkSlateGray);
        SolidBrush brush0 = new SolidBrush(Color.Black);
        SolidBrush brush2 = new SolidBrush(Color.FloralWhite);
        SolidBrush brush3 = new SolidBrush(Color.DodgerBlue);
        SolidBrush brush4 = new SolidBrush(Color.Red);

        //Entities
        Rectangle player1 = new Rectangle(10, 170, 1, 1);
        Rectangle upDownCheck = new Rectangle(10, 170, 1, 31);
        Rectangle rightLeftCheck = new Rectangle(10, 170, 31, 1);

        //Player Values
        int playerDirection;
        int playerSpeed = 3;
        int playerSight = 1350;
        int playerSize = 27;

        //Key Down Values
        private bool wDown;
        private bool aDown;
        private bool sDown;
        private bool dDown;

        //Player Current Spawn
        int playerSpawnX = 580;
        int playerSpawnY = 326;
        int playerSpawnLevel = 0;

        //Is Player Touching Values
       bool bottomBorderTouching = false;
        bool topBorderTouching = false; 
       bool leftBorderTouching = false;
       bool rightBorderTouching = false;
        #endregion
       
        #region ALL LEVELS:
        int[][] levels = new int[][]
               {
                new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                4,4,1,1,4,4,4,4,4,4,4,4,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                4,4,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,1,1,1,1,1,1,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
            },
                 new int[]
                {
                30,
                4,7,7,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                7,4,1,7,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                7,7,7,7,7,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
            },

        new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,4,4,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,7,7,7,1,1,1,1,4,4,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                4,4,7,7,7,1,1,7,7,7,1,1,1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,1,1,1,1,1,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
            },
        //NEXT LAYER
    new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,4,7,1,7,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,7,7,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                4,4,7,7,7,1,1,7,7,7,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,1,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,1,1,1,7,1,7,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
            },
     new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                4,4,7,7,7,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                1,1,1,1,1,1,4,1,1,1,4,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,4,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
            },
new int[]
       {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,4,7,1,7,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,7,7,7,1,1,1,1,4,1,1,1,1,1,1,1,1,1,4,4,4,4,4,4,
                4,4,7,7,7,1,1,7,7,7,1,1,1,1,4,7,1,1,1,1,1,1,1,1,4,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,4,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,4,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,4,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,4,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
            },
};
        #endregion

        #region Initialize
        public zeldaClone()
        {
            InitializeComponent();
        }
        #endregion

        #region Move Player To
        private void movePlayerTo(int x, int y)
        {
            player1.X = x;
            player1.Y = y;
        }
        #endregion

        #region Create Level
        private void createLevel(int[] level, int rectangleDimension, int offsetX, int offsetY)
        {
            currentTiles.Clear();
            currentTilePallette.Clear();
            int y = 1;
            for (int n = 1; n < level.Length; n++)
            {
                int x = n;
                while (x > level[0])
                {
                    x -= level[0];
                }
                currentTiles.Add(new Rectangle(((x - 1) * rectangleDimension) + offsetX, ((y - 1) * rectangleDimension) + offsetY, rectangleDimension, rectangleDimension));
                currentTilePallette.Add(level[n]);
                if ((n) % (level[0]) == 0)
                {
                    y++;
                    x = 1;
                }

            }
            player1.Width = playerSize;
            player1.Height = playerSize;
            upDownCheck.Width = playerSize;
            upDownCheck.Height = playerSize + 4;
            rightLeftCheck.Height = playerSize;
            rightLeftCheck.Width = playerSize + 4;
        }
        #endregion

        #region Player Timer
        private void playerTimer_Tick(object sender, EventArgs e)
        {
            //coordinate updating
            label1.Text = $"X:{player1.X}\nY:{player1.Y}";

            //player boarder interacts with currentTiles
            for (int n = 0; n < currentTiles.Count; n++)
            {
                if (rightLeftCheck.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 4) && (rightLeftCheck.Left + 20 > currentTiles[n].Right))
                {
                    leftBorderTouching = true;
                }

                if (upDownCheck.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 4) && (upDownCheck.Top + 20 > currentTiles[n].Bottom))
                {
                    topBorderTouching = true;
                }
                
                if (rightLeftCheck.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 4) && (rightLeftCheck.Right - 20 < currentTiles[n].Left))
                {
                    rightBorderTouching = true;
                }

                if (upDownCheck.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 4) && (upDownCheck.Bottom - 20 < currentTiles[n].Top))
                {
                   bottomBorderTouching = true;
                }
            }
            
            //tell me whats happening
            label2.Text = $"{bottomBorderTouching}\n{topBorderTouching}\n{leftBorderTouching}\n{rightBorderTouching}";
            label3.Text = $"{currentLevel}\n{playerDirection}";

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
            for (int n = 0; n < currentTiles.Count; n++)
            {
                if (player1.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 7))
                {
                    movePlayerTo(playerSpawnX, playerSpawnY);
                    createLevel(levels[playerSpawnLevel], Size.Width / levels[playerSpawnLevel][0], 14, 16);
                    currentLevel= playerSpawnLevel;
                }
                if (player1.IntersectsWith(currentTiles[n]) && (currentTilePallette[n] == 4))
                {
                    //UP AND DOWN
                    if ((player1.Top < currentTiles[n].Bottom) && (player1.Bottom < currentTiles[n].Bottom) && (sDown == true)) //Player entering from ABOVE
                    {
                        player1.Y = currentTiles[n].Top - playerSize;
                    }
                    if ((player1.Top > currentTiles[n].Top) && (player1.Bottom > currentTiles[n].Top) && (wDown == true)) //Player entering from BELOW
                    {
                        player1.Y = currentTiles[n].Bottom;
                    }
                    //LEFT AND RIGHT
                    if ((player1.Left < currentTiles[n].Left) && (dDown == true)) //Player entering from LEFT
                    {
                        player1.X = currentTiles[n].Left - playerSize;
                    }
                    if ((player1.Right > currentTiles[n].Right) && (aDown == true)) //Player entering from RIGHT
                    {
                        player1.X = currentTiles[n].Right;
                    }
                }

            }

            //is player at the edge of the world? move to next level 
            switch (player1.Y) 
            {
                case var _ when(player1.Y <= 15): //player goes up a level 
                    movePlayerTo(player1.X, 670);
                    createLevel(levels[currentLevel - layoutWidth], Size.Width / levels[currentLevel - layoutWidth][0], 14, 16);
                    currentLevel -= layoutWidth;
                    break;
                case var _ when (player1.Y >= 675)://player goes down a level
                    movePlayerTo(player1.X, 20);
                    createLevel(levels[currentLevel + layoutWidth], Size.Width / levels[currentLevel + layoutWidth][0], 14, 16);
                    currentLevel += layoutWidth;
                    break;
            }

            switch (player1.X)
            {
                case var _ when (player1.X <= 20)://player goes left a level
                    movePlayerTo(1120, player1.Y);
                    createLevel(levels[currentLevel - 1], Size.Width / levels[currentLevel - 1][0], 14, 16);
                    currentLevel -= 1;
                    break;
                case var _ when (player1.X >= 1130)://player goes right a level
                    movePlayerTo(30, player1.Y);
                    createLevel(levels[currentLevel+1], Size.Width / levels[currentLevel+1][0], 14, 16);
                    currentLevel += 1;
                    break;
            }

            //keep track of player boarder
            upDownCheck.X = player1.X + (player1.Width/2) - (upDownCheck.Width/2);
            upDownCheck.Y = player1.Y + (player1.Height / 2) - (upDownCheck.Height / 2);
            rightLeftCheck.Y = player1.Y + (player1.Height / 2) - (rightLeftCheck.Height / 2);
            rightLeftCheck.X = player1.X + (player1.Width / 2) - (rightLeftCheck.Width / 2);

            bottomBorderTouching = false;
            topBorderTouching = false;
            leftBorderTouching = false;
            rightBorderTouching = false;

            //refresh and do the paint function
            Refresh();
        }
        #endregion

        #region Paint
        //PAINT: paint the currentTiles, then player!
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bool inSight;

            for (int n = 0; n < currentTiles.Count; n++)
            {
                //is the tile in sight of player?
                if (currentTiles[n].Left < player1.X - playerSize - playerSight || currentTiles[n].Left > player1.X + playerSight || currentTiles[n].Top < player1.Y - playerSize - playerSight || currentTiles[n].Top > player1.Y + playerSight)
                { inSight = false; }
                else
                { inSight = true; }
                //what tile is it?
                if (currentTilePallette[n] == 4 && inSight == true)
                {
                    e.Graphics.FillRectangle(brush1, currentTiles[n]);
                }
                if (currentTilePallette[n] == 7 && inSight == true)
                {
                    e.Graphics.FillRectangle(brush2, currentTiles[n]);
                }
                if (currentTilePallette[n] == 1)
                {
                    e.Graphics.FillRectangle(brush0, currentTiles[n]);

                }

            }
            //show player and player boarders
            e.Graphics.FillRectangle(brush2, rightLeftCheck);
            e.Graphics.FillRectangle(brush2, upDownCheck);
            e.Graphics.FillRectangle(brush3, player1);
        }
        #endregion

        #region Check Keys
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
                    playerSpeed = 3;
                    break;
            }
        }

        //=----------------------------------------------------------------------------------------------
        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    playerDirection = 1;
                    break;
                case Keys.A:
                    aDown = true;
                    playerDirection = 4;
                    break;
                case Keys.S:
                    sDown = true;
                    playerDirection = 3;
                    break;
                case Keys.D:
                    dDown = true;
                    playerDirection = 2;
                    break;
                case Keys.Space:
                    playerSpeed = 8;
                    break;
            }
        }
        #endregion

        #region Start Button
        private void playLabel_Click(object sender, EventArgs e)
        {
            playerTimer.Enabled = true;
            playLabel.Visible = false;

            createLevel(levels[0], Size.Width / levels[0][0], 14, 16);
            currentLevel = 0;
            movePlayerTo(580, 326);
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

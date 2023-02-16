﻿using System;
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
    public partial class zeldaClone : Form
    {
        List<Rectangle> tiles = new List<Rectangle>();
        List<int> tilePallette = new List<int>();
        int currentLevel;
        
        int layoutWidth = 3;

        SolidBrush brush1 = new SolidBrush(Color.DarkSlateGray);
        SolidBrush brush0 = new SolidBrush(Color.Black);
        SolidBrush brush2 = new SolidBrush(Color.FloralWhite);
        SolidBrush brush3 = new SolidBrush(Color.DodgerBlue);

        Rectangle player1 = new Rectangle(10, 170, 27, 27);
        Rectangle upDownCheck = new Rectangle(10, 170, 15, 31);
        Rectangle rightLeftCheck = new Rectangle(10, 170, 31, 15);
      
        int playerSpeed = 4;
        int playerSize;

        private bool wDown;
        private bool aDown;
        private bool sDown;
        private bool dDown;

        int playerSpawnX = 580;
        int playerSpawnY = 326;
        int playerSpawnLevel = 0;

       bool bottomBorderTouching = false;
        bool topBorderTouching = false; 
       bool leftBorderTouching = false;
       bool rightBorderTouching = false;

        int[][] levels = new int[][]
               {
                new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
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
                4,1,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,1,1,1,7,1,7,1,1,1,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
            },
                 new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
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
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
            },

        new int[]
                {
                30,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,1,1,1,1,1,1,1,4,7,1,1,1,1,1,1,4,4,4,4,4,4,4,4,
                4,4,1,1,7,1,1,7,7,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                4,4,7,7,7,1,1,7,7,7,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,4,4,4,4,4,4,4,4,
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
                4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
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
                4,4,1,1,7,1,1,7,7,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                4,4,7,7,7,1,1,7,7,7,1,1,1,1,4,7,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,7,1,7,1,1,1,7,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,4,4,4,4,
                4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
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
        public zeldaClone()
        {
            InitializeComponent();
        }

        //=----------------------------------------------------------
        
        //=----------------------------------------------------------
        private void movePlayerTo(int x, int y)
        {
            player1.X = x;
            player1.Y = y;
        }

        //=----------------------------------------------------------
        private void createLevel(int[] level, int rectangleDimension, int offsetX, int offsetY)
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
                tiles.Add(new Rectangle(((x - 1) * rectangleDimension) + offsetX, ((y - 1) * rectangleDimension) + offsetY, rectangleDimension, rectangleDimension));
                tilePallette.Add(level[n]);
                if ((n) % (level[0]) == 0)
                {
                    y++;
                    x = 1;
                }

            }
            playerSize = player1.Width;
        }


        //=----------------------------------------------------------------------------------------------
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
            label3.Text = $"{currentLevel}";

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
                    movePlayerTo(playerSpawnX, playerSpawnY);
                    createLevel(levels[playerSpawnLevel], Size.Width / levels[playerSpawnLevel][0], 14, 16);
                    currentLevel= playerSpawnLevel;
                }
                if (player1.IntersectsWith(tiles[n]) && (tilePallette[n] == 4))
                {
                    //UP AND DOWN
                    if ((player1.Top < tiles[n].Bottom) && (player1.Bottom < tiles[n].Bottom) && (sDown == true)) //Player entering from ABOVE
                    {
                        player1.Y = tiles[n].Top - playerSize;
                    }
                    else if ((player1.Top > tiles[n].Top) && (player1.Bottom > tiles[n].Top) && (wDown == true)) //Player entering from BELOW
                    {
                        player1.Y = tiles[n].Bottom;
                    }
                    //LEFT AND RIGHT
                    else if ((player1.Left < tiles[n].Left) && (dDown == true)) //Player entering from LEFT
                    {
                        player1.X = tiles[n].Left - playerSize;
                    }
                    else if ((player1.Right > tiles[n].Right) && (aDown == true)) //Player entering from RIGHT
                    {
                        player1.X = tiles[n].Right;
                    }
                }

            }

            //is player at the edge of the world? move to next level NOT WORKING. FIX FIX FIX
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


        //=----------------------------------------------------------------------------------------------
        //PAINT: paint the tiles, then player!
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
                for (int n = 0; n < tiles.Count; n++)
                {
                if (tilePallette[n] == 4) { 
                    e.Graphics.FillRectangle(brush1, tiles[n]);              
                }
                    if (tilePallette[n] == 7)
                    {
                        e.Graphics.FillRectangle(brush2, tiles[n]);
                    }
                    if (tilePallette[n] == 1)
                    {
                        e.Graphics.FillRectangle(brush0, tiles[n]);

                    }

                }
            e.Graphics.FillRectangle(brush2, rightLeftCheck);
            e.Graphics.FillRectangle(brush2, upDownCheck);
            e.Graphics.FillRectangle(brush3, player1);
            }


        //=----------------------------------------------------------------------------------------------
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

        //=----------------------------------------------------------------------------------------------
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


        //=----------------------------------------------------------------------------------------------
        private void playLabel_Click(object sender, EventArgs e)
        {
            playerTimer.Enabled = true;
            playLabel.Visible = false;

            createLevel(levels[0], Size.Width / levels[0][0],14, 16);
            currentLevel= 0;
            movePlayerTo(580, 326);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

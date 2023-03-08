using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Player_Test
{
    internal class Player
    {
        int playerDirection;
        int playerSpeed = 3;
        int playerSight = 250;
        int playerSize = 27;

        int playerSpawnX = 580;
        int playerSpawnY = 326;
        int playerSpawnLevel = 0;

        bool bottomBorderTouching = false;
        bool topBorderTouching = false;
        bool leftBorderTouching = false;
        bool rightBorderTouching = false;

        Rectangle upDownCheck = new Rectangle(10, 170, 1, 31);
        Rectangle rightLeftCheck = new Rectangle(10, 170, 31, 1);

        public Player(int inputPlayerSize)
        {
           playerSize= inputPlayerSize;
        }

    }
}

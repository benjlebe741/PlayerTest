using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player_Test
{
    internal class Level
    {
        int[] levelPath = { };
        int[] levelSpray = { };
        int levelWidth = 30;

        public Level(int[] _levelPath, int[] _levelSpray, int _levelWidth) 
        {
        levelPath = _levelPath;
        levelSpray = _levelSpray;
        levelWidth = _levelWidth;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    /// <summary>
    /// 启动器
    /// </summary>
    class Program
    {
        static void Main(String[] arg)
        {
            GameCore gameCore = new GameCore();
            gameCore.createNum();
            gameCore.createNum();
            DrawMap(gameCore.Map);
            while (true)
            {
                KeyDown(gameCore);
                if (gameCore.ChangeFlag)
                {
                    gameCore.createNum();
                    DrawMap(gameCore.Map);
                }
            }
        }

        private static void DrawMap(int[,] map)
        {
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(map[i,j] + "\t");
                }
                Console.WriteLine();
            }
        }
        private static void KeyDown(GameCore core)
        {

            switch ((int)Console.ReadKey().KeyChar)
            {
                case 119:
                    core.Move(Direction.UP);
                    break;
                case 115:
                    core.Move(Direction.DOWN);
                    break;
                case 97:
                    core.Move(Direction.LEFT);
                    break;
                case 100:
                    core.Move(Direction.RIGHT);
                    break;
            }
        }
    }
}

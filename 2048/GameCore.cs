using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    /// <summary>
    /// 核心逻辑算法类
    /// </summary>
    class GameCore
    {
        public GameCore()
        {
            map = new int[4, 4];
            removeZeroArray = new int[4];
            mergeArray = new int[4];
            blankSpace = new List<Location>(16);
            random = new Random();
            originMap = new int[4, 4];
        }

        private int[] removeZeroArray; // 去零中间数组
        private int[] mergeArray; // 移动中间数组
        private List<Location> blankSpace; // 存放空白位置的数组
        private int[,] map; // 主数组
        private Random random;
        private int[,] originMap;
        public int[,] OriginMap { get { return this.originMap; } }
        public Boolean ChangeFlag { get; set; }
        public int[,] Map { get { return this.map; } }

        // 去零方法
        private void RemoveZero(int[] arr)
        {
            Array.Clear(removeZeroArray, 0, 4);
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    removeZeroArray[index++] = arr[i];
                }
            }
            removeZeroArray.CopyTo(arr, 0);
        }
        // 合并方法
        private void Merge(int[] arr)
        {
            RemoveZero(arr);
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] != 0 && arr[i] == arr[i + 1])
                {
                    arr[i] += arr[i + 1];
                    arr[i + 1] = 0;
                    // 合并点
                }
            }
            RemoveZero(arr);
        }

        // 上移方法
        private void MoveUp()
        {
            Array.Clear(mergeArray, 0, 4);
            for (int i = 0; i < map.GetLength(1); i++)
            {
                // 获取上移一维数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    mergeArray[j] = map[j, i];
                }
                Merge(mergeArray);
                // 赋值给原数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    map[j, i] = mergeArray[j];
                }
            }
        }

        // 下移方法
        private void MoveDown()
        {
            Array.Clear(mergeArray, 0, 4);
            for (int i = 0; i < map.GetLength(1); i++)
            {
                // 获取下移一维数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    mergeArray[j] = map[map.GetLength(0) - 1 - j, i];
                }
                Merge(mergeArray);
                // 赋值给原数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    map[map.GetLength(0) - 1 - j, i] = mergeArray[j];
                }
            }
        }

        // 左移方法
        private void MoveLeft()
        {
            Array.Clear(mergeArray, 0, 4);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                // 获取左移一维数组
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    mergeArray[j] = map[i, j];
                }
                Merge(mergeArray);
                // 赋值给原数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    map[i, j] = mergeArray[j];
                }
            }
        }

        // 右移方法
        private void MoveRight()
        {
            Array.Clear(mergeArray, 0, 4);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                // 获取右移一维数组
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    mergeArray[j] = map[i, map.GetLength(1)-1-j];
                }
                Merge(mergeArray);
                // 赋值给原数组
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    map[i, map.GetLength(1) - 1 - j] = mergeArray[j];
                }
            }
        }

        // 公开的移动方法，使用枚举作为移动方向
        public void Move(Direction direction)
        {
            Array.Copy(map, originMap, map.Length);
            ChangeFlag = false;
            switch (direction)
            {
                case Direction.UP:
                    MoveUp();
                    break;
                case Direction.DOWN:
                    MoveDown();
                    break;
                case Direction.LEFT:
                    MoveLeft();
                    break;
                case Direction.RIGHT:
                    MoveRight();
                    break;
            }
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i,j] != originMap[i, j])
                    {
                        ChangeFlag = true;
                        return;
                    }
                }
            }
        }


        // 挑出空白位置
        private void GetBlank()
        {
            blankSpace.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                    {
                        blankSpace.Add(new Location(i, j));
                    }
                }
            }
        }

        // 随机生成数字，90%几率2，10%几率4
        public void createNum()
        {
            GetBlank();
            if (blankSpace.Count != 0)
            {
                int index = random.Next(0, blankSpace.Count);
                Location loc = blankSpace[index];
                map[loc.RIndex, loc.CIndex] = random.Next(0, 10) == 1 ? 4 : 2;
            }
        }
    }
}

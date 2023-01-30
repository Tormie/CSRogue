using System;
namespace Rogueish
{
    public class Utils
    {
        public int CalculateDistance(int xPos, int yPos)
        {
            return Math.Abs(Program.player.xPos - xPos) + Math.Abs(Program.player.yPos - yPos);
        }
    }
}

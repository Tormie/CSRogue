using System;
namespace Rogueish
{
    public class RogueEnemy : RogueCharacterBase
    {
        bool isPlayerSeen = false;
        int viewRange = 5;
        public RogueEnemy(int _xPos, int _yPos, string _name, int _hp, char _charSymbol)
        {
            xPos = _xPos;
            yPos = _yPos;
            name = _name;
            maxhp = _hp;
            hp = _hp;
            charSymbol = _charSymbol;
            Program.levelMap[xPos, yPos] = charSymbol;
            isEnemy = true;
        }


        public void MoveEnemy()
        {
            if (Program.ut.CalculateDistance(xPos, yPos) <= viewRange)
            {
                isPlayerSeen = true;
            }    
            if (isPlayerSeen)
            {
                if (Math.Abs(Program.player.xPos - xPos) > Math.Abs(Program.player.yPos - yPos))
                {
                    if (xPos > Program.player.xPos)
                    {
                        MoveChar(-1, 0);
                    }
                    else
                    {
                        MoveChar(1, 0);
                    }
                }
                else
                {
                    if (yPos > Program.player.yPos)
                    {
                        MoveChar(0, -1);
                    }
                    else
                    {
                        MoveChar(0, 1);
                    }
                }
            }
        }
    }
}

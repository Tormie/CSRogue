using System;
using System.Collections;
using System.Linq;
namespace Rogueish
{
    public class RoguePlayer : RogueCharacterBase
    {
        public char[] skills = { 'h', 'b', 'j', 'r' };
        public int viewDistance = 5;
        int potionAmount = 2;
        public RoguePlayer(int _xPos, int _yPos, string _name, int _hp, char _charSymbol)
        {
            xPos = _xPos;
            yPos = _yPos;
            name = _name;
            maxhp = _hp;
            hp = _hp;
            charSymbol = _charSymbol;
            isEnemy = false;
            damage = 5;
            hitPercentage = 15;
        }

        public void MovePlayer(int dX, int dY)
        {
            MoveChar(dX, dY);
        }

        public void UsePotion()
        {
            if (hp < maxhp)
            {
                if (potionAmount > 0)
                {
                    Program.actionLog += "You drink a potion and heal " + (maxhp - hp) + " hit points.\n";
                    hp = maxhp;
                } else
                {
                    Program.actionLog += "You have run out of potions.\n";
                }
            } else
            {
                Program.actionLog += "You are at full health, you do not need a health potion.\n";
            }
            
        }
    }
}

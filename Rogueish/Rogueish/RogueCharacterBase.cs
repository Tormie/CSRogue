using System;
using System.Linq;

namespace Rogueish
{
    public class RogueCharacterBase
    {
        public int xPos;
        public int yPos;
        public int charLevel = 1;
        public int charXP = 0;
        public int reqXP = 2;
        public string name;
        public int maxhp = 1;
        public int hp = 1;
        public char charSymbol;
        public bool isEnemy;
        public int damage = 1;
        public int gold = 0;
        public int hitPercentage = 75;
        RogueEnemy target = null;

        public RogueCharacterBase()
        {
        }

        public void MoveChar(int dX, int dY)
        {
            if (Program.levelMap[xPos+dX,yPos+dY] != '.')
            {
                if (!isEnemy && Program.levelMap[xPos + dX, yPos + dY] == '|' || Program.levelMap[xPos + dX, yPos + dY] == '-')
                {
                    Program.actionLog += "You walk into a wall!\n";
                    dX = 0;
                    dY = 0;
                }
                else if (isEnemy && Program.enemyCharList.Contains(Program.levelMap[xPos + dX, yPos + dY]))
                {
                    Program.actionLog += "Enemies bump into eachother and cannot move.\n";
                    dX = 0;
                    dY = 0;
                }
                else if (isEnemy && Program.levelMap[xPos + dX, yPos + dY] == '@')
                {
                    Program.actionLog += "Enemy " + name + " attacks you!\n";
                    dX = 0;
                    dY = 0;
                }
                else if (!isEnemy && Program.enemyCharList.Contains(Program.levelMap[xPos + dX, yPos + dY]))
                {
                    target = null;
                    foreach (RogueEnemy r in Program.enemyList)
                    {
                        if (r.xPos == xPos + dX && r.yPos == yPos + dY)
                        {
                            target = r;
                        }
                    }    
                    Program.actionLog += "You attack the enemy "+target.name+ ".\n";
                    Attack(dX, dY, true);
                    dX = 0;
                    dY = 0;
                }
            }
            Program.levelMap[xPos, yPos] = '.';
            xPos += dX;
            yPos += dY;
            Program.levelMap[xPos, yPos] = charSymbol;
            if (dX != 0 || dY != 0)
            {
                Program.PrintMap();
            }
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0 && isEnemy)
            {
                Program.actionLog += name + " died!\n";
                Program.actionLog += "You gain experience.\n";
                Program.player.GainXP(1);
            }
        }

        public void GainXP(int xp)
        {
            charXP += xp;
            if (charXP >= reqXP)
            {
                charLevel++;
                Program.actionLog += "You have gained a level!\n";
                maxhp += 2;
                hp = maxhp;
                xp = 0;
                reqXP += reqXP;
                Program.PrintMap();
            }
        }

        public void Attack(int dX, int dY, bool isPlayer)
        {
            if (isPlayer)
            {
                RogueEnemy enemyTarget = null;
                foreach (RogueEnemy e in Program.enemyList)
                {
                    Random rnd = new Random();
                    int attackRoll = rnd.Next(0, 100);
                    if (attackRoll <= hitPercentage)
                    {
                        Program.actionLog += "You attack and hit!\n";
                        if (e.xPos == xPos + dX && e.yPos == yPos + dY)
                        {
                            e.TakeDamage(damage);
                            Program.actionLog += "You deal " + damage + " damage.\n";
                            if (e.hp <= 0)
                            {
                                Program.actionLog += e.name + " died\n";
                                enemyTarget = e;
                            }
                        }
                    }
                    else
                    {
                        Program.actionLog += "You attack and miss!\n";
                    }
                }
                if (enemyTarget != null)
                {
                    Program.enemyList.Remove(enemyTarget);
                    Program.levelMap[enemyTarget.xPos, enemyTarget.yPos] = '.';
                }
            }
        }



        public void Attack2(int dX, int dY)
        {
            RogueEnemy enemyTarget = null;
            foreach (RogueEnemy e in Program.enemyList)
            {
                Random rnd = new Random();
                int attackRoll = rnd.Next(0, 100);
                if (attackRoll <= hitPercentage)
                {
                    Console.WriteLine("You attack and hit!");
                    if (e.xPos == xPos + dX && e.yPos == yPos + dY)
                    {
                        e.TakeDamage(damage);
                        if (e.hp <= 0)
                        {
                            Console.WriteLine(e.name + " died");
                            enemyTarget = e;
                        }
                    }
                } else
                {
                    Console.WriteLine("You attack and miss!");
                }
            }
            if (enemyTarget != null)
            {
                Program.enemyList.Remove(enemyTarget);
                Program.levelMap[enemyTarget.xPos, enemyTarget.yPos] = '.';
            }
        }
    }
}

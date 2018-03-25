using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class TurnManager
    {
        List<Unit> playingUnits;
        private Random rng = new Random();
        private int current;
        List<Player> players;
        public TurnManager(List<Player> players)
        {
            this.players = players;
            current = 0;
            playingUnits = new List<Unit>();
        }

        public void createAttackPriorityList()
        {
            foreach (Player p in players)
            {
                playingUnits.AddRange(p.units);
            }
            shuffle(playingUnits);
            playingUnits = playingUnits.OrderBy(o => o.attackPriority).ToList();
        }

        public Unit popUnit()
        {
            Unit u = playingUnits[current];
            current = (current + 1) % playingUnits.Count;
            return u;
        }


        public Unit peekUnit()
        {
            return playingUnits[current];
        }

        public void removeUnit(Unit killed)
        {
            int i = playingUnits.IndexOf(killed);
            if (i == -1)
            {
                throw new Exception("Unit was not registered in the turn list.");
            }
            playingUnits.RemoveAt(i);
            if (i < current)
            {
                current--;
            }

            bool[] hasUnits = new bool[players.Count];
            int playerWithUnitCount = 0;
            foreach (Unit u in playingUnits)
            {
                hasUnits[u.owner.id - 1] = true;
            }

            foreach (bool b in hasUnits)
            {
                if (b)
                {
                    playerWithUnitCount++;
                }
            }

            if (playerWithUnitCount < 2)
            {
                endGame();
            }
        }

        public void printQueue()
        {
            for (int i = 0; i < playingUnits.Count; i++)
            {
                playingUnits[(i + current) % playingUnits.Count].printUnit();
                Console.WriteLine();
            }
        }
        public void shuffle(List<Unit> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Unit value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void endGame()
        {
            playingUnits[0].owner.printPlayer();
            Console.WriteLine(" wins!");
        }
    }
}

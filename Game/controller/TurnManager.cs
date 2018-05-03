using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class TurnManager
    {

        public List<Unit> playingUnits
        {
            get; set;
        }
        private Random rng = new Random();
        /// <summary>
        /// Index into playingUnits pointing at current playing unit
        /// Loops.
        /// </summary>
        private int current;
        List<Player> players;
        public TurnManager(List<Player> players)
        {
            this.players = players;
            current = 0;
            playingUnits = new List<Unit>();
        }

        /// <summary>
        /// Creates circular list of playing units
        /// thats is ordered by unit speed
        /// for same speed units it is randomized
        /// </summary>
        public void createAttackPriorityList()
        {
            foreach (Player p in players)
            {
                playingUnits.AddRange(p.units);
            }
            shuffle(playingUnits);
            playingUnits = playingUnits.OrderBy(o => o.attackPriority).ToList();
        }

        /// <summary>
        /// Pops top unit from the list and puts her on 
        /// the bottom of the turn queue
        /// </summary>
        /// <returns></returns>
        public Unit popUnit()
        {
            Unit u = playingUnits[current];
            current = (current + 1) % playingUnits.Count;
            return u;
        }

        /// <summary>
        /// Peeks the queue for the current unit
        /// without chaning the turn oreder
        /// </summary>
        /// <returns></returns>
        public Unit peekUnit()
        {
            return playingUnits[current];
        }

        /// <summary>
        /// Removes unit from the circular turn queue
        /// Used when unit dies
        /// </summary>
        /// <param name="killed"></param>
        public void removeUnit(Unit killed)
        {
            int i = playingUnits.IndexOf(killed);
            if (i == -1)
            {
                throw new Exception("Unit was not registered in the turn list.");
            }
            
            playingUnits.RemoveAt(i);
            if (i <= current)
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

        /// <summary>
        /// Prints the turn order of units in game
        /// </summary>
        public void printQueue()
        {
            for (int i = 0; i < playingUnits.Count; i++)
            {
                playingUnits[(i + current) % playingUnits.Count].printUnit();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Randomizes the queue order
        /// </summary>
        /// <param name="list"></param>
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

        /// <summary>
        /// Finds out what player won
        /// </summary>
        public void endGame()
        {
            playingUnits[0].owner.printPlayer();
            Console.WriteLine(" wins!");
        }
    }
}

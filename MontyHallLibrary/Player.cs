namespace MontyHallLibrary
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {
        private bool strategy;

        /// <summary></summary>
        /// <param name="strategy"> true - поменять дверь, false - не менять дверь </param>
        public Player(bool strategy)
        {
            this.strategy = strategy;
        }

        /// <summary>
        /// Играть в игру с ведущим
        /// </summary>
        /// <returns> true - выйграл, false - проиграл
        /// </returns>
        public bool Play()
        {
            Emcee emcee = new Emcee();

            int choosedDoor = GlobalRandom.Next(0, 3);

            emcee.FirstChoice(choosedDoor);

            return emcee.SecondChoice(strategy);
        }
    }
}

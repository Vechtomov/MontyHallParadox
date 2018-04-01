using System;

namespace MontyHallLibrary
{
    /// <summary>
    /// Ведущий
    /// </summary>
    public class Emcee
    {
        private const int doorsCount = 3;

        /// <summary>
        /// Двери, true - за дверью автомобиль, false - коза
        /// </summary>
        private bool[] doors;

        /// <summary>
        /// Номер выбранной двери
        /// </summary>
        private int choosedDoor = -1;
        
        public Emcee()
        {
            doors = new bool[doorsCount];

            int doorWithCar = GlobalRandom.Next(0, doorsCount);
            doors[doorWithCar] = true;
        }

        /// <summary>
        /// Первый выбор: номер выбранной двери
        /// </summary>
        /// <param name="choosedDoor">
        /// Номер выбранной двери(отсчет с 0)
        /// </param>
        public void FirstChoice(int choosedDoor)
        {
            if (choosedDoor < 0 || doorsCount - 1 < choosedDoor) {
                throw new IndexOutOfRangeException("Номер двери должен быть от 0 до " + (doorsCount - 1).ToString());
            }

            // запоминаем номер выбранной двери
            this.choosedDoor = choosedDoor;
        }

        /// <summary>
        /// Второй выбор: менять ли дверь
        /// </summary>
        /// <param name="changeDoor"> 
        /// true - поменять дверь, false - не менять дверь 
        /// </param>
        /// <returns> 
        /// true - выйграл, false - проиграл
        /// </returns>
        public bool SecondChoice(bool changeDoor)
        {
            // проверяем, выбрал ли игрок дверь
            if(choosedDoor == -1) {
                throw new Exception("Сначала выберите дверь.");
            }

            return (!changeDoor && doors[choosedDoor]) || (changeDoor && !doors[choosedDoor]);
        }
    }
}

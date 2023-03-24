using System;

namespace SeaBatle {
    /// <summary>
    /// База даних діючих кораблів
    /// </summary>
    public class ShipDataBase {
        public int numOfVeryBigShips { get; private set; }
        public int numOfBigShips { get; private set; }
        public int numOfMiddleShips { get; private set; }
        public int numOfSmallShips { get; private set; }
        private bool allShipsDestroyed;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="numOfVeryBigShips">Кількість діючих 4 палубних кораблів</param>
        /// <param name="numOfBigShips">Кількість діючих 3 палубних кораблів</param>
        /// <param name="numOfMiddleShips">Кількість діючих 2 палубних кораблів</param>
        /// <param name="numOfSmallShips">Кількість діючих 1 палубних кораблів</param>
        public ShipDataBase(int numOfVeryBigShips, int numOfBigShips, int numOfMiddleShips, int numOfSmallShips) {
            this.numOfVeryBigShips = numOfVeryBigShips;
            this.numOfBigShips = numOfBigShips;
            this.numOfMiddleShips = numOfMiddleShips;
            this.numOfSmallShips = numOfSmallShips;
            this.allShipsDestroyed = false;
        }
        public ShipDataBase() { }

        /// <summary>
        /// Задає значення змінної numOfVeryBigShips
        /// </summary>
        /// <param name="numOfVeryBigShips">Кількість діючих 4 палубних кораблів</param>
        public void SetNumOfVeryBigShips(int numOfVeryBigShips) {
            if (numOfVeryBigShips > 0) this.numOfVeryBigShips = numOfVeryBigShips;
            else this.numOfVeryBigShips = 0;
        }

        /// <summary>
        /// Задає значення змінної numOfBigShips
        /// </summary>
        /// <param name="numOfBigShips">Кількість діючих 3 палубних кораблів</param>
        public void SetNumOfBigShips(int numOfBigShips) {
            if (numOfBigShips > 0) this.numOfBigShips = numOfBigShips;
            else this.numOfBigShips = 0;
        }

        /// <summary>
        /// Задає значення змінної numOfMiddleShips
        /// </summary>
        /// <param name="numOfMiddleShips">Кількість діючих 2 палубних кораблів</param>
        public void SetNumOfMiddleShips(int numOfMiddleShips) {
            if (numOfMiddleShips > 0) this.numOfMiddleShips = numOfMiddleShips;
            else this.numOfMiddleShips = 0;
        }

        /// <summary>
        /// Задає значення змінної numOfSmallShips
        /// </summary>
        /// <param name="numOfSmallShips">Кількість діючих 1 палубних кораблів</param>
        public void SetNumOfSmallShips(int numOfSmallShips) {
            if (numOfSmallShips > 0) this.numOfSmallShips = numOfSmallShips;
            else this.numOfSmallShips = 0;
        }

        /// <summary>
        /// Повертає значення змінної allShipsDestroyed
        /// </summary>
        /// <returns>allShipsDestroyed – булева змінна, що надає значення чи є знищенними усі кораблі</returns>
        public bool GetAllShipsDestroyed() {
            if (numOfVeryBigShips == 0 && numOfBigShips == 0 && numOfMiddleShips == 0 && numOfSmallShips == 0) this.allShipsDestroyed = true;
            return this.allShipsDestroyed;
        }

        public void SetAllShipsDestroyed(bool allShipsDestroyed) {
            this.allShipsDestroyed = allShipsDestroyed;
        }
    }
}

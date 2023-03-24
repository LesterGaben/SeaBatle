using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace SeaBatle {
    /// <summary>
    /// Клас комп'ютера, який виконує всі дії бота в грі
    /// </summary>
    public class Bot {
        private const int buttonSize = 50;
        private const int mapSize = 10;
        private const int iOfShipCell = 1;
        private const int iOfEmptyCell = 0;
        private readonly int enemyMapRetreat;
        public readonly int retreat = 760;
        public double[,] myMap { get; set; }
        public Button[,] myButtons { get; set; }
        public double[,] enemyMap { get; set; }
        public Button[,] enemyButtons { get; set;}
        public List<Ship> myShips { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="enemyMap">Циферна карта противника бота</param>
        /// <param name="enemyButtons">Карта кнопок противника бота</param>
        /// <param name="enemyMapRetreat">Відступ карти противника від початку вікна вправо</param>
        public Bot(double[,] enemyMap, Button[,] enemyButtons, int enemyMapRetreat) {
            myMap = new double[mapSize, mapSize];
            myButtons = new Button[mapSize, mapSize];
            this.enemyMap = enemyMap;
            this.enemyButtons = enemyButtons;
            myShips = new List<Ship>();
            this.enemyMapRetreat = enemyMapRetreat;
            CreateMap();
            PutShipOnMap();
        }

        /// <summary>
        /// Конструктор №2
        /// </summary>
        /// <param name="buttons">Матриця кнопок карти</param>
        /// <param name="map">Матриця цифер карти</param>
        public Bot(Button[,] buttons, double[,] map) {
            this.myButtons = buttons;
            this.myMap = map;
            myShips = new List<Ship>();
        }

        public Bot(double[,] enemyMap, Button[,] enemyButtons, int enemyMapRetreat, double[,] myMap, Button[,] myButtons, List<Ship> myShips) {
            this.enemyMap = enemyMap;
            this.enemyButtons = enemyButtons;
            this.enemyMapRetreat = enemyMapRetreat;
            this.myMap = myMap;
            this.myButtons = myButtons;
            this.myShips = myShips;
        }

        /// <summary>
        /// Створює пусту мапу
        /// </summary>
        public void CreateMap() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    myMap[i, j] = 0;
                    Button button = new Button {
                        Location = new Point(retreat + j * buttonSize, 60 + i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                        BackColor = Color.White
                    };
                    myButtons[i, j] = button;
                }
            }
        }

        /// <summary>
        /// Розміщує кораблі на мапу
        /// </summary>
        public void PutShipOnMap() {
            for (int i = 4; i >= 1; i--) {
                ConfigureShips(i);
            }
        }

        /// <summary>
        /// Створює кораблі
        /// </summary>
        /// <param name="size">Розмір корабля</param>
        private void ConfigureShips(int size) {
            int posX = 0, posY = 0;
            bool horizontally, shipIsPutted;
            for (int i = 0; i <= 4 - size; i++) {
                shipIsPutted = false;
                while (!shipIsPutted) {
                    Ship ship = new Ship(size);
                    Random random = new Random(((int)DateTime.Now.Ticks));
                    horizontally = Convert.ToBoolean(random.Next(0, 2));
                    posX = random.Next(0, mapSize);
                    posY = random.Next(0, mapSize);
                    if (horizontally) {
                        var maps = ship.PutShipHorizontally(myMap, myButtons, posX, posY, ref shipIsPutted);
                        ship.orientation = "Horizontally";
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    else {
                        var maps = ship.PutShipVertically(myMap, myButtons, posX, posY, ref shipIsPutted);
                        ship.orientation = "Vertically";
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    if (shipIsPutted) myShips.Add(ship);
                }
            }
        }

        /// <summary>
        /// Змінює інформацію про стан кораблів противника комп'ютера
        /// </summary>
        /// <param name="ship">Корабель</param>
        /// <param name="enemyShipsData">База данних кораблів противника бота</param>
        private void ChangeInfAboutEnemyShips(Ship ship, ShipDataBase enemyShipsData) {
            if (ship.size == 4) enemyShipsData.SetNumOfVeryBigShips(enemyShipsData.numOfVeryBigShips - 1);
            else if (ship.size == 3) enemyShipsData.SetNumOfBigShips(enemyShipsData.numOfBigShips - 1);
            else if (ship.size == 2) enemyShipsData.SetNumOfMiddleShips(enemyShipsData.numOfMiddleShips - 1);
            else if (ship.size == 1) enemyShipsData.SetNumOfSmallShips(enemyShipsData.numOfSmallShips - 1);
            ship.destructionRecorded = true;
            if (enemyShipsData.GetAllShipsDestroyed()) MessageBox.Show("Гру закінчено. Виграв комп'ютер!", "Не повезло!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// Стріляє по мапі противника комп'ютера
        /// </summary>
        /// <param name="enemyShipsData">База данних кораблів противника бота</param>
        /// <param name="enemyShips">Масив кораблів противника бота</param>
        /// <returns>hit – булеве значення вдалості попадання по мапі противника комп’ютера</returns>
        public bool Shoot(ShipDataBase enemyShipsData, List<Ship> enemyShips) {
            bool hit = false;

            Random random = new Random(((int)DateTime.Now.Ticks));

            int posX = random.Next(0, mapSize);
            int posY = random.Next(0, mapSize);
            while(enemyButtons[posX, posY].Text == "X") {
                posX = random.Next(0, mapSize);
                posY = random.Next(0, mapSize);
            }
            if ((int)enemyMap[posX, posY] == iOfEmptyCell) {
                hit = false;
                MarkEmptyMapCell(posX, posY);
            }
            else {
                hit = true;
                MarkDestroyedShipPart(posX, posY);
            }
            double iOfDestroyedSmallShip = 3.1;
            if(hit && enemyMap[posX, posY] != iOfDestroyedSmallShip) {
                int i = 0;
                while(posY - 1 - i >= 0 && (int)enemyMap[posX, posY - 1 - i] == iOfShipCell) {
                    MarkDestroyedShipPart(posX, posY - 1 - i);
                    i++;
                }
                i = 0;
                while(posY + 1 + i < mapSize && (int)enemyMap[posX, posY + 1 + i] == iOfShipCell) {
                    MarkDestroyedShipPart(posX, posY + 1 + i);
                    i++;
                }
                i = 0;
                while (posX - 1 - i >= 0 && (int)enemyMap[posX - 1 - i, posY] == iOfShipCell) {
                    MarkDestroyedShipPart(posX - 1 - i, posY);
                    i++;
                }
                i = 0;
                while (posX + 1 + i < mapSize && (int)enemyMap[posX + 1 + i, posY] == iOfShipCell) {
                    MarkDestroyedShipPart(posX + 1 + i, posY);
                    i++;
                }
            }
            for (int k = 0; k < mapSize; k++) {
                var maps = enemyShips[k].MarkFieldsNearDestroyedShip(enemyMap, enemyButtons, enemyMapRetreat, enemyMapRetreat);
                if (enemyShips[k].destroyed && !enemyShips[k].destructionRecorded) ChangeInfAboutEnemyShips(enemyShips[k], enemyShipsData);
                enemyMap = maps.Item1;
                enemyButtons = maps.Item2;
            }
            if (enemyShipsData.GetAllShipsDestroyed()) {
                hit = false;
            }
            return hit;
        }

        /// <summary>
        /// Відмічує на мапі противника комп'ютера знищену частину корабля
        /// </summary>
        /// <param name="posX">Індекс рядка матриці</param>
        /// <param name="posY">Індекс стовпця матриці</param>
        private void MarkDestroyedShipPart(int posX, int posY) {
            enemyButtons[posX, posY].Text = "X";
            enemyButtons[posX, posY].Font = new Font("Times New Roman", 22F);
            enemyMap[posX, posY] += 2;
        }

        /// <summary>
        /// Відмічує на мапі противника бота попадання по пустій клітинці мапи
        /// </summary>
        /// <param name="posX">Індекс рядка матриці</param>
        /// <param name="posY">Індекс стовпця матриці</param>
        private void MarkEmptyMapCell(int posX, int posY) {
            enemyButtons[posX, posY].Text = "X";
            enemyButtons[posX, posY].Font = new Font("Times New Roman", 8F);
            enemyButtons[posX, posY].BackColor = Color.CornflowerBlue;
            enemyMap[posX, posY] += 2;
        }
    }
}

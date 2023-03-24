using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SeaBatle {
    /// <summary>
    /// Клас корабель
    /// </summary>
    public class Ship {
        
        public double index { get; set;}
        
        public bool destroyed { get;  set; }
        public bool destructionRecorded { get; set; }
        public int size { get; set; }
        public List<Button> buttons { get; set; }
        public string orientation { get; set; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="size">розмір корабля</param>
        public Ship(int size) {
            this.size = size;
            this.buttons = new List<Button>();
            index = (double)size / 10 + 1;
            destroyed = false;
            destructionRecorded = false;
        }
        public Ship() { }

        /// <summary>
        /// Розміщує корабель горизонтально
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="buttonsMap">Матриця кнопок карти</param>
        /// <param name="x">Координати початку корабля по Х</param>
        /// <param name="y">координати початку корабля по Y</param>
        /// <param name="shipIsPutted">Чи є поставленим корабель</param>
        /// <returns>Матриці цифер та кнопок ігрової карти</returns>
        public (double[,], Button[,]) PutShipHorizontally(double[,] map, Button[,] buttonsMap, int x, int y, ref bool shipIsPutted) {
            if (!WillTheShipGoOffTheMapHorizontally(map, x, y)) {
                for (int i = 0; i < this.size; i++) {
                    buttonsMap[y, x + 1 * i].BackColor = Color.Red;
                    map[y, x + 1 * i] = index;
                    buttons.Add(buttonsMap[y, x + 1 * i]);
                }
                var maps = MarkEmptyFieldsHorizontally(map, buttonsMap, y, x);
                map = maps.Item1;
                buttonsMap = maps.Item2;
                shipIsPutted = true;
                return (map, buttonsMap);
            }
            shipIsPutted = false;
            return (map, buttonsMap);
        }

        /// <summary>
        /// Розміщує корабель вертикально
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="buttonsMap">Матриця кнопок карти</param>
        /// <param name="x">Координати початку корабля по Х</param>
        /// <param name="y">координати початку корабля по Y</param>
        /// <param name="shipIsPutted">Чи є поставленим корабель</param>
        /// <returns>Матриці цифер та кнопок ігрової карти</returns>
        public (double[,], Button[,]) PutShipVertically(double[,] map, Button[,] buttonsMap, int x, int y, ref bool shipIsPutted) {
            if (!WillTheShipGoOffTheMapVertically(map, x, y)) {
                for (int i = 0; i < this.size; i++) {
                    buttonsMap[y + 1 * i, x].BackColor = Color.Red;
                    map[y + 1 * i, x] = index;
                    buttons.Add(buttonsMap[y + 1 * i, x]);
                }
                shipIsPutted = true;
                var maps = MarkEmptyFieldsVertically(map, buttonsMap, y, x);
                map = maps.Item1;
                buttonsMap = maps.Item2;
                return (map, buttonsMap);
            }
            shipIsPutted = false;
            return (map, buttonsMap);
        }

        /// <summary>
        /// Дізнається чи вийде корабель, що розташований горизонтально за межі мапи
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="x">Координати початку корабля по Х</param>
        /// <param name="y">Координати початку корабля по Y</param>
        /// <returns>True або false</returns>
        private bool WillTheShipGoOffTheMapHorizontally(double[,] map, int x, int y) {
            for (int i = 0; i < this.size; i++) {
                if (x + 1 * i >= Math.Sqrt(map.Length) ||
                    map[y, x + 1 * i] != 0) return true;
            }
            return false;
        }

        /// <summary>
        /// Дізнається чи вийде корабель,що розташований вертикально за межі мапи
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="x">Координати початку корабля по Х</param>
        /// <param name="y">Координати початку корабля по Y</param>
        /// <returns>True або false</returns>
        private bool WillTheShipGoOffTheMapVertically(double[,] map, int x, int y) {
            for (int i = 0; i < this.size; i++) {
                if (y + 1 * i >= Math.Sqrt(map.Length) ||
                   map[y + 1 * i, x] != 0) return true;
            }
            return false;
        }

        /// <summary>
        /// Позначає спеціальним індексом зону навколо корабля, який розташований горизонтально, в радіусі одної клітинки
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="buttonsMap">Матриця кнопок карти</param>
        /// <param name="line">Індекс рядка на якому розташований початок корабля</param>
        /// <param name="col">Індекс стовпця на якому розташований початок корабля</param>
        /// <returns>Матриці цифер та кнопок ігрової карти</returns>
        private (double[,], Button[,]) MarkEmptyFieldsHorizontally(double[,] map, Button[,] buttonsMap, int line, int col) {
            int length = (int)Math.Sqrt(map.Length);
            for (int i = 0; i < 3; i++) {
                if (col - 1 >= 0 && line - 1 + i < length && line - 1 + i >= 0 && map[line - 1 + i, col - 1] == 0) {
                    map[line - 1 + i, col - 1] = 0.1;
                }
                if (col + size < length && line - 1 + i < length && line - 1 + i >= 0 && map[line - 1 + i, col + size] == 0) {
                    map[line - 1 + i, col + size] = 0.1;
                }
            }
            for (int i = 0; i < this.size; i++) {
                if (line - 1 >= 0 && col + i < length && map[line - 1, col + i] == 0) {
                    map[line - 1, col + i] = 0.1;
                }
                if (line + 1 < length && col + i < length && map[line + 1, col + i] == 0) {
                    map[line + 1, col + i] = 0.1;
                }
            }
            return (map, buttonsMap);
        }

        /// <summary>
        /// Позначає спеціальним індексом зону навколо корабля, який розташований вертикально, в радіусі одної клітинки
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="buttonsMap">Матриця кнопок карти</param>
        /// <param name="line">Індекс рядка на якому розташований початок корабля</param>
        /// <param name="col">Індекс стовпця на якому розташований початок корабля</param>
        /// <returns>Матриці цифер та кнопок ігрової карти</returns>
        private (double[,], Button[,]) MarkEmptyFieldsVertically(double[,] map, Button[,] buttonsMap, int line, int col) {
            int length = (int)Math.Sqrt(map.Length);
            for (int i = 0; i < 3; i++) {
                if (line - 1 >= 0 && col - 1 + i < length && col - 1 + i >= 0 && map[line - 1, col - 1 + i] == 0) {
                    map[line - 1, col - 1 + i] = 0.1;
                }
                if (line + size < length && col - 1 + i < length && col - 1 + i >= 0 && map[line + size, col - 1 + i] == 0) {
                    map[line + size, col - 1 + i] = 0.1;
                }
            }
            for (int i = 0; i < this.size; i++) {
                if (col - 1 >= 0 && line + i < length && map[line + i, col - 1] == 0) {
                    map[line + i, col - 1] = 0.1;
                }
                if (col + 1 < length && line + i < length && map[line + i, col + 1] == 0) {
                    map[line + i, col + 1] = 0.1;
                }
            }
            return (map, buttonsMap);
        }

        /// <summary>
        /// Позначає зону навколо знищеного корабля в радіусі однієї клітинки знищеною
        /// </summary>
        /// <param name="map">Матриця цифер карти</param>
        /// <param name="buttonsMap">Матриця кнопок карти</param>
        /// <param name="xRetreat">відступ мапи по Х</param>
        /// <param name="yRetreat">відступ мапи по Y</param>
        /// <returns>Матриці цифер та кнопок ігрової карти</returns>
        public (double[,], Button[,]) MarkFieldsNearDestroyedShip(double[,] map, Button[,] buttonsMap, int xRetreat, int yRetreat) {
            if (this.destroyed) return (map, buttonsMap);
            destroyed = false;
            int length = (int)Math.Sqrt(map.Length);
            int col = (int)(this.buttons[0].Location.X - xRetreat) / buttons[0].Size.Width, line = (int)(this.buttons[0].Location.Y - yRetreat) / buttons[0].Size.Width;
            if (this.orientation == "Horizontally") {
                for (int i = 0; i < this.size; i++) {
                    if (col + i < length) {
                        if (map[line, col + i] == index + 2) destroyed = true;
                        else {
                            destroyed = false;
                            this.destroyed = destroyed;
                            break;
                        }
                    }
                }
                if (this.destroyed) {
                    for (int i = 0; i < 3; i++) {
                        if (col - 1 >= 0 && line - 1 + i < length && line - 1 + i >= 0 && map[line - 1 + i, col - 1] == 0.1) {
                            map[line - 1 + i, col - 1] += 2;
                            buttonsMap[line - 1 + i, col - 1].BackColor = Color.CornflowerBlue;
                            buttonsMap[line - 1 + i, col - 1].Text = "X";
                            buttonsMap[line - 1 + i, col - 1].Font = new Font("Times New Roman", 8F);
                        }
                        if (col + size < length && line - 1 + i < length && line - 1 + i >= 0 && map[line - 1 + i, col + size] == 0.1) {
                            map[line - 1 + i, col + size] += 2;
                            buttonsMap[line - 1 + i, col + size].BackColor = Color.CornflowerBlue;
                            buttonsMap[line - 1 + i, col + size].Text = "X";
                            buttonsMap[line - 1 + i, col + size].Font = new Font("Times New Roman", 8F);
                        }
                    }
                    for (int i = 0; i < this.size; i++) {
                        if (line - 1 >= 0 && col + i < length && map[line - 1, col + i] == 0.1) {
                            map[line - 1, col + i] += 2;
                            buttonsMap[line - 1, col + i].BackColor = Color.CornflowerBlue;
                            buttonsMap[line - 1, col + i].Text = "X";
                            buttonsMap[line - 1, col + i].Font = new Font("Times New Roman", 8F);
                        }
                        if (line + 1 < length && col + i < length && map[line + 1, col + i] == 0.1) {
                            map[line + 1, col + i] += 2;
                            buttonsMap[line + 1, col + i].BackColor = Color.CornflowerBlue;
                            buttonsMap[line + 1, col + i].Text = "X";
                            buttonsMap[line + 1, col + i].Font = new Font("Times New Roman", 8F);
                        }
                    }
                }
            }
            else if (this.orientation == "Vertically") {
                for (int i = 0; i < this.size; i++) {
                    if (line + i < length) {
                        if (map[line + i, col] > index) destroyed = true;
                        else {
                            destroyed = false;
                            break;
                        }
                    }
                }
                if (this.destroyed) {
                    for (int i = 0; i < 3; i++) {
                        if (line - 1 >= 0 && col - 1 + i < length && col - 1 + i >= 0 && map[line - 1, col - 1 + i] == 0.1) {
                            map[line - 1, col - 1 + i] += 2;
                            buttonsMap[line - 1, col - 1 + i].BackColor = Color.CornflowerBlue;
                            buttonsMap[line - 1, col - 1 + i].Text = "X";
                            buttonsMap[line - 1, col - 1 + i].Font = new Font("Times New Roman", 8F);
                        }
                        if (line + size < length && col - 1 + i < length && col - 1 + i >= 0 && map[line + size, col - 1 + i] == 0.1) {
                            map[line + size, col - 1 + i] += 2;
                            buttonsMap[line + size, col - 1 + i].BackColor = Color.CornflowerBlue;
                            buttonsMap[line + size, col - 1 + i].Text = "X";
                            buttonsMap[line + size, col - 1 + i].Font = new Font("Times New Roman", 8F);
                        }
                    }
                    for (int i = 0; i < this.size; i++) {
                        if (col - 1 >= 0 && line + i < length && map[line + i, col - 1] == 0.1) {
                            map[line + i, col - 1] += 2;
                            buttonsMap[line + i, col - 1].BackColor = Color.CornflowerBlue;
                            buttonsMap[line + i, col - 1].Text = "X";
                            buttonsMap[line + i, col - 1].Font = new Font("Times New Roman", 8F);
                        }
                        if (col + 1 < length && line + i < length && map[line + i, col + 1] == 0.1) {
                            map[line + i, col + 1] += 2;
                            buttonsMap[line + i, col + 1].BackColor = Color.CornflowerBlue;
                            buttonsMap[line + i, col + 1].Text = "X";
                            buttonsMap[line + i, col + 1].Font = new Font("Times New Roman", 8F);
                        }
                    }
                }
            }
            
            return (map, buttonsMap);
        }
    }
}

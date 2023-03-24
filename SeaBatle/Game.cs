using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SeaBatle {
    /// <summary>
    /// Ігрове вікно програми
    /// </summary>
    public partial class Game : Form {

        private readonly Form mainForm;
        private readonly ShipDataBase myShipsData;
        private readonly ShipDataBase enemyShipsData;
        private readonly Bot bot;

        private const int yRetreat = 60;

        private const int xRetreat = 60;
        private const int mapSize = 10;
        private const int buttonSize = 50;
        private const int iOfEmptyCell = 0;
        private const int iOfShipCell = 1;
        private const string symbols = "АБВГҐДЕЄЖЗ";
        private Label veryBigShipInf;
        private Label bigShipInf;
        private Label middleShipInf;
        private Label smallShipInf;

        private double[,] myMap;
        private Button[,] myButtons;
        private double[,] enemyMap;
        private Button[,] enemyButtons;
        private List<Ship> myShips;
        private List<Ship> enemyShips;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mainForm">Головне вікно всієї програми</param>
        /// <param name="myMap">Матриця цифер мапи гравця</param>
        /// <param name="myButtons">Матриця кнопок мапи гравця</param>
        /// <param name="myShips">Масив кораблів гравця</param>
        public Game(Form mainForm, double[,] myMap, Button[,] myButtons, List<Ship> myShips) {
            this.mainForm = mainForm;
            this.myMap = myMap;
            this.myButtons = myButtons;
            this.myShips = myShips;
            this.FormClosed += new FormClosedEventHandler(ProgramExit);
            this.bot = new Bot(this.myMap, this.myButtons, xRetreat);
            this.enemyMap = bot.myMap;
            this.enemyShips = bot.myShips;
            this.enemyButtons = bot.myButtons;
            this.myShipsData = new ShipDataBase(1, 2, 3, 4);
            this.enemyShipsData = new ShipDataBase(1, 2, 3, 4);
            DisplayPlayerMap();
            PrepearingBotMapForGame();
            DisplayEnemyMap();
            CreateInfoFields();
            InitializeComponent();
            this.Width = 1700;
        }

        public Game (Form mainForm, double[,] myMap, Button[,] myButtons, List<Ship> myShips, ShipDataBase myData, double[,] enemyMap, Button[,] enemyButtons, List<Ship> enemyShips, ShipDataBase enemyData) {
            this.mainForm = mainForm;
            this.myMap = myMap;
            this.myButtons = myButtons;
            this.myShips = myShips;
            this.myShipsData = myData;
            this.FormClosed += new FormClosedEventHandler(ProgramExit);
            this.enemyMap = enemyMap;
            this.enemyButtons = enemyButtons;
            this.enemyShips = enemyShips;
            this.enemyShipsData = enemyData;
            this.bot = new Bot(this.myMap, this.myButtons, xRetreat, this.enemyMap, this.enemyButtons, this.enemyShips);
            DisplayPlayerMap();
            PrepearingBotMapForGame();
            DisplayEnemyMap1();
            CreateInfoFields();
            InitializeComponent();
            this.Width = 1700;
        }

        public void InputProgressInFile(string file_name, double[,] numMap, List<Ship> ships, ShipDataBase shipDataBase, int xRetreat) {
            string text = "";
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    text += Convert.ToString(numMap[i, j]) + ";";
                }
                text += "\n";
            }
            string temp = "";
            foreach (Ship ship in ships) {

                temp += ship.index.ToString() + ";" + Convert.ToInt32(ship.destroyed).ToString() + ";" + Convert.ToInt32(ship.destructionRecorded).ToString() + ";" + ship.size.ToString() + ";";
                string temp1 = "";
                foreach (Button button in ship.buttons) {

                    int x = (int)(button.Location.X - xRetreat) / buttonSize, y = (int)(button.Location.Y - yRetreat) / buttonSize;
                    temp1 += y.ToString() + "," + x.ToString() + ":";
                }
                temp1 = temp1.Remove(temp1.Length - 1);
                temp += temp1 + ";";
                temp += ship.orientation;
                temp += "\n";
            }
            text += temp;
            text += shipDataBase.numOfVeryBigShips + ";" + shipDataBase.numOfBigShips + ";" + shipDataBase.numOfMiddleShips + ";" + shipDataBase.numOfSmallShips + ";" + Convert.ToInt32(shipDataBase.GetAllShipsDestroyed());
            File.WriteAllText(file_name, text);
        }
        private void saveButton_Click(object sender, EventArgs e) {
            if (!(myShipsData.GetAllShipsDestroyed() || enemyShipsData.GetAllShipsDestroyed())) {
                
                InputProgressInFile("BotProgress.txt", enemyMap, enemyShips, enemyShipsData, bot.retreat);
                InputProgressInFile("PersonProgress.txt", myMap, myShips, myShipsData, xRetreat);
            }
            else {
                MessageBox.Show("Ви не можете зберегти закінчену гру");
            }
        }


        /// <summary>
        /// Виводить на екран інформаційні поля про кількість діючих кораблів противника
        /// </summary>
        private void CreateInfoFields() {
            Label label = new Label() {
                Text = "К-сть діючих кораблів противника:",
                Font = new Font("Times New Roman", 14F),
                Size = new Size(Convert.ToInt32(buttonSize * 7.5), buttonSize),
                Location = new Point(1300, 50),
            };
            this.Controls.Add(label);
            veryBigShipInf = new Label() {
                Text = "4-палубні кораблі: " + enemyShipsData.numOfVeryBigShips.ToString(),
                Font = new Font("Times New Roman", 13F),
                Size = new Size(buttonSize * 5, buttonSize),
                Location = new Point(1350, 100),
            };
            this.Controls.Add(veryBigShipInf);
            bigShipInf = new Label() {
                Text = "3-палубні кораблі: " + enemyShipsData.numOfBigShips.ToString(),
                Font = new Font("Times New Roman", 13F),
                Size = new Size(buttonSize * 5, buttonSize),
                Location = new Point(1350, 150),
            };
            this.Controls.Add(bigShipInf);
            middleShipInf = new Label() {
                Text = "2-палубні кораблі: " + enemyShipsData.numOfMiddleShips.ToString(),
                Font = new Font("Times New Roman", 13F),
                Size = new Size(buttonSize * 5, buttonSize),
                Location = new Point(1350, 200),
            };
            this.Controls.Add(middleShipInf);
            smallShipInf = new Label() {
                Text = "1-палубні кораблі: " + enemyShipsData.numOfSmallShips.ToString(),
                Font = new Font("Times New Roman", 13F),
                Size = new Size(buttonSize * 5, buttonSize),
                Location = new Point(1350, 250),
            };
            this.Controls.Add(smallShipInf);
        }

        /// <summary>
        /// Виводить на екран мапу гравця
        /// </summary>
        private void DisplayPlayerMap() {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    this.Controls.Add(myButtons[i, j]);
                    SignMapCoordinates(i, j, xRetreat);
                }
            }
            Label label = new Label {
                Text = "Карта гравця",
                Location = new Point(220, 570),
                Size = new Size(buttonSize * 5, buttonSize),
                Font = new Font("Times New Roman", 14F)
            };
            this.Controls.Add(label);
        }

        /// <summary>
        /// Виводить на екран мапу противника
        /// </summary>
        private void DisplayEnemyMap() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    enemyButtons[i, j].BackColor = Color.White;
                    this.Controls.Add(enemyButtons[i, j]);
                    SignMapCoordinates(i, j, bot.retreat);
                }
            }
            Label label = new Label {
                Text = "Карта противника",
                Location = new Point(890, 570),
                Size = new Size(buttonSize * 5, buttonSize),
                Font = new Font("Times New Roman", 14F)
            };
            this.Controls.Add(label);
        }

        private void DisplayEnemyMap1() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    this.Controls.Add(enemyButtons[i, j]);
                    SignMapCoordinates(i, j, bot.retreat);
                }
            }
            Label label = new Label {
                Text = "Карта противника",
                Location = new Point(890, 570),
                Size = new Size(buttonSize * 5, buttonSize),
                Font = new Font("Times New Roman", 14F)
            };
            this.Controls.Add(label);
        }


        /// <summary>
        /// Підписує координати ігрових карт
        /// </summary>
        /// <param name="i">Індекс рядка матриці карти</param>
        /// <param name="j">Індекс стовпця матриці карти</param>
        /// <param name="xRetreat">Відступ мапи від початку вікна вправо</param>
        private void SignMapCoordinates(int i, int j, int xRetreat) {
            if (i == 0) {
                Button temp = new Button {
                    Location = new Point(xRetreat + j * buttonSize, yRetreat - buttonSize + i * buttonSize),
                    Size = new Size(buttonSize, buttonSize),
                    BackColor = Color.Gray,
                    Text = symbols[j].ToString(),
                    Font = new Font("Times New Roman", 14F)
                };
                this.Controls.Add(temp);
                if (j == 0) {
                    Button temp1 = new Button {
                        Location = new Point(xRetreat - buttonSize + j * buttonSize, yRetreat - buttonSize + i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                        BackColor = Color.Gray
                    };
                    this.Controls.Add(temp1);
                }
            }
            if (j == 0) {
                Button temp2 = new Button {
                    Location = new Point(xRetreat - buttonSize + j * buttonSize, yRetreat + i * buttonSize),
                    Size = new Size(buttonSize, buttonSize),
                    BackColor = Color.Gray,
                    Text = (i + 1).ToString(),
                    Font = new Font("Times New Roman", 14F)
                };
                this.Controls.Add(temp2);
            }
        }

        /// <summary>
        /// Готує мапу противника до ігрового процесу
        /// </summary>
        private void PrepearingBotMapForGame() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    enemyButtons[i, j].Click += new EventHandler(GameProcess);
                }
            }
        }

        /// <summary>
        /// Змінює інформацію про діючі кораблі противника
        /// </summary>
        /// <param name="ship">Корабель</param>
        private void ChangeInfAboutEnemyShips(Ship ship) {
            if (ship.size == 4) {
                ReduceNumOfWholeShips(veryBigShipInf);
                enemyShipsData.SetNumOfVeryBigShips(enemyShipsData.numOfVeryBigShips - 1);
            }
            else if (ship.size == 3) {
                ReduceNumOfWholeShips(bigShipInf);
                enemyShipsData.SetNumOfBigShips(enemyShipsData.numOfBigShips - 1);
            }
            else if (ship.size == 2) {
                ReduceNumOfWholeShips(middleShipInf);
                enemyShipsData.SetNumOfMiddleShips(enemyShipsData.numOfMiddleShips - 1);
            }
            else if (ship.size == 1) {
                ReduceNumOfWholeShips(smallShipInf);
                enemyShipsData.SetNumOfSmallShips(enemyShipsData.numOfSmallShips - 1);
            }
            ship.destructionRecorded = true;
            if (enemyShipsData.GetAllShipsDestroyed()) MessageBox.Show("Гру закінчено. Ви виграли!!!", "Вітаємо!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// Змінює кількість діючих кораблів противника на інформаційних полях
        /// </summary>
        /// <param name="label">Інформаційна строка</param>
        private void ReduceNumOfWholeShips(Label label) {
            string temp = "";
            int numOfShips = Convert.ToInt32(label.Text[label.Text.Length - 1].ToString());
            if (numOfShips == 0) {
                return;
            }
            for (int i = 0; i < label.Text.Length - 1; i++) {
                temp += label.Text[i];
            }
            temp += (numOfShips - 1).ToString();
            label.Text = temp;
        }

        /// <summary>
        /// Реалізує управління ігровим процесом 
        /// </summary>
        /// <param name="sender">Кнопка на мапі на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась при натисканні кнопки</param>
        private void GameProcess(object sender, EventArgs e) {
            if (enemyShipsData.GetAllShipsDestroyed() || myShipsData.GetAllShipsDestroyed()) return;
            Button pressedButton = sender as Button;

            bool playerTurn = Shoot(pressedButton);
            for (int i = 0; i < enemyShips.Count; i++) {
                var maps = enemyShips[i].MarkFieldsNearDestroyedShip(enemyMap, enemyButtons, bot.retreat, yRetreat);
                if (enemyShips[i].destroyed && !enemyShips[i].destructionRecorded) ChangeInfAboutEnemyShips(enemyShips[i]);
                enemyMap = maps.Item1;
                enemyButtons = maps.Item2;
            }
            if (!playerTurn) {
                while (bot.Shoot(myShipsData, myShips));
            }
            if (myShipsData.GetAllShipsDestroyed()) ShowNotDestroyedEnemyShips();
        }

        /// <summary>
        /// Виводить на екран не знищені кораблі противника, у випадку виграшу противника
        /// </summary>
        private void ShowNotDestroyedEnemyShips() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    if((int)enemyMap[i, j] == iOfShipCell) {
                        enemyButtons[i, j].BackColor = Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// Стріляє по мапі противника
        /// </summary>
        /// <param name="pressedButton">Натиснута кнопка(клітинка мапи противника)</param>
        /// <returns>hit – булеве значення вдалості попадання по мапі противника</returns>
        private bool Shoot(Button pressedButton) {
            bool hit = false;
            int i = (int)(pressedButton.Location.Y - yRetreat) / buttonSize;
            int j = (int)(pressedButton.Location.X - bot.retreat) / buttonSize;
            if (enemyButtons[i, j].Text == "X") hit = true;
            else if ((int)enemyMap[i, j] == iOfEmptyCell) {
                hit = false;
                enemyButtons[i, j].Text = "X";
                enemyButtons[i, j].Font = new Font("Times New Roman", 8F);
                enemyButtons[i, j].BackColor = Color.CornflowerBlue;
                enemyMap[i, j] += 2;
            }
            else if ((int)enemyMap[i, j] == iOfShipCell) {
                hit = true;
                enemyButtons[i, j].Text = "X";
                enemyButtons[i, j].BackColor = Color.Red;
                enemyButtons[i, j].Font = new Font("Times New Roman", 22F);
                enemyMap[i, j] += 2;
            }
            this.Controls.Add(enemyButtons[i, j]);
            return hit;
        }

        /// <summary>
        /// Виходить зі всієї програми
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась при натисканні кнопки</param>
        private void ProgramExit(object sender, EventArgs e) {
            mainForm.Close();
        }

        /// <summary>
        /// Починає гру заново
        /// </summary>
        /// <param name="sender">Кнопка "Почати заново гру" на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась при натисканні кнопки</param>
        private void gameRestartButton_Click(object sender, EventArgs e) {
            PreGameWindow pregameWindow = new PreGameWindow(mainForm);
            pregameWindow.Show();
            this.Hide();
        }

        
    }
}

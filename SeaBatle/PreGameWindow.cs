using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SeaBatle {
    /// <summary>
    /// Вікно програми на якому користувач розміщує кораблі на мапі
    /// </summary>
    public partial class PreGameWindow : Form {

        private readonly Form mainForm;
        private const int mapSize = 10;
        private const int buttonSize = 50;
        private const int retreat = 60;
        private readonly ShipDataBase shipData;
        private const int iOfEmptyCell = 0;
        private const int iOfShipCell = 1;

        private const string symbols = "АБВГҐДЕЄЖЗ";
        private double[,] myMap;
        private Button[,] myButtons;

        private List<Ship> myShips;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mainForm">головне вікно програми</param>
        public PreGameWindow(Form mainForm) {
            this.mainForm = mainForm;
            this.myShips = new List<Ship>();
            this.myMap = new double[mapSize, mapSize];
            this.myButtons = new Button[mapSize, mapSize];
            this.shipData = new ShipDataBase(1, 2, 3, 4);
            this.FormClosed += new FormClosedEventHandler(ProgramExit);
            CreateMap();
            InitializeComponent();
            this.Width = mapSize * buttonSize * 2;
            this.Height = mapSize * buttonSize * 3 / 2;
        }

        public PreGameWindow(Form mainForm, double[,] myMap, Button[,] myButtons, List<Ship> myShips, ShipDataBase myData) {
            this.mainForm = mainForm;
            this.myMap = myMap;
            this.myButtons = myButtons;
            this.myShips = myShips;
            this.shipData = myData;
            this.FormClosed += new FormClosedEventHandler(ProgramExit);
            DisplayMap();
            InitializeComponent();
            this.Width = mapSize * buttonSize * 2;
            this.Height = mapSize * buttonSize * 3 / 2;
        }

        private void DisplayMap() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    this.Controls.Add(myButtons[i, j]);
                    SignMapCoordinates(i, j);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            string text = "";
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    text += Convert.ToString(myMap[i, j]) + ";";
                }
                text += "\n";
            }
            string temp = "";
            foreach (Ship ship in myShips) {

                temp += ship.index.ToString() + ";" + Convert.ToInt32(ship.destroyed).ToString() + ";" + Convert.ToInt32(ship.destructionRecorded).ToString() + ";" + ship.size.ToString() + ";";
                string temp1 = "";
                foreach (Button button in ship.buttons) {

                    int x = (int)(button.Location.X - retreat) / buttonSize, y = (int)(button.Location.Y - retreat) / buttonSize;
                    temp1 += y.ToString() + "," + x.ToString() + ":";
                }
                temp1 = temp1.Remove(temp1.Length - 1);
                temp += temp1 + ";";
                temp += ship.orientation;
                temp += "\n";
            }
            text += temp;
            text += shipData.numOfVeryBigShips + ";" + shipData.numOfBigShips + ";" + shipData.numOfMiddleShips + ";" + shipData.numOfSmallShips + ";" + Convert.ToInt32(shipData.GetAllShipsDestroyed());
            File.WriteAllText("Ships.txt", text);
        }

        /// <summary>
        /// Створює та виводить на екран пусту мапу користувача
        /// </summary>
        public void CreateMap() {
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    myMap[i, j] = iOfEmptyCell;

                    Button button = new Button {
                        Location = new Point(retreat + j * buttonSize, retreat + i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                        BackColor = Color.White
                    };
                    myButtons[i, j] = button;
                    this.Controls.Add(button);
                    SignMapCoordinates(i, j);
                }
            }
        }

        /// <summary>
        /// Виводить на екран координати ігрової мапи
        /// </summary>
        /// <param name="i">Індекс рядка матриці</param>
        /// <param name="j">Індекс рядка матриці</param>
        private void SignMapCoordinates(int i, int j) {
            if (i == 0) {
                Button temp = new Button {
                    Location = new Point(retreat + j * buttonSize, retreat - buttonSize + i * buttonSize),
                    Size = new Size(buttonSize, buttonSize),
                    BackColor = Color.Gray,
                    Text = symbols[j].ToString(),
                    Font = new Font("Times New Roman", 14F)
                };
                this.Controls.Add(temp);
                if (j == 0) {
                    Button temp1 = new Button {
                        Location = new Point(retreat - buttonSize + j * buttonSize, retreat - buttonSize + i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                        BackColor = Color.Gray
                    };
                    this.Controls.Add(temp1);
                }
            }
            if (j == 0) {
                Button temp2 = new Button {
                    Location = new Point(retreat - buttonSize + j * buttonSize, retreat + i * buttonSize),
                    Size = new Size(buttonSize, buttonSize),
                    BackColor = Color.Gray,
                    Text = (i + 1).ToString(),
                    Font = new Font("Times New Roman", 14F)
                };
                this.Controls.Add(temp2);
            }
        }


        /// <summary>
        /// Опрацьовує клацання на CheckedBox(прапорець) “4 палубний корабель”
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void veryBigShipCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (veryBigShipCheckBox.Checked) {
                bigShipCheckBox.Checked = false;
                middleShipCheckBox.Checked = false;
                smallShipCheckBox.Checked = false;
                if (shipData.numOfVeryBigShips >= iOfShipCell) {
                    for (int i = 0; i < mapSize; i++) {
                        for (int j = 0; j < mapSize; j++) {
                            myButtons[i, j].MouseDown += new MouseEventHandler(ConfigureVeryBigShip);
                        }
                    }
                }
                else {
                    MessageBox.Show("Уже використана максимальна кількість 4-х палубних кораблів!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    veryBigShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання по мапі, щоб розташувати 4 палубний корабель
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void ConfigureVeryBigShip(object sender, MouseEventArgs e) {

            Button pressedButton = sender as Button;
            if (veryBigShipCheckBox.Checked && shipData.numOfVeryBigShips >= iOfShipCell) {
                int x = (int)(pressedButton.Location.X - retreat) / buttonSize, y = (int)(pressedButton.Location.Y - retreat) / buttonSize;
                if (myMap[y, x] == iOfEmptyCell) {

                    Ship veryBigShip = new Ship(4);
                    bool shipIsPutted = false;
                    if (e.Button == MouseButtons.Left) {
                        veryBigShip.orientation = "Horizontally";
                        var maps = veryBigShip.PutShipHorizontally(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    else if (e.Button == MouseButtons.Right) {
                        veryBigShip.orientation = "Vertically";
                        var maps = veryBigShip.PutShipVertically(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    if (shipIsPutted) {
                        shipData.SetNumOfVeryBigShips(shipData.numOfVeryBigShips - 1);
                        myShips.Add(veryBigShip);
                    }
                    else MessageBox.Show("В даній точці не вийде розташувати корабель, бо його частина вийде за межі карти(або буде дуже близько до іншого корабля/наклададиметься на корабель)!!!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    veryBigShipCheckBox.Checked = false;
                }
                else {
                    MessageBox.Show("В даній точці по правилам гри ставити корабель не можна!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    veryBigShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання на CheckedBox(прапорець) “3 палубний корабель”
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void bigShipCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (bigShipCheckBox.Checked) {
                veryBigShipCheckBox.Checked = false;
                middleShipCheckBox.Checked = false;
                smallShipCheckBox.Checked = false;
                if (shipData.numOfBigShips >= iOfShipCell) {
                    for (int i = 0; i < mapSize; i++) {
                        for (int j = 0; j < mapSize; j++) {
                            myButtons[i, j].MouseDown += new MouseEventHandler(ConfigureBigShips);
                        }
                    }
                }
                else {
                    MessageBox.Show("Уже використана максимальна кількість 3-х палубних кораблів!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bigShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання по мапі, щоб розташувати 3 палубний корабель
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void ConfigureBigShips(object sender, MouseEventArgs e) {

            Button pressedButton = sender as Button;
            if (bigShipCheckBox.Checked && shipData.numOfBigShips >= iOfShipCell) {
                int x = (int)(pressedButton.Location.X - retreat) / buttonSize, y = (int)(pressedButton.Location.Y - retreat) / buttonSize;
                if (myMap[y, x] == iOfEmptyCell) {
                    
                    Ship bigShip = new Ship(3);
                    bool shipIsPutted = false;
                    if (e.Button == MouseButtons.Left) {
                        bigShip.orientation = "Horizontally";
                        var maps = bigShip.PutShipHorizontally(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    else if (e.Button == MouseButtons.Right) {
                        bigShip.orientation = "Vertically";
                        var maps = bigShip.PutShipVertically(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    if (shipIsPutted) {
                        shipData.SetNumOfBigShips(shipData.numOfBigShips - 1);
                        myShips.Add(bigShip);
                    }
                    else MessageBox.Show("В даній точці не вийде розташувати корабель, бо його частина вийде за межі карти(або буде дуже близько до іншого корабля/наклададиметься на корабель)!!!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bigShipCheckBox.Checked = false;
                }
                else {
                    MessageBox.Show("В даній точці по правилам гри ставити корабель не можна!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bigShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання на CheckedBox(прапорець) “2 палубний корабель”
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void middleShipCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (middleShipCheckBox.Checked) {
                veryBigShipCheckBox.Checked = false;
                bigShipCheckBox.Checked = false;
                smallShipCheckBox.Checked = false;
                if (shipData.numOfMiddleShips >= iOfShipCell) {
                    for (int i = 0; i < mapSize; i++) {
                        for (int j = 0; j < mapSize; j++) {
                            myButtons[i, j].MouseDown += new MouseEventHandler(ConfigureMiddleShips);
                        }
                    }
                }
                else {
                    MessageBox.Show("Уже використана максимальна кількість 2 палубних кораблів!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    middleShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання по мапі, щоб розташувати 2 палубний корабель
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void ConfigureMiddleShips(object sender, MouseEventArgs e) {

            Button pressedButton = sender as Button;
            if (middleShipCheckBox.Checked && shipData.numOfMiddleShips >= iOfShipCell) {
                int x = (int)(pressedButton.Location.X - retreat) / buttonSize, y = (int)(pressedButton.Location.Y - retreat) / buttonSize;
                if (myMap[y, x] == iOfEmptyCell) {

                    Ship middleShip = new Ship(2);
                    bool shipIsPutted = false;
                    if (e.Button == MouseButtons.Left) {
                        middleShip.orientation = "Horizontally";
                        var maps = middleShip.PutShipHorizontally(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    else if (e.Button == MouseButtons.Right) {
                        middleShip.orientation = "Vertically";
                        var maps = middleShip.PutShipVertically(myMap, myButtons, x, y, ref shipIsPutted);
                        myMap = maps.Item1;
                        myButtons = maps.Item2;
                    }
                    if (shipIsPutted) {
                        shipData.SetNumOfMiddleShips(shipData.numOfMiddleShips - 1);
                        myShips.Add(middleShip);
                    }
                    else MessageBox.Show("В даній точці не вийде розташувати корабель, бо його частина вийде за межі карти(або буде дуже близько до іншого корабля/наклададиметься на корабель)!!!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    middleShipCheckBox.Checked = false;
                }
                else {
                    MessageBox.Show("В даній точці по правилам гри ставити корабель не можна!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    middleShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання на CheckedBox(прапорець) “1 палубний корабель”
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void smallShipCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (smallShipCheckBox.Checked) {
                veryBigShipCheckBox.Checked = false;
                bigShipCheckBox.Checked = false;
                middleShipCheckBox.Checked = false;
                if (shipData.numOfSmallShips >= iOfShipCell) {
                    for (int i = 0; i < mapSize; i++) {
                        for (int j = 0; j < mapSize; j++) {
                            myButtons[i, j].MouseDown += new MouseEventHandler(ConfigureSmallShips);
                        }
                    }
                }
                else {
                    MessageBox.Show("Уже використана максимальна кількість 1 палубних кораблів!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    smallShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Опрацьовує клацання по мапі, щоб розташувати 1 палубний корабель
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void ConfigureSmallShips(object sender, MouseEventArgs e) {

            Button pressedButton = sender as Button;
            if (smallShipCheckBox.Checked && shipData.numOfSmallShips >= iOfShipCell) {
                int x = (int)(pressedButton.Location.X - retreat) / buttonSize, y = (int)(pressedButton.Location.Y - retreat) / buttonSize;
                if (myMap[y, x] == iOfEmptyCell) {

                    Ship smallShip = new Ship(1) {
                        orientation = "Horizontally"
                    };
                    bool shipIsPutted = false;
                    var maps = smallShip.PutShipHorizontally(myMap, myButtons, x, y, ref shipIsPutted);
                    myMap = maps.Item1;
                    myButtons = maps.Item2;

                    if (shipIsPutted) {
                        shipData.SetNumOfSmallShips(shipData.numOfSmallShips - 1);
                        myShips.Add(smallShip);
                    }
                    else MessageBox.Show("В даній точці не вийде розташувати корабель, бо його частина вийде за межі карти(або буде дуже близько до іншого корабля/наклададиметься на корабель)!!!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    smallShipCheckBox.Checked = false;
                }
                else {
                    MessageBox.Show("В даній точці по правилам гри ставити корабель не можна!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    smallShipCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Переходить у вікно гри
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void resumeButton_Click(object sender, EventArgs e) {
            if (shipData.numOfVeryBigShips <= 0 && shipData.numOfBigShips <= 0 && shipData.numOfMiddleShips <= 0 && shipData.numOfSmallShips <= 0) {
                Game game = new Game(mainForm, myMap, myButtons, myShips);
                game.Show();
                this.Hide();
            }
            else MessageBox.Show("Ви не можете перейти у вікно гри, бо ви не розташували всі кораблі!", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Опрацьовує клацання по кнопці "Авто", автоматично розташувуючи кораблі на мапі
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void autoLocation_Click(object sender, EventArgs e) {
            shipData.SetNumOfVeryBigShips(0);
            shipData.SetNumOfBigShips(0);
            shipData.SetNumOfMiddleShips(0);
            shipData.SetNumOfSmallShips(0);
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    myMap[i, j] = iOfEmptyCell;
                    myButtons[i, j].BackColor = Color.White;
                }
            }
            Bot bot = new Bot(myButtons, myMap);
            bot.PutShipOnMap();
            myButtons = bot.myButtons;
            myMap = bot.myMap;
            myShips = bot.myShips;
        }

        /// <summary>
        /// Виходить зі всієї програми
        /// </summary>
        /// <param name="sender">Кнопка на яку натиснули</param>
        /// <param name="e">Подія, яка викликалась </param>
        private void ProgramExit(object sender, EventArgs e) {
            mainForm.Close();
        }

        
    }
}
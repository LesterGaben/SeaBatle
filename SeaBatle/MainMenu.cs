using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace SeaBatle {
    /// <summary>
    /// Головне меню програми
    /// </summary>
    public partial class MainMenu : Form {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainMenu() {
            InitializeComponent();
        }

        /// <summary>
        /// Опрацьовує клацання на кнопку button1
        /// </summary>
        /// <param name="sender">кнопка на яку натиснули</param>
        /// <param name="e">подія, яка викликалась при натисканні кнопки</param>
        private void button1_Click(object sender, EventArgs e) {
            PreGameWindow pregameWindow = new PreGameWindow(this);
            pregameWindow.Show();
            this.Hide();
        }

        private void continuebutton_Click(object sender, EventArgs e) {
            if(File.Exists("PersonProgress.txt") && File.Exists("BotProgress.txt")) {
                var personData = TakeProgressFromFile("PersonProgress.txt", true);
                var botdata = TakeProgressFromFile("BotProgress.txt", false);
                Game game = new Game(this, personData.Item1, personData.Item2, personData.Item3, personData.Item4, botdata.Item1, botdata.Item2, botdata.Item3, botdata.Item4);
                game.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Неможливо продовжити незбережену гру");
            }
        }

        private (double[,], Button[,], List<Ship>, ShipDataBase) TakeProgressFromFile(string filename, bool personFile, int mapSize = 10) {
            double[,] numMap = new double[mapSize,mapSize];
            var text = File.ReadAllLines(filename);
            for (int i = 0; i < mapSize; i++) {
                string[] nums = text[i].Split(';');
                for (int j = 0; j < mapSize; j++) {
                    numMap[i, j] = Convert.ToDouble(nums[j]);
                }
            }
            Button[,] buttonsMap = new Button[mapSize,mapSize];
            buttonsMap = GenerateButtonMap(numMap, personFile);
            List<Ship> ships = new List<Ship>();
            for (int i = mapSize; i < 20; i++) {
                string[] temp = text[i].Split(';');
                Ship ship = new Ship();
                ship.index = Convert.ToDouble(temp[0]);
                ship.destroyed = Convert.ToBoolean(Convert.ToInt32(temp[1]));
                ship.destructionRecorded = Convert.ToBoolean(Convert.ToInt32(temp[2]));
                ship.size = Convert.ToInt32(temp[3]);
                string[] points = temp[4].Split(':');
                List<Button> buttons = new List<Button>();
                foreach (string point in points) {
                    string[] parts = point.Split(',');
                    int y = Convert.ToInt32(parts[0]);
                    int x = Convert.ToInt32(parts[1]);
                    buttons.Add(buttonsMap[y, x]);
                }
                ship.buttons = buttons;
                ship.orientation = temp[5];
                ships.Add(ship);
            }
            ShipDataBase shipData = new ShipDataBase();
            string[] data = text[20].Split(';');
            shipData.SetNumOfVeryBigShips(Convert.ToInt32(data[0]));
            shipData.SetNumOfBigShips(Convert.ToInt32(data[1]));
            shipData.SetNumOfMiddleShips(Convert.ToInt32(data[2]));
            shipData.SetNumOfSmallShips(Convert.ToInt32(data[3]));
            shipData.SetAllShipsDestroyed(Convert.ToBoolean(Convert.ToInt32(data[4])));
            return (numMap, buttonsMap, ships, shipData);

        }

        private Button[,] GenerateButtonMap(double[,] numsMap, bool personFile, int mapSize = 10, int yRetreat = 60, int buttonSize = 50, int xRetreat = 760) {
            if (personFile) xRetreat = 60;
            Button[,] buttonsMap = new Button[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    Button button = new Button() {
                        Location = new Point(xRetreat + j * buttonSize, yRetreat + i * buttonSize),
                        Size = new Size(buttonSize, buttonSize),
                    };
                    if((int)numsMap[i, j] == 0) {
                        button.BackColor = Color.White;

                    }
                    else if((int)numsMap[i, j] == 1) {
                        if(personFile) button.BackColor = Color.Red;
                        else button.BackColor = Color.White;
                    }
                    else if((int)numsMap[i, j] == 2) {
                        button.BackColor = Color.CornflowerBlue;
                        button.Font = new Font("Times New Roman", 8F);
                        button.Text = "X";

                    }
                    else if((int)numsMap[i, j] == 3) {
                        button.BackColor = Color.Red;
                        button.Text = "X";
                        button.Font = new Font("Times New Roman", 22F);
                    }
                    buttonsMap[i, j] = button;
                }
            }
            return buttonsMap;
        }

        private void button2_Click(object sender, EventArgs e) {
            if(File.Exists("Ships.txt")) {
                var personData = TakeProgressFromFile("Ships.txt", true);
                PreGameWindow preGameWindow = new PreGameWindow(this, personData.Item1, personData.Item2, personData.Item3, personData.Item4);
                preGameWindow.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Неможливо продовжити розставляти кораблі!");
            }
        }
    }
}

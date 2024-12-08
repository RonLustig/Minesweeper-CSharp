using Sweeper;
using System.Text;
using Timer = System.Windows.Forms.Timer;
using System.Diagnostics;

//Ron Lustig
//11/8/2022
//CS 3020
//Assigtment 3

namespace MinesSweeper
{
    public partial class Form1 : Form
    {
        double averageTime = 0;
        double WinLoseRatio = 0;
        int buttoncounter = 5;
        int winCount = 0;
        int LoseCount = 0;
        int lifetimeWin = 0;
        int lifetimeLose = 0;
        int gameOver = 0;
        static int TOTALROW = 10;
        static int TOTALCOL = 10;
        Cell[,] button = new Cell[TOTALROW, TOTALCOL];
        Label[,] label = new Label[TOTALROW, TOTALCOL];
         int elapsedTime = 0;
        int firstTry = 0;
        Button startgame = new Button();
        private Label mylab = new Label();
        //
        private Timer timer = new Timer();
        //
        private string answer;
        private int min = 0;
        private int hour = 0;
        private int sec = 0;
        int lifetimeSec = 0;

        //Constructor
        public Form1()
        {
            InitializeComponent();
            LoadRecords();
            butt();
           
        }

        /// <summary>
        /// This method Creates the game itself including the Cells,labels and calling the timer and menu strip methods
        /// </summary>
        public  void butt()
        {
            for (int row = 0; row < button.GetLength(0); row++)
            {
                for (int col = 0; col < button.GetLength(1); col++)

                {
                //Creating button cell which creates the grid
                    button[col, row] = new Cell();
                    button[col, row].X = row;
                    button[col, row].Y = col;
                    button[col, row].Z = 1000;
                    button[col, row].W = 0;
                    button[col, row].Location = new Point(col * 100 + 100, row * 100 + 100);
                    button[col, row].ForeColor = System.Drawing.Color.White;
                    button[col, row].BackColor = Color.Gray;
                    button[col, row].CellClick += OnCellClick;
                 
                    Controls.Add(button[col, row]);
                   

                }
            }

            startgame.Location = new Point(300, 0);
            startgame.Name = "startgame";
            startgame.Height = 40;
            startgame.Width = 300;
            startgame.Text = "startover";
            this.startgame.Click += new System.EventHandler(this.startgame_Click);
            startgame.Font = new Font(startgame.Font.FontFamily, 10);
            startgame.ForeColor = System.Drawing.Color.Red;
            Controls.Add(startgame);

            label1.Location = new Point(600, 0);
            label1.Text = "You lost:  " + Convert.ToString(LoseCount) + "   " + "You Won:  " + Convert.ToString(winCount);

            //Calling the methods 
              theTimer();

            menueStrip();


        }//end of the method 



        /// <summary>
        /// This methid Creates the menuStrip with their events for each option in the menu strips
        /// </summary>
        public void menueStrip()
        {

            //create menueStrip Control with a new window
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem windowMenu = new ToolStripMenuItem("Menu");

            //Menu for Game:
            ToolStripMenuItem windowGameMenu = new ToolStripMenuItem("Game");
            windowMenu.DropDownItems.Add(windowGameMenu);


            ToolStripMenuItem windowlifetime = new ToolStripMenuItem("Show Lifetime Statistics", null, new EventHandler(windowlifetime_Click));
            windowGameMenu.DropDownItems.Add(windowlifetime);

            ToolStripMenuItem windowRestart = new ToolStripMenuItem("Restart", null, new EventHandler(windowRestart_Click));
            windowGameMenu.DropDownItems.Add(windowRestart);


            ToolStripMenuItem windowExit = new ToolStripMenuItem("Exit", null, new EventHandler(windowExit_Click));
            windowGameMenu.DropDownItems.Add(windowExit);


            //Menu for the Help:
            ToolStripMenuItem windowHelp = new ToolStripMenuItem("Help");
            windowMenu.DropDownItems.Add(windowHelp);

            ToolStripMenuItem windowInstructions = new ToolStripMenuItem("Instructions", null, new EventHandler(windowInstructions_Click));
            windowHelp.DropDownItems.Add(windowInstructions);


            ToolStripMenuItem windowAbout = new ToolStripMenuItem("About", null, new EventHandler(windowAbout_Click));
            windowHelp.DropDownItems.Add(windowAbout);


            //Adding the menu strips to the contorls 
            ms.MdiWindowListItem = windowMenu;

            ms.Items.Add(windowMenu);


            this.MainMenuStrip = ms;

            this.Controls.Add(ms);
         
        }       

        /// <summary>
        /// This is the event of the lifetime statistics menu strip
        /// When user clicks on the lifetime stats it will show the total amount of wins,loses, win/lose ratio and the average time of completion after winning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowlifetime_Click(object sender, EventArgs e)
        {
            // averageTime and WinLoseRatio 
            averageTime = (double)lifetimeSec / (lifetimeWin+ lifetimeLose);
            WinLoseRatio = (double)lifetimeWin/lifetimeLose;
            //Rounding the averageTime and WinLoseRatio to just 3 decimal places
            Math.Round(WinLoseRatio, 3);
            Math.Round(averageTime, 3);
            MessageBox.Show("You won: " + lifetimeWin.ToString() + " " + "You lost: " + lifetimeLose.ToString() + " " + "Win/Lose =  " + WinLoseRatio.ToString() + " "+ "Average Time " + averageTime.ToString());
        }
        /// <summary>
        /// This event handler of the Restart menu strip that will simply call the restart event handler and restart the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowRestart_Click(object sender, EventArgs e)
        {
            startgame_Click(this, new EventArgs());
        }
        
        /// <summary>
        /// This is the instructions event handler of the instructions menu strips 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In this game you will have 10 bombs at random spots on the board, Your goal is to not touch the bombs which are the red boxes on the grid, " +
                "When you choose a spot on the grid a white spots will spread until it sees a bomb near by or it will simply display a number if the bomb is adjecent to the spot you choose" +
                "if You can choose and guess all of the spots that don't contain a bomb you win, the game will also keep track of the win and lose ratio as well as time");
          
        }
        /// <summary>
        /// This is the about menu strip that will show the about of the game and info 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coded By Ron Lustig on 11/4/2022 at CS3020");
        }
        /// <summary>
        /// This menustrip will exit the game using this event handler of the menu strip 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
        /// <summary>
        /// This method reads the total lifetimeWin,lifetimeLoses and average times from the stats.txt file
        /// </summary>
        private void LoadRecords()
        {

            try
            {
                StreamReader reader = new StreamReader("stats.txt");
                if (!reader.EndOfStream)
                {
                    lifetimeWin = int.Parse(reader.ReadLine());
                    lifetimeLose = int.Parse(reader.ReadLine());
                    lifetimeSec = int.Parse(reader.ReadLine());
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// This method creates the timer 
        /// </summary>
        public void theTimer()
        {
           
          //Creating the timer Tick and interval to count every second
            timer.Interval = (1000);
            timer.Tick += timer1_Tick;
           

           //This part is for the display 
            mylab.Text = "Time = 0sec";
            mylab.Location = new Point(900, 0);
            mylab.AutoSize = true;
            mylab.Font = new Font("Calibri", 10);
            mylab.BorderStyle = BorderStyle.Fixed3D;
            mylab.BackColor = Color.Lavender;
            mylab.ForeColor = Color.Green;
            mylab.Padding = new Padding(6);
            this.Controls.Add(mylab);
        }

        /// <summary>
        /// Timer Tick method that will ensure that the clock will tick and move every second 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender,EventArgs e)
        {

            lifetimeSec++;
            sec++;


            answer = sec.ToString();
            mylab.Text = "Time = " + answer + " sec";


        }
        /// <summary>
        /// This method starts the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCellClick(object? sender, CellClickEventArgs e)
        {
            //Starts the timer
            timer.Start();

            //Checks if its the first click or first try of the game 
            if (firstTry == 0)
            {
               
                
                button[e.Y, e.X].Z = 0;
                button[e.Y, e.X].BackColor = Color.White;
                button[e.Y, e.X].W = 1;
                button[e.Y, e.X].setTdisableButton();

                //Calling the methods at the logic class 
                Logic.placeBombs(button, e.X, e.Y);
                Logic.addNumbers(button);
                Logic.FindOpenSpace(button);
                Logic.Spots(button);

                //After it does all of those things its no longer the first try
                firstTry++;
            }
            else
            {
                activate_virtual_button(button, e.X, e.Y);
            }

        }//end of OnCellClick
        /// <summary>
        /// This method runs the game and checks if a user clicked on the bomb,clicked on open space or won the game
        /// This method also counts the amount of times of the wins or loses based off the if statements 
        /// it reveal numbers if needed and update the timer based off the situation
        /// it also disables the buttons as well when needed to
        /// </summary>
        /// <param name="button"></param>
        /// <param name="xx"></param>
        /// <param name="yy"></param>
        public void activate_virtual_button(Cell[,] button, int xx, int yy)
        {

            //If user clicked on the bomb
            if (button[yy, xx].Z == 100)
            {
                
                timer.Stop();
                button[yy, xx].setTdisableButton();

                Logic.RevealBoard(button);
               
                MessageBox.Show("You are BOMB!");
                LoseCount++;
               lifetimeLose=lifetimeLose+1;
                writeRecords();
                label1.Text = "You lost:  " + Convert.ToString(LoseCount) + "   " + "You Won:  " + Convert.ToString(winCount);
                gameOver = 1;
                
            }
            else
            {
                //If user clicked on open spot
                if (button[yy, xx].Z == 1000)
                {
                    
                    button[yy, xx].Z = 0;
                    button[yy, xx].BackColor = Color.White;
                    button[yy, xx].W = 1;
                    button[yy, xx].setTdisableButton();
                    Logic.FindOpenSpace(button);
                    Logic.Spots(button);
                    
                }
                else
                {
                    //If user clicked on open spot that is adjacent to a bomb
                    Logic.revealNumber(button, xx, yy);
                }
                //If the game was over and returned 1 then user won and it will update everying that is needed to be updated 
                if (Logic.Gameover(button) == 1)
                {
                    timer.Stop();

                    MessageBox.Show("game over you win");
                    winCount++;
                    lifetimeWin = lifetimeWin + 1;
                    writeRecords();
                    label1.Text = "You lost:  " + Convert.ToString(LoseCount) + "   " + "You Won:  " + Convert.ToString(winCount);
                    button[yy, xx].setTdisableButton();
                }
            }


           

        }//end of method
          
        /// <summary>
        /// This event will start the game again and initalizes everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void startgame_Click(object sender, EventArgs e)
        {
            min = 0;
            hour = 0;
            sec = 0;

            timer.Stop();

            label1.Text = "You lost:  " + Convert.ToString(LoseCount) + "   "+ "You Won:  " + Convert.ToString(winCount);
            //Reseting the game and initializes the firstTry integer
            Logic.ResetGame(button);
            firstTry = 0;
           


            buttoncounter++;
            
               
        }


        /// <summary>
        /// This method writes into the file the lifetimeWin,lifetimeLose and the lifetimeSec
        /// </summary>
        private void writeRecords()
        {
            

            try
            {
                StreamWriter writer = new StreamWriter("stats.txt");
                writer.WriteLine(lifetimeWin.ToString());
                writer.WriteLine(lifetimeLose.ToString());
                writer.WriteLine(lifetimeSec.ToString());              
                writer.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }   
        
    }
}
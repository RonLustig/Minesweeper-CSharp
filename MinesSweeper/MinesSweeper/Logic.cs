using Sweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesSweeper
{
    public class Logic
    {
        static int numberofbum = 10;

        /// <summary>
        /// This method places the Bombs at random spots and also marks them as bombs so if someone clicks on them it will bomb them
        /// </summary>
        /// <param name="button"></param>
        /// <param name="xx"></param>
        /// <param name="yy"></param>
        public static void placeBombs(Cell[,] button,int xx,int yy)
        {
            //Declaring the random variable 
            Random rnd = new Random();
            //These variables will represent the random spots of the bombs
            int numX;
            int numY;
            int count = 0;
            //While there are more bombs that needs to be placed then continue and create the bombs and put on the board
            while (count< numberofbum)
            { 
                numX = rnd.Next(0, numberofbum);
                numY = rnd.Next(0, numberofbum);
                int iftrue = 0;
                if ((button[numY, numX].Z == 100) || (button[numY, numX].Z == 0))
                    iftrue = 1;
                if ((numY + 1) < numberofbum)
                    if (button[numY + 1, numX].Z == 0)
                        iftrue = 1;
                if ((numX + 1) < numberofbum)
                    if (button[numY , numX+1].Z == 0)
                        iftrue = 1;
                if ((numY - 1)>=0)
                    if (button[numY - 1, numX].Z == 0)
                        iftrue = 1;
                if ((numX - 1) >= 0)
                    if (button[numY, numX - 1].Z == 0)
                        iftrue = 1;
                if ((numY + 1) < numberofbum && (numX + 1)< numberofbum)
                    if (button[numY + 1, numX + 1].Z == 0)
                        iftrue = 1;
                if ((numY - 1)>=0 && (numX - 1) >=0)
                    if (button[numY - 1, numX - 1].Z == 0)
                        iftrue = 1;
                if ((numY - 1) >= 0 && (numX + 1) < numberofbum)
                    if (button[numY - 1, numX + 1].Z == 0)
                        iftrue = 1;
                if ((numY + 1)< numberofbum && (numX - 1) >=0)
                    if (button[numY + 1, numX - 1].Z == 0)
                        iftrue = 1;
                //Then set the bombs and mark the bombs using the Z and W 
                if (iftrue == 0)
                {
                    button[numY, numX].Z = 100;
                    button[numY, numX].W = 1;
                   // button[numY, numX].BackColor = Color.Red;
                    count++;
                }
              

            }
           
        }//placeBombs

        /// <summary>
        /// This method checks if the user hit any bombs by checking to see if each button.Z is equals to 100
        /// If it did hit a spot that was marked with 100 using the Z then add the bomb number 
        /// </summary>
        /// <param name="button"></param>
        public static void addNumbers(Cell[,] button)
        {
            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                    int numBombs = 0;


                    if (button[i, j].Z != 100)
                    {
                        if ((i+j >= 0) && (j >= 0) && (i + 1 < button.GetLength(0)) && (j < button.GetLength(1)))
                        {
                            if (button[i + 1, j].Z == 100)
                            {
                                numBombs++;
                            }
                        }//checking bounds

                        if ((i >= 0) && (j + 1) >= 0 && (i < button.GetLength(0)) && (j + 1) < button.GetLength(1))
                        {
                            if (button[i, j + 1].Z == 100)
                            {
                                numBombs++;
                            }
                        }//checking bounds
                        if ((i - 1) >= 0 && (j >= 0) && (i - 1) < button.GetLength(0) && (j< button.GetLength(1)))
                        {
                            if (button[i - 1, j].Z == 100)
                            {
                                numBombs++;
                            }
                        }//cheking bounds
                        if ((i >= 0) && (j - 1 >= 0) && (i< button.GetLength(0)) && (j - 1< button.GetLength(1)))
                        {
                            if (button[i, j - 1].Z == 100)
                            {
                                numBombs++;
                            }
                        }//cheking bounds
                        if ((i + 1>= 0) && (j + 1 >= 0) && (i + 1 < button.GetLength(0)) && (j + 1 < button.GetLength(1)))
                        {
                            if (button[i + 1, j + 1].Z == 100)
                            {
                                numBombs++;
                            }
                        }//cheking bounds 
                        if ((i - 1>= 0) && (j - 1 >= 0) && (i - 1 < button.GetLength(0)) && (j - 1< button.GetLength(1)))
                        {
                            if (button[i - 1, j - 1].Z == 100)
                            {
                                numBombs++;
                            }
                        }//cheking bounds 
                        if ((i + 1 >= 0) && (j - 1 >= 0) && (i + 1< button.GetLength(0)) && (j - 1< button.GetLength(1)))
                        {
                            if (button[i + 1, j - 1].Z == 100)
                            {
                                numBombs++;
                            }
                        }//cheking bounds 
                        if ((i - 1 >= 0) && (j + 1>= 0) && (i - 1< button.GetLength(0)) && (j + 1< button.GetLength(1)))
                        {
                            if (button[i - 1, j + 1].Z == 100)
                            {
                                numBombs++;
                            }

                           
                        }//cheking bounds 
                        //Checks if its a bomb and if it is then add it and set the current button
                        if (numBombs > 0)
                        button[i, j].Z = numBombs;

                       

                    }//end of big if 
                }

            }
        }//end of addNumbers

/// <summary>
/// This method looks for open spaces where there are no bombs around
/// If there are no bombs around then it will set the Z value of the spot to 0 and paint the spot in white
/// It also disables the button as it is a clear spot that the user was able to guess correctly
/// </summary>
/// <param name="button"></param>
        public static void FindOpenSpace(Cell[,] button)
        {
            int flag=0;
            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                   
                    if (button[i, j].Z == 0)
                    {
                        if ((i + j >= 0) && (j >= 0) && (i + 1 < button.GetLength(0)) && (j < button.GetLength(1)))
                        {
                            if (button[i + 1, j].Z == 1000)
                            {
                                button[i + 1, j].Z = 0;
                                button[i + 1, j].BackColor= Color.White;
                                
                                button[i + 1, j].W = 1;
                                button[i+1,j].setTdisableButton();
                                flag = 1;
                            }
                        }//checking bounds

                        if ((i >= 0) && (j + 1) >= 0 && (i < button.GetLength(0)) && (j + 1) < button.GetLength(1))
                        {
                            if (button[i, j + 1].Z == 1000)
                            {
                                button[i, j+1].Z = 0;
                                button[i, j+1].BackColor = Color.White;
                                button[i, j + 1].W= 1;
                                button[i, j + 1].setTdisableButton();
                                flag = 1;
                            }
                        }//checking bounds
                        if ((i - 1) >= 0 && (j >= 0) && (i - 1) < button.GetLength(0) && (j < button.GetLength(1)))
                        {
                            if (button[i - 1, j].Z == 1000)
                            {
                                button[i - 1, j].Z = 0;
                                button[i - 1, j].BackColor = Color.White;
                                button[i - 1, j].W= 1;
                                button[i-1, j].setTdisableButton();
                                flag = 1;
                            }
                        }//cheking bounds
                        if ((i >= 0) && (j - 1 >= 0) && (i < button.GetLength(0)) && (j - 1 < button.GetLength(1)))
                        {
                            if (button[i, j - 1].Z == 1000)
                            {
                                button[i, j-1].Z = 0;
                                button[i, j - 1].BackColor= Color.White;
                                button[i, j - 1].W = 1;
                                button[i, j -1].setTdisableButton();
                                flag = 1;
                            }
                        }//cheking bounds
                        if ((i + 1 >= 0) && (j + 1 >= 0) && (i + 1 < button.GetLength(0)) && (j + 1 < button.GetLength(1)))
                        {
                            if (button[i + 1, j + 1].Z == 1000)
                            {
                                button[i + 1, j+1].Z = 0;
                                button[i + 1, j + 1].BackColor = Color.White;
                                button[i + 1, j + 1].W = 1;
                                button[i+1, j + 1].setTdisableButton();
                                flag = 1;
                            }
                        }//cheking bounds 
                        if ((i - 1 >= 0) && (j - 1 >= 0) && (i - 1 < button.GetLength(0)) && (j - 1 < button.GetLength(1)))
                        {
                            if (button[i - 1, j - 1].Z == 1000)
                            {
                                button[i - 1, j-1].Z = 0;
                                button[i - 1, j - 1].BackColor=Color.White;
                                button[i - 1, j - 1].W= 1;
                                button[i-1, j - 1].setTdisableButton();
                                flag = 1;
                            }
                        }//cheking bounds 
                        if ((i + 1 >= 0) && (j - 1 >= 0) && (i + 1 < button.GetLength(0)) && (j - 1 < button.GetLength(1)))
                        {
                            if (button[i + 1, j - 1].Z == 1000)
                            {
                                button[i + 1, j-1].Z = 0;
                                button[i + 1, j - 1].BackColor=Color.White;
                                button[i + 1, j - 1].W = 1;
                                button[i+1, j - 1].setTdisableButton();
                                flag = 1;
                            }
                        }//cheking bounds 
                        if ((i - 1 >= 0) && (j + 1 >= 0) && (i - 1 < button.GetLength(0)) && (j + 1 < button.GetLength(1)))
                        {
                            if (button[i - 1, j + 1].Z == 1000)
                            {
                                button[i - 1, j+1].Z = 0;
                                button[i - 1, j + 1].BackColor= Color.White;
                                button[i - 1, j + 1].W = 1;
                                button[i-1, j + 1].setTdisableButton();
                                flag = 1;
                            }

                            // button[i, j].Z = numBombs;
                        }//cheking bounds 
                        

                    }//end of big if 

                }//for
           
            } //for

            if (flag == 0)
                return;
            else
                FindOpenSpace(button);
       }//FindOpenSpace


/// <summary>
/// This method is being used to display or reveal the number of bombs that are adjecent to the spots 
/// It checks to see the number of bombs that are adjecent to spot and will place the number that correlates to the amount of bombs that are there
/// It will reveal the number and color of the button based off the amount of bombs that are near by 
/// </summary>
/// <param name="button"></param>
/// <param name="x"></param>
/// <param name="y"></param>
        public static void revealNumber(Cell[,] button,int x,int y)
        {
         
            if (button[y, x].Z == 1)
                    {


               button[y, x].BackColor = Color.Yellow;
                button[y, x].setTxt("1");
                button[y, x].setTdisableButton();
                button[y, x].W = 1;

                
            }
                    if (button[y, x].Z == 2)
                    {
                button[y, x].setTxt("2");

              button[y, x].BackColor = Color.BlueViolet;
                button[y, x].setTdisableButton();
                button[y, x].W = 1;



            }
            if (button[y, x].Z == 3)
                    {
               button[y, x].BackColor = Color.Green;
                button[y, x].setTxt("3");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;
            }
            if (button[y, x].Z == 4)
                    {
             button[y, x].BackColor = Color.Purple;
                button[y, x].setTxt("4");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;

            }
            if (button[y, x].Z == 5)
                    {
              button[y, x].BackColor = Color.Brown;
                button[y, x].setTxt("5");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;

            }

            if (button[y, x].Z == 6)
                    {
               button[y, x].BackColor = Color.Pink;
                button[y, x].setTxt("6");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;

            }
            if (button[y, x].Z == 7)
                    {
              button[y, x].BackColor = Color.Orange;
                button[y, x].setTxt("7");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;

            }
            if (button[y, x].Z == 8)
                    {
               button[y, x].BackColor = Color.Blue;
                button[y, x].setTxt("8");

                button[y, x].setTdisableButton();
                button[y, x].W = 1;

            }

        }//revealNumber
        /// <summary>
        /// This method reveals the open spots that the user choose and tare directly adjacent or near the bombs. Then it set the Z value of the spot to 1 to represent that it is
        /// an open spot that was revealed by the user. After that it calls the method that reveals the number of bombs that are adjecent or near the spot
        /// <param name="button"></param>
        public static void Spots(Cell[,] button)
        {

            for (int i = 0; i < button.GetLength(0); i++)
            {

                for (int j = 0; j < button.GetLength(1); j++)
                {


                    if (i+1< button.GetLength(0))
                    {
                        if (button[i + 1, j].Z == 0)
                        {
                            button[i,j].W = 1;
                            revealNumber(button, j, i);

                        }
                    }

                    if (j+1< button.GetLength(1))
                    {
                        if (button[i, j + 1].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }
                   }


                    if (i - 1 >= 0) 
                    {
                    if    (button[i - 1, j].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }
                     }


                    if ((i + 1 < button.GetLength(0)) && (j + 1 < button.GetLength(1)))
                    {
                   if     (button[i + 1, j + 1].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }
                    }

                    if ((i - 1 >= 0) && (j - 1 >= 0))
                    {
                        if (button[i - 1, j - 1].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }

                    }

                    if ((i + 1 < button.GetLength(0)) && (j - 1 >= 0))
                    {


                        if (button[i + 1, j - 1].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }

                    }

                    if ((i - 1 >= 0) && (j + 1 < button.GetLength(1)))
                    {

                        if (button[i - 1, j + 1].Z == 0)
                        {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }
                    }
                       
                    
                    if (j-1>=0)
                    { 
                     if (button[i, j - 1].Z == 0)
                    {
                            button[i, j].W = 1;
                            revealNumber(button, j, i);
                        }

                    }

                }

            }
     

        }//spots

        /// <summary>
        /// This method checks if the game is over
        /// If all of the W value of all of the buttons on the grid are adding up to 100 without being bombed it means that the game is over
        /// This method then returns 1 if the game is over and if the buttons on the grid do add up to 100 and if not it will retrun 0 
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public  static int Gameover(Cell[,] button)
        {
            int x = 0;
            for (int i=0; i<button.GetLength(0); i++)
            {

                for (int j=0; j<button.GetLength(1); j++)
                {
                    x=x+button[i,j].W;

                    
                }                

            }
            
            if (x == 100)
            {
                //Returns 1 if game is over
                return 1;
            }
            else
            {
                //Retruns 0 if the game is not over  
                return 0;
            }

        }//game


        /// <summary>
        /// This method Reveals the board based off if its a blank spot or a bomb 
        /// </summary>
        /// <param name="button"></param>

        public static void RevealBoard(Cell[,] button)
        {

          for (int i=0; i< button.GetLength(0); i++)
            {
                for (int j=0; j< button.GetLength(1); j++)
                {

                    revealNumber(button, i, j);
                   if (button[i,j].Z==1000)
                    {
                        button[i, j].BackColor=Color.White;
                    }//the end of if 
                    if (button[i, j].Z == 100)
                    {
                        button[i, j].BackColor = Color.Red;
                        button[i, j].setTxt("B");
                    }//the end of if 

                }

            }



        }//RevealBoard
        /// <summary>
        /// This method resets the game and brings the board back to the beggining 
        /// It brings back the original color and restarts everything 
        /// </summary>
        /// <param name="button"></param>
        public static void ResetGame(Cell[,] button)
        {

             for (int i=0; i<button.GetLength(0); i++)
            {
            
                for (int j=0; j<button.GetLength(1); j++)
                {
                    button[i,j].Z = 1000;
                   button[i, j].BackColor = Color.Gray;
                    button[i, j].setTxt("");
                    
                    button[i,j].W = 0;
                   button[i, j].setEnableButton();
                   
                    

                }
            }

        }
      

    }
}

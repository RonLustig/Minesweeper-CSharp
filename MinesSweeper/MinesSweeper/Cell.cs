using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sweeper
{
    public partial class Cell : UserControl
    {

        public event EventHandler<CellClickEventArgs> CellClick;
        //Creating the parameters such as the button and the cell 
        Button button2 = new Button();
        Size cellSize = new Size(100, 100);
        int x;
        int y;
        int z;
        int w;
        String txt = new String("m");

        /// <summary>
        /// This method create the cell with the eventhandlers and more such as the size and paint
        /// </summary>
        public Cell()
        {
            this.Size = cellSize;
           
            button2.Size = this.Size;
            //this.Text = button2.Text;
            button2.Click += ButtonClick_EventHandler;
            this.Controls.Add(button2);
            button2.Paint += new System.Windows.Forms.PaintEventHandler(this.button_Paint);

        }
        /// <summary>
        /// This is a setter for the text that will be used to display the numbers texts 
        /// </summary>
        /// <param name="boxTxt"></param>
        public void setTxt(String boxTxt)
        {
           button2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
           
            button2.Text = boxTxt;

        }
        /// <summary>
        /// This is a setter that is used to remove a button
        /// </summary>
        public void removebutton()
        {
            Controls.Remove(button2);
        }
        /// <summary>
        /// This event handler disables the button
        /// </summary>
        public void setTdisableButton()
        {
            button2.Enabled = false;
           button2.Visible = true;

        }
        /// <summary>
        /// This setter enables the button 
        /// </summary>
        public void setEnableButton()
        {
            button2.Enabled = true;
            
            button2.Invalidate();
            button2.Update();
          
        }
        /// <summary>
        /// This is an event handler that will paint the buttons and make them 3D and Clickable 
        /// As well as painting the buttons grey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, button2.ClientRectangle,

        SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,

        SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,

        SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,

        SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }
        /// <summary>
        /// This event handler will make the button not visible when needed to make it not visible 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonClick_EventHandler(object? sender, EventArgs e)
        {

           button2.Visible = false;
            CellClickEventArgs args = new CellClickEventArgs(X, Y, Z,W);
            OnCellClick(this, args);
        }

        /// <summary>
        /// Event handler of OnCellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCellClick(object sender, CellClickEventArgs e)
        {
            CellClick?.Invoke(sender, e);
        }



/// <summary>
/// Feilds for the CellSize,X,Y,Z,W
/// </summary>
        public Size CellSize { get => cellSize; }
      
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public int Z { get => z; set => z = value; }

        public int W { get => w; set => w = value; }
      

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Creating x,y,z,w that will represent the spots and will help determining if the game is over 
/// </summary>
public class CellClickEventArgs : EventArgs
{

    int x;
    int y;
    int z;
    int w;
   


    public CellClickEventArgs(int x, int y, int z, int w)
    {

        this.X = x;
        this.Y = y;
        this.Z = z;
        this.W = w;
       

    }
   

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    public int W { get => w; set => w = value; }

}

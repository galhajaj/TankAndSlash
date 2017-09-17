using System;

namespace AssemblyCSharp
{
    public class Fence
    {
        
        public Fence(int w, int h)
        {
            fenceObjects = new int[w, h];

        }

        int[,] fenceObjects;
        int fenceWidth;
        int fenceHeight;
    }
}


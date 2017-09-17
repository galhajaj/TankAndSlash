using System;

namespace AssemblyCSharp
{
    public class GenerateFence
    {
        public GenerateFence()
        {
        }

        public enum FenceColor
        {
            FC_None = 0,
            FC_Red = 1,
            FC_Green = 2,
            FC_Blue = 3,
            FC_Yellow = 4,
        }

        public static Shape GenerateShape()
        {
            int rand = UnityEngine.Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    return new Shape1();
                case 2:
                    return new Shape2();
                default:
                    return new Shape1();
            }
        }
        void AddShapeToFence(Shape shape, FenceColor color)
        {
            Tuple<int,int>[][] shapeVec = shape.GetShape();
            int rand = UnityEngine.Random.Range(0, 4);
            for (int i=0 ; i<4; ++i)
            {
                bool validShape = true;
                foreach (Tuple<int,int> tup in shapeVec[(rand + i)%4])
                {
                    if (fence[tup.First + currentH, tup.Second + currentW] != FenceColor.FC_None)
                    {
                        validShape = false;
                        break;
                    }
                }
                if (validShape == true)
                {
                    foreach (Tuple<int,int> tup in shapeVec[(rand + i)%4])
                    {
                        fence[tup.First + currentH, tup.Second + currentW] = color;
                    }
                }   
            }
        }
        void CreateFence(int height, int width)
        {
            fence = new FenceColor[height,width];
            Array.Clear(fence,(int)FenceColor.FC_None,height*width);

            FenceColor last = FenceColor.FC_Blue;
            //bottom side
            for (int i = 0; i < width;)
            {
                Shape shape = GenerateShape();
                AddShapeToFence(shape, GenerateColor(ref last));
            }
        }

        void AddShapeToGrid(Shape shape, FenceColor color)
        {
            
        }

        public FenceColor GenerateColor(ref FenceColor last)
        {
            last = (FenceColor)(((int)last + 1) % 4 + 1);
            return last;
        }
        int currentH=0;
        int currentW=0;
        FenceColor[,] fence;
    }
}


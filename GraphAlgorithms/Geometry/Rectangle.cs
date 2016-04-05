﻿namespace GraphAlgorithms.Geometry
{
    public struct Rectangle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} W: {Width} H: {Height}";
        }
    }
}
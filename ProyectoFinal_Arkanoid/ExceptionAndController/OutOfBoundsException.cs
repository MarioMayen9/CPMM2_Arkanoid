using System;

namespace ProyectoFinal_Arkanoid
{
    public class OutOfBoundsException : Exception
    {
        public OutOfBoundsException(string Message) : base(Message) { }
    }
}
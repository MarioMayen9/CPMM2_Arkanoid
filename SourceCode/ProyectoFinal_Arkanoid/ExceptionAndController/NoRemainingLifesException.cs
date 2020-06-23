using System;

namespace ProyectoFinal_Arkanoid
{
    public class NoRemainingLifesException : Exception
    {
        public NoRemainingLifesException(string Message) : base(Message) { }
    }
}
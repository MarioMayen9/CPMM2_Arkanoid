using System;

namespace ProyectoFinal_Arkanoid
{
    public class WrongKeyPressedException : Exception
    {
        public WrongKeyPressedException(string Message) : base(Message) { }
    }
}
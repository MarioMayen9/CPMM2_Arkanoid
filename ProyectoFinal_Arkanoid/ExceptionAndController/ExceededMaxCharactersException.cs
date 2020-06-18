using System;

namespace ProyectoFinal_Arkanoid
{
    public class ExceededMaxCharactersException : Exception
    {
        public ExceededMaxCharactersException(string Message) : base(Message) { }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstSourceGenerator.Lib
{
    public struct Token
    {
        public TokenType Type;
        public string Value;
        public int Line;
        public int Column;
    }
}

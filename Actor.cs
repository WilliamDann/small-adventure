using System.Collections.Generic;
using System;
using System.Text.Json;

namespace Adventure
{
    public class Actor
    {
        public int Hp           { get; set; }
        public int MapHp        { get; set; }
        public string Name      { get; set; }

        public string Character { get; set; }

        public Encounter Encounter { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HugsLib;
using HugsLib.Settings;

namespace IV
{
    public class Base : ModBase
    {
        public override string ModIdentifier
        {
            get { return "MedicalIV"; }
        }

        public static Base Instance { get; private set; }

        public Base()
        {
            Instance = this;
        }
    }
}

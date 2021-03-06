﻿using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Util
{
    public class InstrumentPicture
    {
        private static Uri guitar = new Uri("ms-appx:///Assets/Images/Small_Inst/guitar150x150.png");
        private static Uri piano = new Uri("ms-appx:///Assets/Images/Small_Inst/piano150x150.png");
        private static Uri drums = new Uri("ms-appx:///Assets/Images/Small_Inst/bass150x150.png");
        private static Uri voice = new Uri("ms-appx:///Assets/Images/Small_Inst/mic150x150.png");

        private static Uri guitarLarge = new Uri("ms-appx:///Assets/Images/Instruments/guitar.png");
        private static Uri voiceLarge = new Uri("ms-appx:///Assets/Images/Instruments/guitar.png");
        public static Uri getImageUri(type instrumentType)
        {

            switch (instrumentType)
            {
                case type.Voice: return voice;

                case type.Guitar: return guitar;

                case type.Drums: return drums;

                case type.Piano: return piano;

            }

            return null;
        }

        public static Uri getLargeImages(type instrumentType)
        {

            switch (instrumentType)
            {
                case type.Voice: return voiceLarge;

                case type.Guitar: return guitarLarge;

                //case type.Drums: return drums;

                //case type.Piano: return piano;

            }

            return null;
        }
    }
}

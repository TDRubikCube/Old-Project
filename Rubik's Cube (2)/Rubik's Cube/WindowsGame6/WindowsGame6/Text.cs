using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubikCube
{
    class Text
    {
        public string mainTitle;
        public string optionsTitle;
        public string freePlayTitle;
        public string tutorialTitle;
        public string optionsFreeText;
        public string tutorialFreeText;
        public string tutorialFreeText2;
        public string freePlayScramble;
        public string freePlaySolve;

        public Text()
        {
            mainTitle = "Rubik's Cube -  Main Menu";
            freePlayTitle = "Rubik's Cube -  Free Play";
            optionsTitle = "Rubik's Cube -  Options";
            tutorialTitle = "Rubik's Cube -  Tutorial";
            optionsFreeText = "Press the right key to change songs";
            tutorialFreeText = "Use the L,R,U,D,F,B keys to turn the cube";
            tutorialFreeText2 = "Hold Shift to turn the cube counter-clockwise";
            freePlayScramble = "Scramble";
            freePlaySolve = "Solve";
        }

        public void English()
        {
            mainTitle = "Rubik's Cube -  Main Menu";
            freePlayTitle = "Rubik's Cube -  Free Play";
            optionsTitle = "Rubik's Cube -  Options";
            tutorialTitle = "Rubik's Cube -  Tutorial";
            optionsFreeText = "Press the right key to change songs";
            tutorialFreeText = "Use the L,R,U,D,F,B keys to turn the cube";
            tutorialFreeText2 = "Hold Shift to turn the cube counter-clockwise";
            freePlayScramble = "Scramble";
            freePlaySolve = "Solve";
        }

        public void Hebrew()
        {
            mainTitle = "י ש א ר  ט י ר פ ת - ת י ר ג נ ו ה  ה י ב ו ק";
            freePlayTitle = "י ש פ ו ח  ק ח ש מ - ת י ר ג נ ו ה  ה י ב ו ק";
            optionsTitle = "ת ו ר ד ג ה - ת י ר ג נ ו ה  ה י ב ו ק";
            tutorialTitle = "ה כ ר ד ה - ת י ר ג נ ו ה  ה י ב ו ק";
            optionsFreeText = "ר י ש  ר י ב ע ה ל  י ד כ  י נ מ י ה  ץ ח ה  ר ו ת פ כ  ל ע  ץ ח ל";
            tutorialFreeText = "ה י ב ו ק ה  ת א  ז י ז ה ל  י ד כ  L,R,U,D,F,B  ם י ש ק מ ה  ל ע  ץ ח ל";
            tutorialFreeText2 = "ן ו ע ש ה  ן ו ו כ  ד ג נ  ה י ב ו ק ה  ת א  ב ב ו ס ל  י ד כ  Shift ת א  ק ז ח ה";
            freePlayScramble = "ב ב ר ע";
            freePlaySolve = "ר ו ת פ";
        }
    }
}

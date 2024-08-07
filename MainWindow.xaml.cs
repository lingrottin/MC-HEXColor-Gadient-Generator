﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace MC_HEXColor_Gadient_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int alright = 0;
        private static int languagee = 0;
        private const bool Legacy = false;
        private const bool Json = true;
        public MainWindow()
        {
            InitializeComponent();
            Languages.Initilize_Language();
            MainWindow_InitLanguage(Languages.en_US);
            EnglishSwitcher.IsChecked = true;
            Bold.IsChecked = false;
            Italic.IsChecked = false;
            Underline.IsChecked = false;
            Strikethrough.IsChecked = false;
        }
        int Check()
        {
            int returnint = 0;
            if (!(StartColorInput.Text.ToCharArray().Length == 7 && StartColorInput.Text.ToCharArray()[0] == '#'))
            {
                MessageBox.Show(Languages.Get("ColorDisplayError", languagee) + " in StartColor", "Error");
                returnint = 1;
            }
            if (!(EndColorInput.Text.ToCharArray().Length == 7 && EndColorInput.Text.ToCharArray()[0] == '#'))
            {
                MessageBox.Show(Languages.Get("ColorDisplayError", languagee) + " in EndColor", "Error");
                returnint = 1;
            }
            return returnint;
        }
        public void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (Check() == 1) { return; }
            bool OutputType = Json;
            if (TypeCombo.SelectedIndex == 0)
            {
                OutputType = Json;
            }
            else
            {
                OutputType = Legacy;
            }
            alright = 0;
            int[] start = ParseColor(StartColorInput.Text);
            int[] end = ParseColor(EndColorInput.Text);
            if (alright == 1) { OutputBox.Text = "ERROR"; return; }
            string variable = StartGenerate(InputBox.Text, start, end, OutputType);
            OutputBox.Text = variable;
            return;

        }
        static int[] ParseColor(string color)
        {
            /*string RR = color.Substring(1, 2);
            string GG = color.Substring(3, 2);
            string BB = color.Substring(5, 2);
                       @return[0] = 0;
            @return[1] = 0;
            @return[2] = 0;
            int R, G, B;
            if( !(
                int.TryParse("0x"+RR, out R)&&
                int.TryParse("0x"+GG, out G)&&
                int.TryParse("0x"+BB, out B) ) )
            {
                MessageBox.Show(Languages.Get("ColorDisplayError", languagee), "Error");
                return @return;
            }*/
            Color ccolor = new Color();
            try
            {
                ccolor = (Color)ColorConverter.ConvertFromString(color);
            }
            catch (FormatException)
            {
                MessageBox.Show(Languages.Get("ColorDisplayError", languagee), "Error");
                alright = 1;
            }
            int[] @return = new int[3];
            @return[0] = (int)ccolor.R;
            @return[1] = (int)ccolor.G;
            @return[2] = (int)ccolor.B;

            return @return;
        }

        void MainWindow_InitLanguage(int lang)
        {
            TypeLabel.Content = Languages.Get("TypeLabel", lang);
            StartColorLabel.Content = Languages.Get("StartColorLabel", lang);
            EndColorLabel.Content = Languages.Get("EndColorLabel", lang);
            InputLabel.Content = Languages.Get("InputLabel", lang);
            OutputLabel.Content = Languages.Get("OutputLabel", lang);
            Generate.Content = Languages.Get("Start", lang);
            Bold.Content = Languages.Get("Bold", lang);
            Underline.Content = Languages.Get("Underline", lang);
            Italic.Content = Languages.Get("Italic", lang);
            Strikethrough.Content = Languages.Get("Strikethrough", lang);
            Obfuscated.Content = Languages.Get("Obfuscated", lang);
            ReplaceLabel.Content = Languages.Get("Replace", lang);
            return;
        }


        string StartGenerate(string Input, int[] StartColor, int[] EndColor, bool Type)
        {
            string @return = "\0";
            if (Input.Length <= 1)
            {
                @return = Languages.Get("LengthNotEnough", languagee);
                return @return;
            }

            //计算
            decimal ColorPlR, ColorPlG, ColorPlB;
            if (StartColor[0] == EndColor[0]) { ColorPlR = 0; }
            else
            {
                ColorPlR = (decimal)Math.Abs((StartColor[0] - EndColor[0]) / (Input.Length - 1) + (decimal)0.500000000);
            }
            if (StartColor[1] == EndColor[1]) { ColorPlG = 0; }
            else
            {
                ColorPlG = (decimal)Math.Abs((StartColor[1] - EndColor[1]) / (Input.Length - 1) + (decimal)0.500000000);
            }
            if (StartColor[2] == EndColor[2]) { ColorPlB = 0; }
            else
            {
                ColorPlB = (decimal)Math.Abs((StartColor[2] - EndColor[2]) / (Input.Length - 1) + (decimal)0.500000000);
            }
            int[] ColorsR = new int[Input.Length];
            int[] ColorsG = new int[Input.Length];
            int[] ColorsB = new int[Input.Length];
            for (int i = 0; i < Input.Length; i++)
            {
                if (StartColor[0] < EndColor[0])
                {
                    ColorsR[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[0] + ColorPlR * i)));
                }
                else
                {
                    ColorsR[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[0] - ColorPlR * i)));
                }
                if (StartColor[1] < EndColor[1])
                {
                    ColorsG[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[1] + ColorPlG * i)));
                }
                else
                {
                    ColorsG[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[1] - ColorPlG * i)));
                }
                if (StartColor[2] < EndColor[2])
                {
                    ColorsB[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[2] + ColorPlB * i)));
                }
                else
                {
                    ColorsB[i] = (int)Math.Floor((decimal)(Math.Abs(StartColor[2] - ColorPlB * i)));
                }
            }

            //输出
            string[] colors_string = new string[Input.Length];
            string[,] storeString = new string[2, 5];
            storeString[0, 0] = "bold";
            storeString[0, 1] = "italic";
            storeString[0, 2] = "strikethrough";
            storeString[0, 3] = "underlined";
            storeString[0, 4] = "obfuscated";
            storeString[1, 0] = "l";
            storeString[1, 1] = "o";
            storeString[1, 2] = "m";
            storeString[1, 3] = "n";
            storeString[1, 4] = "k";
            bool[] Template = new bool[5];
#pragma warning disable 8629
            Template[0] = (bool)Bold.IsChecked;
            Template[1] = (bool)Italic.IsChecked;
            Template[2] = (bool)Strikethrough.IsChecked;
            Template[3] = (bool)Underline.IsChecked;
            Template[4] = (bool)Obfuscated.IsChecked;
#pragma warning restore 8629
            for (int i = 0; i < colors_string.Length; i++)
            {
                colors_string[i] = "#" + ColorsR[i].ToString("x2") + ColorsG[i].ToString("x2") + ColorsB[i].ToString("x2");
            }
            if (Type == Json)
            {
                @return = "[";
                for (int i = 0; i < colors_string.Length; i++)
                {
                    /*
                        每个字符的JSON：
                        {"text":"Char","color":"#xxxxxx"}
                    */
                    @return += "{";
                    for (int ii = 0; ii < 5; ii++)
                    {
                        if (Template[ii])
                        {
                            @return += $"\"{storeString[0, ii]}\":true,";
                        }
                    }
                    @return += $"\"text\":\"{Input.ToCharArray()[i]}\",\"color\":\"{colors_string[i]}\"}}";
                    if (i != colors_string.Length - 1) @return += ",";
                }
                @return += "]";
            }
            else
            {
                string replstring = "§";
                if (ReplaceBox.Text != null) { replstring = ReplaceBox.Text; }
                for (int i = 0; i < Input.Length; i++)
                {

                    @return += $"{replstring}{colors_string[i]}";
                    for (int ii = 0; ii < 5; ii++)
                    {
                        if (Template[ii])
                        {
                            @return += $"{replstring}{storeString[1, ii]}";
                        }
                    }
                    @return += Input.ToCharArray()[i].ToString();

                }
            }

            return @return;
        }

        public void ChineseSwitch(object sender, RoutedEventArgs e)
        {
            MainWindow_InitLanguage(Languages.zh_CN);
            languagee = Languages.zh_CN;
            EnglishSwitcher.IsChecked = false;
            ChineseSwitcher.IsChecked = true;
            return;
        }

        public void EnglishSwitch(object sender, RoutedEventArgs e)
        {
            MainWindow_InitLanguage(Languages.en_US);
            languagee = Languages.en_US;
            ChineseSwitcher.IsChecked = false;
            EnglishSwitcher.IsChecked = true;
            return;
        }

    }
    public class Structs
    {
        public struct IntNString
        {
            private int returnInt;
            private string returnString;

            public IntNString(int returnInt, string returnString)
            {
                this.returnInt = returnInt;
                this.returnString = returnString;
            }

            public int ReturnInt { get => returnInt; set => returnInt = value; }
            public string ReturnString { get => returnString; set => returnString = value; }


        }
    }
    public class Languages
    {
        public const int en_US = 0;
        public const int zh_CN = 1;
        static private Hashtable[] lang = { new Hashtable(), new Hashtable() };
        static public void Initilize_Language()
        {
            lang[0].Add("TypeLabel", "Output\ntype");
            lang[0].Add("StartColorLabel", "Starting\nColor");
            lang[0].Add("ColorDisplayError", "Invaild value");
            lang[0].Add("EndColorLabel", "Ending\nColor");
            lang[0].Add("InputLabel", "Text to be gadiented");
            lang[0].Add("OutputLabel", "Output");
            lang[0].Add("LengthNotEnough", "Inputted string's length must be bigger than 1");
            lang[0].Add("Start", "Generate");
            lang[0].Add("Bold", "Bold");
            lang[0].Add("Italic", "Italic");
            lang[0].Add("Strikethrough", "Strikethrough");
            lang[0].Add("Underline", "Underlined");
            lang[0].Add("Obfuscated", "Obfuscated");
            lang[0].Add("Replace", "String used to replace §:");



            lang[1].Add("TypeLabel", "输出类型");
            lang[1].Add("StartColorLabel", "起始颜色");
            lang[1].Add("ColorDisplayError", "此值无效");
            lang[1].Add("EndColorLabel", "终止颜色");
            lang[1].Add("InputLabel", "要应用渐变的文字");
            lang[1].Add("OutputLabel", "输出");
            lang[1].Add("LengthNotEnough", "输入的字符串长度应大于1");
            lang[1].Add("Start", "生成");
            lang[1].Add("Bold", "粗体");
            lang[1].Add("Italic", "斜体");
            lang[1].Add("Strikethrough", "删除线");
            lang[1].Add("Underline", "下划线");
            lang[1].Add("Obfuscated", "随机字符效果");
            lang[1].Add("Replace", "用于替换§的字符串:");

        }
#pragma warning disable 8602
#pragma warning disable 8603
        static public string Get(string item, int Language)
        {
            if (lang[Language][item] == null) return "Null";
            return lang[Language][item].ToString();
        }
#pragma warning restore 8602
#pragma warning restore 8603
    }
}
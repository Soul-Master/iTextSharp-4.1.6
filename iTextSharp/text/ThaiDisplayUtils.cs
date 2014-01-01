using System;
using System.Text;

namespace iTextSharp.text
{
    /*
     * The MIT License
     *
     * Copyright (c) 2008 Virasak Dungsrikaew (virasak@gmail.com)
     *
     * Permission is hereby granted, free of charge, to any person obtaining a copy
     * of this software and associated documentation files (the "Software"), to deal
     * in the Software without restriction, including without limitation the rights
     * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
     * copies of the Software, and to permit persons to whom the Software is
     * furnished to do so, subject to the following conditions:

     * The above copyright notice and this permission notice shall be included in
     * all copies or substantial portions of the Software.

     * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
     * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
     * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
     * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
     * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
     * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
     * THE SOFTWARE.
     */

    internal class ThaiDisplayUtils
    {
        #region Character Constants

        // Lower level characters
        public const char SaraU = (char) 0xE38;
        public const char SaraUu = (char) 0xE39;
        public const char Phinthu = (char) 0xE3A;

        // Lower level characters after pullDown
        public const char SaraUDown = (char) 0xF718;
        public const char SaraUuDown = (char) 0xF719;
        public const char PhinthuDown = (char) 0xF71A;

        // Upper level 1 characters
        public const char MaiHanAkat = (char) 0xE31;
        public const char SaraAm = (char) 0xE33;
        public const char SaraI = (char) 0xE34;
        public const char SaraIi = (char) 0xE35;
        public const char SaraUe = (char) 0xE36;
        public const char SaraUee = (char) 0xE37;
        public const char MaiTaiKhu = (char) 0xE47;

        // Upper level 1 characters after shift left
        public const char MaiHanAkatLeftShift = (char) 0xF710;
        public const char SaraILeftShift = (char) 0xF701;
        public const char SaraIiLeftShift = (char) 0xF702;
        public const char SaraUeLeftShift = (char) 0xF703;
        public const char SaraUeeLeftShift = (char) 0xF704;
        public const char MaiTaiKhuLeftShift = (char) 0xF712;

        // Upper level 2 characters
        public const char MaiEk = (char) 0xE48;
        public const char MaiTho = (char) 0xE49;
        public const char MaiTri = (char) 0xE4A;
        public const char MaiChattawa = (char) 0xE4B;
        public const char Thanthakhat = (char) 0xE4C;
        public const char Nikhahit = (char) 0xE4D;

        // Upper level 2 characters after pull down
        public const char MaiEkDown = (char) 0xF70A;
        public const char MaiThoDown = (char) 0xF70B;
        public const char MaiTriDown = (char) 0xF70C;
        public const char MaiChattawaDown = (char) 0xF70D;
        public const char ThanthakhatDown = (char) 0xF70E;

        // Upper level 2 characters after pull down and shift left
        public const char MaiEkPullDownAndLeftShift = (char) 0xF705;
        public const char MaiThoPullDownAndLeftShift = (char) 0xF706;
        public const char MaiTriPullDownAndLeftShift = (char) 0xF707;
        public const char MaiChattawaPullDownAndLeftShift = (char) 0xF708;
        public const char ThanthakhatPullDownAndLeftShift = (char) 0xF709;

        // Upper level 2 characters after shift left
        public const char MaiEkLeftShift = (char) 0xF713;
        public const char MaiThoLeftShift = (char) 0xF714;
        public const char MaiTriLeftShift = (char) 0xF715;
        public const char MaiChattawaLeftShift = (char) 0xF716;
        public const char ThanthakhatLeftShift = (char) 0xF717;
        public const char NikhahitLeftShift = (char) 0xF711;

        // Up tail characters
        public const char PoPla = (char) 0x0E1B;
        public const char FoFa = (char) 0x0E1D;
        public const char FoFan = (char) 0x0E1F;
        public const char LoChula = (char) 0x0E2C;

        // Down tail characters
        public const char ThoThan = (char) 0xE10;
        public const char YoYing = (char) 0xE0D;
        public const char DoChada = (char) 0xE0E;
        public const char ToPatak = (char) 0xE0F;
        public const char Ru = (char) 0xE24;
        public const char Lu = (char) 0xE26;

        // Cut tail characters
        public const char ThoThanCutTail = (char) 0xF700;
        public const char YoYingCutTail = (char) 0xF70F;

        // for exploded SARA_AM (NIKHAHIT + SARA_AA)
        public const char SaraAa = (char) 0xE32;

        #endregion

        public static String ToDisplayString(String value)
        {
            return new String(ToDisplayString(value.ToCharArray()));
        }

        public static void ToDisplayString(StringBuilder value)
        {
            var content = value.ToString().ToCharArray();
            content = ToDisplayString(content);
            value.Length = 0;
            value.Append(content);
        }

        public static char[] ToDisplayString(char[] content)
        {
            content = ExplodeSaraAm(content);

            var length = content.Length;
            var pch = 'a'; //previous character start with dummy value

            // Replace upper and lower character with un-overlapped character
            for (var i = 0; i < length; i++)
            {
                var ch = content[i];

                if (IsUpperLevel1(ch) && IsUpTail(pch))
                {
                    // Level 1 and up-tail
                    content[i] = ShiftLeft(ch);
                }
                else if (IsUpperLevel2(ch))
                {
                    // Level 2
                    if (IsLowerLevel(pch))
                    {
                        pch = content[i - 2];
                    }

                    if (IsUpTail(pch))
                    {
                        content[i] = PullDownAndShiftLeft(ch);
                    }
                    else if (IsLeftShiftUpperLevel1(pch))
                    {
                        content[i] = ShiftLeft(ch);
                    }
                    else if (!IsUpperLevel1(pch))
                    {
                        content[i] = PullDown(ch);
                    }
                }
                else if (IsLowerLevel(ch) && IsDownTail(pch))
                {
                    // Lower level and down-tail
                    var cutch = CutTail(pch);
                    if (pch != cutch)
                    {
                        content[i - 1] = cutch;
                    }
                    else
                    {
                        content[i] = PullDown(ch);
                    }
                }
                pch = content[i];
            }

            return content;
        }

        private static char[] ExplodeSaraAm(char[] content)
        {
            var count = CountSaraAm(content);

            if (count == 0)
            {
                return (char[])content.Clone();
            }

            var newContent = new char[content.Length + count]; // other chars length + 2*(SARA_AM length)
            var j = 0;

            // Exploded SARA_AM to NIKHAHIT + SARA_AA
            for (var i = 0; i < content.Length; i++)
            {
                var ch = content[i];

                if (i < content.Length - 1 && content[i + 1] == SaraAm)
                {
                    if (IsUpperLevel2(ch))
                    {
                        newContent[j++] = Nikhahit;
                        newContent[j++] = ch;
                    }
                    else
                    {
                        newContent[j++] = ch;
                        newContent[j++] = Nikhahit;
                    }
                }
                else if (ch == SaraAm)
                {
                    newContent[j++] = SaraAa;
                }
                else
                {
                    newContent[j++] = ch;
                }
            }

            return newContent;
        }

        private static int CountSaraAm(char[] content)
        {
            var count = 0;

            for (var i = 0; i < content.Length; i++)
            {
                if (content[i] == SaraAm)
                {
                    count++;
                }
            }
            return count;
        }

        private static bool IsUpTail(char ch)
        {
            return ch == PoPla || ch == FoFa || ch == FoFan || ch == LoChula;
        }

        private static bool IsDownTail(char ch)
        {
            return ch == ThoThan || ch == YoYing || ch == DoChada
                   || ch == ToPatak || ch == Ru || ch == Lu;
        }

        private static bool IsUpperLevel1(char ch)
        {
            return ch == MaiHanAkat || ch == SaraI || ch == SaraIi
                   || ch == SaraUe || ch == SaraUee || ch == MaiTaiKhu
                   || ch == Nikhahit;
        }

        private static bool IsLeftShiftUpperLevel1(char ch)
        {
            return ch == MaiHanAkatLeftShift || ch == SaraILeftShift || ch == SaraIiLeftShift
                   || ch == SaraUeLeftShift || ch == SaraUeeLeftShift || ch == MaiTaiKhuLeftShift
                   || ch == NikhahitLeftShift;
        }


        private static bool IsUpperLevel2(char ch)
        {
            return ch == MaiEk || ch == MaiTho || ch == MaiTri
                   || ch == MaiChattawa || ch == Thanthakhat;
        }

        public static bool IsLowerLevel(char ch)
        {
            return ch == SaraU || ch == SaraUu || ch == Phinthu;
        }

        public static char PullDownAndShiftLeft(char ch)
        {
            switch (ch)
            {
                case MaiEk:
                    return MaiEkPullDownAndLeftShift;
                case MaiTho:
                    return MaiThoPullDownAndLeftShift;
                case MaiTri:
                    return MaiTriPullDownAndLeftShift;
                case MaiChattawa:
                    return MaiChattawaPullDownAndLeftShift;
                case MaiHanAkat:
                    return MaiHanAkatLeftShift;
                case Thanthakhat:
                    return ThanthakhatPullDownAndLeftShift;
                default:
                    return ch;
            }
        }

        public static char ShiftLeft(char ch)
        {
            switch (ch)
            {
                case MaiEk:
                    return MaiEkLeftShift;
                case MaiTho:
                    return MaiThoLeftShift;
                case MaiTri:
                    return MaiTriLeftShift;
                case MaiChattawa:
                    return MaiChattawaLeftShift;
                case MaiHanAkat:
                    return MaiHanAkatLeftShift;
                case SaraI:
                    return SaraILeftShift;
                case SaraIi:
                    return SaraIiLeftShift;
                case SaraUe:
                    return SaraUeLeftShift;
                case SaraUee:
                    return SaraUeeLeftShift;
                case MaiTaiKhu:
                    return MaiTaiKhuLeftShift;
                case Nikhahit:
                    return NikhahitLeftShift;
                default:
                    return ch;
            }
        }

        private static char PullDown(char ch)
        {
            switch (ch)
            {
                case MaiEk:
                    return MaiEkDown;
                case MaiTho:
                    return MaiThoDown;
                case MaiTri:
                    return MaiTriDown;
                case MaiChattawa:
                    return MaiChattawaDown;
                case Thanthakhat:
                    return ThanthakhatDown;
                case SaraU:
                    return SaraUDown;
                case SaraUu:
                    return SaraUuDown;
                case Phinthu:
                    return PhinthuDown;
                default:
                    return ch;
            }
        }
        
        private static char CutTail(char ch)
        {
            switch (ch)
            {
                case ThoThan:
                    return ThoThanCutTail;
                case YoYing:
                    return YoYingCutTail;
                default:
                    return ch;
            }
        }
    }
}
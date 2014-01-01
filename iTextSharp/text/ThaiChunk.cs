using System;
using iTextSharp.text.pdf.draw;

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
    public class Chunk : _Chunk
    {
        public Chunk()
        {
        }

        public Chunk(_Chunk ck)
            : this(ck.Content, ck.Font)
        {
        }

        public Chunk(String content, Font font)
            : base(content, font)
        {
            ManageContent();
        }

        public Chunk(String content)
            : base(content)
        {
            ManageContent();
        }

        public Chunk(char c, Font font)
            : base(c, font)
        {
            ManageContent();
        }

        public Chunk(char c)
            : base(c)
        {
            ManageContent();
        }

        public Chunk(Image image, float offsetX, float offsetY)
            : base(image, offsetX, offsetY)
        {
            
        }

        public Chunk(IDrawInterface separator)
            : base(separator)
        {

        }

        public Chunk(IDrawInterface separator, bool vertical)
            : base(separator, vertical)
        {

        }

        public Chunk(IDrawInterface separator, float tabPosition)
            : base(separator, tabPosition)
        {

        }

        public Chunk(IDrawInterface separator, float tabPosition, bool newline)
            : base(separator, tabPosition, newline)
        {

        }

        public Chunk(Image image, float offsetX, float offsetY, bool changeLeading)
            : base(image, offsetX, offsetY, changeLeading)
        {

        }

        private void ManageContent()
        {
            ThaiDisplayUtils.ToDisplayString(content);
        }
    }
}

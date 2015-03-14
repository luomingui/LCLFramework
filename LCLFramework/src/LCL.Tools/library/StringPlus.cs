using System.Text;

namespace SF.Tools
{
    /// <summary>
    /// ×Ö·û´®²Ù×÷Àà
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough]
    public class StringPlus
    {
        private StringBuilder str = new StringBuilder();

        public string Append(string Text)
        {
            this.str.Append(Text);
            return this.str.ToString();
        }

        public string AppendLine( )
        {
            this.str.Append("\r\n");
            return this.str.ToString();
        }

        public string AppendLine(string Text)
        {
            this.str.Append(Text + "\r\n");
            return this.str.ToString();
        }

        public string AppendSpace(int SpaceNum, string Text)
        {
            this.str.Append(this.Space(SpaceNum));
            this.str.Append(Text);
            return this.str.ToString();
        }

        public string AppendSpaceLine(int SpaceNum, string Text)
        {
            this.str.Append(this.Space(SpaceNum));
            this.str.Append(Text);
            this.str.Append("\r\n");
            return this.str.ToString();
        }

        public void DelLastChar(string strchar)
        {
            string str = this.str.ToString();
            int length = str.LastIndexOf(strchar);
            if (length > 0)
            {
                this.str = new StringBuilder();
                this.str.Append(str.Substring(0, length));
            }
        }

        public void DelLastComma( )
        {
            string str = this.str.ToString();
            int length = str.LastIndexOf(",");
            if (length > 0)
            {
                this.str = new StringBuilder();
                this.str.Append(str.Substring(0, length));
            }
        }

        public void Remove(int Start, int Num)
        {
            this.str.Remove(Start, Num);
        }

        public string Space(int SpaceNum)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < SpaceNum; i++)
            {
                builder.Append("\t");
            }
            return builder.ToString();
        }

        public override string ToString( )
        {
            return this.str.ToString();
        }

        public string Value
        {
            get
            {
                return this.str.ToString();
            }
        }
    }
}


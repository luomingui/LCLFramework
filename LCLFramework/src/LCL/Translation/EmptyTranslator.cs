using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 不支持更换其它语言的翻译器
    /// </summary>
    internal class EmptyTranslator : Translator
    {
        internal override bool Enabled
        {
            get { return false; }
            set { }
        }

        protected override void OnCurrentCultureChanged()
        {
            base.OnCurrentCultureChanged();
            //不论语言怎么变，都设置回开发语言。
            this.CurrentCulture = LEnvironment.CurrentCulture;
        }

        protected override bool TranslateCore(string devLanguage, out string result)
        {
            result = devLanguage;
            return true;
        }

        protected override string TranslateReverseCore(string currentLanguage)
        {
            return currentLanguage;
        }

        public override IList<string> GetSupportCultures()
        {
            return new string[] { LEnvironment.CurrentCulture };
        }
    }
}

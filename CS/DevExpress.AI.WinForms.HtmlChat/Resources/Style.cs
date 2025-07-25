namespace DevExpress.AI.WinForms.HtmlChat {
    using System.Collections.Concurrent;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;

    abstract class Style {
        readonly string htmlName, cssName;
        protected Style(string htmlName = null, string cssName = null) {
            if(string.IsNullOrEmpty(htmlName)) {
                string typeName = GetTypeName();
                this.htmlName = typeName.Substring(0, typeName.Length - nameof(Style).Length);
            }
            else this.htmlName = htmlName;
            if(string.IsNullOrEmpty(cssName)) {
                string typeName = GetTypeName();
                this.cssName = typeName.Substring(0, typeName.Length - nameof(Style).Length);
            }
            else this.cssName = cssName;
        }
        string GetTypeName() {
            return this.GetType().Name;
        }
        string htmlCore;
        public string Html {
            get { return htmlCore ?? (htmlCore = ReadText(htmlName, nameof(Html))); }
        }
        string cssCore;
        public string Css {
            get { return cssCore ?? (cssCore = ReadText(cssName, nameof(Css))); }
        }
        #region ReadText
        readonly static ConcurrentDictionary<string, string> texts = new ConcurrentDictionary<string, string>();
        static string ReadText(string name, string type) {
            string resourceName = "DevExpress.AI.WinForms.HtmlChat.Resources." + $@"{type}.{name}.{type}";
            return texts.GetOrAdd(resourceName, x => {
                using(var stream = typeof(Style).Assembly.GetManifestResourceStream(x))
                using(var reader = new System.IO.StreamReader(stream))
                    return reader.ReadToEnd();
            });
        }
        #endregion ReadText
        #region Apply
        public void Apply(HtmlContentControl control) {
            control.HtmlImages = SvgChatImages.SvgImages;
            control.HtmlTemplate.Set(Html, Css);
        }
        public void Apply(HtmlContentPopup popup) {
            popup.HtmlImages = SvgChatImages.SvgImages;
            popup.HtmlTemplate.Set(Html, Css);
        }
        public void Apply(DevExpress.Utils.Html.HtmlTemplate template) {
            template.Set(Html, Css);
        }
        #endregion Apply
    }
    public static class SvgChatImages {
        static SvgChatImages() {
            SvgImages = SvgImageCollection.FromResources("DevExpress.AI.WinForms.HtmlChat.Resources.Svg", typeof(SvgChatImages).Assembly);
        }
        public static SvgImageCollection SvgImages {
            get;
            private set;
        }
    }
}
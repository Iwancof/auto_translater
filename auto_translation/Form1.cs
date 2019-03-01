using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Web;


namespace auto_translation
{
    
    public partial class Form1 : Form
    {
        RavSoft.GoogleTranslator.Translator trans = new RavSoft.GoogleTranslator.Translator();
        ClipBoardWatcher cbw;

        public Form1() {
            InitializeComponent();

            ComboBox_In.Items.Add("Afrikaans");
            ComboBox_In.Items.Add("Albanian");
            ComboBox_In.Items.Add("Arabic");
            ComboBox_In.Items.Add("Armenian");
            ComboBox_In.Items.Add("Azerbaijani");
            ComboBox_In.Items.Add("Basque");
            ComboBox_In.Items.Add("Belarusian");
            ComboBox_In.Items.Add("Bengali");
            ComboBox_In.Items.Add("Bulgarian");
            ComboBox_In.Items.Add("Catalan");
            ComboBox_In.Items.Add("Chinese");
            ComboBox_In.Items.Add("Croatian");
            ComboBox_In.Items.Add("Czech");
            ComboBox_In.Items.Add("Danish");
            ComboBox_In.Items.Add("Dutch");
            ComboBox_In.Items.Add("English");
            ComboBox_In.Items.Add("Esperanto");
            ComboBox_In.Items.Add("Estonian");
            ComboBox_In.Items.Add("Filipino");
            ComboBox_In.Items.Add("Finnish");
            ComboBox_In.Items.Add("French");
            ComboBox_In.Items.Add("Galician");
            ComboBox_In.Items.Add("German");
            ComboBox_In.Items.Add("Georgian");
            ComboBox_In.Items.Add("Greek");
            ComboBox_In.Items.Add("Haitian Creole");
            ComboBox_In.Items.Add("Hebrew");
            ComboBox_In.Items.Add("Hindi");
            ComboBox_In.Items.Add("Hungarian");
            ComboBox_In.Items.Add("Icelandic");
            ComboBox_In.Items.Add("Indonesian");
            ComboBox_In.Items.Add("Irish");
            ComboBox_In.Items.Add("Italian");
            ComboBox_In.Items.Add("Japanese");
            ComboBox_In.Items.Add("Korean");
            ComboBox_In.Items.Add("Lao");
            ComboBox_In.Items.Add("Latin");
            ComboBox_In.Items.Add("Latvian");
            ComboBox_In.Items.Add("Lithuanian");
            ComboBox_In.Items.Add("Macedonian");
            ComboBox_In.Items.Add("Malay");
            ComboBox_In.Items.Add("Maltese");
            ComboBox_In.Items.Add("Norwegian");
            ComboBox_In.Items.Add("Persian");
            ComboBox_In.Items.Add("Polish");
            ComboBox_In.Items.Add("Portuguese");
            ComboBox_In.Items.Add("Romanian");
            ComboBox_In.Items.Add("Russian");
            ComboBox_In.Items.Add("Serbian");
            ComboBox_In.Items.Add("Slovak");
            ComboBox_In.Items.Add("Slovenian");
            ComboBox_In.Items.Add("Spanish");
            ComboBox_In.Items.Add("Swahili");
            ComboBox_In.Items.Add("Swedish");
            ComboBox_In.Items.Add("Tamil");
            ComboBox_In.Items.Add("Telugu");
            ComboBox_In.Items.Add("Thai");
            ComboBox_In.Items.Add("Turkish");
            ComboBox_In.Items.Add("Ukrainian");
            ComboBox_In.Items.Add("Urdu");
            ComboBox_In.Items.Add("Vietnamese");
            ComboBox_In.Items.Add("Welsh");
            ComboBox_In.Items.Add("Yiddish");

            foreach(string ele in ComboBox_In.Items) {
                ComboBox_Ou.Items.Add(ele);
            }

            ComboBox_In.SelectedIndex = ComboBox_In.Items.IndexOf("English");
            ComboBox_Ou.SelectedIndex = ComboBox_Ou.Items.IndexOf("Japanese");
        }
        private void Form1_Load(object sender, EventArgs e) {
            cbw = new ClipBoardWatcher(); 
            cbw.DrawClipBoard += (sender2, e2) => {
                if (Clipboard.ContainsText()) {
                    string showMess = "原文 : " + Clipboard.GetText() + "\n";
                    showMess += "翻訳 : " + trans.Translate(Clipboard.GetText(), (string)ComboBox_In.SelectedItem, (string)ComboBox_Ou.SelectedItem);
                    Console.WriteLine(showMess);
                    MessageBox.Show(showMess,
                        "Auto translate",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            };
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            cbw.Dispose();
        }

        private void Exit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Change_Button_Click(object sender, EventArgs e) {
            int tmp = ComboBox_Ou.SelectedIndex;
            ComboBox_Ou.SelectedIndex = ComboBox_In.SelectedIndex;
            ComboBox_In.SelectedIndex = tmp;
        }
    }

    public class ClipBoardWatcher : IDisposable
    {
        ClipBoardWatcherForm form;

        /// <summary>
        /// クリップボードに内容に変更があると発生します。
        /// </summary>
        public event EventHandler DrawClipBoard;

        /// <summary>
        /// ClipBoardWatcherクラスを初期化して
        /// クリップボードビューアチェインに登録します。
        /// 使用後は必ずDispose()メソッドを呼び出して下さい。
        /// </summary>
        public ClipBoardWatcher() {
            form = new ClipBoardWatcherForm();
            form.StartWatch(raiseDrawClipBoard);
        }

        private void raiseDrawClipBoard() {
            if (DrawClipBoard != null) {
                DrawClipBoard(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ClipBoardWatcherクラスを
        /// クリップボードビューアチェインから削除します。
        /// </summary>
        public void Dispose() {
            form.Dispose();
        }

        private class ClipBoardWatcherForm : Form
        {
            [DllImport("user32.dll")]
            private static extern IntPtr SetClipboardViewer(IntPtr hwnd);
            [DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
            [DllImport("user32.dll")]
            private static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);

            const int WM_DRAWCLIPBOARD = 0x0308;
            const int WM_CHANGECBCHAIN = 0x030D;

            IntPtr nextHandle;
            System.Threading.ThreadStart proc;

            public void StartWatch(System.Threading.ThreadStart proc) {
                this.proc = proc;
                nextHandle = SetClipboardViewer(this.Handle);
            }

            protected override void WndProc(ref Message m) {
                if (m.Msg == WM_DRAWCLIPBOARD) {
                    SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    proc();
                } else if (m.Msg == WM_CHANGECBCHAIN) {
                    if (m.WParam == nextHandle) {
                        nextHandle = m.LParam;
                    } else {
                        SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    }
                }
                base.WndProc(ref m);
            }

            protected override void Dispose(bool disposing) {
                ChangeClipboardChain(this.Handle, nextHandle);
                base.Dispose(disposing);
            }
        }
    }
}


// Copyright (c) 2015 Ravi Bhavnani
// License: Code Project Open License
// http://www.codeproject.com/info/cpol10.aspx



namespace RavSoft.GoogleTranslator
{
    /// <summary>
    /// Translates text using Google's online language tools.
    /// </summary>
    public class Translator
    {
        #region Properties

        /// <summary>
        /// Gets the supported languages.
        /// </summary>
        public static IEnumerable<string> Languages {
            get {
                Translator.EnsureInitialized();
                return Translator._languageModeMap.Keys.OrderBy(p => p);
            }
        }

        /// <summary>
        /// Gets the time taken to perform the translation.
        /// </summary>
        public TimeSpan TranslationTime {
            get;
            private set;
        }

        /// <summary>
        /// Gets the url used to speak the translation.
        /// </summary>
        /// <value>The url used to speak the translation.</value>
        public string TranslationSpeechUrl {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public Exception Error {
            get;
            private set;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Translates the specified source text.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="sourceLanguage">The source language.</param>
        /// <param name="targetLanguage">The target language.</param>
        /// <returns>The translation.</returns>
        public string Translate
            (string sourceText,
             string sourceLanguage,
             string targetLanguage) {
            // Initialize
            this.Error = null;
            this.TranslationSpeechUrl = null;
            this.TranslationTime = TimeSpan.Zero;
            DateTime tmStart = DateTime.Now;
            string translation = string.Empty;

            try {
                // Download translation
                string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                                            Translator.LanguageEnumToIdentifier(sourceLanguage),
                                            Translator.LanguageEnumToIdentifier(targetLanguage),
                                            HttpUtility.UrlEncode(sourceText));
                string outputFile = Path.GetTempFileName();
                using (WebClient wc = new WebClient()) {
                    wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    wc.DownloadFile(url, outputFile);
                }

                // Get translated text
                if (File.Exists(outputFile)) {

                    // Get phrase collection
                    string text = File.ReadAllText(outputFile);
                    int index = text.IndexOf(string.Format(",,\"{0}\"", Translator.LanguageEnumToIdentifier(sourceLanguage)));
                    if (index == -1) {
                        // Translation of single word
                        int startQuote = text.IndexOf('\"');
                        if (startQuote != -1) {
                            int endQuote = text.IndexOf('\"', startQuote + 1);
                            if (endQuote != -1) {
                                translation = text.Substring(startQuote + 1, endQuote - startQuote - 1);
                            }
                        }
                    } else {
                        // Translation of phrase
                        text = text.Substring(0, index);
                        text = text.Replace("],[", ",");
                        text = text.Replace("]", string.Empty);
                        text = text.Replace("[", string.Empty);
                        text = text.Replace("\",\"", "\"");

                        // Get translated phrases
                        string[] phrases = text.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; (i < phrases.Count()); i += 2) {
                            string translatedPhrase = phrases[i];
                            if (translatedPhrase.StartsWith(",,")) {
                                i--;
                                continue;
                            }
                            translation += translatedPhrase + "  ";
                        }
                    }

                    // Fix up translation
                    translation = translation.Trim();
                    translation = translation.Replace(" ?", "?");
                    translation = translation.Replace(" !", "!");
                    translation = translation.Replace(" ,", ",");
                    translation = translation.Replace(" .", ".");
                    translation = translation.Replace(" ;", ";");

                    // And translation speech URL
                    this.TranslationSpeechUrl = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx",
                                                               HttpUtility.UrlEncode(translation), Translator.LanguageEnumToIdentifier(targetLanguage), translation.Length);
                }
            } catch (Exception ex) {
                this.Error = ex;
            }

            // Return result
            this.TranslationTime = DateTime.Now - tmStart;
            return translation;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Converts a language to its identifier.
        /// </summary>
        /// <param name="language">The language."</param>
        /// <returns>The identifier or <see cref="string.Empty"/> if none.</returns>
        private static string LanguageEnumToIdentifier
            (string language) {
            string mode = string.Empty;
            Translator.EnsureInitialized();
            Translator._languageModeMap.TryGetValue(language, out mode);
            return mode;
        }

        /// <summary>
        /// Ensures the translator has been initialized.
        /// </summary>
        private static void EnsureInitialized() {
            if (Translator._languageModeMap == null) {
                Translator._languageModeMap = new Dictionary<string, string>();
                Translator._languageModeMap.Add("Afrikaans", "af");
                Translator._languageModeMap.Add("Albanian", "sq");
                Translator._languageModeMap.Add("Arabic", "ar");
                Translator._languageModeMap.Add("Armenian", "hy");
                Translator._languageModeMap.Add("Azerbaijani", "az");
                Translator._languageModeMap.Add("Basque", "eu");
                Translator._languageModeMap.Add("Belarusian", "be");
                Translator._languageModeMap.Add("Bengali", "bn");
                Translator._languageModeMap.Add("Bulgarian", "bg");
                Translator._languageModeMap.Add("Catalan", "ca");
                Translator._languageModeMap.Add("Chinese", "zh-CN");
                Translator._languageModeMap.Add("Croatian", "hr");
                Translator._languageModeMap.Add("Czech", "cs");
                Translator._languageModeMap.Add("Danish", "da");
                Translator._languageModeMap.Add("Dutch", "nl");
                Translator._languageModeMap.Add("English", "en");
                Translator._languageModeMap.Add("Esperanto", "eo");
                Translator._languageModeMap.Add("Estonian", "et");
                Translator._languageModeMap.Add("Filipino", "tl");
                Translator._languageModeMap.Add("Finnish", "fi");
                Translator._languageModeMap.Add("French", "fr");
                Translator._languageModeMap.Add("Galician", "gl");
                Translator._languageModeMap.Add("German", "de");
                Translator._languageModeMap.Add("Georgian", "ka");
                Translator._languageModeMap.Add("Greek", "el");
                Translator._languageModeMap.Add("Haitian Creole", "ht");
                Translator._languageModeMap.Add("Hebrew", "iw");
                Translator._languageModeMap.Add("Hindi", "hi");
                Translator._languageModeMap.Add("Hungarian", "hu");
                Translator._languageModeMap.Add("Icelandic", "is");
                Translator._languageModeMap.Add("Indonesian", "id");
                Translator._languageModeMap.Add("Irish", "ga");
                Translator._languageModeMap.Add("Italian", "it");
                Translator._languageModeMap.Add("Japanese", "ja");
                Translator._languageModeMap.Add("Korean", "ko");
                Translator._languageModeMap.Add("Lao", "lo");
                Translator._languageModeMap.Add("Latin", "la");
                Translator._languageModeMap.Add("Latvian", "lv");
                Translator._languageModeMap.Add("Lithuanian", "lt");
                Translator._languageModeMap.Add("Macedonian", "mk");
                Translator._languageModeMap.Add("Malay", "ms");
                Translator._languageModeMap.Add("Maltese", "mt");
                Translator._languageModeMap.Add("Norwegian", "no");
                Translator._languageModeMap.Add("Persian", "fa");
                Translator._languageModeMap.Add("Polish", "pl");
                Translator._languageModeMap.Add("Portuguese", "pt");
                Translator._languageModeMap.Add("Romanian", "ro");
                Translator._languageModeMap.Add("Russian", "ru");
                Translator._languageModeMap.Add("Serbian", "sr");
                Translator._languageModeMap.Add("Slovak", "sk");
                Translator._languageModeMap.Add("Slovenian", "sl");
                Translator._languageModeMap.Add("Spanish", "es");
                Translator._languageModeMap.Add("Swahili", "sw");
                Translator._languageModeMap.Add("Swedish", "sv");
                Translator._languageModeMap.Add("Tamil", "ta");
                Translator._languageModeMap.Add("Telugu", "te");
                Translator._languageModeMap.Add("Thai", "th");
                Translator._languageModeMap.Add("Turkish", "tr");
                Translator._languageModeMap.Add("Ukrainian", "uk");
                Translator._languageModeMap.Add("Urdu", "ur");
                Translator._languageModeMap.Add("Vietnamese", "vi");
                Translator._languageModeMap.Add("Welsh", "cy");
                Translator._languageModeMap.Add("Yiddish", "yi");
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// The language to translation mode map.
        /// </summary>
        public static Dictionary<string, string> _languageModeMap;

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLib = Microsoft.Office.Interop.Word;

namespace Lab.Classes
{
    class WordReport
    {
        private FileInfo _fileInfo;

        public WordReport(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("Файл не найден");
            }
        }

        internal bool Process(Dictionary<string, string> items)
        {
            WordLib.Application app = null;
            try
            {
                app = new WordLib.Application();
                Object file = _fileInfo.FullName;

                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var item in items)
                {
                    WordLib.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = WordLib.WdFindWrap.wdFindContinue;
                    Object replace = WordLib.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing,
                        Replace: replace);
                }

                Object newFileName = Path.Combine("G:\\Курс 3\\Контракт", _fileInfo.Name.Replace(".docx", "_1") + ".docx");
                app.ActiveDocument.SaveAs(newFileName);
                app.ActiveDocument.Close();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }

            }
            return false;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Economy.Common
{
    public class MailParser
    {
        protected XDocument ToDocument(string file)
        {
            file = string.Format("<data>{0}</data>", file);
            var doc = XDocument.Parse(file);
            return doc;
        }
        
        protected virtual string CleanFile(string file)
        {
            var result = RemoveReplaceTag(file, "<!--", ">", "");
            result = RemoveAttributs(result);
            result = result.Replace(@"\r\n", string.Empty);
            result = RemoveReplaceTag(result, "<head", ">", "</head>");
            result = RemoveReplaceTag(result, "<html", ">", "</html>");
            result = RemoveReplaceTag(result, "<body", ">", "</body>");
            result = RemoveReplaceTag(result, "<br", ">", "</br>", " ");
            result = RemoveReplaceTag(result, "</html", ">", "</html>", " ");
            result = RemoveReplaceTag(result, "<font", ">", "</font>");
            result = RemoveReplaceTag(result, "<bgcolor", ">", "</bgcolor>");
            result = RemoveReplaceTag(result, "<meta", ">", "</meta>");
            result = RemoveReplaceTag(result, "<hr", ">", "</hr>");
            result = RemoveReplaceTag(result, "<b", ">", "</b>");
            result = RemoveReplaceTag(result, "<SMS-оповещение", ">", "</SMS-оповещение>", " ");
            result = RemoveReplaceTag(result, "</SMS-оповещение", ">", "</SMS-оповещение>", " ");
            result = RemoveReplaceTag(result, "<strong", ">", "</strong>");
            result = result.Replace("&nbsp;", " ");
            result = result.Replace("&nbsp", " ");
            result = result.Replace("\r", string.Empty).Replace("\n", string.Empty);
            result = CloseTable(result);
            result = Repair(result);

            return result;
        }

        protected string CloseTable(string text)
        {
            var rows = text.Split(new[] { "<table>" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = rows[i].Replace("</table>", string.Empty);
            }
            var data = string.Join(@"</table>" + '\n' + '\r' + "<table>", rows);
            return string.Format("<table>{0}</table>", data);
        }

        protected string Repair(string text)
        {
            int startP = 0;
            var stack = new Stack<string>();
            int f1;
            do
            {
                f1 = text.IndexOf("<", startP, StringComparison.Ordinal);
                startP = f1;
                var isClose = (text[f1 + 1] == '/');

                if (f1 != -1 && !isClose)
                {
                    var tag = GetTag(text, f1);
                    startP += tag.Length + 2;

                    if (tag == "td" && stack.Count > 0 && stack.Peek() == "td")
                    {
                        text = text.Insert(f1, string.Format("</{0}>", tag));
                        startP += tag.Length + 3;
                    }
                    else
                    {
                        stack.Push(tag);
                    }
                }
                else if (!(f1 == -1 && stack.Count == 0))
                {
                    var tag = GetTag(text, f1 + 1);
                    startP += tag.Length + 3;

                    string opTag;
                    do
                    {
                        opTag = stack.Pop();
                        if (!opTag.Equals(tag))
                        {
                            text = text.Insert(f1, string.Format("</{0}>", opTag));
                            startP += opTag.Length + 3;
                        }
                    } while (!opTag.Equals(tag) && stack.Count > 0);
                }
            } while (f1 != -1);
            return text;
        }

        protected string GetTag(string text, int start)
        {
            if (start == -1)
                return string.Empty;
            start++;
            var fic = text.IndexOf(">", start, StringComparison.Ordinal);
            var ficsp = text.IndexOf(" ", start, StringComparison.Ordinal);
            if (ficsp != -1 && ficsp < fic)
            {
                fic = ficsp;
            }
            var tag = text.Substring(start, fic - start);
            return tag;
        }

        protected string RemoveReplaceTag(string text, string spanStartPattern, string spanEndPattern,
            string spanClosePattern, string insert = null)
        {
            int startP = 0;
            var end = false;
            do
            {
                var spanStart = text.IndexOf(spanStartPattern, startP, StringComparison.OrdinalIgnoreCase);
                if (spanStart != -1)
                {
                    var spanEnd = text.IndexOf(spanEndPattern, spanStart, StringComparison.OrdinalIgnoreCase);
                    text = text.Remove(spanStart, spanEnd - spanStart + spanEndPattern.Length);
                    if (!string.IsNullOrEmpty(insert))
                        text = text.Insert(spanStart, insert);
                    if (!string.IsNullOrEmpty(spanClosePattern))
                    {
                        var spanCloseStart = text.IndexOf(spanClosePattern, spanStart, StringComparison.OrdinalIgnoreCase);
                        if (spanCloseStart != -1)
                        {
                            text = text.Remove(spanCloseStart, spanClosePattern.Length);
                            if (!string.IsNullOrEmpty(insert))
                                text = text.Insert(spanCloseStart, insert);
                        }
                    }
                    startP = spanStart;
                }
                else
                {
                    end = true;
                }
            } while (!end);
            return text;
        }
        protected string RemoveAttributs(string text)
        {
            var blocs = text.Split(new[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < blocs.Length; index++)
            {
                var bloc = blocs[index];
                int start = bloc.IndexOf(" ", StringComparison.Ordinal);
                if (start != -1 && bloc[0] != 'a')
                {
                    int end = bloc.IndexOf(">", StringComparison.Ordinal);
                    if (start < end)
                    {
                        blocs[index] = bloc.Remove(start, end - start);
                    }
                }
            }
            return string.Join("<", blocs);
        }

    }
}

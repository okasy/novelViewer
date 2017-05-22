using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;


namespace novelViewer
{
    public class WebScraping
    {
        public string GetHtml(string url)
        {
			// 指定されたURLに対してのRequestを作成します。
			var req = (HttpWebRequest)WebRequest.Create(url);

			// html取得文字列
			string html = "";

			// 指定したURLに対してReqestを投げてResponseを取得します。
			using (var res = (HttpWebResponse)req.GetResponse())
			using (var resSt = res.GetResponseStream())
			// 取得した文字列をUTF8でエンコードします。
			using (var sr = new StreamReader(resSt, Encoding.UTF8))
			{
				// HTMLを取得する。
				html = sr.ReadToEnd();
			}

			return html;
        }

        public string GetTitle(string html)
        {
            // 正規化表現
            // 大文字小文字区別なし       : RegexOptions.IgnoreCase
            // 「.」を改行にも適応する設定: RegexOptions.Singleline
            var reg = new Regex(@"<title>(?<title>.*?)</title>",
                         RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // html文字列内から条件にマッチしたデータを抜き取ります。
            var m = reg.Match(html);

            // 条件にマッチした文字列内からKey("title部分")にマッチした値を抜き取ります。
            return m.Groups["title"].Value;

        }
    }
}

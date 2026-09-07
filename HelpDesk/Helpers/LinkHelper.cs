using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace HelpDesk.Helpers;

public static class LinkHelper
{
    private static readonly Regex UrlRegex = new(
        @"https?://[^\s<]+",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static IHtmlContent Linkify(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return HtmlString.Empty;
        }

        int currentPosition = 0;
        var result = new System.Text.StringBuilder();

        foreach (Match match in UrlRegex.Matches(text))
        {
            result.Append(text[currentPosition..match.Index]);

            string url = match.Value;
            result.Append("<a href=\""+WebUtility.HtmlEncode(url)+"\">"+url+"</a>");

            currentPosition = match.Index + match.Length;
        }
        result.Append(text[currentPosition..]);

        return new HtmlString(result.ToString());
    }
}
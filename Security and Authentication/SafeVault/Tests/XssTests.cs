using System.Text.RegularExpressions;
using Xunit;
using FluentAssertions;

public class XssTests
{
    [Fact]
    public void Should_Sanitize_XSS_Attempts()
    {
        string input = "<script>alert('XSS');</script>";
        string sanitized = Sanitize(input);

        sanitized.Should().NotContain("<script>").And.NotContain("alert");
    }

    private string Sanitize(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty); // basic tag stripper
    }
}

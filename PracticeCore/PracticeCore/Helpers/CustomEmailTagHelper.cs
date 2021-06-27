using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Helpers
{
    public class CustomEmailTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "jahid@gmail.com");
            output.Attributes.Add("class", "custon-class");
            output.Content.SetContent("My Email");
        }
    }
}

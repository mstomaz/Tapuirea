using System.Text.RegularExpressions;

namespace blog_rpg.Routing
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) { return null; }

            var slug = Regex.Replace(value.ToString()!,
                                     @"\s+",
                                     "-",
                                     RegexOptions.CultureInvariant)
                            .ToLowerInvariant();

            slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

            return slug;
        }

    }
}


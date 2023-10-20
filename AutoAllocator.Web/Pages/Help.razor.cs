using System.Reflection;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace AutoAllocator.Web.Pages;

public partial class Help
{
    [Inject] private HttpClient Http { get; set; } = null!;
    private string? _mdContent;
    private string? _htmlContent;

    protected override async Task OnInitializedAsync()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "AutoAllocator.Web.Resources.README.md";

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream))
        {
            _mdContent = await reader.ReadToEndAsync();
        }
        
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions() // Enables GitHub Flavored Markdown (GFM)
            .Build();
        
        _htmlContent = Markdown.ToHtml(_mdContent, pipeline);
    }
}

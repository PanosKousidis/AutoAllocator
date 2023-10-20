using System.Reflection;
using System.Text;
using AutoAllocator.Logic.Services;
using AutoAllocator.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AutoAllocator.Web.Pages;

public partial class Index
{
    [Inject] public IParser? Parser { get; set; }
    [Inject] public FileService? FileService { get; set; }
    
    private async Task HandleSupervisorFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null!)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var contents = await reader.ReadToEndAsync();
            if (FileService != null && Parser != null) 
                FileService.SupervisorsList = Parser.ParseSupervisors(contents);
        }
    }
    private async Task HandleStudentsFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null!)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var contents = await reader.ReadToEndAsync();
            if (FileService != null && Parser != null)
                FileService.StudentsList = Parser.ParseStudents(contents);
        }
    }
    
    private string studentsSampleUrl;
    private string supervisorsSampleUrl;

    protected override async Task OnInitializedAsync()
    {
        studentsSampleUrl = await GetCsvDataUrl("students.sample.csv");
        supervisorsSampleUrl = await GetCsvDataUrl("supervisors.sample.csv");
    }

    private async Task<string> GetCsvDataUrl(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"AutoAllocator.Web.Resources.{fileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var csvContent = await reader.ReadToEndAsync();
        
        return $"data:text/csv;base64,{Convert.ToBase64String(Encoding.UTF8.GetBytes(csvContent))}";
    }
}
using AutoAllocator.Logic.Model;
using AutoAllocator.Logic.Services;
using AutoAllocator.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AutoAllocator.Web.Pages;

public partial class AllocationsPage
{
    private List<Allocation>? Allocations { get; set; }
    [Inject] private FileService? FileService { get; set; }
    [Inject] private IAllocator? Allocator { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Allocations = FileService?.SupervisorsList is null || FileService.StudentsList is null
                ? null
                : Allocator?.Allocate(FileService.SupervisorsList, FileService.StudentsList)
                    .OrderBy(x=>x.Student.Name)
                    .ToList();
        
            StateHasChanged();  // This will trigger a UI update
        }
        await base.OnAfterRenderAsync(firstRender);
    }

}
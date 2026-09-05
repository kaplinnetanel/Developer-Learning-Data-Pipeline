using Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Services;

public class ProcessingService
{
    private readonly DataService _dataService;

    public ProcessingService(DataService dataService)
    {
        _dataService = dataService;
    }

    public async Task SaveAsync(DeveloperLearning data)
    {
        await _dataService.CreateAsync(data);
    }
}
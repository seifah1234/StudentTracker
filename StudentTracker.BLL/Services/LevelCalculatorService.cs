using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.BLL.Interfaces;
using StudentTracker.BLL.Services;
using StudentTracker.DAL.Entities;

namespace StudentTracker.BLL.Services
{
    public class LevelCalculatorService : ILevelCalculatorService
    {
        public readonly ILevelCalculatorService _levelCalculatorService;
        

        public LevelCalculatorService(ILevelCalculatorService levelCalculatorService)
        {
            _levelCalculatorService = levelCalculatorService;
        }
        Task<decimal> ILevelCalculatorService.CalculateLevelAsync(decimal totalPoints, CancellationToken cancellationToken)
        {
             decimal level = Math.Floor(totalPoints / 600) + 1; // Example logic: 1 level for every 100 points
            return Task.FromResult(level);
        }
    }
}

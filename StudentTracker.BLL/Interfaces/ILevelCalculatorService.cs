using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface ILevelCalculatorService
    {
        Task<decimal> CalculateLevelAsync(decimal totalPoints, CancellationToken cancellationToken = default);
    }
}

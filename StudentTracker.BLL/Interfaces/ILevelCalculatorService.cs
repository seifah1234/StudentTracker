using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface ILevelCalculatorService
    {
        Task<StudentLevel> CalculateLevelAsync(decimal totalPoints,decimal maxPoints, CancellationToken cancellationToken = default);
        Task<decimal> CalculatePercentageAsync(decimal totalPoints, decimal maxPoints, CancellationToken cancellationToken = default);

    }
}

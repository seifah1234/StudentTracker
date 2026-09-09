using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.BLL.Interfaces;
using StudentTracker.BLL.Services;
using StudentTracker.DAL.Entities;
using StudentTracker.Shared.Enums;

namespace StudentTracker.BLL.Services
{
    public class LevelCalculatorService : ILevelCalculatorService
    {

        public Task<decimal> CalculatePercentageAsync(decimal totalPoints, decimal maxPoints, CancellationToken cancellationToken = default)
        {
            if (maxPoints == 0)
            {
                throw new ArgumentException("Max points cannot be zero.", nameof(maxPoints));
            }
            decimal percentage = (totalPoints / maxPoints) * 100;
            return Task.FromResult(percentage);
        }

        public Task<StudentLevel> CalculateLevelAsync(decimal totalPoints,decimal maxPoints, CancellationToken cancellationToken)
        {
            if (totalPoints < 0)
            {
                throw new ArgumentException("Total points cannot be negative.", nameof(totalPoints));
            }
            var percentage = CalculatePercentageAsync(totalPoints, maxPoints, cancellationToken).Result;

            if (percentage >= 90)
            {
                return Task.FromResult(StudentLevel.Excellent);
            }
            else if (percentage >= 70)
            {
                return Task.FromResult(StudentLevel.Normal);
            }
            else if (percentage >= 50)
            {
                return Task.FromResult(StudentLevel.Poor);
            }
            else
            {
                return Task.FromResult(StudentLevel.Failing);
            }
        }


    }
}

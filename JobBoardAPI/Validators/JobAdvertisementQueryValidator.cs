using FluentValidation;
using JobBoardAPI.Entities;
using JobBoardAPI.Models;

namespace JobBoardAPI.Validators
{
    public class JobAdvertisementQueryValidator : AbstractValidator<JobAdvertisementQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15];
        private string[] allowedSortByColumnNames = [nameof(JobAdvertisement.Name), nameof(JobAdvertisement.Description), nameof(JobAdvertisement.PostedOn)];
        public JobAdvertisementQueryValidator()
        {
            RuleFor(ja => ja.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(ja => ja.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(ja => ja.SortBy)
                .Must(value =>  string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}

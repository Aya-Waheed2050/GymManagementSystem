namespace BusinessLogic.ViewModels.PlanViewModels
{
    public class PlanUpdateResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
        public UpdatePlanViewModel? Plan { get; set; }
    }
}

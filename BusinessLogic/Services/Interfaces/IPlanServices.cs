namespace BusinessLogic.Services.Interfaces
{
    public interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel GetPlanById(int id);
        PlanUpdateResult? GetPlanToUpdate(int PlanId);
        bool UpdatePlan(int PlanId, UpdatePlanViewModel model);
        bool ToggleStatus(int PlanId);
    }
}

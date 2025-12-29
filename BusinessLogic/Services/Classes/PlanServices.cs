using System.Numerics;
using DataAccess.Models.Plans;

namespace BusinessLogic.Services.Classes
{
    public class PlanServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IPlanServices
    {
     

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll() ?? [];

            return _mapper.Map<IEnumerable<PlanViewModel>>(Plans);

        }

        public PlanViewModel GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id) ?? null;
            return _mapper.Map<PlanViewModel>(Plan);
        }

        public PlanUpdateResult? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);

            if (plan is null)
                return new PlanUpdateResult
                {
                    IsValid = false,
                    Message = "Plan not found."
                };

            if (!plan.IsActive)
                return new PlanUpdateResult
                {
                    IsValid = false,
                    Message = "You can't edit a deactivated plan."
                };

            if (HasActiveMembership(planId))
                return new PlanUpdateResult
                {
                    IsValid = false,
                    Message = "This plan has active memberships and can't be edited."
                };

            return new PlanUpdateResult
            {
                IsValid = true,
                Plan = _mapper.Map<UpdatePlanViewModel>(plan)
            };
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || !Plan.IsActive ||HasActiveMembership(PlanId)) 
                return false;

            try
            {
                _mapper.Map(updatedPlan, Plan);
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ToggleStatus(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMembership(PlanId)) 
                return false;

            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.UpdatedAt = DateTime.Now;

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {

                return false;
            }
        }

        #region Helper Methods

        private bool HasActiveMembership(int PlanId)
        {
            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>()
                                              .GetAll(X => X.PlanId == PlanId && X.Status == "Active");
            return ActiveMemberShip.Any();
        }

        #endregion
    }
}

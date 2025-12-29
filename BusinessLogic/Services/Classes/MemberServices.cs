namespace BusinessLogic.Services.Classes
{
    public class MemberServices(IUnitOfWork _unitOfWork, 
           IAttachmentService _attachmentService,
           IMapper _mapper) 
        : IMemberServices
    {
         
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll() ?? [];
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Members);
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                if (IsPhoneExist(createMember.Phone) || IsEmailExist(createMember.Email)) 
                    return false;

                var photoName = _attachmentService.Upload("members", createMember.Photo);
                if (string.IsNullOrEmpty(photoName))
                    return false;

                var member = _mapper.Map<CreateMemberViewModel, Member>(createMember);
                member.Photo = photoName;

                _unitOfWork.GetRepository<Member>().Add(member);

                var isCreated = _unitOfWork.SaveChanges() > 0;
                if (!isCreated)
                {
                    _attachmentService.Delete("members", photoName);
                    return false;
                }

                return isCreated;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) 
                return null;

            var MemberViewModel = _mapper.Map<Member, MemberViewModel>(Member);
            //Active Membership
            var Membership = _unitOfWork.GetRepository<Membership>()
                                        .GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                                        .FirstOrDefault();

            if (Membership is not null)
            {
                MemberViewModel.MembershipStratDate = Membership.CreatedAt.ToShortDateString();
                MemberViewModel.MembershipEndDate = Membership.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(Membership.PlanId);
                MemberViewModel.PlanName = Plan?.Name;
            }

            return MemberViewModel;
        }

        public UpdateMemberViewModel? UpdateMemberById(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null)
                return null;
            return _mapper.Map<Member,UpdateMemberViewModel>(Member);
        }

        public bool UpdateMemberDetails(int MemberId, UpdateMemberViewModel updatedMember)
        {
            try
            {
                var MemberRepo = _unitOfWork.GetRepository<Member>();
                var emailExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Email == updatedMember.Email && X.Id != MemberId);
                var phoneExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Phone == updatedMember.Phone && X.Id != MemberId);

                if (emailExist.Any() || phoneExist.Any()) return false;

                var Member = MemberRepo.GetById(MemberId);
                if (Member is null) 
                    return false;

                _mapper.Map(updatedMember, Member);

                MemberRepo.Update(Member);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();
            var MembershipRepo = _unitOfWork.GetRepository<Membership>();
            var MemberSessionRepo = _unitOfWork.GetRepository<MemberSession>();

            var Member = MemberRepo.GetById(MemberId);
            if (Member is null) 
                return false;

            var SessionIDs = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(X => X.MemberId == MemberId).Select(X => X.SessionId);

            var HasActiveSession = _unitOfWork.GetRepository<Session>()
                .GetAll(X => SessionIDs.Contains(X.Id) && X.StartDate > DateTime.Now).Any();

            if (HasActiveSession) 
                return false;

            var Membership = MembershipRepo.GetAll(X => X.MemberId == MemberId);
            try
            {
                if (Membership.Any())
                {
                    foreach (var member in Membership)
                    {
                        MembershipRepo.Delete(member);
                    }
                }
                MemberRepo.Delete(Member);
                var isDeleted =  _unitOfWork.SaveChanges() > 0;
                if (isDeleted)
                    _attachmentService.Delete("members", Member.Photo);
                
                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HealthViewModel? GetMemberHealthDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord is null) 
                return null;

            return _mapper.Map<HealthRecord, HealthViewModel>(MemberHealthRecord);

        }

        #region Helper Methods

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        }

        #endregion
    }
}

using Models.DTOs.Request;
using Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IAdminRepo
    {
        #region roles
        Task<RoleResponse?> GetRoleById(RoleRequest roleRequest);
        Task<List<RoleResponse>> GetAllRoles();
        Task<bool> DeleteRole(RoleRequest roleRequest);
        Task<int> SaveRole(SaveRoleReques saveRoleRequest);
        #endregion

        #region subjects
        Task<SubjectResponse?> GetSubjectById(SubjectRequest subjectRequest);
        Task<List<SubjectResponse>> GetAllSubjects();
        Task<bool> DeleteSubject(SubjectRequest subjectRequest);
        Task<int> SaveSubject(SaveSubjectRequest saveSubjectRequest);
        #endregion
    }
}

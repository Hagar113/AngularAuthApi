using DataAccess.IRepo;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class AdminRepo: IAdminRepo
    {
        private readonly ApplicationDBContext _context;
       
        public AdminRepo(ApplicationDBContext context)
        {
            _context = context;
        }


        #region subjects

        #endregion


        #region roles


        public async Task<RoleResponse?> GetRoleById(RoleRequest roleRequest)
        {
            try
            {
                
                var role = await _context.roles
                    .Where(r => r.id == roleRequest.RoleId)
                    .FirstOrDefaultAsync();

                if (role != null)
                {
                    
                    RoleResponse res = new RoleResponse
                    {
                        id = role.id,
                        name = role.Name,
                        roleCode = role.code,
                    };
                    return res;
                }
                else
                {
                  
                    return null;
                }
            }
            catch
            {
            
                return null;
            }
        }
    
        public async Task<List<RoleResponse>> GetAllRoles()
        {
            try
            {
                List<RoleResponse> roleResponses = new List<RoleResponse>();
                var roles = await _context.roles.ToListAsync();
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        roleResponses.Add(new RoleResponse
                        {
                            id = role.id,
                            name = role.Name,
                            roleCode=role.code,
                          
                        });
                    }
                }
                return roleResponses;
            }
            catch
            {
                return new List<RoleResponse>();
            }
        }

        public async Task<bool> DeleteRole(RoleRequest roleRequest)
        {
            try
            {
                var role = await _context.roles.FindAsync(roleRequest.RoleId);

                if (role != null)
                {
                    _context.roles.Remove(role);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<int> SaveRole(SaveRoleReques saveRoleRequest)
        {
            try
            {
                if (saveRoleRequest.roleId == null || saveRoleRequest.roleId<= 0)
                {
                    return await AddNewRole(saveRoleRequest);
                }
                else
                {
                    return await UpdateRole(saveRoleRequest);
                }
            }
            catch (Exception ex)
            {
              
                return -1;
            }
        }

        private async Task<int> AddNewRole(SaveRoleReques saveRoleRequest)
        {
            try
            {
             
                Roles role = new Roles
                {
                    Name = saveRoleRequest.roleName,
                    code = saveRoleRequest.roleCode
                  
                };

             
                await _context.roles.AddAsync(role);

             
                await _context.SaveChangesAsync();

                
                return 1;
            }
            catch
            {
             
                return -1;
            }
        }

        private async Task<int> UpdateRole(SaveRoleReques saveRoleRequest)
        {
            try
            {
                
                var role = await _context.roles
                    .Where(r => r.id == saveRoleRequest.roleId)
                    .FirstOrDefaultAsync();

                if (role != null)
                {
                  
                    role.Name = saveRoleRequest.roleName;
                    role.code= saveRoleRequest.roleCode;
                   

                  
                    _context.Entry(role).State = EntityState.Modified;

                 
                    await _context.SaveChangesAsync();

                  
                    return 1;
                }
                else
                {
                 
                    return -2;
                }
            }
            catch
            {
           
                return -1;
            }
        }

        #endregion

        #region subject

        public async Task<SubjectResponse?> GetSubjectById(SubjectRequest subjectRequest)
        {
            try
            {
                var subject = await _context.subjects
                    .Where(s => s.Id == subjectRequest.SubjectId)
                    .FirstOrDefaultAsync();

                if (subject != null)
                {
                    SubjectResponse res = new SubjectResponse
                    {
                        id = subject.Id,
                        name = subject.Name,
                        academicYear= subject.AcademicYear,
                    };
                    return res;
                }
                else
                {
                   
                    return null;
                }
            }
            catch
            {
                // Optionally log the error
                return null;
            }
        }

        public async Task<List<SubjectResponse>> GetAllSubjects()
        {
            try
            {
                List<SubjectResponse> subjectResponses = new List<SubjectResponse>();
                var subjects = await _context.subjects.ToListAsync();
                if (subjects != null)
                {
                    foreach (var subject in subjects)
                    {
                        subjectResponses.Add(new SubjectResponse
                        {
                            id = subject.Id,
                            name = subject.Name,
                            academicYear = subject.AcademicYear,

                        });
                    }
                }
                return subjectResponses;
            }
            catch
            {
                return new List<SubjectResponse>();
            }
        }

        public async Task<bool> DeleteSubject(SubjectRequest subjectRequest)
        {
            try
            {
                var subject = await _context.subjects.FindAsync(subjectRequest.SubjectId);

                if (subject != null)
                {
                    _context.subjects.Remove(subject);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
            
                return false;
            }
        }

        public async Task<int> SaveSubject(SaveSubjectRequest saveSubjectRequest)
        {
            try
            {
                if (saveSubjectRequest.SubjectId == null || saveSubjectRequest.SubjectId <= 0)
                {
                    return await AddNewSubject(saveSubjectRequest);
                }
                else
                {
                    return await UpdateSubject(saveSubjectRequest);
                }
            }
            catch
            {
                return -1;
            }
        }

        private async Task<int> AddNewSubject(SaveSubjectRequest saveSubjectRequest)
        {
            try
            {
                Subjects subject = new Subjects
                {
                    Name = saveSubjectRequest.SubjectName,
                    AcademicYear = saveSubjectRequest.academicYear,
                };

                await _context.subjects.AddAsync(subject);
                await _context.SaveChangesAsync();

                return 1;
            }
            catch
            {
                return -1;
            }
        }
        private async Task<int> UpdateSubject(SaveSubjectRequest saveSubjectRequest)
        {
            try
            {
                var subject = await _context.subjects
                    .Where(s => s.Id == saveSubjectRequest.SubjectId)
                    .FirstOrDefaultAsync();

                if (subject != null)
                {
                    subject.Name = saveSubjectRequest.SubjectName;
                    subject.AcademicYear = saveSubjectRequest.academicYear;
                    _context.Entry(subject).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            catch
            {
                return -1;
            }
        }


        #endregion

    }
}

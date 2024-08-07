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

        public async Task<int> SaveRole(SaveRoleReques saveRoleRequest, List<int> selectedPageIds)
        {
            try
            {
                // Check if the role is new or existing
                int roleId;
                if (saveRoleRequest.id == null || saveRoleRequest.id <= 0)
                {
                    roleId = await AddNewRole(saveRoleRequest);
                }
                else
                {
                    roleId = await UpdateRole(saveRoleRequest);
                }

                // Save or update the role-page relationships
                if (roleId > 0)
                {
                    await SaveRolePages(roleId, selectedPageIds);
                    return 1; // Success
                }
                else
                {
                    return -1; // Failure to save role
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return -1; // Error occurred
            }
        }

        private async Task SaveRolePages(int roleId, List<int> pageIds)
        {
            // Remove existing role-page associations for this role
            var existingRolePages = await _context.rolepage
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();

            _context.rolepage.RemoveRange(existingRolePages);

            // Add new role-page associations
            var rolePages = pageIds.Select(pageId => new RolePage
            {
                RoleId = roleId,
                PageId = pageId
            });

            await _context.rolepage.AddRangeAsync(rolePages);
            await _context.SaveChangesAsync();
        }

        private async Task<int> AddNewRole(SaveRoleReques saveRoleRequest)
        {
            try
            {
                Roles role = new Roles
                {
                    Name = saveRoleRequest.name,
                    code = saveRoleRequest.roleCode
                };

                await _context.roles.AddAsync(role);
                await _context.SaveChangesAsync();

                return role.id; // Return the new role ID
            }
            catch
            {
                return -1; // Failure
            }
        }

        private async Task<int> UpdateRole(SaveRoleReques saveRoleRequest)
        {
            try
            {
                var role = await _context.roles
                    .Where(r => r.id == saveRoleRequest.id)
                    .FirstOrDefaultAsync();

                if (role != null)
                {
                    role.Name = saveRoleRequest.name;
                    role.code = saveRoleRequest.roleCode;

                    _context.Entry(role).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return role.id; // Return the updated role ID
                }
                else
                {
                    return -2; // Role not found
                }
            }
            catch
            {
                return -1; // Failure
            }
        }


        private async Task SaveRolePageAssociations(int roleId, List<int> pageIds)
        {
            try
            {
                // Remove existing associations
                var existingAssociations = _context.rolepage.Where(rp => rp.RoleId == roleId);
                _context.rolepage.RemoveRange(existingAssociations);

                // Add new associations
                var rolePageAssociations = pageIds.Select(pageId => new RolePage
                {
                    RoleId = roleId,
                    PageId = pageId
                }).ToList();

                await _context.rolepage.AddRangeAsync(rolePageAssociations);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Handle exception (logging, etc.)
            }
        }

        #endregion

        #region subject

        public async Task<SubjectResponse?> GetSubjectById(SubjectRequest subjectRequest)
        {
            try
            {
                var subject = await _context.subjects
                    .Where(s => s.Id == subjectRequest.SubjectId && (s.isDeleted == false || s.isDeleted == null))
                    .FirstOrDefaultAsync();

                if (subject != null)
                {
                    SubjectResponse res = new SubjectResponse
                    {
                        id = subject.Id,
                        name = subject.Name,
                        academicYear = subject.AcademicYear
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

                // Get all subjects where IsDeleted is false
                var subjects = await _context.subjects.Where(s => !s.isDeleted ?? false).ToListAsync();

                if (subjects != null)
                {
                    foreach (var subject in subjects)
                    {
                        subjectResponses.Add(new SubjectResponse
                        {
                            id = subject.Id,
                            name = subject.Name,
                            academicYear = subject.AcademicYear
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
                    subject.isDeleted = true; 
                    _context.subjects.Update(subject);
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


        #region pages

        public async Task<PageResponse?> GetPageById(PageRequest pageRequest)
        {
            try
            {
                var page = await _context.pages
                    .Where(p => p.id == pageRequest.PageId)
                    .FirstOrDefaultAsync();

                if (page != null)
                {
                    PageResponse res = new PageResponse
                    {
                        id = page.id,
                        name = page.name,
                        path = page.path,
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
                // Log the exception
                return null;
            }
        }

        public async Task<List<PageDto>> GetAllPages()
        {
            try
            {
                List<PageDto> pageResponses = new List<PageDto>();
                var pages = await _context.pages.ToListAsync();
                if (pages != null)
                {
                    foreach (var page in pages)
                    {
                        pageResponses.Add(new PageDto
                        {
                            PageId= page.id,
                            PageName= page.name,
                            PagePath= page.path,
                        });
                    }
                }
                return pageResponses;
            }
            catch
            {
                // Log the exception
                return new List<PageDto>();
            }
        }

        public async Task<bool> DeletePage(PageRequest pageRequest)
        {
            try
            {
                var page = await _context.pages.FindAsync(pageRequest.PageId);

                if (page != null)
                {
                    _context.pages.Remove(page);
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

        public async Task<int> SavePage(SavePageRequest savePageRequest)
        {
            try
            {
                if (savePageRequest.id == null || savePageRequest.id <= 0)
                {
                    return await AddNewPage(savePageRequest);
                }
                else
                {
                    return await UpdatePage(savePageRequest);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return -1;
            }
        }

        private async Task<int> AddNewPage(SavePageRequest savePageRequest)
        {
            try
            {
                Pages page = new Pages
                {
                    name = savePageRequest.name,
                    
                };

                await _context.pages.AddAsync(page);
                await _context.SaveChangesAsync();

                return 1;
            }
            catch
            {
                // Log the exception
                return -1;
            }
        }

        private async Task<int> UpdatePage(SavePageRequest savePageRequest)
        {
            try
            {
                var page = await _context.pages
                    .Where(p => p.id == savePageRequest.id)
                    .FirstOrDefaultAsync();

                if (page != null)
                {
                    page.name = savePageRequest.name;

                    _context.Entry(page).State = EntityState.Modified;
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
                // Log the exception
                return -1;
            }
        }

        #endregion


    }
}

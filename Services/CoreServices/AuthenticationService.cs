using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;
using Shared.Dtos;
using Shared.Dtos.Authentication;
using Shared.Dtos.Instructor;
using Shared.Dtos.Student;
using Shared.Enums;

namespace Services.CoreServices
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IManagerService managerService;
        public AuthenticationService(RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signinmanage
            , UserManager<ApplicationUser> userManager, IManagerService managerService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _signInManager = signinmanage;
            this.managerService = managerService;
        }

        public async Task<string> SignUpAsync(RegisterDto _user)
        {
            try
            {
                var User = new ApplicationUser()
                {
                    UserName = _user.UserName,
                    Email = _user.Email,
                };
                var Result = await userManager.CreateAsync(User, _user.Password);
                if (Result.Succeeded)
                {
                    var createdUser = await userManager.FindByEmailAsync(_user.Email);
                    switch (_user.UserType)
                    {
                        case UserTypeEnum.Student:
                            await managerService.StudentServices.AddStudentAsync(new StudentDto()
                            {
                                Email = _user.Email,
                                FirstName = _user.FirstName,
                                LastName = _user.LastName,
                                PhoneNumber = _user.PhoneNumber,
                                UserId = createdUser!.Id
                            });
                            await userManager.AddToRoleAsync(createdUser, "Student");
                            break;
                        case UserTypeEnum.Instructor:
                            await managerService.InstructorServices.AddInstructorAsync(new InstructorDto()
                            {
                                Email = _user.Email,
                                FirstName = _user.FirstName,
                                LastName = _user.LastName,
                                PhoneNumber = _user.PhoneNumber,
                                UserId = createdUser!.Id
                            });
                            await userManager.AddToRoleAsync(createdUser, "Instructor");
                            break;
                        default: break;
                    }
                    return null;
                }
                else
                {
                    var errors = string.Join(" | ", Result.Errors.Select(e => e.Description));
                    return errors;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<string> SignInAsync(LoginDto _user)
        {
            if (_user == null)
            {
                return "Invalid Data";
            }
            var User = await userManager.FindByEmailAsync(_user.Email);
            if (User != null)
            {
                var Flag = await userManager.CheckPasswordAsync(User, _user.Password);
                if (Flag)
                {
                    await _signInManager.SignInAsync(User, true);
                    return null!;
                }
                else
                    return "Invalid Email or Password";
            }
            return "Invalid Email or Password";
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUserDataDto> GetCurrentUserAsync(string userName)
        {
            var User = await userManager.FindByNameAsync(userName);
            return new ApplicationUserDataDto()
            {
                Email = User!.Email,
                UserId = User.Id
            };
        }
    }
}

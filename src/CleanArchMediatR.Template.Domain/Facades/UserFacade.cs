using CleanArchMediatR.Template.Domain.Interfaces.Facade;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Domain.Facades
{
    public class UserFacade: IUserFacade
    {
        private readonly IUserService _userService;
        public UserFacade(IUserService userService) { 
            _userService = userService;
        }
    }
}

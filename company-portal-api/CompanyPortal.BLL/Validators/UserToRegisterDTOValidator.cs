﻿using CompanyPortal.DTO.DTOs.User;
using FluentValidation;

namespace CompanyPortal.BLL.Validators;

public class UserToRegisterDTOValidator : AbstractValidator<UserToRegisterDTO>
{
    public UserToRegisterDTOValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}


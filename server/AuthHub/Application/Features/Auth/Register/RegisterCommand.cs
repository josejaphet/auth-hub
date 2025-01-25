using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register;
public sealed record RegisterCommand(string UserName,
    string Email,
    string Password,
    string PhoneNumber
    ) : ICommand<Guid>;

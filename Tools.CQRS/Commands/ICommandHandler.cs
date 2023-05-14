﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.CQRS.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        IResult Execute(TCommand command);
    }
}

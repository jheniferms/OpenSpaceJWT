using OpenSpace.Models;
using OpenSpace.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpace.Sevices.Interface
{
    public interface ILoginService
    {
        User Auth(LoginRequest request);
    }
}

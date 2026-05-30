using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities;

public class AuthUserPermission : BaseEntity
{
    public int AuthUserPermissionId{ get; set; }
    public int AuthUserId { get; set; }
    public AuthUser AuthUser { get; set; } = null!;

    public int AuthPermissionId { get; set; }
    public AuthPermission AuthPermission { get; set; } = null!;
}

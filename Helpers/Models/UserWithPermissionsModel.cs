namespace Helpers.Models;

public class UserWithPermissionsModel
{
    public UserWithPermissionsModel()
    {
    }

    public string Email { get; set; }
    public Guid UserId { get; set; }

    public IEnumerable<PermissionModel> Permissions { get; set; }

    public IEnumerable<string> PermissionsNames
    {
        get
        {
            var set = new HashSet<string>();
            foreach (var permission in Permissions)
            {
                set.Add(permission.Name);
                set.Order();
            }

            return set;
        }
    }
}

public class PermissionModel
{
    public PermissionModel()
    {
    }

    public string Name { get; set; }
}
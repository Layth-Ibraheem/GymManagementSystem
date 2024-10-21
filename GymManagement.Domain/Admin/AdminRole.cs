using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Admin
{
    public enum AdminRole
    {
        AllRolls = -1,
        CreateGym = 1,
        CreateRoom = 2,
        AddPlayer = 4,
        AddTrainer = 8,
        DeactivateGym = 16,
        DeactivateRoom = 32,
        UpdateAdmin = 64,
        GetAllGyms = 128,
        GetAllRooms = 256,
        UpdateRoles = 512,
        GetAllSubscriptions = 1024,
    }
    //public class AdminRole : SmartEnum<AdminRole>
    //{
    //    public static readonly AdminRole AllRolls = new AdminRole(nameof(AllRolls), -1);
    //    public static readonly AdminRole CreateGym = new AdminRole(nameof(CreateGym), 1);
    //    public static readonly AdminRole CreateRoom = new AdminRole(nameof(CreateRoom), 2);
    //    public static readonly AdminRole AddPlayer = new AdminRole(nameof(AddPlayer), 4);
    //    public static readonly AdminRole AddTrainer = new AdminRole(nameof(AddTrainer), 8);
    //    public static readonly AdminRole DeactivateGym = new AdminRole(nameof(DeactivateGym), 16);
    //    public static readonly AdminRole DeactivateRoom = new AdminRole(nameof(DeactivateRoom), 32);
    //    public static readonly AdminRole UpdateAdmin = new AdminRole(nameof(UpdateAdmin), 64);
    //    public static readonly AdminRole GetAllGyms = new AdminRole(nameof(GetAllGyms), 128);
    //    public static readonly AdminRole GetAllRooms = new AdminRole(nameof(GetAllRooms), 256);
    //    public static readonly AdminRole UpdateRoles = new AdminRole(nameof(UpdateRoles), 512);
    //    public static readonly AdminRole GetAllSubscriptions = new AdminRole(nameof(GetAllSubscriptions), 1024);


    //    public AdminRole(string name, int value) : base(name, value)
    //    {
    //    }
    //}
}

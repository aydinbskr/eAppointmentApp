using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Domain.Enums
{
    public sealed class DepartmentEnum : SmartEnum<DepartmentEnum>
    {
        public static readonly DepartmentEnum Acil = new("Acil", 1);
        public static readonly DepartmentEnum Radyoloji = new("Radyoloji", 2);
        public DepartmentEnum(string name, int value) : base(name, value)
        {
        }
    }
}

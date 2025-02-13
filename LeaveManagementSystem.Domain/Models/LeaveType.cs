﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Domain.Models;

public class LeaveType : BaseEntity
{
    

    [MaxLength(50)]
    public string Name { get; set; }
    public int NumberOfDays { get; set; }
}

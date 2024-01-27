﻿using System;
using System.Collections.Generic;

namespace CompleteDevNet.Infrastructure.DataOracle;

public partial class TDeveloper
{
    public decimal Id { get; set; }

    public Guid Identguid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phonenumber { get; set; }

    public string? Skillset { get; set; }

    public string? Hobby { get; set; }
}

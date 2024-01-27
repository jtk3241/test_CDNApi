using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Core.SystemRelated;
public class DatabaseSettings
{
    [Required]
    public string DatabaseProvider { get; set; } = string.Empty;
    [Required]
    public string DatabaseSchema { get; set; } = string.Empty;
    [Required]
    public string TnsAdmin { get; set; } = string.Empty;
}

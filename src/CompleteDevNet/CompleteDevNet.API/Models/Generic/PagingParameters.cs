using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class PagingParameters
{
    private int? _pageSize;
    private const int const_MaxPageSize = 200;
    /// <summary>
    /// Page size of retrieved records. Default (if not entered) = 100
    /// </summary>
    [Range(1, const_MaxPageSize, ErrorMessage = "Only positive number allowed. Minimum 1, Maximum 200.")]
    public int? PageSize
    {
        get
        {
            return _pageSize ?? DefaultPageSize;
        }
        set
        {
            if (value == null)
            {
                _pageSize = DefaultPageSize;
            }
            else
            {
                if (value > const_MaxPageSize)
                {
                    _pageSize = const_MaxPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
    /// <summary>
    /// Page number of retrieved records, starts from 0. Default (if not entered) = 0
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int? PageNumber { get; set; } = 0;

    [NonSerialized]
    public int DefaultPageSize = 100;
    [NonSerialized]
    public int MaximumPageSize = const_MaxPageSize;
    [NonSerialized]
    public int DefaultPageNumber = 0;
}

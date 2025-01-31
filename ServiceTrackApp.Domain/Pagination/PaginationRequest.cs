﻿namespace ServiceTrackApp.Domain.Pagination;

public class PaginationRequest
{
    private const int MaxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 5;
    public string? OrderBy { get; set; }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
}
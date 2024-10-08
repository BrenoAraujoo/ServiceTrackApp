﻿using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class JobPosition : ValueObject
{
    public string? Value { get; set; }

    public JobPosition(string value)
    {
        if(string.IsNullOrEmpty(value) &&!IsValid(value))
            throw new ArgumentException(ErrorMessage.InvalidJobPosition);
        Value = value;
    }
    
    public override bool IsValid(object value)
    {
        var valueString = value as string;
        if (valueString == null) return false;
        return valueString.Length is  <= 40;
    }
    

    
}
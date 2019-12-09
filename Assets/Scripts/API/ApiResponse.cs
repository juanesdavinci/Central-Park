using System;

[Serializable]
public class ApiResponse
{
    public bool success;
}

public class ApiSuccessResponse : ApiResponse
{
}

public class ApiErrorResponse : ApiResponse
{
    public string err;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public enum ECommonErrors
{
    Default = 0,
    AccessTokenError = 401,
    TimeOutError = 408,
    DatabaseError = 429,
    BackendServerError = 503,
    ServerGateWayError = 504
}

public static class CJooBackendCommonErrors
{
    static Dictionary<int, ECommonErrors> dicCommonError = new Dictionary<int, ECommonErrors>()
    {
        {(int)ECommonErrors.Default, ECommonErrors.Default},
        {(int)ECommonErrors.AccessTokenError, ECommonErrors.AccessTokenError},
        {(int)ECommonErrors.TimeOutError, ECommonErrors.TimeOutError},
        {(int)ECommonErrors.DatabaseError, ECommonErrors.DatabaseError},
        {(int)ECommonErrors.BackendServerError, ECommonErrors.BackendServerError},
        {(int)ECommonErrors.ServerGateWayError, ECommonErrors.ServerGateWayError}
    };

    public static ECommonErrors GetServerMessage(BackendReturnObject pBro)
    {
        if(IsAvailableWithServer(pBro) == true)
        {
            return ECommonErrors.Default;
        }
        string strStatusCode = pBro.GetStatusCode();
        int intStatusCode = System.Convert.ToInt32(strStatusCode);
        ECommonErrors ee = dicCommonError[intStatusCode];
        return ee;
    }

    public static bool IsAvailableWithServer(BackendReturnObject pBro)
    {
        string strStatusCode = pBro.GetStatusCode();
        int intStatusCode = System.Convert.ToInt32(strStatusCode);
        
        switch (intStatusCode)
        {
            case 401:
            case 429:
            case 408:
            case 503:
            case 504:
                return false;
                
            default:
                return true;
        }
    }
   
    
}

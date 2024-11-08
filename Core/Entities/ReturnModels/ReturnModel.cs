﻿
using System.Net;
using System.Text.Json.Serialization;

namespace Core.Entities.ReturnModels;

public class ReturnModel<TData>
{
    public TData? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public HttpStatusCode Status { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;
    [JsonIgnore] public string? UrlAsCreated { get; set; }


    public static ReturnModel<TData> Success(TData data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ReturnModel<TData>()
        {
            Data = data,
            Status = status
        };
    }

    public static ReturnModel<TData> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ReturnModel<TData>()
        {
            ErrorMessage = errorMessage,
            Status = status
        };
    }

    public static ReturnModel<TData> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ReturnModel<TData>()
        {
            ErrorMessage = [errorMessage],
            Status = status

        };
    }

    public static object Failure(string v)
    {
        throw new NotImplementedException();
    }
}

public class ReturnModel
{

    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public HttpStatusCode Status { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;


    public static ReturnModel Success(HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ReturnModel()
        {

            Status = status
        };
    }

    public static ReturnModel Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ReturnModel()
        {
            ErrorMessage = errorMessage,
            Status = status
        };
    }

    public static ReturnModel Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ReturnModel()
        {
            ErrorMessage = [errorMessage],
            Status = status

        };
    }

    public static object? Success(string v)
    {
        throw new NotImplementedException();
    }

<<<<<<< HEAD
   
=======
    public static object? Success(global::ToDoProject.Model.ToDos.Dtos.Create.Response.CreateToDoResponseDto data)
    {
        throw new NotImplementedException();
    }
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
}

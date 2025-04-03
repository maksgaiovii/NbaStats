﻿namespace NbaStats.BLL.Exceptions
{
    public class EntityNotFoundException(string entityName, object id)
        : Exception($"{entityName} with id {id} was not found.");
}
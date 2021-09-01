using System;

namespace Core.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityId, string entityType) : base($"Nie znaleziono {entityType} o id: {entityId}")
        {
        }
    }
}

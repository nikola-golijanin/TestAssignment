using System.ComponentModel.DataAnnotations;

namespace TestAssignmentApi.Dtos;

public record CreateNewClientDto([Required] string ClientName) { };

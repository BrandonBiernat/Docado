using Docado.BusinessLogic.Interfaces;

namespace Docado.Api.Controllers.ViewModels;

public class UserViewModel(IUserEntity entity) {
    string id {  get; set; } = entity.Id;
    string email { get; set; } = entity.Email;
    string firstName { get; set; } = entity.FirstName;
    string lastName { get; set; } = entity.LastName;
}
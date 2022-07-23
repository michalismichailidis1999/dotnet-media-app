using System.Text.Json;

namespace MediaApp.Application.Services.UserService;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationHandler _authenticationHandler;

    protected override string CachingKey => "user:";

    public UserService(
        IUserRepository repository, 
        IPasswordHasher passwordHasher, 
        IAuthenticationHandler authenticationHandler,
        ICachingDB cachingDB,
        IMapper mapper
    ) : base(cachingDB, mapper)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _authenticationHandler = authenticationHandler;
    }

    public async Task<UserServiceResponse> Register(User user)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUserWithCurrentEmail = await _repository.FindByEmail(user.Email);
        if (existingUserWithCurrentEmail is not null) userServiceResponse.AddError($"Email '{user.Email}' is already taken");

        var existingUserWithCurrentUsername = await _repository.FindByUsername(user.Username);
        if (existingUserWithCurrentUsername is not null) userServiceResponse.AddError($"Username '{user.Username}' is already taken");

        if (!userServiceResponse.HasErrors())
        {
            if(user.UpdatePassword(_passwordHasher.Hash(user.Password)).HasErrors())
            {
                userServiceResponse.AddErrors(user.GetErrors());
                return userServiceResponse;
            }

            await _repository.Create(user);

            userServiceResponse.Payload = user;
            userServiceResponse.Token = _authenticationHandler.CreateAccessToken(user);

            _cachingDB.CreateEntry(CachingKey + user.Id.ToString(), _mapper.Map<UserCachingModel>(user));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> Login(User user)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUser = await _repository.FindByEmail(user.Email);
        if (existingUser is null) userServiceResponse.AddError($"User with email '{user.Email}' not found");

        if (!userServiceResponse.HasErrors())
        {
            var passwordIsValid = _passwordHasher.Verify(user.Password, existingUser!.Password);

            if(!passwordIsValid)
            {
                userServiceResponse.AddError($"Password was incorrect. Please enter your password again");
                return userServiceResponse;
            }

            userServiceResponse.Payload = existingUser;
            userServiceResponse.Token = _authenticationHandler.CreateAccessToken(existingUser);

            _cachingDB.CreateEntry(CachingKey + existingUser.Id.ToString(), _mapper.Map<UserCachingModel>(existingUser));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> UpdateUsername(Guid id, string username)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUser = await _repository.FindByUsername(username);
        if (existingUser is not null)
        {
            if (!existingUser.Id.Equals(id)) userServiceResponse.AddError($"Username '{username}' is already in use");

            if (!userServiceResponse.HasErrors()) userServiceResponse.Payload = existingUser;

            return userServiceResponse;
        }

        var user = await _repository.FindById(id);
        if (user is null) userServiceResponse.AddError($"User with id '{id}' not found");

        if (!userServiceResponse.HasErrors())
        {
            user!.UpdateUsername(username);

            if (user.HasErrors())
            {
                userServiceResponse.AddErrors(user.GetErrors());
                return userServiceResponse;
            }

            await _repository.Update(user);

            _cachingDB.CreateEntry(CachingKey + user.Id.ToString(), _mapper.Map<UserCachingModel>(user));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> UpdateEmail(Guid id, string email)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUser = await _repository.FindByEmail(email);
        if (existingUser is not null)
        {
            if(!existingUser.Id.Equals(id)) userServiceResponse.AddError($"Email '{email}' is already in use");

            if (!userServiceResponse.HasErrors()) userServiceResponse.Payload = existingUser;

            return userServiceResponse;
        }

        var user = await _repository.FindById(id);
        if (user is null) userServiceResponse.AddError($"User with id '{id}' not found");

        if (!userServiceResponse.HasErrors())
        {
            user!.UpdateEmail(email);

            if (user.HasErrors())
            {
                userServiceResponse.AddErrors(user.GetErrors());
                return userServiceResponse;
            }

            await _repository.Update(user);

            _cachingDB.CreateEntry(CachingKey + user.Id.ToString(), _mapper.Map<UserCachingModel>(user));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> UpdateFirstName(Guid id, string firstName)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUser = await _repository.FindById(id);
        if (existingUser is null) userServiceResponse.AddError($"User with id '{id}' not found");

        if (!userServiceResponse.HasErrors())
        {
            existingUser!.UpdateFirstName(firstName);

            if (existingUser.HasErrors())
            {
                userServiceResponse.AddErrors(existingUser.GetErrors());
                return userServiceResponse;
            }

            await _repository.Update(existingUser);

            _cachingDB.CreateEntry(CachingKey + existingUser.Id.ToString(), _mapper.Map<UserCachingModel>(existingUser));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> UpdateLastName(Guid id, string lastName)
    {
        var userServiceResponse = new UserServiceResponse();

        var existingUser = await _repository.FindById(id);
        if (existingUser is null) userServiceResponse.AddError($"User with id '{id}' not found");

        if (!userServiceResponse.HasErrors())
        {
            existingUser!.UpdateLastName(lastName);

            if (existingUser.HasErrors())
            {
                userServiceResponse.AddErrors(existingUser.GetErrors());
                return userServiceResponse;
            }

            await _repository.Update(existingUser);

            _cachingDB.CreateEntry(CachingKey + existingUser.Id.ToString(), _mapper.Map<UserCachingModel>(existingUser));
        }

        return userServiceResponse;
    }

    public async Task<UserServiceResponse> DeleteUser(Guid id)
    {
        var userServiceResponse = new UserServiceResponse();

        var user = await _repository.FindById(id);
        if (user is null) userServiceResponse.AddError($"User with id '{id}' not found");

        if (!userServiceResponse.HasErrors())
        {
            await _repository.Delete(user!);
        }

        return userServiceResponse;
    }
}
